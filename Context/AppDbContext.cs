﻿using Microsoft.EntityFrameworkCore;

namespace Api_Test.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student>? Students { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }
    }
}
