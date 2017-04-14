using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Empty_TaskApplication.Models;

namespace Empty_TaskApplication.Repository
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("name=DefaultConnectionString")
        {

        }

        public DbSet<DateInfo> DateInfo { get; set; }
        public DbSet<Task> Task { get; set; }
    }
}