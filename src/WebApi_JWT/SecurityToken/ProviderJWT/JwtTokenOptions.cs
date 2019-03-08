namespace SecurityToken.ProviderJWT
{
    internal class JwtTokenOptions
    {
        private JwtTokenOptions(string issuer, string audience, string secret_Key, string subject)
        {
            Issuer = issuer;
            Audience = audience;
            Secret_Key = secret_Key;
            Subject = subject;
        }

        public  string Issuer { get; private set; }
        public  string Audience { get; private set; }
        public  string Secret_Key { get; private set; }
        public string Subject { get; private set; }

        public static JwtTokenOptions Values = null;

        public static JwtTokenOptions Factory(string issuer, string audience, string secret_Key, string subject)
        {
            return new JwtTokenOptions(issuer, audience, secret_Key, subject);
        }
    }
}
