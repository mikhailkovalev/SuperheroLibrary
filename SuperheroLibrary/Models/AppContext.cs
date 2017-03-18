﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SuperheroLibrary.Models
{
    public class AppContext: DbContext
    {
        public AppContext(): base("DefaultConnection")
        { }

        public DbSet<User> Users { get; set; }
    }
}