using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services.ViewModel.Books.Post
{
    public class FilterBooksListView
    {
        public IEnumerable<Book> Books { get; set; }        
    }

}
