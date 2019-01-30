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
            ((System.ComponentModel.ISupportInitialize)(this.cantidadBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "No. de Procesos:";
            // 
            // IngresaButton
            // 
            this.IngresaButton.Location = new System.Drawing.Point(117, 165);
            this.IngresaButton.Name = "IngresaButton";
            this.IngresaButton.Size = new System.Drawing.Size(75, 23);
            this.IngresaButton.TabIndex = 2;
            this.IngresaButton.Text = "Ingresar";
            this.IngresaButton.UseVisualStyleBackColor = true;
            this.IngresaButton.Click += new System.EventHandler(this.IngresaButton_Click);
            // 
            // cantidadBox
            // 
            this.cantidadBox.Location = new System.Drawing.Point(160, 121);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 259);
            this.Controls.Add(this.cantidadBox);
            this.Controls.Add(this.IngresaButton);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.cantidadBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button IngresaButton;
        private System.Windows.Forms.NumericUpDown cantidadBox;
    }
}

