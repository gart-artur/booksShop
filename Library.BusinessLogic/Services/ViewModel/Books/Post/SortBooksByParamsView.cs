using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
   public class SortBooksByParamsView
    {
        public string Name { get; set; }
        public int MaxPrice{ get; set; }
        public int MinPrice { get; set; }
    }
}
