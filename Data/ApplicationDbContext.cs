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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           // builder.Conventions.Remove<Plua>
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
       // public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<vido> vidos { get; set; }
        public DbSet<Cont> conts { get; set; }
        public DbSet<Navbar> navbars { get; set; }
        public DbSet<NavbarRole> navroles { get; set; }
        //------------------------------------------
        public DbSet<_firma> _firmas { get; set; }
        public DbSet<_beden> _bedens { get; set; }
        public DbSet<_gender> _genders { get; set; }
        public DbSet<_item_categoriy> _item_categoriys { get; set; }
        public DbSet<_item_color> _item_colors { get; set; }
        public DbSet<_item_desen> _item_desens { get; set; }
        public DbSet<_item_marka> _item_markas { get; set; }
        public DbSet<_item_materal> _item_materals { get; set; }
        public DbSet<_item_sales> _item_saless { get; set; }
        public DbSet<_item_stil> _item_stils { get; set; }
        public DbSet<_itemdetail> _itemdetails { get; set; }
        public DbSet<_items_photo> _items_photos { get; set; }
       // public DbSet<_items_qaime> _items_qaimes { get; set; }
        public DbSet<_kullanimAlani> _kullanimAlanis { get; set; }
        public DbSet<_kumashtipi> _kumashtipis { get; set; }
        public DbSet<_qelip> _qelips { get; set; }
        public DbSet<_qoltipi> _qoltipis { get; set; }
        public DbSet<_ShippingDetail> _ShippingDetails { get; set; }
        public DbSet<_yaka> _yakas { get; set; }

    }
}
