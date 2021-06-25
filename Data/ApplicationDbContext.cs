using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnBazar.Models;
//using KamCorAPI.Models.Authent;

namespace OnBazar.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Conventions.Remove<Plua>
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        // public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<page> pages { get; set; }
        public DbSet<vido> vidos { get; set; }
        public DbSet<Cont> conts { get; set; }
        public DbSet<Navbar> navbars { get; set; }
        public DbSet<NavbarRole> navroles { get; set; }
        //------------------------------------------
        public DbSet<gender> genders { get; set; }
        public DbSet<store> stores { get; set; }
        public DbSet<beden> bedens { get; set; }     
        public DbSet<color> colors { get; set; }
        public DbSet<desen> desens { get; set; }
        public DbSet<marka> markas { get; set; }
        public DbSet<material> materials { get; set; }       
        public DbSet<stil> stils { get; set; }        
        public DbSet<kullanimAlani> kullanimAlanis { get; set; }
        public DbSet<kumashtipi> kumashtipis { get; set; }
        public DbSet<qelip> qelips { get; set; }
        public DbSet<qoltipi> qoltipis { get; set; }
        public DbSet<yaka> yakas { get; set; }       
        public DbSet<costhistory> costhistorys { get; set; }
        public DbSet<scrapreason> scrapreasons { get; set; }
        // public DbSet<unitmeasure> unitmeasures { get; set; }
        // public DbSet<supplier> suppliers { get; set; }       
        public DbSet<prodphoto> productphotos { get; set; }
        public DbSet<product> products { get; set; }
        public DbSet<categoriy> categoriys { get; set; }
        public DbSet<orderm> orderms { get; set; }
        public DbSet<orderdet> orderdets { get; set; }
        public DbSet<shipper> shippers { get; set; }
        // public DbSet<_qaime> _qaimes { get; set; }
        // public DbSet<_sales> _saless { get; set; }


    }
}
