using System.Drawing;
using System.Windows.Forms;

namespace topaz
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Topaz.SigPlusNET sigPlusNET1;
        private System.Windows.Forms.Panel signaturePanel;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Button clearButton;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.signaturePanel = new System.Windows.Forms.Panel();
            this.sigPlusNET1 = new Topaz.SigPlusNET();
            this.doneButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // signaturePanel
            // 
            this.signaturePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.signaturePanel.Location = new System.Drawing.Point(10, 12);
            this.signaturePanel.BackColor = Color.White;
            this.signaturePanel.Name = "signaturePanel";
            this.signaturePanel.Size = new System.Drawing.Size(320, 114);
            
            // 
            // Clear Button
            //
            this.clearButton.Location = new System.Drawing.Point(344, 11);
            this.clearButton.Name = "cmdStart";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 0;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // 
            // Done Button
            //
            this.doneButton.Location = new System.Drawing.Point(344, 103);
            this.doneButton.Name = "cmdStart";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 0;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);

            // 
            // sigPlusNET1
            // 
            this.sigPlusNET1.BackColor = System.Drawing.Color.White;
            this.sigPlusNET1.ForeColor = System.Drawing.Color.Black;
            this.sigPlusNET1.Location = new System.Drawing.Point(20, 0);
            this.sigPlusNET1.Name = "sigPlusNET1";
            this.sigPlusNET1.Size = new System.Drawing.Size(320, 114);
            this.sigPlusNET1.TabIndex = 0;
            this.sigPlusNET1.Text = "sigPlusNET1";
            this.sigPlusNET1.PenUp += new System.EventHandler(this.sigPlusNET1_PenUp);
            this.signaturePanel.Controls.Add(this.sigPlusNET1);

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 138);
            this.Controls.Add(this.signaturePanel);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.doneButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Signature Capture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);  // Add the Load event
            this.ResumeLayout(false);
        }

        #endregion
    }
}
