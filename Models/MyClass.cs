using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnBazar.Models
{
    public class MyClass
    {
    }
    /* [Table("pages")]//счета-фактуры
     public class Page
     {
         [Key]
         [Required(AllowEmptyStrings = true), MaxLength(36)]
         public string Pid { get; set; }
         public string pagename { get; set; }


     }*/
   
    [Table("Carts")]
    public class Cart
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string cid { get; set; }
        [MaxLength(50)]
        public string csubject { get; set; }        
        [MaxLength(50)]
        public string charda { get; set; }
        [MaxLength(2)]
        public string clan { get; set; }
        public int csira { get; set; }        
        [MaxLength(150)]
        public string cheader { get; set; }        
        public string ctex { get; set; }       
        public float cpris { get; set; }
        [MaxLength(50)]
        public string cbuton { get; set; }
        [MaxLength(36)]
        public string nid { get; set; }
        public virtual Navbar navbars { get; set; }
        [MaxLength(36)]
        public string vid { get; set; }
        public virtual vido vidos { get; set; }       
        public DateTime ctarix { get; set; }
    }
    [Table("vidos")]
    public class vido
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string vid { get; set; }
        public Byte[] vidio { get; set; }
        [MaxLength(150)]
        public string url { get; set; }
    }
    [Table("conts")]
    public class Cont
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string cid { get; set; }
        [ MaxLength(36)]
        public string userId { get; set; }
        [StringLength(150)]
        public string yourname { get; set; }
        [StringLength(250)]
        public string subject { get; set; }       
        public string message { get; set; }
       // [StringLength(10)]
        public DateTime tarix { get; set; }
        public bool isdelete { get; set; }
    }
    [Table("navbars")]
    public class Navbar
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string nid { get; set; }
        [MaxLength(36)]
        public string pid { get; set; }
        [MaxLength(150)]
        public string ntitle { get; set; }
        [MaxLength(150)]
        public string npath { get; set; }
        [MaxLength(50)]
        public string nicon { get; set; }
        [MaxLength(2)]
        public string nlan { get; set; }
        public int ncsay { get; set; }
        [MaxLength(36)]
        public string nrol { get; set; }
        public int ink { get; set; }
        public bool nisparent { get; set; }
    }
    [Table("navroles")]
    public class NavbarRole
    {      
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string nrid { get; set; }
        [MaxLength(36)]
        public string nid { get; set; }
        [MaxLength(36)]
        public string RoleId { get; set; }
    }
    /*  public class Menu
      {
          List<Menu> MenuList;
          public int MenuId { get; set; }
          public string MenuName { get; set; }
          public string MenuHref { get; set; }
          public List<Menu> GetMenu(int id)
          {
              MenuList = new List<Menu>();
              if (id == 1)
              {
                  MenuList.Add(new Menu() { MenuId = 1, MenuName = "Add Employee", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 2, MenuName = "View Employee", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 3, MenuName = "Delete Employee", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 4, MenuName = "Edit Employee", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 5, MenuName = "Logout", MenuHref = "#" });
              }
              else
              {
                  MenuList.Add(new Menu() { MenuId = 1, MenuName = "Edit Details", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 2, MenuName = "My Task", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 3, MenuName = "Contact Us", MenuHref = "#" });
                  MenuList.Add(new Menu() { MenuId = 4, MenuName = "Logout", MenuHref = "#" });
              }
              return MenuList;
          }
      }*/
    /* public List<Navbar> GetMenu(string id)
       {
           NavbarList = new List<Navbar>();
         //  if (id == "admin")
          // {

         //  }
         //  else
         //  {
               NavbarList.Add(new Navbar() { navid = "1", parentid = "0", title = "Ana Səhvə", path = "/", icon = "" });
               NavbarList.Add(new Navbar() { navid = "2", parentid = "0", title = "Обучающие Курсы", path = "lessin/courses", icon = "" });
               NavbarList.Add(new Navbar() { navid = "3", parentid = "0", title = "LESSONS", path = "lessin/lessons", icon = "" });
               NavbarList.Add(new Navbar() { navid = "4", parentid = "0", title = "TESTIMONIALS", path = "lessin/testimonials", icon = "" });
               NavbarList.Add(new Navbar() { navid = "5", parentid = "0", title = "BLOG", path = "lessin/blog", icon = "" });
               NavbarList.Add(new Navbar() { navid = "6", parentid = "0", title = "ABOUT", path = "lessin/about", icon = "" });
               NavbarList.Add(new Navbar() { navid = "7", parentid = "0", title = "CONTACT", path = "lessin/contact", icon = "" });

               NavbarList.Add(new Navbar() { navid = "8", parentid = "0", title = "Adminstrator", path = "admins", icon = "" });
               NavbarList.Add(new Navbar() { navid = "9", parentid = "8", title = "ADMIN", path = "admins/admin", icon = "" });
               NavbarList.Add(new Navbar() { navid = "10", parentid = "8", title = "ROLES", path = "admins/role", icon = "" });
               NavbarList.Add(new Navbar() { navid = "11", parentid = "8", title = "Add CART", path = "admins/cartlar", icon = "" });
               NavbarList.Add(new Navbar() { navid = "12", parentid = "8", title = "Add VIDEO", path = "admins/addvideo", icon = "" });
               NavbarList.Add(new Navbar() { navid = "13", parentid = "8", title = "Add PAGE", path = "admins/addpage", icon = "" });
           //}
           return NavbarList;
       }*/
}

