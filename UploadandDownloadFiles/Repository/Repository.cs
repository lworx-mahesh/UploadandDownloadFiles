using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UploadandDownloadFiles.DbcontextData;
using UploadandDownloadFiles.IRepository;

namespace UploadandDownloadFiles.Repository
{
 
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly docusigndbcontext _context;
        private DbSet<T> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(docusigndbcontext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> GetAsync(long id)
        {
            return await _entities.FindAsync(id);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity is null</exception>
        public async Task<int?> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            _entities.Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }
        public int? Insert(T entity)
        {

            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity is null");
                _entities.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public int? InsertTranscommit(T entity)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    if (entity == null)
                        throw new ArgumentNullException("entity is null");
                    _entities.Add(entity);
                    _context.Entry(entity).State = EntityState.Added;
                    transaction.Commit();
                    return _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public async Task<int?> InsertMultiAsync(List<T> entity)
        {
            try
            {

                if (entity == null)
                    throw new ArgumentNullException("entity is null");
                entity.ForEach(a => { _context.Entry(a).State = EntityState.Added; });
                return await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity is null</exception>
        public async Task<int?> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            _context.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity is null</exception>
        public async Task<int?> DeleteAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            _context.Entry(entity).State = EntityState.Deleted;
            _entities.Remove(entity);
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Remove(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Deleted;
            _entities.Remove(entity);
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<int?> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public int? SaveChanges()
        {
            return _context.SaveChanges();
        }
        /// <summary>
        /// Wheres the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public List<T> Wherecondtion(Expression<Func<T, bool>> predicate)
        {
            try
            {

                var list = new List<T>(_context.Set<T>().Where(predicate));
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">entity is null</exception>
        public async Task<int?> UpdateAsync(List<T> entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");

            entity.ForEach(a => { _context.Entry(a).State = EntityState.Modified; });
            return await SaveChangesAsync();
        }

        /// <summary>
        /// Executes the store procedure.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<DbDataReader> ExecStoreProcedure(string procName, string entity, params SqlParameter[] parameters)
        {
            var query = _queryBuilder(procName, parameters);
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                var reader = await command.ExecuteReaderAsync();
                return reader;
            }
        }

        /// <summary>
        /// Executes the with json result asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parserString">The parser string.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public async Task<T> ExecuteWithJsonResultAsync(string name, string parserString, params SqlParameter[] parameters)
        {
            var query = _queryBuilder(name, parameters);
            T result;
            var connectionObject = _context.Database.GetDbConnection();
            connectionObject.Open();
            using (var command = connectionObject.CreateCommand())
            {
                command.CommandText = query;
                var reader = await command.ExecuteReaderAsync();

                try
                {
                    var jsonResult = new StringBuilder();
                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                    }
                    JObject jsonResponse = JObject.Parse(jsonResult.ToString());
                    result = JsonConvert.DeserializeObject<T>(Convert.ToString(jsonResponse[parserString]));
                }
                catch (Exception)
                {
                    //TODO: Add logging
                    result = default(T);
                }
                finally
                {
                    connectionObject.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Executes the with json result.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parserString">The parser string.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public List<T> ExecuteWithJsonResult(string name, string parserString, params SqlParameter[] parameters)
        {
            var query = _queryBuilder(name, parameters);
            List<T> result;
            var connectionObject = _context.Database.GetDbConnection();
            connectionObject.Open();
            using (var command = connectionObject.CreateCommand())
            {
                command.CommandText = query;
                var reader = command.ExecuteReader();

                try
                {
                    var jsonResult = new StringBuilder();
                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                    }
                    JObject jsonResponse = JObject.Parse(jsonResult.ToString());
                    var objResponse = jsonResponse[parserString];
                    if (objResponse != null)
                    {
                        return JsonConvert.DeserializeObject<List<T>>(Convert.ToString(objResponse));
                    }
                    return (List<T>)Activator.CreateInstance(typeof(List<T>));
                    //result = JsonConvert.DeserializeObject<T>(Convert.ToString(jsonResponse[parserString]));
                }
                catch (Exception ex)
                {
                    //TODO: Add logging
                    result = default(List<T>);
                }
                finally
                {
                    connectionObject.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Queries the builder.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        private string _queryBuilder(string name, params SqlParameter[] parameters)
        {
            var query = string.Empty;
            if (parameters != null && parameters.Any())
            {
                var paramsString = parameters.Any(a => a.ParameterName.Contains("@")) ?
                    string.Join(",", parameters.Select(p => $" {p.ParameterName}='{p.SqlValue}'"))
                    : string.Join(",", parameters.Select(p => $" @{p.ParameterName}='{p.SqlValue}'"));
                query = $"Exec {name} {paramsString}";
            }
            return query;
        }

        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public T Find(params object[] keyValues)
        {
            return _entities.Find(keyValues);
        }


        /// <summary>
        /// Selects the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal IQueryable<T> Select(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = _entities;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (orderBy != null)
                query = orderBy(query);

            //if (filter != null)
            //    query = query.AsExpandable().Where(filter);

            if (page != null && pageSize != null)
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);

            return query;
        }

        /// <summary>
        /// Selects the asynchronous.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includes">The includes.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        internal async Task<IEnumerable<T>> SelectAsync(
            Expression<Func<T, bool>> query = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return Select(query, orderBy, includes, page, pageSize).AsEnumerable();
        }

        /// <summary>
        /// Executes the type of the store procedure without return.
        /// </summary>
        /// <param name="procName">Name of the proc.</param>
        /// <param name="entity">The entity.</param>
        /// <param name="parameters">The parameters.</param>
        public void ExecuteStoreProcedureWithoutReturnType(string procName, string entity, params SqlParameter[] parameters)
        {
            var connectionObject = _context.Database.GetDbConnection();
            connectionObject.Open();
            try
            {
                var query = _queryBuilder(procName, parameters);
                using (var command = _context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                return;
            }
            finally
            {
                connectionObject.Close();
            }
        }


        public List<T> ExecStoreProcedureWithReturnType(string name, string parserString, params SqlParameter[] parameters)
        {
            var query = _queryBuilder(name, parameters);
            List<T> result = new List<T>();
            var connectionObject = _context.Database.GetDbConnection();
            connectionObject.Open();
            using (var command = connectionObject.CreateCommand())
            {
                command.CommandText = query;
                var reader = command.ExecuteReader();

                try
                {
                    Type TypeT = typeof(T);
                    ConstructorInfo ctor = TypeT.GetConstructor(Type.EmptyTypes);
                    if (ctor == null)
                    {
                        throw new InvalidOperationException($"Type {TypeT.Name} does not have a default constructor.");
                    }
                    //var results = new List<T>();
                    var properties = typeof(T).GetProperties();

                    while (reader.Read())
                    {
                        T newInst = (T)ctor.Invoke(null);
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string propName = reader.GetName(i);
                            PropertyInfo propInfo = TypeT.GetProperty(propName);
                            if (propInfo != null)
                            {
                                object value = reader.GetValue(i);
                                if (value == DBNull.Value)
                                {
                                    propInfo.SetValue(newInst, null);
                                }
                                else
                                {
                                    propInfo.SetValue(newInst, value);
                                }
                            }
                        }

                        result.Add(newInst);
                        // return newInst;
                    }
                    return result;
                }


                catch (Exception ex)
                {
                    //TODO: Add logging
                    result = default(List<T>);
                }
                finally
                {
                    connectionObject.Close();
                }
            }
            return result;
        }

        public int? InsertAndGetId(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity is null");
                _entities.Add(entity);
                _context.Entry(entity).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            //Returns primaryKey value
            var idProperty = entity.GetType().GetProperty("Id").GetValue(entity, null);
            return (int)idProperty;
        }

        public int? Update(T entity, params Expression<Func<T, object>>[] properties)
        {
            try
            {


                _context.Entry(entity).State = EntityState.Modified;


                return _context.SaveChanges();
                //_context.Dispose();



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateDbEntryAsync exception: " + ex.Message);
                return 0;
            }
        }


        public int? UpdateTranscommit(T entity, params Expression<Func<T, object>>[] properties)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {


                    _context.Entry(entity).State = EntityState.Modified;

                    transaction.Commit();
                    return _context.SaveChanges();
                    //_context.Dispose();



                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    System.Diagnostics.Debug.WriteLine("UpdateDbEntryAsync exception: " + ex.Message);
                    return 0;
                }
            }
        }


        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }

        public void AddRange(IEnumerable<T> entities)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    _context.Set<T>().AddRange(entities);
                    transaction.Commit();
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                }

            }
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }





}
