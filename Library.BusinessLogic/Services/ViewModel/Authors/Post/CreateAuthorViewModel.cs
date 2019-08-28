using System.ComponentModel.DataAnnotations;

namespace Library.BusinessLogic.Services.ViewModel.Authors.Post
{
    public class CreateAuthorViewModel
    {

       /* public int Id { get; set; }*/
        [Display(Name = "Author Name")]
        public string Name { get; set;  }
    }
}
