using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnBazar.Models;
using OnBazar.Models.Abstract;
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
        public DbSet<_firma> _firmas { get; set; }
        public DbSet<_beden> _bedens { get; set; }
        public DbSet<_gender> _genders { get; set; }
        public DbSet<_categoriy> _categoriys { get; set; }
        public DbSet<_color> _colors { get; set; }
        public DbSet<_desen> _desens { get; set; }
        public DbSet<_marka> _markas { get; set; }
        public DbSet<_material> _materials { get; set; }       
        public DbSet<_stil> _stils { get; set; }        
        public DbSet<_kullanimAlani> _kullanimAlanis { get; set; }
        public DbSet<_kumashtipi> _kumashtipis { get; set; }
        public DbSet<_qelip> _qelips { get; set; }
        public DbSet<_qoltipi> _qoltipis { get; set; }
        public DbSet<_yaka> _yakas { get; set; }
        public DbSet<_itemdetail> _itemdetails { get; set; }
        public DbSet<_photo> _photos { get; set; }
        // public DbSet<_qaime> _qaimes { get; set; }
        // public DbSet<_sales> _saless { get; set; }
        public DbSet<orderm> orderms { get; set; }
        public DbSet<orderd> orderds { get; set; }
        public DbSet<shipDetail> shipDetails { get; set; }

    }
}
