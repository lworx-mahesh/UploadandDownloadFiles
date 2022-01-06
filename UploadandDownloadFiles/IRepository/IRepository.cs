﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace UploadandDownloadFiles.IRepository
{
  
    public interface IRepository<T> where T : class
    {
        void AddRange(IEnumerable<T> entities);
        T GetById(int id);
        int? UpdateTranscommit(T entity, params Expression<Func<T, object>>[] properties);
        int? InsertTranscommit(T entity);
        List<T> Wherecondtion(Expression<Func<T, bool>> predicate);
        List<T> GetList();
        int? Update(T entity, params Expression<Func<T, object>>[] properties);
        int? InsertAndGetId(T entity);
        IEnumerable<T> GetAll();
        Task<T> GetAsync(long id);
        Task<int?> InsertAsync(T entity);
        int? Insert(T entity);
        Task<int?> UpdateAsync(T entity);
        Task<int?> DeleteAsync(T entity);
        void Remove(T entity);
        Task<int?> SaveChangesAsync();
        int? SaveChanges();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task<int?> UpdateAsync(List<T> entity);
        Task<int?> InsertMultiAsync(List<T> entity);
        Task<T> ExecuteWithJsonResultAsync(string name, string parserString, params SqlParameter[] parameters);
        List<T> ExecuteWithJsonResult(string name, string parserString, params SqlParameter[] parameters);
        void ExecuteStoreProcedureWithoutReturnType(string procName, string entity, params SqlParameter[] parameters);
        T Find(params object[] keyValues);

        List<T> ExecStoreProcedureWithReturnType(string name, string parserString, params SqlParameter[] parameters);

    }

}
