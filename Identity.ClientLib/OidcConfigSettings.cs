namespace Identity.ClientLib
{
    public class OidcConfigSettings
    {
        public string Authority { get; set; } = string.Empty;
        public string ClientID { get; set; } = string.Empty;
        public List<string> Scopes { get; set; } = new();
        public string ClientSecret { get; set; } = string.Empty;
    }
}