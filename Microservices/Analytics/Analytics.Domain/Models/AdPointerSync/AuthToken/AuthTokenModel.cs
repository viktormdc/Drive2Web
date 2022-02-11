namespace Analytics.Domain.Models.AdPointerSync.AuthToken
{
    public class AuthTokenModel
    {
        public string tokenType { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
