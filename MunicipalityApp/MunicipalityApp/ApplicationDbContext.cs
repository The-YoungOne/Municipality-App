using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace MunicipalityApp
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext() : base("name=DatabaseConnection") // Name of the connection string in the App.config
        {
        }

        // DbSet for your Issues model
        public DbSet<Issue> Issues { get; set; }
    }
}
