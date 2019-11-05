using System;
using System.ComponentModel.DataAnnotations;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Enter new name")]
        public string Name { get; set; }
        [Display(Name = "Enter new price")]
        public int Price { get; set; }
        [Display(Name ="Enter new date of creation")]
        public DateTime DateCreated { get; set; }
        public string Author { get; set; }
    }
}
