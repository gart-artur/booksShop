using System.Collections.Generic;


namespace Library.BusinessLogic.Services.ViewModel.Authors.Post
{
    public class GetAllAuthorViewModelItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetAllAuthorViewModelItem> Authors { get; set; }
    }
}
