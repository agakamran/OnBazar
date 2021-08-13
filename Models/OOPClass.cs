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
    [Table("bedens")]
    public class beden
    {

        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string bedenId { get; set; }
        [MaxLength(10)]
        public string bedeni { get; set; }
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
        [Required, MaxLength(36)]
        public string genId { get; set; }
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
    }
    //[Table("UnitMeasures")]
    //public class unitmeasure//olchu vahidi
    //{
    //    [Key]
    //    [Required(AllowEmptyStrings = true), MaxLength(36)]
    //    public string unmeaID { get; set; }
    //    public string unmeaname { get; set; }
    //}
    [Table("stores")]
    public class store //Магазин  firmas
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
        //public decimal storpercent { get; set; } = 0;
        public int storvoen { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
    [Table("costhistorys")]
    public class costhistory //xerc tarixi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string costID { get; set; }
        [Required, MaxLength(150)]
        public string costName { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
    }
    [Table("scrapreasons")]
    public class scrapreason //qaytarma sebebi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ormID { get; set; }
        [Required, MaxLength(150)]
        public string ScrapReasoname { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
    }
    //[Table("suppliers")]
    //public class supplier //pastavshik
    //{
    //    [Key]
    //    [Required(AllowEmptyStrings = true), MaxLength(36)]
    //    public string supId { get; set; }
    //    [MaxLength(100)]
    //    public string supname { get; set; }
    //    [MaxLength(150)]
    //    public string supaddress { get; set; }
    //    [MaxLength(150)]
    //    public string supphone { get; set; }
    //    [MaxLength(150)]
    //    public string supdescription { get; set; }
    //    [MaxLength(36)]
    //    public string storId { get; set; }        
    //    public bool status { get; set; }
    //    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    //}
    [Table("prodphotos")]
    public class prodphoto
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string photoId { get; set; }
        [MaxLength(36)]
        public string proId { get; set; }
        [MaxLength(100)]
        public string photourl { get; set; }
    }
    [Table("categoriys")]
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
    }
    [Table("products")]
    public class product
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string proId { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        [MaxLength(36)]
        public string catId { get; set; }
        [MaxLength(36)]
        public string markaId { get; set; }        
        [MaxLength(36)]
        public string qelipId { get; set; }
        [MaxLength(36)]
        public string matId { get; set; }
        [MaxLength(36)]
        public string yakaId { get; set; }
        [MaxLength(36)]
        public string qolId { get; set; }
        [MaxLength(36)]
        public string stilId { get; set; }
        [MaxLength(36)]
        public string desId { get; set; }
        [MaxLength(36)]
        public string kulalanId { get; set; }
        [MaxLength(36)]
        public string kumashId { get; set; }
        //--------------------------------------------
        [MaxLength(36)]
        public string bedenId { get; set; }
        [MaxLength(36)]
        public string colId { get; set; }
        [MaxLength(250)]
        public string prodname { get; set; }
       // [MaxLength(25)]
        public decimal barcode { get; set; }
        [Required, MaxLength(36)]
        public string storId { get; set; }          //magaza
        public int boxquantity { get; set; }          //количество коробок 
        public decimal unitsinstock { get; set; }    //Единицы на складе     запасы       
        public decimal buy_unitprice { get; set; }   //Цена за единицу      alish
        public decimal sell_unitprice { get; set; }   //Цена за единицу      satish       
        public Nullable<decimal> discount { get; set; }//скидка    
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public bool Discontinued { get; set; }       //Снято с производства
        [Required, MaxLength(10)]
        public string opr { get; set; }               //emeliyyat  
        public bool delivery { get; set; } //dastavka
        //--------------------------------------------------------------------------
    }
    
    [Table("orderdets")]
    public class orderdet
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ordId { get; set; }
        [Required, MaxLength(36)]
        public string proId { get; set; }
        [MaxLength(36)]
        public string ormId { get; set; }//order master     
        [Required, MaxLength(36)]
        public string storId { get; set; }        
        [MaxLength(36)]
        public string UserId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }

    }
    [Table("orderms")]
    public class orderm //sifarish master
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ormId { get; set; }    
        public Nullable<decimal> summ { get; set; }       
        public Nullable<DateTime> orderdate { get; set; } = DateTime.Now;//Дата заказа
        public Nullable<DateTime> requireddate { get; set; }//Требуемая дата
        public Nullable<DateTime> shippeddate { get; set; }//Дата доставки
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
    [Table("shippers")]
    public class shipper
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string shipId { get; set; }
        [MaxLength(36)]
        public string ormId { get; set; }
        [Required, MaxLength(50)]
        public string storname { get; set; }//kimden 
        [Required, MaxLength(36)]
        public string userId { get; set; }//kime
        [MaxLength(50)]
        public string ShipCountry { get; set; }//olke
        [MaxLength(100)]
        public string shipregion { get; set; }    // hansi region   
        [Required, MaxLength(50)]
        public string shipsity { get; set; }//sheher
        [Required, MaxLength(50)]
        public string shipstrit { get; set; }//kuche
        [Required, MaxLength(10)]
        public string shiphouse { get; set; }
        //[MaxLength(150)]
        //public string shipaddress { get; set; }      
        [MaxLength(20)]
        public string shippostalcode { get; set; }
        [Required, MaxLength(20)]
        public string shipphone { get; set; }
        [Required, MaxLength(50)]
        public string shipemail { get; set; }
        public bool GiftWrap { get; set; }
        public Nullable<int> ShipVia { get; set; }//Доставить через
        public Nullable<decimal> Freight { get; set; }//Груз
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
    enum cari
    {
        Alış = 0,
        Satış = 1,
        Qaytarma = 2
    }
    //[Table("orderds")]
    //public class orderd //sifarish detal
    //{
    //    [Key]
    //    [Required(AllowEmptyStrings = true), MaxLength(36)]
    //    public string ordId { get; set; }
    //    [Required, MaxLength(36)]
    //    public string proId { get; set; }
    //    public IList<product> products { get; set; } = new List<product>();
    //    [Required]
    //    public decimal Unitbuyprice { get; set; }
    //    [Required]
    //    public decimal Unitselprice { get; set; }
    //    [Required]
    //    public int quantity { get; set; }
    //    [Required]
    //    public int discount { get; set; }
    //    //[MaxLength(50)]
    //    public bool delivery { get; set; }
    //    [Required]
    //    public DateTime qaimedate { get; set; }
    //    [Required, MaxLength(36)]
    //    public string storId { get; set; }
    //    public IList<Store> stores { get; set; } = new List<Store>();
    //    [Required, MaxLength(36)]
    //    public string supId { get; set; }
    //    public IList<supplier> suppliers { get; set; } = new List<supplier>();
    //    [MaxLength(36)]
    //    public string UserId { get; set; }
    //    [Required, MaxLength(10)]
    //    public string opr { get; set; }
    //    public DateTime ModifiedDate { get; set; }
    //}    
    //[Table("shipmethods")]
    //public class shipmethod //Способ доставки
    //{
    //    [Key]
    //    [Required(AllowEmptyStrings = true), MaxLength(36)]
    //    public string shipdet_Id { get; set; }
    //    [Required, MaxLength(36)]
    //    public string userId { get; set; }
    //    [Required, MaxLength(50)]
    //    public string client_sity { get; set; }
    //    [Required, MaxLength(50)]
    //    public string client_strit { get; set; }
    //    [Required, MaxLength(10)]
    //    public string client_house { get; set; }
    //    [Required, MaxLength(10)]
    //    public string client_flat { get; set; }
    //    [Required, MaxLength(20)]
    //    public string client_phone { get; set; }
    //    [Required, MaxLength(50)]
    //    public string client_email { get; set; }
    //    public bool GiftWrap { get; set; }
    //}
}
