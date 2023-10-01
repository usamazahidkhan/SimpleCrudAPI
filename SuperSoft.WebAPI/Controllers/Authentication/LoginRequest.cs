namespace SuperSoft.WebAPI.Controllers.Authentication
{
    public sealed class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}