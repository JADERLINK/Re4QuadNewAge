
namespace Re4QuadExtremeEditor.src.Forms
{
    partial class CameraForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonGetPos = new System.Windows.Forms.Button();
            this.buttonSetPos = new System.Windows.Forms.Button();
            this.floatBoxCamX = new ControlUtils.FloatBox();
            this.floatBoxCamY = new ControlUtils.FloatBox();
            this.floatBoxCamZ = new ControlUtils.FloatBox();
            this.floatBoxYaw = new ControlUtils.FloatBox();
            this.floatBoxPitch = new ControlUtils.FloatBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(6, 222);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(228, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(171, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(171, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Z";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "Yaw";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 169);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Pitch";
            // 
            // buttonGetPos
            // 
            this.buttonGetPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGetPos.Location = new System.Drawing.Point(6, 29);
            this.buttonGetPos.Name = "buttonGetPos";
            this.buttonGetPos.Size = new System.Drawing.Size(228, 23);
            this.buttonGetPos.TabIndex = 1;
            this.buttonGetPos.Text = "Get Camera Position";
            this.buttonGetPos.UseVisualStyleBackColor = true;
            this.buttonGetPos.Click += new System.EventHandler(this.buttonGetPos_Click);
            // 
            // buttonSetPos
            // 
            this.buttonSetPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetPos.Location = new System.Drawing.Point(6, 193);
            this.buttonSetPos.Name = "buttonSetPos";
            this.buttonSetPos.Size = new System.Drawing.Size(228, 23);
            this.buttonSetPos.TabIndex = 7;
            this.buttonSetPos.Text = "Set Camera Position";
            this.buttonSetPos.UseVisualStyleBackColor = true;
            this.buttonSetPos.Click += new System.EventHandler(this.buttonSetPos_Click);
            // 
            // floatBoxCamX
            // 
            this.floatBoxCamX.Location = new System.Drawing.Point(6, 58);
            this.floatBoxCamX.MaxLength = 100;
            this.floatBoxCamX.MaxValue = 3.402823E+38F;
            this.floatBoxCamX.MinValue = -3.402823E+38F;
            this.floatBoxCamX.Name = "floatBoxCamX";
            this.floatBoxCamX.Size = new System.Drawing.Size(159, 21);
            this.floatBoxCamX.TabIndex = 2;
            this.floatBoxCamX.Text = "0.000000";
            this.floatBoxCamX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatBoxCamX.Value = 0F;
            // 
            // floatBoxCamY
            // 
            this.floatBoxCamY.Location = new System.Drawing.Point(6, 85);
            this.floatBoxCamY.MaxLength = 100;
            this.floatBoxCamY.MaxValue = 3.402823E+38F;
            this.floatBoxCamY.MinValue = -3.402823E+38F;
            this.floatBoxCamY.Name = "floatBoxCamY";
            this.floatBoxCamY.Size = new System.Drawing.Size(159, 21);
            this.floatBoxCamY.TabIndex = 3;
            this.floatBoxCamY.Text = "0.000000";
            this.floatBoxCamY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatBoxCamY.Value = 0F;
            // 
            // floatBoxCamZ
            // 
            this.floatBoxCamZ.Location = new System.Drawing.Point(6, 112);
            this.floatBoxCamZ.MaxLength = 100;
            this.floatBoxCamZ.MaxValue = 3.402823E+38F;
            this.floatBoxCamZ.MinValue = -3.402823E+38F;
            this.floatBoxCamZ.Name = "floatBoxCamZ";
            this.floatBoxCamZ.Size = new System.Drawing.Size(159, 21);
            this.floatBoxCamZ.TabIndex = 4;
            this.floatBoxCamZ.Text = "0.000000";
            this.floatBoxCamZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatBoxCamZ.Value = 0F;
            // 
            // floatBoxYaw
            // 
            this.floatBoxYaw.Location = new System.Drawing.Point(6, 139);
            this.floatBoxYaw.MaxLength = 100;
            this.floatBoxYaw.MaxValue = 360F;
            this.floatBoxYaw.MinValue = -360F;
            this.floatBoxYaw.Name = "floatBoxYaw";
            this.floatBoxYaw.Size = new System.Drawing.Size(159, 21);
            this.floatBoxYaw.TabIndex = 5;
            this.floatBoxYaw.Text = "0.000000";
            this.floatBoxYaw.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatBoxYaw.Value = 0F;
            // 
            // floatBoxPitch
            // 
            this.floatBoxPitch.Location = new System.Drawing.Point(6, 166);
            this.floatBoxPitch.MaxLength = 100;
            this.floatBoxPitch.MaxValue = 89F;
            this.floatBoxPitch.MinValue = -89F;
            this.floatBoxPitch.Name = "floatBoxPitch";
            this.floatBoxPitch.Size = new System.Drawing.Size(159, 21);
            this.floatBoxPitch.TabIndex = 6;
            this.floatBoxPitch.Text = "0.000000";
            this.floatBoxPitch.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.floatBoxPitch.Value = 0F;
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(7, 7);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(227, 15);
            this.labelInfo.TabIndex = 8;
            this.labelInfo.Text = "The values are on a scale of 1/100";
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(239, 253);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.floatBoxPitch);
            this.Controls.Add(this.floatBoxYaw);
            this.Controls.Add(this.floatBoxCamZ);
            this.Controls.Add(this.floatBoxCamY);
            this.Controls.Add(this.floatBoxCamX);
            this.Controls.Add(this.buttonSetPos);
            this.Controls.Add(this.buttonGetPos);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonClose);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CameraForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Camera";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CameraForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonGetPos;
        private System.Windows.Forms.Button buttonSetPos;
        private ControlUtils.FloatBox floatBoxCamX;
        private ControlUtils.FloatBox floatBoxCamY;
        private ControlUtils.FloatBox floatBoxCamZ;
        private ControlUtils.FloatBox floatBoxYaw;
        private ControlUtils.FloatBox floatBoxPitch;
        private System.Windows.Forms.Label labelInfo;
    }
}