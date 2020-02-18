using System.ComponentModel.DataAnnotations;

namespace TaskBoard.ViewModels
{
    public class AuthorizationModel
    {

        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}
