using System.Data;
using EFCore.ExtendingDbContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCore.ExtendingDbContext
{
    public class MusicDbContext : DbContext, IMusicDbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {
        }
        
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Review> Reviews { get; set; }

        private IDbContextTransaction _transaction;
        
        // transactions are handled by default in EF when calling SaveChanges, i.e. all changes will be saved or none of them will
        // if you want to make multiple writes in a single atomic way then you can control transactions and call SaveChanges multiple times
        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}