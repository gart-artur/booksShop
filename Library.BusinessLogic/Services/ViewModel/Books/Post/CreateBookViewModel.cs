using System;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
    public  class CreateBookViewModel
    {      
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
    }  
}
