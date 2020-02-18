using System.ComponentModel.DataAnnotations;

namespace TaskBoard.ViewModels
{
    public class RegistrationModel : AuthorizationModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
