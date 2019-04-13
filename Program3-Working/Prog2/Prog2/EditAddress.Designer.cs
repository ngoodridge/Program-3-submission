namespace Prog2
{
    partial class EditAddress
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
            this.components = new System.ComponentModel.Container();
            this.AddressCombo = new System.Windows.Forms.ComboBox();
            this.Edit_OK = new System.Windows.Forms.Button();
            this.Edit_Cancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // AddressCombo
            // 
            this.AddressCombo.FormattingEnabled = true;
            this.AddressCombo.Location = new System.Drawing.Point(12, 12);
            this.AddressCombo.Name = "AddressCombo";
            this.AddressCombo.Size = new System.Drawing.Size(154, 21);
            this.AddressCombo.TabIndex = 0;
            this.AddressCombo.Validating += new System.ComponentModel.CancelEventHandler(this.AddressCombo_validating);
            this.AddressCombo.Validated += new System.EventHandler(this.AddressCombo_validated);
            // 
            // Edit_OK
            // 
            this.Edit_OK.Location = new System.Drawing.Point(12, 39);
            this.Edit_OK.Name = "Edit_OK";
            this.Edit_OK.Size = new System.Drawing.Size(75, 23);
            this.Edit_OK.TabIndex = 1;
            this.Edit_OK.Text = "OK";
            this.Edit_OK.UseVisualStyleBackColor = true;
            this.Edit_OK.Click += new System.EventHandler(this.Edit_OK_Click);
            // 
            // Edit_Cancel
            // 
            this.Edit_Cancel.CausesValidation = false;
            this.Edit_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Edit_Cancel.Location = new System.Drawing.Point(93, 39);
            this.Edit_Cancel.Name = "Edit_Cancel";
            this.Edit_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Edit_Cancel.TabIndex = 2;
            this.Edit_Cancel.Text = "Cancel";
            this.Edit_Cancel.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // EditAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 71);
            this.Controls.Add(this.Edit_Cancel);
            this.Controls.Add(this.Edit_OK);
            this.Controls.Add(this.AddressCombo);
            this.Name = "EditAddress";
            this.Text = "EditAddress";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox AddressCombo;
        private System.Windows.Forms.Button Edit_OK;
        private System.Windows.Forms.Button Edit_Cancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}