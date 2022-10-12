namespace reg_log.Models
{
    using System.ComponentModel.DataAnnotations;
    public class UserLogin
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
    public class UserRegister
    {
        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not much")]
        public string? ConfirmPassword { get; set; }

    }
}