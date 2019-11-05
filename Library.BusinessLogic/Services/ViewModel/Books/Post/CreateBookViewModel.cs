using System;
using System.ComponentModel.DataAnnotations;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
    public  class CreateBookViewModel
    {        
        [Display(Name = "Enter Book name")]
        public string Name { get; set; }
        [Display(Name = "Enter Price")]
        public int Price { get; set; }
        [Display(Name = "Data of created your Book")]
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
    }  
}
