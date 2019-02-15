namespace SimuladorProcesoPorLotes
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.IngresaButton = new System.Windows.Forms.Button();
            this.cantidadBox = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.cantidadBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.830189F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(170)))), ((int)(((byte)(51)))));
            this.label1.Location = new System.Drawing.Point(3, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "No. de Procesos:";
            // 
            // IngresaButton
            // 
            this.IngresaButton.Location = new System.Drawing.Point(115, 141);
            this.IngresaButton.Name = "IngresaButton";
            this.IngresaButton.Size = new System.Drawing.Size(75, 23);
            this.IngresaButton.TabIndex = 2;
            this.IngresaButton.Text = "Ingresar";
            this.IngresaButton.UseVisualStyleBackColor = true;
            this.IngresaButton.Click += new System.EventHandler(this.IngresaButton_Click);
            // 
            // cantidadBox
            // 
            this.cantidadBox.Location = new System.Drawing.Point(142, 25);
            this.cantidadBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantidadBox.Name = "cantidadBox";
            this.cantidadBox.Size = new System.Drawing.Size(120, 20);
            this.cantidadBox.TabIndex = 3;
            this.cantidadBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(85)))));
            this.panel1.Controls.Add(this.cantidadBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 56);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 59);
            this.panel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(101)))), ((int)(((byte)(114)))));
            this.ClientSize = new System.Drawing.Size(304, 190);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.IngresaButton);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.cantidadBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button IngresaButton;
        private System.Windows.Forms.NumericUpDown cantidadBox;
        private System.Windows.Forms.Panel panel1;
    }
}

