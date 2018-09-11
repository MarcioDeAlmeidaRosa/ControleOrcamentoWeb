using System.ComponentModel.DataAnnotations;

namespace ControleOrcamentoWeb.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}