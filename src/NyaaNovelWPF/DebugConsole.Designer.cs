namespace NyaaNovel
{
    partial class DebugConsole
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
            this.ConsoleText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // ConsoleText
            // 
            this.ConsoleText.Location = new System.Drawing.Point(4, 3);
            this.ConsoleText.Name = "ConsoleText";
            this.ConsoleText.Size = new System.Drawing.Size(595, 338);
            this.ConsoleText.TabIndex = 0;
            this.ConsoleText.Text = "";
            // 
            // DebugConsole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 343);
            this.Controls.Add(this.ConsoleText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DebugConsole";
            this.Text = "DebugConsole";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox ConsoleText;
    }
}