using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnBazar.Models
{
    [Table("genders")]
    public class gender
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string genId { get; set; }
        [Required, MaxLength(20)]
        public string genname { get; set; }
    }
    [Table("bedenis")]
    public class bedeni
    {      

        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string bedenId { get; set; }
        [MaxLength(10)]
        public string beden { get; set; }
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
        public string catId { get; set; }
        public virtual IList<categoriy> _categoriys { get; set; } = new List<categoriy>();
        [Required, MaxLength(36)]
        public string genId { get; set; }
        public IList<gender> genders { get; set; } = new List<gender>();

    }
    [Table("_categoriys")]
    public class categoriy
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string catId { get; set; }
        [MaxLength(36)]
        public string parid { get; set; }
        [Required, MaxLength(50)]
        public string catname { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        public IList<gender> genders { get; set; } = new List<gender>();
    }
    [Table("colors")]
    public class color
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string colId { get; set; }
        [Required, MaxLength(50)]
        public string colname { get; set; }
        [MaxLength(100)]
        public string colurl { get; set; }
    }
    [Table("desens")]
    public class desen
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string desId { get; set; }
        [Required, MaxLength(50)]
        public string desname { get; set; }
    }
    [Table("markas")]
    public class marka
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string markaId { get; set; }
        [Required, MaxLength(100)]
        public string markaname { get; set; }
    }
    [Table("materials")]
    public class material
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string matId { get; set; }
        [Required, MaxLength(50)]
        public string matname { get; set; }
    }
    [Table("stils")]
    public class stil
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string stilId { get; set; }
        [Required, MaxLength(50)]
        public string stilname { get; set; }
    }
    [Table("kullanimAlanis")]
    public class kullanimAlani
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kulalanId { get; set; }
        [Required, MaxLength(50)]
        public string kullanimname { get; set; }
    }
    [Table("kumashtipis")]
    public class kumashtipi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kumashId { get; set; }
        [Required, MaxLength(50)]
        public string kumashname { get; set; }
    }
    [Table("qelips")]
    public class qelip
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qelipId { get; set; }
        [Required, MaxLength(50)]
        public string qelipname { get; set; }
    }
    [Table("qoltipis")]
    public class qoltipi
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qolId { get; set; }
        [Required, MaxLength(50)]
        public string qoltipiname { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        public IList<gender> genders { get; set; } = new List<gender>();
    }
    [Table("yakas")]
    public class yaka
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string yakaId { get; set; }
        [Required, MaxLength(50)]
        public string yakaname { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        public IList<gender> genders { get; set; } = new List<gender>();
    }
    [Table("products")]
    public class product
    {       
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string proId { get; set; }        
        [MaxLength(36)]
        public string genId { get; set; } 
        public virtual IList<gender> genders { get; set; } = new List<gender>();
        [MaxLength(36)]
        public string catId { get; set; }
        public virtual IList<categoriy> categoriys { get; set; } = new List<categoriy>();
        [MaxLength(36)]
        public string markaId { get; set; }
        public virtual IList<marka> markas { get; set; } = new List<marka>();
        [MaxLength(36)]
        public string bedenId { get; set; }
        public virtual IList<bedeni> bedenis { get; set; } = new List<bedeni>();
        [MaxLength(36)]
        public string colId { get; set; }
        public virtual IList<color> colors { get; set; } = new List<color>();
        [MaxLength(36)]
        public string qelipId { get; set; }
        public virtual IList<qelip> qelips { get; set; } = new List<qelip>();
        [MaxLength(36)]
        public string matId { get; set; }
        public virtual IList<material> materials { get; set; } = new List<material>();
        [MaxLength(36)]
        public string yakaId { get; set; }
        public virtual IList<yaka> yakas { get; set; } = new List<yaka>();
        [MaxLength(36)]
        public string qolId { get; set; }
        public virtual IList<qoltipi> _qoltipis { get; set; } = new List<qoltipi>();
        [MaxLength(36)]
        public string stilId { get; set; }
        public virtual IList<stil> stils { get; set; } = new List<stil>();
        [MaxLength(36)]
        public string desId { get; set; }
        public virtual IList<desen> desens { get; set; } = new List<desen>();
        [MaxLength(36)]
        public string kulalanId { get; set; }
        public virtual IList<kullanimAlani> kullanimAlanis { get; set; } = new List<kullanimAlani>();
        [MaxLength(36)]
        public string kumashId { get; set; }
        public virtual IList<kumashtipi> kumashtipis { get; set; } = new List<kumashtipi>();
        [MaxLength(250)]
        public string prodname { get; set; }
        [MaxLength(25)]
        public string barcode { get; set; }
        [Required, MaxLength(36)]
        public string unmeaID { get; set; }
        public IList<UnitMeasure> UnitMeasures { get; set; } = new List<UnitMeasure>();//vahidi
        [Required]
        public bool status { get; set; } = false;
        public DateTime ModifiedDate { get; set; }
        
    }    
    public class CostHistory //xerc tarixi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string costID { get; set; }        
        [Required, MaxLength(150)]
        public string costName{ get; set; }
        public DateTime date { get; set; }
    }    
    public class ScrapReason //qaytarma sebebi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ormID { get; set; }
        [Required, MaxLength(150)]
        public string ScrapReasoname { get; set; }
        public DateTime date { get; set; }
    }
    [Table("UnitMeasures")]
    public class UnitMeasure//olchu vahidi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string unmeaID { get; set; }
        public string unmeaname { get; set; }
    }
    [Table("orderms")]
    public class orderm //sifarish master
    {        
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ormID { get; set; }
        public string ordname { get; set; } 
        public decimal summ { get; set; }
        public DateTime ordtarix { get; set; }
    }
    [Table("orderds")]
    public class orderd //sifarish detal
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ordId { get; set; }
        [Required, MaxLength(36)]
        public string proId { get; set; }
        public IList<product> products { get; set; } = new List<product>();
        [Required]
        public decimal Unitbuyprice { get; set; }
        [Required]
        public decimal Unitselprice { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int discount { get; set; }
        //[MaxLength(50)]
        public bool delivery { get; set; }
        [Required]
        public DateTime qaimedate { get; set; }
        [Required, MaxLength(36)]
        public string storId { get; set; }
        public IList<Store> stores { get; set; } = new List<Store>();
        [Required, MaxLength(36)]
        public string supId { get; set; }
        public IList<supplier> suppliers { get; set; } = new List<supplier>();
        [MaxLength(36)]
        public string UserId { get; set; }
        [Required, MaxLength(10)]
        public string opr { get; set; }
        public DateTime ModifiedDate { get; set; }
    }    
    enum cari
    {
        Alış = 0,
        Satış = 1,
        Qaytarma = 2
    }
    [Table("ProductPhotos")]
    public class ProductPhoto
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string photoId { get; set; }
        [MaxLength(36)]
        public string itemId { get; set; }
        [MaxLength(100)]
        public string photourl { get; set; }
    }
    [Table("suppliers")]
    public class supplier //pastavshik
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string supId { get; set; }        
        [MaxLength(100)]
        public string supname { get; set; }
        [MaxLength(150)]
        public string supaddress { get; set; }
        [MaxLength(150)]
        public string supphone { get; set; }
        [MaxLength(150)]
        public string supdescription { get; set; }
        [MaxLength(36)]
        public string storId { get; set; }
        public IList<Store> Stores { get; set; } = new List<Store>();
        public DateTime ModifiedDate { get; set; }
        public bool status { get; set; }
    }
    [Table("shipmethods")]
    public class ShipMethod //Способ доставки
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string shipdet_Id { get; set; }
        [Required, MaxLength(36)]
        public string userId { get; set; }
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
    [Table("Stores")]
    public class Store //Магазин  firmas
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string storId { get; set; }
        [Required, MaxLength(50)]
        public string storname { get; set; }
        [MaxLength(150)]
        public string storadress { get; set; }
        [MaxLength(20)]
        public string storphone { get; set; }
        [Required, MaxLength(50)]
        public string storemail { get; set; }
        [Required, MaxLength(36)]
        public string userId { get; set; }
        public int storvoen { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
