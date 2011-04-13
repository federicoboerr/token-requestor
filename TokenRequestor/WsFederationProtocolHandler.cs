namespace GetToken
{
    using System.Text.RegularExpressions;

    public class WsFederationProtocolHandler : IProtocolHandler
    {
        public const string Pattern = @"<input type=""hidden"" name=""wresult"" value=""(?<token>[^""]*)""";

        public bool CanProcess(string response)
        {
            return Regex.IsMatch(response, Pattern);
        }

        public string Process(string response)
        {
            var tokenHtmlEncoded = Regex.Match(response, Pattern).Groups["token"].Value;
            var token = System.Web.HttpUtility.HtmlDecode(tokenHtmlEncoded);

            return token;
        }
    }
}
