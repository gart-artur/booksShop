
using Library.BusinessLogic.Services.ViewModel.Authors.Post;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
  public  class CreateBookViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Enter Book name")]
        public string Name { get; set; }
        [Display(Name = "Enter Price")]
        public int Price { get; set; }
        [Display(Name = "Data of created your Book")]
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }


    }
  
}
