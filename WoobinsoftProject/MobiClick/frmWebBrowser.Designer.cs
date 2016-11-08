namespace MobiClick
{
    partial class frmWebBrowser
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
            this.naverWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // naverWebBrowser
            // 
            this.naverWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naverWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.naverWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.naverWebBrowser.Name = "naverWebBrowser";
            this.naverWebBrowser.Size = new System.Drawing.Size(469, 706);
            this.naverWebBrowser.TabIndex = 0;
            // 
            // frmWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 706);
            this.Controls.Add(this.naverWebBrowser);
            this.Name = "frmWebBrowser";
            this.Text = "frmWebBrowser";
            this.Load += new System.EventHandler(this.frmWebBrowser_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser naverWebBrowser;
    }
}