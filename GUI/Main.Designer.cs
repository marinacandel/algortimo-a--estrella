namespace GUI
{
    partial class Main
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
            this.pbMaze = new System.Windows.Forms.PictureBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnMaze = new System.Windows.Forms.Button();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.X = new System.Windows.Forms.TextBox();
            this.Y = new System.Windows.Forms.TextBox();
            this.Xlabel = new System.Windows.Forms.Label();
            this.Ylabel = new System.Windows.Forms.Label();
            this.tiempo = new System.Windows.Forms.Label();
            this.tiempo1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMaze)).BeginInit();
            this.SuspendLayout();
            // 
            // pbMaze
            // 
            this.pbMaze.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbMaze.Location = new System.Drawing.Point(6, 46);
            this.pbMaze.Margin = new System.Windows.Forms.Padding(2);
            this.pbMaze.Name = "pbMaze";
            this.pbMaze.Size = new System.Drawing.Size(889, 431);
            this.pbMaze.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMaze.TabIndex = 0;
            this.pbMaze.TabStop = false;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGo.Location = new System.Drawing.Point(607, 9);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(85, 24);
            this.btnGo.TabIndex = 31;
            this.btnGo.Text = "Buscar Ruta";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnRecorrer_Click);
            // 
            // btnMaze
            // 
            this.btnMaze.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnMaze.Location = new System.Drawing.Point(6, 9);
            this.btnMaze.Name = "btnMaze";
            this.btnMaze.Size = new System.Drawing.Size(98, 24);
            this.btnMaze.TabIndex = 30;
            this.btnMaze.Text = "Nuevo Laberinto";
            this.btnMaze.UseVisualStyleBackColor = true;
            this.btnMaze.Click += new System.EventHandler(this.Btnlaberinto_Click);
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlgorithm.Location = new System.Drawing.Point(325, 7);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(277, 24);
            this.lblAlgorithm.TabIndex = 0;
            this.lblAlgorithm.Text = "Algoritmo A (Estrella)";
            this.lblAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // X
            // 
            this.X.Location = new System.Drawing.Point(135, 12);
            this.X.Name = "X";
            this.X.Size = new System.Drawing.Size(34, 20);
            this.X.TabIndex = 32;
            this.X.Text = "15";
            // 
            // Y
            // 
            this.Y.Location = new System.Drawing.Point(199, 12);
            this.Y.Name = "Y";
            this.Y.Size = new System.Drawing.Size(34, 20);
            this.Y.TabIndex = 33;
            this.Y.Text = "15";
            // 
            // Xlabel
            // 
            this.Xlabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Xlabel.Location = new System.Drawing.Point(110, 9);
            this.Xlabel.Name = "Xlabel";
            this.Xlabel.Size = new System.Drawing.Size(27, 24);
            this.Xlabel.TabIndex = 34;
            this.Xlabel.Text = "X";
            this.Xlabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ylabel
            // 
            this.Ylabel.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ylabel.Location = new System.Drawing.Point(175, 9);
            this.Ylabel.Name = "Ylabel";
            this.Ylabel.Size = new System.Drawing.Size(27, 24);
            this.Ylabel.TabIndex = 35;
            this.Ylabel.Text = "Y";
            this.Ylabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tiempo
            // 
            this.tiempo.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tiempo.Location = new System.Drawing.Point(697, 9);
            this.tiempo.Name = "tiempo";
            this.tiempo.Size = new System.Drawing.Size(88, 24);
            this.tiempo.TabIndex = 36;
            this.tiempo.Text = "tiempo";
            this.tiempo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tiempo1
            // 
            this.tiempo1.Location = new System.Drawing.Point(778, 12);
            this.tiempo1.Name = "tiempo1";
            this.tiempo1.Size = new System.Drawing.Size(34, 20);
            this.tiempo1.TabIndex = 37;
            this.tiempo1.Text = "1";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(902, 484);
            this.Controls.Add(this.tiempo1);
            this.Controls.Add(this.tiempo);
            this.Controls.Add(this.Ylabel);
            this.Controls.Add(this.Xlabel);
            this.Controls.Add(this.Y);
            this.Controls.Add(this.X);
            this.Controls.Add(this.lblAlgorithm);
            this.Controls.Add(this.btnMaze);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.pbMaze);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "A*";
            ((System.ComponentModel.ISupportInitialize)(this.pbMaze)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMaze;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnMaze;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.TextBox X;
        private System.Windows.Forms.TextBox Y;
        private System.Windows.Forms.Label Xlabel;
        private System.Windows.Forms.Label Ylabel;
        private System.Windows.Forms.Label tiempo;
        private System.Windows.Forms.TextBox tiempo1;
    }
}

