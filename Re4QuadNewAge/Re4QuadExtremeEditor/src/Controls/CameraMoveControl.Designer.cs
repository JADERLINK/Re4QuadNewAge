
namespace Re4QuadExtremeEditor.src.Controls
{
    partial class CameraMoveControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraMoveControl));
            this.labelCamModeText = new System.Windows.Forms.Label();
            this.comboBoxCameraMode = new System.Windows.Forms.ComboBox();
            this.labelCamSpeedPercentage = new System.Windows.Forms.Label();
            this.trackBarCamSpeed = new System.Windows.Forms.TrackBar();
            this.labelMoveCamText = new System.Windows.Forms.Label();
            this.pictureBoxMoveCamStrafe = new System.Windows.Forms.PictureBox();
            this.pictureBoxMoveCamInOut = new System.Windows.Forms.PictureBox();
            this.buttonGrid = new System.Windows.Forms.Button();
            this.textBoxGridSize = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveCamStrafe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveCamInOut)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCamModeText
            // 
            this.labelCamModeText.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamModeText.Location = new System.Drawing.Point(128, 51);
            this.labelCamModeText.Name = "labelCamModeText";
            this.labelCamModeText.Size = new System.Drawing.Size(119, 13);
            this.labelCamModeText.TabIndex = 2;
            this.labelCamModeText.Text = "Camera Mode:";
            this.labelCamModeText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxCameraMode
            // 
            this.comboBoxCameraMode.DisplayMember = "1";
            this.comboBoxCameraMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameraMode.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCameraMode.Items.AddRange(new object[] {
            "Fly",
            "Orbit",
            "Top",
            "Bottom",
            "Left",
            "Right",
            "Front",
            "Back"});
            this.comboBoxCameraMode.Location = new System.Drawing.Point(127, 67);
            this.comboBoxCameraMode.Name = "comboBoxCameraMode";
            this.comboBoxCameraMode.Size = new System.Drawing.Size(120, 22);
            this.comboBoxCameraMode.TabIndex = 3;
            this.comboBoxCameraMode.TabStop = false;
            this.comboBoxCameraMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameraMode_SelectedIndexChanged);
            // 
            // labelCamSpeedPercentage
            // 
            this.labelCamSpeedPercentage.AutoSize = true;
            this.labelCamSpeedPercentage.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCamSpeedPercentage.Location = new System.Drawing.Point(120, 5);
            this.labelCamSpeedPercentage.Name = "labelCamSpeedPercentage";
            this.labelCamSpeedPercentage.Size = new System.Drawing.Size(112, 14);
            this.labelCamSpeedPercentage.TabIndex = 0;
            this.labelCamSpeedPercentage.Text = "Cam speed: 100%";
            this.labelCamSpeedPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarCamSpeed
            // 
            this.trackBarCamSpeed.LargeChange = 10;
            this.trackBarCamSpeed.Location = new System.Drawing.Point(120, 20);
            this.trackBarCamSpeed.Maximum = 100;
            this.trackBarCamSpeed.Name = "trackBarCamSpeed";
            this.trackBarCamSpeed.Size = new System.Drawing.Size(130, 45);
            this.trackBarCamSpeed.SmallChange = 5;
            this.trackBarCamSpeed.TabIndex = 1;
            this.trackBarCamSpeed.TabStop = false;
            this.trackBarCamSpeed.TickFrequency = 10;
            this.trackBarCamSpeed.Value = 50;
            this.trackBarCamSpeed.Scroll += new System.EventHandler(this.trackBarCamSpeed_Scroll);
            // 
            // labelMoveCamText
            // 
            this.labelMoveCamText.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMoveCamText.Location = new System.Drawing.Point(0, 102);
            this.labelMoveCamText.Name = "labelMoveCamText";
            this.labelMoveCamText.Size = new System.Drawing.Size(121, 18);
            this.labelMoveCamText.TabIndex = 6;
            this.labelMoveCamText.Text = "Move Camera";
            this.labelMoveCamText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBoxMoveCamStrafe
            // 
            this.pictureBoxMoveCamStrafe.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxMoveCamStrafe.BackgroundImage")));
            this.pictureBoxMoveCamStrafe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxMoveCamStrafe.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxMoveCamStrafe.Name = "pictureBoxMoveCamStrafe";
            this.pictureBoxMoveCamStrafe.Size = new System.Drawing.Size(96, 96);
            this.pictureBoxMoveCamStrafe.TabIndex = 37;
            this.pictureBoxMoveCamStrafe.TabStop = false;
            this.pictureBoxMoveCamStrafe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamStrafe_MouseDown);
            this.pictureBoxMoveCamStrafe.MouseLeave += new System.EventHandler(this.pictureBoxMoveCamStrafe_MouseLeave);
            this.pictureBoxMoveCamStrafe.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamStrafe_MouseMove);
            this.pictureBoxMoveCamStrafe.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamStrafe_MouseUp);
            // 
            // pictureBoxMoveCamInOut
            // 
            this.pictureBoxMoveCamInOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxMoveCamInOut.BackgroundImage")));
            this.pictureBoxMoveCamInOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBoxMoveCamInOut.Location = new System.Drawing.Point(101, 3);
            this.pictureBoxMoveCamInOut.Name = "pictureBoxMoveCamInOut";
            this.pictureBoxMoveCamInOut.Size = new System.Drawing.Size(20, 96);
            this.pictureBoxMoveCamInOut.TabIndex = 38;
            this.pictureBoxMoveCamInOut.TabStop = false;
            this.pictureBoxMoveCamInOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamInOut_MouseDown);
            this.pictureBoxMoveCamInOut.MouseLeave += new System.EventHandler(this.pictureBoxMoveCamInOut_MouseLeave);
            this.pictureBoxMoveCamInOut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamInOut_MouseMove);
            this.pictureBoxMoveCamInOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMoveCamInOut_MouseUp);
            // 
            // buttonGrid
            // 
            this.buttonGrid.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGrid.Location = new System.Drawing.Point(126, 93);
            this.buttonGrid.Name = "buttonGrid";
            this.buttonGrid.Size = new System.Drawing.Size(65, 22);
            this.buttonGrid.TabIndex = 4;
            this.buttonGrid.TabStop = false;
            this.buttonGrid.Text = "Grid";
            this.buttonGrid.UseVisualStyleBackColor = true;
            this.buttonGrid.Click += new System.EventHandler(this.buttonGrid_Click);
            // 
            // textBoxGridSize
            // 
            this.textBoxGridSize.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxGridSize.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGridSize.Location = new System.Drawing.Point(200, 94);
            this.textBoxGridSize.MaxLength = 4;
            this.textBoxGridSize.Name = "textBoxGridSize";
            this.textBoxGridSize.Size = new System.Drawing.Size(45, 20);
            this.textBoxGridSize.TabIndex = 5;
            this.textBoxGridSize.TabStop = false;
            this.textBoxGridSize.Text = "100";
            this.textBoxGridSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxGridSize.TextChanged += new System.EventHandler(this.textBoxGridSize_TextChanged);
            this.textBoxGridSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxGridSize_KeyPress);
            // 
            // CameraMoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxGridSize);
            this.Controls.Add(this.pictureBoxMoveCamStrafe);
            this.Controls.Add(this.pictureBoxMoveCamInOut);
            this.Controls.Add(this.buttonGrid);
            this.Controls.Add(this.labelCamSpeedPercentage);
            this.Controls.Add(this.labelMoveCamText);
            this.Controls.Add(this.labelCamModeText);
            this.Controls.Add(this.comboBoxCameraMode);
            this.Controls.Add(this.trackBarCamSpeed);
            this.Name = "CameraMoveControl";
            this.Size = new System.Drawing.Size(250, 126);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarCamSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveCamStrafe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMoveCamInOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelCamModeText;
        private System.Windows.Forms.ComboBox comboBoxCameraMode;
        private System.Windows.Forms.Label labelCamSpeedPercentage;
        private System.Windows.Forms.TrackBar trackBarCamSpeed;
        private System.Windows.Forms.Label labelMoveCamText;
        private System.Windows.Forms.PictureBox pictureBoxMoveCamStrafe;
        private System.Windows.Forms.PictureBox pictureBoxMoveCamInOut;
        private System.Windows.Forms.Button buttonGrid;
        private System.Windows.Forms.TextBox textBoxGridSize;
    }
}
