using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using wennovation_hub.Models;

namespace wennovation_hub.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Bookings> Bookings { get; set; }
    public DbSet<Locations> Locations { get; set; }
    public DbSet<Admins> Admins { get; set; }
    public DbSet<Wennovator> Wennovators { get; set; }
    public DbSet<Consult> Consults { get; set; }
    public DbSet<BookingRates> BookingRates { get; set; }
}
