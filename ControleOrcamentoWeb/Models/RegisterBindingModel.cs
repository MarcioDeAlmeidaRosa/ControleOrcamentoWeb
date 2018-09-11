using System.ComponentModel.DataAnnotations;

namespace ControleOrcamentoWeb.Models
{
    public class RegisterBindingModel
    {
        [EmailAddress]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo E-mail é obrigatório.")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessage = "A senha não confere com a confirmação de senha.")]
        [Required(ErrorMessage = "O campo Confirmar senha é obrigatório.")]
        public string ConfirmacaoSenha { get; set; }
    }
}