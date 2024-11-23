namespace Readings_Guide.Cores.Dtos
{
    public class AuthModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsAuthntecation { get; set; }
        public DateTime ExpireOn { get; set; }
        public List<string> Roles { get; set; }
    }
}
