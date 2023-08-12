using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentInfo> StudentInfos { get; set; }
        public DbSet<FamilyMemberInfo> FamilyMembers { get; set; }
        public DbSet<Country> Countries { get; set; }

    }
}