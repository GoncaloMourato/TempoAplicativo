using System.ComponentModel.DataAnnotations;

namespace TempoAplicativo.Models
{
    public class LoginViewModel
    {
        public string Id { get; set; }
        [Required]

        public string UserName { get; set; }



        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
