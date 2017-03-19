using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace stms_api.Models
{
    public class stmsContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public stmsContext() : base("name=stmsContext")
        {
        }

        public System.Data.Entity.DbSet<Student> Students { get; set; }
        public System.Data.Entity.DbSet<Course> Courses { get; set; }
        public System.Data.Entity.DbSet<KeyPool> KeyPools { get; set; }
        public System.Data.Entity.DbSet<Class> Classes { get; set; }
        public System.Data.Entity.DbSet<Trainer> Trainers { get; set; }
        public System.Data.Entity.DbSet<Company> Companies { get; set; }
        public System.Data.Entity.DbSet<RegistrationHeader> RegistrationHeaders { get; set; }
        public System.Data.Entity.DbSet<RegistrationItem> RegistrationItems { get; set; }

    }
}
