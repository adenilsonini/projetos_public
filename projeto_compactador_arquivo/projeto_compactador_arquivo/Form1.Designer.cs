
namespace projeto_compactador_arquivo
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grouper1 = new projeto_compactador_arquivo.Grouper();
            this.optrar = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.opt7z = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtdest = new System.Windows.Forms.TextBox();
            this.txtcaminho_comp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grouper1
            // 
            this.grouper1.BackgroundColor = System.Drawing.Color.White;
            this.grouper1.BackgroundGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.grouper1.BackgroundGradientMode = projeto_compactador_arquivo.Grouper.GroupBoxGradientMode.Vertical;
            this.grouper1.BorderColor = System.Drawing.Color.Black;
            this.grouper1.BorderThickness = 1F;
            this.grouper1.Controls.Add(this.optrar);
            this.grouper1.Controls.Add(this.button2);
            this.grouper1.Controls.Add(this.opt7z);
            this.grouper1.Controls.Add(this.button3);
            this.grouper1.Controls.Add(this.button1);
            this.grouper1.Controls.Add(this.txtdest);
            this.grouper1.Controls.Add(this.txtcaminho_comp);
            this.grouper1.Controls.Add(this.label2);
            this.grouper1.Controls.Add(this.label1);
            this.grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this.grouper1.GroupImage = null;
            this.grouper1.GroupTitle = "Forme de compactar arquivos";
            this.grouper1.Location = new System.Drawing.Point(12, 12);
            this.grouper1.Name = "grouper1";
            this.grouper1.Padding = new System.Windows.Forms.Padding(20);
            this.grouper1.PaintGroupBox = false;
            this.grouper1.RoundCorners = 10;
            this.grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this.grouper1.ShadowControl = true;
            this.grouper1.ShadowThickness = 3;
            this.grouper1.Size = new System.Drawing.Size(602, 143);
            this.grouper1.TabIndex = 0;
            // 
            // optrar
            // 
            this.optrar.AutoSize = true;
            this.optrar.Location = new System.Drawing.Point(470, 45);
            this.optrar.Name = "optrar";
            this.optrar.Size = new System.Drawing.Size(104, 19);
            this.optrar.TabIndex = 6;
            this.optrar.Text = "Compactar .rar";
            this.optrar.UseVisualStyleBackColor = true;
            this.optrar.CheckedChanged += new System.EventHandler(this.optrar_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.Location = new System.Drawing.Point(408, 104);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(33, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // opt7z
            // 
            this.opt7z.AutoSize = true;
            this.opt7z.Checked = true;
            this.opt7z.Location = new System.Drawing.Point(470, 20);
            this.opt7z.Name = "opt7z";
            this.opt7z.Size = new System.Drawing.Size(101, 19);
            this.opt7z.TabIndex = 5;
            this.opt7z.TabStop = true;
            this.opt7z.Text = "Compactar .7z";
            this.opt7z.UseVisualStyleBackColor = true;
            this.opt7z.CheckedChanged += new System.EventHandler(this.opt7z_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button3.ForeColor = System.Drawing.Color.Green;
            this.button3.Location = new System.Drawing.Point(470, 74);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 53);
            this.button3.TabIndex = 4;
            this.button3.Text = "Compactar";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(408, 56);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtdest
            // 
            this.txtdest.Location = new System.Drawing.Point(10, 104);
            this.txtdest.Name = "txtdest";
            this.txtdest.Size = new System.Drawing.Size(392, 23);
            this.txtdest.TabIndex = 1;
            // 
            // txtcaminho_comp
            // 
            this.txtcaminho_comp.Location = new System.Drawing.Point(10, 56);
            this.txtcaminho_comp.Name = "txtcaminho_comp";
            this.txtcaminho_comp.ReadOnly = true;
            this.txtcaminho_comp.Size = new System.Drawing.Size(392, 23);
            this.txtcaminho_comp.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(307, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Pasta destino dos arquivo Compactado";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pasta origem dos arquivos";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 164);
            this.Controls.Add(this.grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Compactador de arquivos /.7z/.rar";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grouper1.ResumeLayout(false);
            this.grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Grouper grouper1;
        private System.Windows.Forms.TextBox txtcaminho_comp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtdest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RadioButton opt7z;
        private System.Windows.Forms.RadioButton optrar;
    }
}

