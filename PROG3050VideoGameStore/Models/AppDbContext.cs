﻿using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Numerics;

namespace PROG3050VideoGameStore.Models
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }
        public DbSet<ProfilePreferences> ProfilePreferencesList { get; set; }

        public DbSet<UserAddress> UserAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            





        }

    }
}
