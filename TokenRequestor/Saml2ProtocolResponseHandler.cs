namespace GetToken
{
    using System;
    using System.Text.RegularExpressions;

    public class Saml2ProtocolResponseHandler : IProtocolHandler
    {
        public const string Pattern = @"<input type=""hidden"" name=""SAMLResponse"" value=""(?<token>[^""]*)""";

        public bool CanProcess(string response)
        {
            return Regex.IsMatch(response, Pattern);
        }

        public string Process(string response)
        {
            var tokenInBase64 = Regex.Match(response, Pattern).Groups["token"].Value;
            var token = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(tokenInBase64));

            return token;
        }
    }
}
