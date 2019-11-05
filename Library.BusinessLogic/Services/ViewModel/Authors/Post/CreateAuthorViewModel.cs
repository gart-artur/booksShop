using System.ComponentModel.DataAnnotations;

namespace Library.BusinessLogic.Services.ViewModel.Authors.Post
{
    public class CreateAuthorViewModel
    {
        [Display(Name = "Author Name")]
        public string Name { get; set;  }
    }
}
