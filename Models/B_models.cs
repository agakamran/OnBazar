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
        public string genId { get; set; }
        [Required, MaxLength(20)]
        public string genname { get; set; }
    }
    [Table("_firmas")]
    public class _firma
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string firmaId { get; set; }
        [Required, MaxLength(50)]
        public string firmaname { get; set; }
        [ MaxLength(150)]
        public string firmaunvan { get; set; }
        [MaxLength(20)]
        public string firmatelefon { get; set; }
        [Required, MaxLength(50)]
        public string firmaemail { get; set; }
        [Required, MaxLength(36)]
        public string userId { get; set; }
        public int voen { get; set; }
    }
    [Table("_bedens")]
    public class _beden
    {
        public _beden()
        {
            this._genders = new HashSet<_gender>();
            this._categoriys = new HashSet<_categoriy>();
        }
       
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string bedenId { get; set; }
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
        public string catId { get; set; }
        public virtual ICollection<_categoriy> _categoriys { get; set; }
        [Required, MaxLength(36)]
        public string genId { get; set; }
        public virtual ICollection<_gender> _genders { get; set; }
        
    }   
    [Table("_categoriys")]
    public class _categoriy{
        public _categoriy()
        {
            this._genders = new HashSet<_gender>();
        }
        public virtual ICollection<_gender> _genders { get; set; }
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
    [Table("_colors")]
    public class _color
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string colId { get; set; }
        [Required, MaxLength(50)]
        public string color { get; set; }
        [MaxLength(100)]
        public string urlcolor { get; set; }
    }
    [Table("_desens")]
    public class _desen
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string desId { get; set; }
        [Required, MaxLength(50)]
        public string desname { get; set; }
    }
    [Table("_markas")]
    public class _marka
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string markaId { get; set; }
        [Required, MaxLength(100)]
        public string markaname { get; set; }
    }
    [Table("_materials")]
    public class _material
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string matId { get; set; }
        [Required, MaxLength(50)]
        public string matname { get; set; }
    }   
    [Table("_stils")]
    public class _stil
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string stilId { get; set; }
        [Required, MaxLength(50)]
        public string stilname { get; set; }
    }    
    [Table("_kullanimAlanis")]
    public class _kullanimAlani
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kulalanId { get; set; }
        [Required, MaxLength(50)]
        public string kullanimname { get; set; }        
    }
    [Table("_kumashtipis")]
    public class _kumashtipi
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string kumashId { get; set; }
        [Required, MaxLength(50)]
        public string kumashname { get; set; }
    }
    [Table("_qelips")]
    public class _qelip
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qelipId { get; set; }
        [Required, MaxLength(50)]
        public string qelipname { get; set; }
    }
    [Table("_qoltipis")]
    public class _qoltipi
    {
        public _qoltipi()
        {
            this._genders = new HashSet<_gender>();
        }
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qolId { get; set; }
        [Required, MaxLength(50)]
        public string qoltipiname { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        public virtual ICollection<_gender> _genders { get; set; }
    }   
    [Table("_yakas")]
    public class _yaka
    {
        public _yaka()
        {            
            this._genders = new HashSet<_gender>();            
        }
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string yakaId { get; set; }
        [Required, MaxLength(50)]
        public string yakaname { get; set; }
        [MaxLength(36)]
        public string genId { get; set; }
        public virtual ICollection<_gender> _genders { get; set; }
    }
    //--------------------------
    [Table("_qaimes")]
    public class _qaime
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string qaimeId { get; set; }
        [Required, MaxLength(10)]
        public string qaimename { get; set; }
        [Required, MaxLength(15)]
        public DateTime qaimedate { get; set; }
        [Required, MaxLength(36)]
        public string firmaId { get; set; }
    }
    
    [Table("_itemdetails")]
    public class _itemdetail
    {
        public _itemdetail()
        {
            this._firmas = new HashSet<_firma>();
            this._genders = new HashSet<_gender>();
            this._categoriys = new HashSet<_categoriy>();
            this._markas = new HashSet<_marka>();
            this._bedens = new HashSet<_beden>();
            this._colors = new HashSet<_color>();
            this._qelips = new HashSet<_qelip>();
            this._materials = new HashSet<_material>();
            this._yakas = new HashSet<_yaka>();
            this._qoltipis = new HashSet<_qoltipi>();
            this._stils = new HashSet<_stil>();
            this._desens = new HashSet<_desen>();
            this._kullanimAlanis = new HashSet<_kullanimAlani>();
            this._kumashtipis = new HashSet<_kumashtipi>();
        }
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string itemId { get; set; }       
        [Required, MaxLength(36)]
        public string firmaId { get; set; }
        public virtual ICollection<_firma> _firmas { get; set; }
        [ MaxLength(36)]
        public string genId { get; set; }
        public virtual ICollection<_gender> _genders { get; set; }
        [ MaxLength(36)]
        public string catId { get; set; }
        public virtual ICollection<_categoriy> _categoriys { get; set; }
        [MaxLength(36)]
        public string markaId { get; set; }
        public virtual ICollection<_marka> _markas { get; set; }
        [MaxLength(36)]
        public string bedenId { get; set; }
        public virtual ICollection<_beden> _bedens { get; set; }
        [ MaxLength(36)]
        public string colId { get; set; }
        public virtual ICollection<_color> _colors { get; set; }
        [MaxLength(36)]
        public string qelipId { get; set; }
        public virtual ICollection<_qelip> _qelips { get; set; }
        [ MaxLength(36)]
        public string matId { get; set; }
        public virtual ICollection<_material> _materials { get; set; }
        [MaxLength(36)]
        public string yakaId { get; set; }
        public virtual ICollection<_yaka> _yakas { get; set; }
        [MaxLength(36)]
        public string qolId { get; set; }
        public virtual ICollection<_qoltipi> _qoltipis { get; set; }
        [MaxLength(36)]
        public string stilId { get; set; }
        public virtual ICollection<_stil> _stils { get; set; }
        [MaxLength(36)]
        public string desId { get; set; }
        public virtual ICollection<_desen> _desens { get; set; }
        [MaxLength(36)]
        public string kulalanId { get; set; }
        public virtual ICollection<_kullanimAlani> _kullanimAlanis { get; set; }
        [MaxLength(36)]
        public string kumashId { get; set; }
        public virtual ICollection<_kumashtipi> _kumashtipis { get; set; }       
        [ MaxLength(250)]
        public string itemname { get; set; }
        [MaxLength(25)]
        public string code { get; set; }
        [Required]
        public decimal price { get; set; }
        [Required]
        public decimal salesprice { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int discount { get; set; }
        [Required]
        public bool hidden { get; set; } = false;
        //[MaxLength(50)]
        public bool delivery { get; set; }
        [Required]
        public DateTime qaimedate { get; set; }

    }

    [Table("_photos")]
    public class _photo
    {
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string photoId { get; set; }
        [MaxLength(36)]
        public string itemId { get; set; }
        [MaxLength(100)]
        public string photourl { get; set; }
    }
    
   
}
