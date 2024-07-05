
namespace Re4QuadExtremeEditor.src.Controls
{
    partial class Advertising2Control
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelYoutube = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelYoutube
            // 
            this.labelYoutube.BackColor = System.Drawing.Color.Transparent;
            this.labelYoutube.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelYoutube.Font = new System.Drawing.Font("Courier New", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelYoutube.ForeColor = System.Drawing.Color.DarkGray;
            this.labelYoutube.Location = new System.Drawing.Point(0, 0);
            this.labelYoutube.Name = "labelYoutube";
            this.labelYoutube.Size = new System.Drawing.Size(398, 100);
            this.labelYoutube.TabIndex = 0;
            this.labelYoutube.Text = "Subscribe on my channel:\r\nyoutube.com/@JADERLINK";
            this.labelYoutube.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Advertising2Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.labelYoutube);
            this.ForeColor = System.Drawing.SystemColors.GrayText;
            this.Name = "Advertising2Control";
            this.Size = new System.Drawing.Size(398, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelYoutube;
    }
}
