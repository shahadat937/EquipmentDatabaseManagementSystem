namespace SchoolManagement.Application.Models.Identity
{
    public class AuthRequest
    {
        public string Email { get; set; }        
        public string Password { get; set; }
        public int? CaptchaAnswer { get; set; }
        public int? CaptchaSum { get; set; }
    }
}
