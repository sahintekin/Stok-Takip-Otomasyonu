namespace StokTakipOtomasyonu
{
    partial class SifreYenileme
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
            this.textKullanici = new System.Windows.Forms.TextBox();
            this.btnDegistir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maskedTextSifre = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // textKullanici
            // 
            this.textKullanici.Location = new System.Drawing.Point(165, 38);
            this.textKullanici.Name = "textKullanici";
            this.textKullanici.Size = new System.Drawing.Size(100, 27);
            this.textKullanici.TabIndex = 0;
            // 
            // btnDegistir
            // 
            this.btnDegistir.BackColor = System.Drawing.Color.Snow;
            this.btnDegistir.ForeColor = System.Drawing.Color.Red;
            this.btnDegistir.Location = new System.Drawing.Point(145, 132);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(130, 53);
            this.btnDegistir.TabIndex = 2;
            this.btnDegistir.Text = "Değiştir";
            this.btnDegistir.UseVisualStyleBackColor = false;
            this.btnDegistir.Click += new System.EventHandler(this.btnDegistir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kulanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(55, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Şifre";
            // 
            // maskedTextSifre
            // 
            this.maskedTextSifre.Location = new System.Drawing.Point(165, 89);
            this.maskedTextSifre.Mask = "000000";
            this.maskedTextSifre.Name = "maskedTextSifre";
            this.maskedTextSifre.PasswordChar = '*';
            this.maskedTextSifre.Size = new System.Drawing.Size(100, 27);
            this.maskedTextSifre.TabIndex = 5;
            // 
            // SifreYenileme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.ClientSize = new System.Drawing.Size(304, 243);
            this.Controls.Add(this.maskedTextSifre);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDegistir);
            this.Controls.Add(this.textKullanici);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ForeColor = System.Drawing.Color.Aquamarine;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SifreYenileme";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SifreYenileme";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textKullanici;
        private System.Windows.Forms.Button btnDegistir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox maskedTextSifre;
    }
}