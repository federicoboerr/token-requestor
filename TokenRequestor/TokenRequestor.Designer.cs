namespace GetToken
{
    partial class TokenRequestorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.getButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.PostTokenButton = new System.Windows.Forms.Button();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.MinimumSize = new System.Drawing.Size(0, 50);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.PostTokenButton);
            this.splitContainer.Panel1.Controls.Add(this.getButton);
            this.splitContainer.Panel1.Controls.Add(this.urlTextBox);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.webBrowser1);
            this.splitContainer.Size = new System.Drawing.Size(766, 415);
            this.splitContainer.SplitterDistance = 40;
            this.splitContainer.TabIndex = 1;
            // 
            // getButton
            // 
            this.getButton.Location = new System.Drawing.Point(566, 10);
            this.getButton.Name = "getButton";
            this.getButton.Size = new System.Drawing.Size(46, 19);
            this.getButton.TabIndex = 1;
            this.getButton.Text = "Get";
            this.getButton.UseVisualStyleBackColor = true;
            this.getButton.Click += new System.EventHandler(this.getButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.Location = new System.Drawing.Point(9, 10);
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(532, 20);
            this.urlTextBox.TabIndex = 0;
            this.urlTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.urlTextBox_KeyDown);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(766, 371);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser1_Navigating);
            // 
            // PostTokenButton
            // 
            this.PostTokenButton.Location = new System.Drawing.Point(635, 10);
            this.PostTokenButton.Name = "PostTokenButton";
            this.PostTokenButton.Size = new System.Drawing.Size(79, 19);
            this.PostTokenButton.TabIndex = 2;
            this.PostTokenButton.Text = "Post Token";
            this.PostTokenButton.UseVisualStyleBackColor = true;
            this.PostTokenButton.Visible = false;
            this.PostTokenButton.Click += new System.EventHandler(this.PostTokenButton_Click);
            // 
            // TokenRequestorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 415);
            this.Controls.Add(this.splitContainer);
            this.Name = "TokenRequestorForm";
            this.Text = "Token Requestor";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button getButton;
        private System.Windows.Forms.TextBox urlTextBox;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button PostTokenButton;

    }
}

