
namespace TeknikServis
{
    partial class AnaSayfa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaSayfa));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.arızaKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arızaKyıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arızaRaporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arızaBildirimleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yönetimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personelKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.şifreYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.müşteriYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.işDurumYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ürünYönetimiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(100, 800);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.arızaKayıtToolStripMenuItem,
            this.yönetimToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(154, 585);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.Gray;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI Semibold", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 36);
            this.toolStripMenuItem1.Text = "BÖLÜMLER";
            // 
            // arızaKayıtToolStripMenuItem
            // 
            this.arızaKayıtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arızaKyıtToolStripMenuItem,
            this.arızaRaporToolStripMenuItem,
            this.arızaBildirimleriToolStripMenuItem});
            this.arızaKayıtToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.arızaKayıtToolStripMenuItem.Name = "arızaKayıtToolStripMenuItem";
            this.arızaKayıtToolStripMenuItem.Size = new System.Drawing.Size(145, 32);
            this.arızaKayıtToolStripMenuItem.Text = "Arıza";
            // 
            // arızaKyıtToolStripMenuItem
            // 
            this.arızaKyıtToolStripMenuItem.Name = "arızaKyıtToolStripMenuItem";
            this.arızaKyıtToolStripMenuItem.Size = new System.Drawing.Size(259, 32);
            this.arızaKyıtToolStripMenuItem.Text = "Arıza Kayıt";
            this.arızaKyıtToolStripMenuItem.Click += new System.EventHandler(this.arızaKyıtToolStripMenuItem_Click);
            // 
            // arızaRaporToolStripMenuItem
            // 
            this.arızaRaporToolStripMenuItem.Name = "arızaRaporToolStripMenuItem";
            this.arızaRaporToolStripMenuItem.Size = new System.Drawing.Size(259, 32);
            this.arızaRaporToolStripMenuItem.Text = "Arıza Rapor";
            this.arızaRaporToolStripMenuItem.Click += new System.EventHandler(this.arızaRaporToolStripMenuItem_Click);
            // 
            // arızaBildirimleriToolStripMenuItem
            // 
            this.arızaBildirimleriToolStripMenuItem.Name = "arızaBildirimleriToolStripMenuItem";
            this.arızaBildirimleriToolStripMenuItem.Size = new System.Drawing.Size(259, 32);
            this.arızaBildirimleriToolStripMenuItem.Text = "Arıza Bildirimleri";
            this.arızaBildirimleriToolStripMenuItem.Click += new System.EventHandler(this.arızaBildirimleriToolStripMenuItem_Click);
            // 
            // yönetimToolStripMenuItem
            // 
            this.yönetimToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.personelKayıtToolStripMenuItem,
            this.şifreYönetimiToolStripMenuItem,
            this.müşteriYönetimiToolStripMenuItem,
            this.işDurumYönetimiToolStripMenuItem,
            this.ürünYönetimiToolStripMenuItem});
            this.yönetimToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yönetimToolStripMenuItem.Name = "yönetimToolStripMenuItem";
            this.yönetimToolStripMenuItem.Size = new System.Drawing.Size(145, 32);
            this.yönetimToolStripMenuItem.Text = "Yönetim";
            // 
            // personelKayıtToolStripMenuItem
            // 
            this.personelKayıtToolStripMenuItem.Name = "personelKayıtToolStripMenuItem";
            this.personelKayıtToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.personelKayıtToolStripMenuItem.Text = "Personel Kayıt";
            this.personelKayıtToolStripMenuItem.Click += new System.EventHandler(this.personelKayıtToolStripMenuItem_Click);
            // 
            // şifreYönetimiToolStripMenuItem
            // 
            this.şifreYönetimiToolStripMenuItem.Name = "şifreYönetimiToolStripMenuItem";
            this.şifreYönetimiToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.şifreYönetimiToolStripMenuItem.Text = "Şifre Yönetimi";
            this.şifreYönetimiToolStripMenuItem.Click += new System.EventHandler(this.şifreYönetimiToolStripMenuItem_Click);
            // 
            // müşteriYönetimiToolStripMenuItem
            // 
            this.müşteriYönetimiToolStripMenuItem.Name = "müşteriYönetimiToolStripMenuItem";
            this.müşteriYönetimiToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.müşteriYönetimiToolStripMenuItem.Text = "Müşteri yönetimi";
            this.müşteriYönetimiToolStripMenuItem.Click += new System.EventHandler(this.müşteriYönetimiToolStripMenuItem_Click);
            // 
            // işDurumYönetimiToolStripMenuItem
            // 
            this.işDurumYönetimiToolStripMenuItem.Name = "işDurumYönetimiToolStripMenuItem";
            this.işDurumYönetimiToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.işDurumYönetimiToolStripMenuItem.Text = "İş Durum Yönetimi";
            this.işDurumYönetimiToolStripMenuItem.Click += new System.EventHandler(this.işDurumYönetimiToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 552);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuStrip2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(100, 900);
            this.menuStrip2.Location = new System.Drawing.Point(154, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1006, 30);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(476, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(299, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "TEKNİK SERVİS ANASAYFA";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(954, 521);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 64);
            this.button1.TabIndex = 4;
            this.button1.Text = "Giriş Ekranına Dönmek İçin Tıklayınız";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(156, 556);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 29);
            this.label3.TabIndex = 5;
            this.label3.Text = "label3";
            // 
            // ürünYönetimiToolStripMenuItem
            // 
            this.ürünYönetimiToolStripMenuItem.Name = "ürünYönetimiToolStripMenuItem";
            this.ürünYönetimiToolStripMenuItem.Size = new System.Drawing.Size(273, 32);
            this.ürünYönetimiToolStripMenuItem.Text = "Ürün Yönetimi";
            this.ürünYönetimiToolStripMenuItem.Click += new System.EventHandler(this.ürünYönetimiToolStripMenuItem_Click);
            // 
            // AnaSayfa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1160, 585);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "AnaSayfa";
            this.Text = "AnaSayfa";
            this.Load += new System.EventHandler(this.AnaSayfa_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem yönetimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personelKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem arızaKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arızaKyıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arızaRaporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem şifreYönetimiToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem müşteriYönetimiToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem arızaBildirimleriToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem işDurumYönetimiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ürünYönetimiToolStripMenuItem;
    }
}