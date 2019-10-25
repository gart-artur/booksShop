
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
    public class GetAllBooksViewModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public DateTime DateCreated { get; set; }
        public List<GetAllBooksViewModelItem> Books { get; set; }                   
    }
}
