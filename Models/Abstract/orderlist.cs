using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnBazar.Models.Abstract
{
   
    [Table("orderms")]
    public class orderm
    {
        public orderm()
        {
            this.orderds = new HashSet<orderd>();
        }
        [HiddenInput(DisplayValue = false)]
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ormID { get; set; }
        public string ordName { get; set; }
        public string ordTelefon { get; set; }
        public string Unvan { get; set; }
        public DateTime ordtarix { get; set; }
        public DateTime otptarix { get; set; }
        public decimal summ { get; set; }
        public bool getdi { get; set; }
        public virtual ICollection<orderd> orderds { get; set; }
    }
    [Table("orderds")]
    public class orderd
    {
        public orderd()
        {
            this.orderms = new HashSet<orderm>();
        }
        [HiddenInput(DisplayValue = false)]
        [Key]
        [Required(AllowEmptyStrings = true), MaxLength(36)]
        public string ordId { get; set; }
        public string Prodname { get; set; }
        public decimal Qountity { get; set; }
        public decimal Price { get; set; }
        [MaxLength(36)]
        public string ormID { get; set; }
        public virtual ICollection<orderm> orderms { get; set; }
    }
    [Table("shipDetails")]
    public class shipDetail
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

}
