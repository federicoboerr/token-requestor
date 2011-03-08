namespace GetToken
{
    using System;
    using System.IO;
    using System.Windows.Forms;
    using Microsoft.IdentityModel.Tokens;

    public partial class TokenRequestorForm : Form
    {
        private Uri SavedTokenEndpoint;
        private string SavedToken;
        public TokenRequestorForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string token = string.Empty;

            var samlResponseRegex = new System.Text.RegularExpressions.Regex(@"<input type=""hidden"" name=""SAMLResponse"" value=""(?<token>[^""]*)""");
            if (samlResponseRegex.IsMatch(this.webBrowser1.DocumentText))
            {
                var tokenInBase64 = samlResponseRegex.Match(this.webBrowser1.DocumentText).Groups["token"].Value;
                token = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(tokenInBase64));

                //var samlAssertion = XDocument.Parse(token).Descendants().FirstOrDefault(x => x.Name.LocalName == "Assertion");

                //var tokenHandler = Microsoft.IdentityModel.Tokens.SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
                //tokenHandler.Configuration.IssuerTokenResolver = new CustomResolver();
                //var secToken = tokenHandler.ReadToken(XmlReader.Create(new StringReader(samlAssertion.ToString())));
                //var claims = tokenHandler.ValidateToken(secToken);
            }
            
            var wsFederationResponseRegex = new System.Text.RegularExpressions.Regex(@"<input type=""hidden"" name=""wresult"" value=""(?<token>[^""]*)""");
            if (wsFederationResponseRegex.IsMatch(this.webBrowser1.DocumentText))
            {
                var tokenHtmlEncoded = wsFederationResponseRegex.Match(this.webBrowser1.DocumentText).Groups["token"].Value;
                token = System.Web.HttpUtility.HtmlDecode(tokenHtmlEncoded);
            }

            if (!string.IsNullOrEmpty(token) && this.SavedTokenEndpoint != e.Url)
            {
                this.SavedTokenEndpoint = e.Url;
                this.SavedToken = webBrowser1.DocumentText;
                e.Cancel = true;
                webBrowser1.Document.OpenNew(true);
                var fileWriter = File.CreateText("testfile.xml");
                fileWriter.Write(token);
                fileWriter.Close();
                webBrowser1.Url = new Uri(Path.Combine(Application.StartupPath, "testfile.xml"));
            }
            
            this.PostTokenButton.Visible = e.Url.Segments[e.Url.Segments.Length - 1] == "testfile.xml";
        }

        public class CustomResolver : IssuerTokenResolver
        {
            //protected override bool TryResolveSecurityKeyCore(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause, out System.IdentityModel.Tokens.SecurityKey key)
            //{
            //    key = null;
            //    return true;
            //}

            protected override bool TryResolveTokenCore(System.IdentityModel.Tokens.SecurityKeyIdentifier keyIdentifier, out System.IdentityModel.Tokens.SecurityToken token)
            {
                token = null;
                return true;
            }

            protected override bool TryResolveTokenCore(System.IdentityModel.Tokens.SecurityKeyIdentifierClause keyIdentifierClause, out System.IdentityModel.Tokens.SecurityToken token)
            {
                token = null;
                return true;
            }
        }

        private void getButton_Click(object sender, EventArgs e)
        {
            this.NavigateTo(urlTextBox.Text);
        }

        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.NavigateTo(urlTextBox.Text);
            }
        }

        private void NavigateTo(string location)
        {
            this.webBrowser1.Navigate(location);
        }

        private void PostTokenButton_Click(object sender, EventArgs e)
        {
            if (this.SavedToken != null && this.SavedTokenEndpoint != null)
            {
                webBrowser1.Document.OpenNew(true);
                this.webBrowser1.Document.Write(this.SavedToken);
                //this.webBrowser1.Navigate(this.SavedTokenEndpoint);
            }
        }
    }
}
