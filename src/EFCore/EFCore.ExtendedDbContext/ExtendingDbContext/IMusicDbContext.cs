using EFCore.ExtendingDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.ExtendingDbContext
{
    public interface IMusicDbContext
    {
        DbSet<Song> Songs { get; set; }
        DbSet<Artist> Artists { get; set; }
        DbSet<Review> Reviews { get; set; }
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}