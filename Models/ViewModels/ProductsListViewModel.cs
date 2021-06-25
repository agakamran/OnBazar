using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnBazar.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
