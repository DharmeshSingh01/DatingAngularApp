using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.DataContext
{
    public class DataContexts :DbContext
    {
        public DataContexts(DbContextOptions<DataContexts> options) :base(options)
        {

        }

       public  DbSet<Values> value { get; set; }
       public DbSet<User> Users { get; set; }
    }
}
