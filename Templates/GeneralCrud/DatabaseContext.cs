
        public DbSet<{!FIELD}> {!FIELDS} { get; set; }

            modelBuilder.Entity<{!FIELD}>().HasKey(x => x.{!FIELD}Id).HasName("PK_utb{!FIELDS}Id");
            modelBuilder.Entity<{!FIELD}>().HasIndex(x => x.{!FIELD}Name).IsUnique().HasName("utb{!FIELDS}Ndx{!FIELD}Name");
            modelBuilder.Entity<{!FIELD}>().Property(x => x.RowVersion).IsConcurrencyToken();