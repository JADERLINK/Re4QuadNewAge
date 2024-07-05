
namespace Re4QuadExtremeEditor.src.Controls
{
    partial class ObjectMoveControl
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
            this.panelMoveObject = new System.Windows.Forms.Panel();
            this.panelHorizontalObject = new System.Windows.Forms.Panel();
            this.labelObjSpeed = new System.Windows.Forms.Label();
            this.trackBarMoveSpeed = new System.Windows.Forms.TrackBar();
            this.comboBoxMoveMode = new System.Windows.Forms.ComboBox();
            this.buttonDropToGround = new System.Windows.Forms.Button();
            this.checkBoxMoveRelativeCam = new System.Windows.Forms.CheckBox();
            this.checkBoxLockMoveSquareHorizontal = new System.Windows.Forms.CheckBox();
            this.checkBoxLockMoveSquareVertical = new System.Windows.Forms.CheckBox();
            this.checkBoxKeepOnGround = new System.Windows.Forms.CheckBox();
            this.moveObjHorizontal3 = new System.Windows.Forms.PictureBox();
            this.moveObjHorizontal2 = new System.Windows.Forms.PictureBox();
            this.moveObjHorizontal1 = new System.Windows.Forms.PictureBox();
            this.moveObjSquare = new System.Windows.Forms.PictureBox();
            this.moveObjVertical = new System.Windows.Forms.PictureBox();
            this.panelMoveObject.SuspendLayout();
            this.panelHorizontalObject.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMoveSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjSquare)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjVertical)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMoveObject
            // 
            this.panelMoveObject.Controls.Add(this.moveObjSquare);
            this.panelMoveObject.Controls.Add(this.moveObjVertical);
            this.panelMoveObject.Location = new System.Drawing.Point(0, 0);
            this.panelMoveObject.Margin = new System.Windows.Forms.Padding(1);
            this.panelMoveObject.Name = "panelMoveObject";
            this.panelMoveObject.Size = new System.Drawing.Size(120, 98);
            this.panelMoveObject.TabIndex = 0;
            // 
            // panelHorizontalObject
            // 
            this.panelHorizontalObject.Controls.Add(this.moveObjHorizontal3);
            this.panelHorizontalObject.Controls.Add(this.moveObjHorizontal2);
            this.panelHorizontalObject.Controls.Add(this.moveObjHorizontal1);
            this.panelHorizontalObject.Location = new System.Drawing.Point(122, 0);
            this.panelHorizontalObject.Margin = new System.Windows.Forms.Padding(1);
            this.panelHorizontalObject.Name = "panelHorizontalObject";
            this.panelHorizontalObject.Size = new System.Drawing.Size(110, 70);
            this.panelHorizontalObject.TabIndex = 1;
            // 
            // labelObjSpeed
            // 
            this.labelObjSpeed.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelObjSpeed.Location = new System.Drawing.Point(239, 27);
            this.labelObjSpeed.Name = "labelObjSpeed";
            this.labelObjSpeed.Size = new System.Drawing.Size(155, 14);
            this.labelObjSpeed.TabIndex = 3;
            this.labelObjSpeed.Text = "Move speed: 100%";
            this.labelObjSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBarMoveSpeed
            // 
            this.trackBarMoveSpeed.LargeChange = 10;
            this.trackBarMoveSpeed.Location = new System.Drawing.Point(233, 1);
            this.trackBarMoveSpeed.Maximum = 100;
            this.trackBarMoveSpeed.Name = "trackBarMoveSpeed";
            this.trackBarMoveSpeed.Size = new System.Drawing.Size(165, 45);
            this.trackBarMoveSpeed.SmallChange = 5;
            this.trackBarMoveSpeed.TabIndex = 2;
            this.trackBarMoveSpeed.TabStop = false;
            this.trackBarMoveSpeed.TickFrequency = 10;
            this.trackBarMoveSpeed.Value = 50;
            this.trackBarMoveSpeed.Scroll += new System.EventHandler(this.trackBarMoveSpeed_Scroll);
            // 
            // comboBoxMoveMode
            // 
            this.comboBoxMoveMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMoveMode.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold);
            this.comboBoxMoveMode.FormattingEnabled = true;
            this.comboBoxMoveMode.Location = new System.Drawing.Point(3, 102);
            this.comboBoxMoveMode.Name = "comboBoxMoveMode";
            this.comboBoxMoveMode.Size = new System.Drawing.Size(424, 22);
            this.comboBoxMoveMode.TabIndex = 9;
            this.comboBoxMoveMode.TabStop = false;
            this.comboBoxMoveMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxMoveMode_SelectedIndexChanged);
            // 
            // buttonDropToGround
            // 
            this.buttonDropToGround.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDropToGround.Location = new System.Drawing.Point(123, 73);
            this.buttonDropToGround.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDropToGround.Name = "buttonDropToGround";
            this.buttonDropToGround.Size = new System.Drawing.Size(108, 24);
            this.buttonDropToGround.TabIndex = 8;
            this.buttonDropToGround.TabStop = false;
            this.buttonDropToGround.Text = "Drop to ground";
            this.buttonDropToGround.Click += new System.EventHandler(this.buttonDropToGround_Click);
            // 
            // checkBoxMoveRelativeCam
            // 
            this.checkBoxMoveRelativeCam.AutoSize = true;
            this.checkBoxMoveRelativeCam.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxMoveRelativeCam.Location = new System.Drawing.Point(235, 71);
            this.checkBoxMoveRelativeCam.Name = "checkBoxMoveRelativeCam";
            this.checkBoxMoveRelativeCam.Size = new System.Drawing.Size(141, 17);
            this.checkBoxMoveRelativeCam.TabIndex = 6;
            this.checkBoxMoveRelativeCam.TabStop = false;
            this.checkBoxMoveRelativeCam.Text = "Move relative to camera";
            this.checkBoxMoveRelativeCam.UseVisualStyleBackColor = true;
            this.checkBoxMoveRelativeCam.CheckedChanged += new System.EventHandler(this.checkBoxMoveRelativeCam_CheckedChanged);
            // 
            // checkBoxLockMoveSquareHorizontal
            // 
            this.checkBoxLockMoveSquareHorizontal.AutoSize = true;
            this.checkBoxLockMoveSquareHorizontal.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxLockMoveSquareHorizontal.Location = new System.Drawing.Point(235, 43);
            this.checkBoxLockMoveSquareHorizontal.Name = "checkBoxLockMoveSquareHorizontal";
            this.checkBoxLockMoveSquareHorizontal.Size = new System.Drawing.Size(163, 17);
            this.checkBoxLockMoveSquareHorizontal.TabIndex = 4;
            this.checkBoxLockMoveSquareHorizontal.TabStop = false;
            this.checkBoxLockMoveSquareHorizontal.Text = "Lock move square horizontal";
            this.checkBoxLockMoveSquareHorizontal.UseVisualStyleBackColor = true;
            this.checkBoxLockMoveSquareHorizontal.CheckedChanged += new System.EventHandler(this.checkBoxLockMoveSquareHorizontal_CheckedChanged);
            // 
            // checkBoxLockMoveSquareVertical
            // 
            this.checkBoxLockMoveSquareVertical.AutoSize = true;
            this.checkBoxLockMoveSquareVertical.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxLockMoveSquareVertical.Location = new System.Drawing.Point(235, 57);
            this.checkBoxLockMoveSquareVertical.Name = "checkBoxLockMoveSquareVertical";
            this.checkBoxLockMoveSquareVertical.Size = new System.Drawing.Size(150, 17);
            this.checkBoxLockMoveSquareVertical.TabIndex = 5;
            this.checkBoxLockMoveSquareVertical.TabStop = false;
            this.checkBoxLockMoveSquareVertical.Text = "Lock move square vertical";
            this.checkBoxLockMoveSquareVertical.UseVisualStyleBackColor = true;
            this.checkBoxLockMoveSquareVertical.CheckedChanged += new System.EventHandler(this.checkBoxLockMoveSquareVertical_CheckedChanged);
            // 
            // checkBoxKeepOnGround
            // 
            this.checkBoxKeepOnGround.AutoSize = true;
            this.checkBoxKeepOnGround.Font = new System.Drawing.Font("Corbel", 8.25F, System.Drawing.FontStyle.Bold);
            this.checkBoxKeepOnGround.Location = new System.Drawing.Point(235, 85);
            this.checkBoxKeepOnGround.Name = "checkBoxKeepOnGround";
            this.checkBoxKeepOnGround.Size = new System.Drawing.Size(101, 17);
            this.checkBoxKeepOnGround.TabIndex = 7;
            this.checkBoxKeepOnGround.TabStop = false;
            this.checkBoxKeepOnGround.Text = "Keep on ground";
            this.checkBoxKeepOnGround.UseVisualStyleBackColor = true;
            this.checkBoxKeepOnGround.CheckedChanged += new System.EventHandler(this.checkBoxKeepOnGround_CheckedChanged);
            // 
            // moveObjHorizontal3
            // 
            this.moveObjHorizontal3.BackgroundImage = global::Re4QuadExtremeEditor.Properties.Resources.HorizontalDisable;
            this.moveObjHorizontal3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObjHorizontal3.Location = new System.Drawing.Point(2, 49);
            this.moveObjHorizontal3.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveObjHorizontal3.Name = "moveObjHorizontal3";
            this.moveObjHorizontal3.Size = new System.Drawing.Size(106, 20);
            this.moveObjHorizontal3.TabIndex = 6;
            this.moveObjHorizontal3.TabStop = false;
            this.moveObjHorizontal3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal3_MouseDown);
            this.moveObjHorizontal3.MouseLeave += new System.EventHandler(this.moveObjHorizontal3_MouseLeave);
            this.moveObjHorizontal3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal3_MouseMove);
            this.moveObjHorizontal3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal3_MouseUp);
            // 
            // moveObjHorizontal2
            // 
            this.moveObjHorizontal2.BackgroundImage = global::Re4QuadExtremeEditor.Properties.Resources.HorizontalDisable;
            this.moveObjHorizontal2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObjHorizontal2.Location = new System.Drawing.Point(2, 26);
            this.moveObjHorizontal2.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveObjHorizontal2.Name = "moveObjHorizontal2";
            this.moveObjHorizontal2.Size = new System.Drawing.Size(106, 20);
            this.moveObjHorizontal2.TabIndex = 5;
            this.moveObjHorizontal2.TabStop = false;
            this.moveObjHorizontal2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal2_MouseDown);
            this.moveObjHorizontal2.MouseLeave += new System.EventHandler(this.moveObjHorizontal2_MouseLeave);
            this.moveObjHorizontal2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal2_MouseMove);
            this.moveObjHorizontal2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal2_MouseUp);
            // 
            // moveObjHorizontal1
            // 
            this.moveObjHorizontal1.BackgroundImage = global::Re4QuadExtremeEditor.Properties.Resources.HorizontalDisable;
            this.moveObjHorizontal1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObjHorizontal1.Location = new System.Drawing.Point(2, 3);
            this.moveObjHorizontal1.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveObjHorizontal1.Name = "moveObjHorizontal1";
            this.moveObjHorizontal1.Size = new System.Drawing.Size(106, 20);
            this.moveObjHorizontal1.TabIndex = 3;
            this.moveObjHorizontal1.TabStop = false;
            this.moveObjHorizontal1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal1_MouseDown);
            this.moveObjHorizontal1.MouseLeave += new System.EventHandler(this.moveObjHorizontal1_MouseLeave);
            this.moveObjHorizontal1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal1_MouseMove);
            this.moveObjHorizontal1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObjHorizontal1_MouseUp);
            // 
            // moveObjSquare
            // 
            this.moveObjSquare.BackgroundImage = global::Re4QuadExtremeEditor.Properties.Resources.SquareDisable;
            this.moveObjSquare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObjSquare.Location = new System.Drawing.Point(3, 3);
            this.moveObjSquare.Name = "moveObjSquare";
            this.moveObjSquare.Size = new System.Drawing.Size(96, 96);
            this.moveObjSquare.TabIndex = 2;
            this.moveObjSquare.TabStop = false;
            this.moveObjSquare.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObjSquare_MouseDown);
            this.moveObjSquare.MouseLeave += new System.EventHandler(this.moveObjSquare_MouseLeave);
            this.moveObjSquare.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObjSquare_MouseMove);
            this.moveObjSquare.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObjSquare_MouseUp);
            // 
            // moveObjVertical
            // 
            this.moveObjVertical.BackgroundImage = global::Re4QuadExtremeEditor.Properties.Resources.VerticalDisable;
            this.moveObjVertical.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.moveObjVertical.Location = new System.Drawing.Point(100, 3);
            this.moveObjVertical.MinimumSize = new System.Drawing.Size(0, 10);
            this.moveObjVertical.Name = "moveObjVertical";
            this.moveObjVertical.Size = new System.Drawing.Size(20, 96);
            this.moveObjVertical.TabIndex = 3;
            this.moveObjVertical.TabStop = false;
            this.moveObjVertical.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveObjVertical_MouseDown);
            this.moveObjVertical.MouseLeave += new System.EventHandler(this.moveObjVertical_MouseLeave);
            this.moveObjVertical.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveObjVertical_MouseMove);
            this.moveObjVertical.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveObjVertical_MouseUp);
            // 
            // ObjectMoveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxMoveMode);
            this.Controls.Add(this.checkBoxKeepOnGround);
            this.Controls.Add(this.checkBoxMoveRelativeCam);
            this.Controls.Add(this.checkBoxLockMoveSquareVertical);
            this.Controls.Add(this.checkBoxLockMoveSquareHorizontal);
            this.Controls.Add(this.buttonDropToGround);
            this.Controls.Add(this.labelObjSpeed);
            this.Controls.Add(this.panelHorizontalObject);
            this.Controls.Add(this.panelMoveObject);
            this.Controls.Add(this.trackBarMoveSpeed);
            this.Name = "ObjectMoveControl";
            this.Size = new System.Drawing.Size(430, 126);
            this.Resize += new System.EventHandler(this.ObjectMoveControl_Resize);
            this.panelMoveObject.ResumeLayout(false);
            this.panelHorizontalObject.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMoveSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjHorizontal1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjSquare)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.moveObjVertical)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelMoveObject;
        private System.Windows.Forms.Panel panelHorizontalObject;
        private System.Windows.Forms.Label labelObjSpeed;
        private System.Windows.Forms.PictureBox moveObjSquare;
        private System.Windows.Forms.PictureBox moveObjVertical;
        private System.Windows.Forms.PictureBox moveObjHorizontal3;
        private System.Windows.Forms.PictureBox moveObjHorizontal2;
        private System.Windows.Forms.PictureBox moveObjHorizontal1;
        private System.Windows.Forms.TrackBar trackBarMoveSpeed;
        private System.Windows.Forms.ComboBox comboBoxMoveMode;
        private System.Windows.Forms.Button buttonDropToGround;
        private System.Windows.Forms.CheckBox checkBoxMoveRelativeCam;
        private System.Windows.Forms.CheckBox checkBoxLockMoveSquareHorizontal;
        private System.Windows.Forms.CheckBox checkBoxLockMoveSquareVertical;
        private System.Windows.Forms.CheckBox checkBoxKeepOnGround;
    }
}
