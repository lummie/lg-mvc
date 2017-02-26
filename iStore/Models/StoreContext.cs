using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iStore.Models {

    ///<summary>
    /// StoreContext provides the Database context used to persist the model data 
    ///</summary>
    public class StoreContext : DbContext {
        // Items in the store
        public DbSet<StoreItem> StoreItems {get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // would normally store this in configuration but just hard coded to current dir for now
            optionsBuilder.UseSqlite("Filename=./iStore.db");
        }
    }
}