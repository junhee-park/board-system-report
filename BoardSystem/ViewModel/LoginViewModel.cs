using System.ComponentModel.DataAnnotations;

namespace BoardSystem.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "ユーザーIDを入力してください。")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "ユーザーパスワードを入力してください。")]
        public string UserPassword { get; set; }
    }
}
