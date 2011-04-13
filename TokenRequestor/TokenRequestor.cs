namespace GetToken
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Diagnostics;
    using System.Xml;
    using System.Text;
    using System.Net.Mail;
    using System.Net;
    using System.Configuration;

    public partial class TokenRequestorForm : Form
    {
        private Uri SavedTokenEndpoint;
        private string SavedToken;

        private IEnumerable<IProtocolHandler> pipeline = new IProtocolHandler[] { 
            new WsFederationProtocolHandler(), 
            new Saml2ProtocolResponseHandler() 
        };

        public TokenRequestorForm()
        {
            InitializeComponent();

            this.UrlTextBox.Text = "<enter a valid url>";
        }

        private void Browser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            string token = this.AnalyzeResponse(this.Browser.DocumentText);

            if (!string.IsNullOrEmpty(token) && this.SavedTokenEndpoint != e.Url)
            {
                this.SavedTokenEndpoint = e.Url;
                this.SavedToken = this.Browser.DocumentText;
                e.Cancel = true;
                this.Browser.Document.OpenNew(true);
                var file = File.CreateText("token.xml");
                file.Write(token);
                file.Close();

                this.Browser.Url = new Uri(Path.Combine(Application.StartupPath, "token.xml"));
            }
        }

        private string AnalyzeResponse(string response)
        {
            string token = string.Empty;

            foreach (var handler in this.pipeline)
            {
                if (handler.CanProcess(response))
                {
                    token = handler.Process(this.Browser.DocumentText);
                }
            }

            return token;
        }

        private void urlTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.NavigateTo(UrlTextBox.Text);
            }
        }

        private void PostTokenButton_Click(object sender, EventArgs e)
        {
            if (this.SavedToken != null && this.SavedTokenEndpoint != null)
            {
                this.Browser.Document.OpenNew(true);
                this.Browser.Document.Write(this.SavedToken);
            }
        }

        private void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            this.UrlTextBox.Text = e.Url.ToString();
        }

        private void Browser_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            this.BrowserProgessBar.Maximum = (int)e.MaximumProgress;
            this.BrowserProgessBar.Value = (int)e.CurrentProgress;
            this.BrowserProgessBar.ProgressBar.Refresh();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            this.NavigateTo(UrlTextBox.Text);
        }

        private void NavigateTo(string location)
        {
            this.Browser.Navigate(location);
        }

        private void Browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            bool isToken = this.Browser.Url.ToString().Contains("token.xml");
            this.PostTokenButton.Enabled = isToken;
            this.SendTokenButton.Enabled = isToken;
        }

        private void SendTokenButton_Click(object sender, EventArgs e)
        {
            var token = GetTokenFormatted();

            var to = new MailAddress(ConfigurationManager.AppSettings["To"]);
            string subject = ConfigurationManager.AppSettings["Subject"]
                                .Replace("{date}", DateTime.Now.ToString());

            var smtp = new SmtpClient();
            smtp.EnableSsl = smtp.Port == 587 || smtp.Port == 465;

            using (var message = new MailMessage()
            {              
                Subject = subject,
                Body = token
            })
            {
                message.To.Add(to);
                smtp.Send(message);
            }

            MessageBox.Show("Mail sent!");
        }

        private static string GetTokenFormatted()
        {
            var sb = new StringBuilder();
            XmlTextWriter w = new XmlTextWriter(new StringWriter(sb));
            w.Formatting = Formatting.Indented;
            w.IndentChar = '\t';

            XmlReader reader = XmlReader.Create(new FileStream(Path.Combine(Application.StartupPath, "token.xml"), FileMode.Open));
            w.WriteNode(reader, true);
            w.Flush();
            return sb.ToString();
        }
    }
}
