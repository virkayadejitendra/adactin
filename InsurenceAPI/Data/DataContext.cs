using InsurenceAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsurenceAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public   DbSet<Occupation> Occupations { get; set; }
        public  DbSet<RatingFactor> RatingFactors { get; set; }
    }
}
