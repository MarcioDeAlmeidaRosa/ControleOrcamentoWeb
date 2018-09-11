using System.ComponentModel.DataAnnotations;

namespace ControleOrcamentoWeb.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}