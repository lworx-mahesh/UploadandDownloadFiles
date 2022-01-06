using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles.DbcontextData
{
    public class docusigndbcontext : IdentityDbContext<ApplicationUser>
    {
        public docusigndbcontext(DbContextOptions<docusigndbcontext> options) : base(options)
        {

        }
        //public docusigndbcontext(string connectionString) : base(GetOptions(connectionString))
        //{

        //}
        //private static DbContextOptions GetOptions(string connectionString)
        //{
        //    return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        //}
        public Microsoft.EntityFrameworkCore.DbSet<SenddocumentForSigns> SenddocumentForSigns { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<StoreSignerInfo> StoreSignerInfo { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<SignerColourListModel> SignerColourList { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SavePdfBtnValues> SavePdfBtnValues { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<RecipientSignerForDocuments> RecipientSignerForDocuments { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<SaveControlAxis> SaveControlAxis { get; set; }


    }
}
