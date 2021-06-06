using System.Collections.Generic;


namespace Library.BusinessLogic.Services.ViewModel.Authors.Post
{
    public class GetAllAuthorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<GetAllAuthorViewModelItem> Books { get; set; }
    }

    public class GetAllAuthorViewModelItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
