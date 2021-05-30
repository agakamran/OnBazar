using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnBazar.Models
{
    [Table("_genders")]
    public class _gender
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string gender_Id { get; set; }
        [Required, MaxLength(20)]
        public string gender_name { get; set; }
    }
    [Table("_firmas")]
    public class _firma
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string firma_Id { get; set; }
        [Required, MaxLength(50)]
        public string firma_name { get; set; }
        [ MaxLength(150)]
        public string firma_unvan { get; set; }
        [MaxLength(20)]
        public string firma_telefon { get; set; }
        [Required, MaxLength(50)]
        public string firma_email { get; set; }
        [Required, MaxLength(36)]
        public string userId { get; set; }
        public int voen { get; set; }
    }
    [Table("_bedens")]
    public class _beden
    {
      
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string beden_Id { get; set; }
        [MaxLength(10)]
        public string beden  { get; set; }
        [MaxLength(10)]
        public string trEu { get; set; }
        [MaxLength(10)]
        public string uk { get; set; }
        [MaxLength(10)]
        public string us { get; set; }
        [MaxLength(10)]
        public string it { get; set; }
        [MaxLength(10)]
        public string koks { get; set; }
        [MaxLength(10)]
        public string bel { get; set; }
        [MaxLength(10)]
        public string yaka { get; set; }
        [MaxLength(10)]
        public string ayakUz { get; set; }
        [MaxLength(10)]
        public string ichBacakBoyu { get; set; }
        [MaxLength(10)]
        public string kot { get; set; }
       // [MaxLength(10)]
       // public string kanvas { get; set; }
        [MaxLength(10)]
        public string uzunluk { get; set; }
        [Required, MaxLength(36)]
        public string item_categoriy_Id { get; set; }
        [Required, MaxLength(36)]
        public string gender_Id { get; set; }  
    }   
    [Table("_item_categoriys")]
    public class _item_categoriy{
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_categoriy_Id { get; set; }
        [MaxLength(36)]
        public string parid { get; set; }
        [Required, MaxLength(50)]
        public string item_categoriy_name { get; set; }
        [MaxLength(36)]
        public string gender_Id { get; set; }
    }
    [Table("_item_colors")]
    public class _item_color
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_color_Id { get; set; }
        [Required, MaxLength(50)]
        public string item_color { get; set; }
        [MaxLength(100)]
        public string url_color { get; set; }
    }
    [Table("_item_desens")]
    public class _item_desen
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_desen_Id { get; set; }
        [Required, MaxLength(50)]
        public string item_desen_name { get; set; }
    }
    [Table("_item_markas")]
    public class _item_marka
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_marka_Id { get; set; }
        [Required, MaxLength(100)]
        public string item_marka_name { get; set; }
    }
    [Table("_item_materals")]
    public class _item_materal
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_materal_Id { get; set; }
        [Required, MaxLength(50)]
        public string item_materal_name { get; set; }
    }   
    [Table("_item_stils")]
    public class _item_stil
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_stil_Id { get; set; }
        [Required, MaxLength(50)]
        public string item_stil_name { get; set; }
    }    
    [Table("_kullanimAlanis")]
    public class _kullanimAlani
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kulalan_Id { get; set; }
        [Required, MaxLength(50)]
        public string kullanim_name { get; set; }        
    }
    [Table("_kumashtipis")]
    public class _kumashtipi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kumash_Id { get; set; }
        [Required, MaxLength(50)]
        public string kumash_name { get; set; }
    }
    [Table("_qelips")]
    public class _qelip
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qelip_Id { get; set; }
        [Required, MaxLength(50)]
        public string qelip_name { get; set; }
    }
    [Table("_qoltipis")]
    public class _qoltipi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qol_Id { get; set; }
        [Required, MaxLength(50)]
        public string qoltipi_name { get; set; }
        [MaxLength(36)]
        public string gender_Id { get; set; }
    }   
    [Table("_yakas")]
    public class _yaka
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string yaka_Id { get; set; }
        [Required, MaxLength(50)]
        public string yaka_name { get; set; }
        [MaxLength(36)]
        public string gender_Id { get; set; }
    }
    //--------------------------
    [Table("_items_qaimes")]
    public class _items_qaime
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qaime_Id { get; set; }
        [Required, MaxLength(10)]
        public string qaime_name { get; set; }
        [Required, MaxLength(15)]
        public DateTime qaime_date { get; set; }
        [Required, MaxLength(36)]
        public string firma_Id { get; set; }
    }
    [Table("_itemdetails")]
    public class _itemdetail
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_Id { get; set; }       
        [Required, MaxLength(36)]
        public string firma_Id { get; set; }
        [ MaxLength(36)]
        public string gender_Id { get; set; }
        [ MaxLength(36)]
        public string item_categoriy_Id { get; set; }
        [MaxLength(36)]
        public string item_marka_Id { get; set; }
        [MaxLength(36)]
        public string beden_Id { get; set; }
        [ MaxLength(36)]
        public string item_color_Id { get; set; }
        [MaxLength(36)]
        public string qelip_Id { get; set; }
        [ MaxLength(36)]
        public string item_materal_Id { get; set; }
        [MaxLength(36)]
        public string yaka_Id { get; set; }
        [MaxLength(36)]
        public string qol_Id { get; set; }
        [MaxLength(36)]
        public string item_stil_Id { get; set; }
        [MaxLength(36)]
        public string item_desen_Id { get; set; }
        [MaxLength(36)]
        public string kulalan_Id { get; set; }
        [MaxLength(36)]
        public string kumash_Id { get; set; }
        [ MaxLength(250)]
        public string item_name { get; set; }
        [MaxLength(25)]
        public string item_code { get; set; }
        [Required]
        public decimal item_price { get; set; }
        [Required]
        public decimal item_sales_price { get; set; }
        [Required]
        public int item_quantity { get; set; }
        [Required]
        public int item_discount { get; set; }
        [Required]
        public bool item_hidden { get; set; } = false;
        //[MaxLength(50)]
        public bool item_delivery { get; set; }
        [Required]
        public DateTime qaime_date { get; set; }
    }
    [Table("_items_photos")]
    public class _items_photo
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_photo_Id { get; set; }
        [MaxLength(36)]
        public string item_Id { get; set; }
        [MaxLength(100)]
        public string item_photo_url { get; set; }
    }
    //-------------------------
    [Table("_item_sales")]
    public class _item_sales
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string item_sales_Id { get; set; }
        [Required, MaxLength(36)]
        public string item_Id { get; set; }
        [Required, MaxLength(20)]
        public DateTime item_sale_date { get; set; }
        [Required, MaxLength(36)]
        public string shipdet_Id { get; set; }
    }
    [Table("_ShippingDetails")]
    public class _ShippingDetail
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string shipdet_Id { get; set; }
        [Required, MaxLength(36)]
        public string userId { get; set; }
        [Required, MaxLength(150)]
        public string client_name { get; set; }
        [Required, MaxLength(50)]
        public string client_sity { get; set; }
        [Required, MaxLength(50)]
        public string client_strit { get; set; }
        [Required, MaxLength(10)]
        public string client_house { get; set; }
        [Required, MaxLength(10)]
        public string client_flat { get; set; }
        [Required, MaxLength(20)]
        public string client_phone { get; set; }
        [Required, MaxLength(50)]
        public string client_email { get; set; }
        public bool GiftWrap { get; set; }
    }
    //public class ShippingDetails
    //{
    //    [Required(ErrorMessage = "Please enter a name")]
    //    public string Name { get; set; }
    //    [Required(ErrorMessage = "Please enter the first address line")]
    //    public string Line1 { get; set; }
    //    public string Line2 { get; set; }
    //    public string Line3 { get; set; }
    //    [Required(ErrorMessage = "Please enter a city name")]
    //    public string City { get; set; }
    //    [Required(ErrorMessage = "Please enter a state name")]
    //    public string State { get; set; }
    //    public string Zip { get; set; }
    //    [Required(ErrorMessage = "Please enter a country name")]
    //    public string Country { get; set; }
    //    public bool GiftWrap { get; set; }
    //}
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
    public class itemdetailsListViewModel  //ProductsListViewModel
    {
        public IEnumerable<_itemdetail> _itemdetails { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }

    public class shopCart
    {
        private List<shopCartLine> lineCollection = new List<shopCartLine>();
        public void AddItem(_itemdetail product, int quantity)
        {
            shopCartLine line = lineCollection
                .Where(p => p.Product.item_Id == product.item_Id)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new shopCartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(_itemdetail product)
        {
            lineCollection.RemoveAll(l => l.Product.item_Id == product.item_Id);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.item_sales_price * e.Quantity);
            // return lineCollection.Sum(e => e.Product.UntPrice * e.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<shopCartLine> Lines
        {
            get { return lineCollection; }
        }
    }
    public class shopCartLine
    {
        public _itemdetail Product { get; set; }
        public int Quantity { get; set; }
    }
}
