
namespace Re4QuadExtremeEditor.src.Forms
{
    partial class MultiSelectEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiSelectEditorForm));
            this.comboBoxPropertyList = new System.Windows.Forms.ComboBox();
            this.labelChoiseText = new System.Windows.Forms.Label();
            this.labelPropertyDescriptionText = new System.Windows.Forms.Label();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBoxHexadecimal = new System.Windows.Forms.CheckBox();
            this.textBoxHexadecimal = new System.Windows.Forms.TextBox();
            this.checkBoxDecimal = new System.Windows.Forms.CheckBox();
            this.groupBoxProgressiveSum = new System.Windows.Forms.GroupBox();
            this.numericUpDownSumValue = new System.Windows.Forms.NumericUpDown();
            this.labelValueSumText = new System.Windows.Forms.Label();
            this.checkBoxProgressiveSum = new System.Windows.Forms.CheckBox();
            this.numericUpDownDecimal = new System.Windows.Forms.NumericUpDown();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSetValue = new System.Windows.Forms.Button();
            this.checkBoxCurrentValuePlusValueToAdd = new System.Windows.Forms.CheckBox();
            this.groupBoxCurrentValuePlusValueToAdd = new System.Windows.Forms.GroupBox();
            this.numericUpDownValueToAdd = new System.Windows.Forms.NumericUpDown();
            this.labelValueSumText2 = new System.Windows.Forms.Label();
            this.groupBoxProgressiveSum.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSumValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimal)).BeginInit();
            this.groupBoxCurrentValuePlusValueToAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueToAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPropertyList
            // 
            this.comboBoxPropertyList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPropertyList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPropertyList.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxPropertyList.FormattingEnabled = true;
            this.comboBoxPropertyList.Location = new System.Drawing.Point(11, 26);
            this.comboBoxPropertyList.Name = "comboBoxPropertyList";
            this.comboBoxPropertyList.Size = new System.Drawing.Size(762, 23);
            this.comboBoxPropertyList.TabIndex = 1;
            this.comboBoxPropertyList.SelectedIndexChanged += new System.EventHandler(this.comboBoxPropertyList_SelectedIndexChanged);
            // 
            // labelChoiseText
            // 
            this.labelChoiseText.AutoSize = true;
            this.labelChoiseText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelChoiseText.Location = new System.Drawing.Point(8, 8);
            this.labelChoiseText.Name = "labelChoiseText";
            this.labelChoiseText.Size = new System.Drawing.Size(219, 15);
            this.labelChoiseText.TabIndex = 0;
            this.labelChoiseText.Text = "Choose the property to be edited:";
            // 
            // labelPropertyDescriptionText
            // 
            this.labelPropertyDescriptionText.AutoSize = true;
            this.labelPropertyDescriptionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPropertyDescriptionText.Location = new System.Drawing.Point(8, 53);
            this.labelPropertyDescriptionText.Name = "labelPropertyDescriptionText";
            this.labelPropertyDescriptionText.Size = new System.Drawing.Size(139, 15);
            this.labelPropertyDescriptionText.TabIndex = 2;
            this.labelPropertyDescriptionText.Text = "Property description:";
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDescription.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDescription.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDescription.HideSelection = false;
            this.textBoxDescription.Location = new System.Drawing.Point(11, 71);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(762, 149);
            this.textBoxDescription.TabIndex = 3;
            // 
            // checkBoxHexadecimal
            // 
            this.checkBoxHexadecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxHexadecimal.AutoSize = true;
            this.checkBoxHexadecimal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxHexadecimal.Location = new System.Drawing.Point(11, 226);
            this.checkBoxHexadecimal.Name = "checkBoxHexadecimal";
            this.checkBoxHexadecimal.Size = new System.Drawing.Size(172, 17);
            this.checkBoxHexadecimal.TabIndex = 4;
            this.checkBoxHexadecimal.Text = "Hexadecimal editor mode:";
            this.checkBoxHexadecimal.UseVisualStyleBackColor = true;
            this.checkBoxHexadecimal.CheckedChanged += new System.EventHandler(this.checkBoxHexadecimal_CheckedChanged);
            // 
            // textBoxHexadecimal
            // 
            this.textBoxHexadecimal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHexadecimal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxHexadecimal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxHexadecimal.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxHexadecimal.Location = new System.Drawing.Point(11, 249);
            this.textBoxHexadecimal.Name = "textBoxHexadecimal";
            this.textBoxHexadecimal.Size = new System.Drawing.Size(762, 21);
            this.textBoxHexadecimal.TabIndex = 5;
            this.textBoxHexadecimal.WordWrap = false;
            this.textBoxHexadecimal.TextChanged += new System.EventHandler(this.textBoxHexadecimal_TextChanged);
            this.textBoxHexadecimal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexadecimalAndDecimal_KeyDown);
            this.textBoxHexadecimal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHexadecimal_KeyPress);
            // 
            // checkBoxDecimal
            // 
            this.checkBoxDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDecimal.AutoSize = true;
            this.checkBoxDecimal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDecimal.Location = new System.Drawing.Point(11, 276);
            this.checkBoxDecimal.Name = "checkBoxDecimal";
            this.checkBoxDecimal.Size = new System.Drawing.Size(145, 17);
            this.checkBoxDecimal.TabIndex = 6;
            this.checkBoxDecimal.Text = "Decimal editor mode:";
            this.checkBoxDecimal.UseVisualStyleBackColor = true;
            this.checkBoxDecimal.CheckedChanged += new System.EventHandler(this.checkBoxDecimal_CheckedChanged);
            // 
            // groupBoxProgressiveSum
            // 
            this.groupBoxProgressiveSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxProgressiveSum.Controls.Add(this.numericUpDownSumValue);
            this.groupBoxProgressiveSum.Controls.Add(this.labelValueSumText);
            this.groupBoxProgressiveSum.Location = new System.Drawing.Point(9, 326);
            this.groupBoxProgressiveSum.Name = "groupBoxProgressiveSum";
            this.groupBoxProgressiveSum.Size = new System.Drawing.Size(380, 86);
            this.groupBoxProgressiveSum.TabIndex = 9;
            this.groupBoxProgressiveSum.TabStop = false;
            // 
            // numericUpDownSumValue
            // 
            this.numericUpDownSumValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSumValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownSumValue.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownSumValue.Location = new System.Drawing.Point(10, 48);
            this.numericUpDownSumValue.Name = "numericUpDownSumValue";
            this.numericUpDownSumValue.Size = new System.Drawing.Size(364, 21);
            this.numericUpDownSumValue.TabIndex = 1;
            // 
            // labelValueSumText
            // 
            this.labelValueSumText.AutoSize = true;
            this.labelValueSumText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValueSumText.Location = new System.Drawing.Point(6, 26);
            this.labelValueSumText.Name = "labelValueSumText";
            this.labelValueSumText.Size = new System.Drawing.Size(127, 15);
            this.labelValueSumText.TabIndex = 0;
            this.labelValueSumText.Text = "Value to be added:";
            // 
            // checkBoxProgressiveSum
            // 
            this.checkBoxProgressiveSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxProgressiveSum.AutoSize = true;
            this.checkBoxProgressiveSum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxProgressiveSum.Location = new System.Drawing.Point(11, 325);
            this.checkBoxProgressiveSum.Name = "checkBoxProgressiveSum";
            this.checkBoxProgressiveSum.Size = new System.Drawing.Size(126, 17);
            this.checkBoxProgressiveSum.TabIndex = 8;
            this.checkBoxProgressiveSum.Text = "Progressively add";
            this.checkBoxProgressiveSum.UseVisualStyleBackColor = true;
            this.checkBoxProgressiveSum.CheckedChanged += new System.EventHandler(this.checkBoxProgressiveSum_CheckedChanged);
            // 
            // numericUpDownDecimal
            // 
            this.numericUpDownDecimal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownDecimal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownDecimal.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownDecimal.Location = new System.Drawing.Point(11, 300);
            this.numericUpDownDecimal.Name = "numericUpDownDecimal";
            this.numericUpDownDecimal.Size = new System.Drawing.Size(762, 21);
            this.numericUpDownDecimal.TabIndex = 7;
            this.numericUpDownDecimal.ValueChanged += new System.EventHandler(this.numericUpDownDecimal_ValueChanged);
            this.numericUpDownDecimal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HexadecimalAndDecimal_KeyDown);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(593, 426);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 23);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSetValue
            // 
            this.buttonSetValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSetValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSetValue.Location = new System.Drawing.Point(9, 426);
            this.buttonSetValue.Name = "buttonSetValue";
            this.buttonSetValue.Size = new System.Drawing.Size(180, 23);
            this.buttonSetValue.TabIndex = 12;
            this.buttonSetValue.Text = "Set Value";
            this.buttonSetValue.UseVisualStyleBackColor = true;
            this.buttonSetValue.Click += new System.EventHandler(this.buttonSetValue_Click);
            // 
            // checkBoxCurrentValuePlusValueToAdd
            // 
            this.checkBoxCurrentValuePlusValueToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxCurrentValuePlusValueToAdd.AutoSize = true;
            this.checkBoxCurrentValuePlusValueToAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCurrentValuePlusValueToAdd.Location = new System.Drawing.Point(395, 326);
            this.checkBoxCurrentValuePlusValueToAdd.Name = "checkBoxCurrentValuePlusValueToAdd";
            this.checkBoxCurrentValuePlusValueToAdd.Size = new System.Drawing.Size(204, 17);
            this.checkBoxCurrentValuePlusValueToAdd.TabIndex = 10;
            this.checkBoxCurrentValuePlusValueToAdd.Text = "Current value plus value to add";
            this.checkBoxCurrentValuePlusValueToAdd.UseVisualStyleBackColor = true;
            this.checkBoxCurrentValuePlusValueToAdd.CheckedChanged += new System.EventHandler(this.checkBoxCurrentValuePlusValueToAdd_CheckedChanged);
            // 
            // groupBoxCurrentValuePlusValueToAdd
            // 
            this.groupBoxCurrentValuePlusValueToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCurrentValuePlusValueToAdd.Controls.Add(this.numericUpDownValueToAdd);
            this.groupBoxCurrentValuePlusValueToAdd.Controls.Add(this.labelValueSumText2);
            this.groupBoxCurrentValuePlusValueToAdd.Location = new System.Drawing.Point(393, 327);
            this.groupBoxCurrentValuePlusValueToAdd.Name = "groupBoxCurrentValuePlusValueToAdd";
            this.groupBoxCurrentValuePlusValueToAdd.Size = new System.Drawing.Size(380, 86);
            this.groupBoxCurrentValuePlusValueToAdd.TabIndex = 11;
            this.groupBoxCurrentValuePlusValueToAdd.TabStop = false;
            // 
            // numericUpDownValueToAdd
            // 
            this.numericUpDownValueToAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownValueToAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDownValueToAdd.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDownValueToAdd.Location = new System.Drawing.Point(10, 48);
            this.numericUpDownValueToAdd.Name = "numericUpDownValueToAdd";
            this.numericUpDownValueToAdd.Size = new System.Drawing.Size(364, 21);
            this.numericUpDownValueToAdd.TabIndex = 1;
            // 
            // labelValueSumText2
            // 
            this.labelValueSumText2.AutoSize = true;
            this.labelValueSumText2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValueSumText2.Location = new System.Drawing.Point(6, 26);
            this.labelValueSumText2.Name = "labelValueSumText2";
            this.labelValueSumText2.Size = new System.Drawing.Size(127, 15);
            this.labelValueSumText2.TabIndex = 0;
            this.labelValueSumText2.Text = "Value to be added:";
            // 
            // MultiSelectEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.checkBoxCurrentValuePlusValueToAdd);
            this.Controls.Add(this.groupBoxCurrentValuePlusValueToAdd);
            this.Controls.Add(this.checkBoxProgressiveSum);
            this.Controls.Add(this.buttonSetValue);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.numericUpDownDecimal);
            this.Controls.Add(this.groupBoxProgressiveSum);
            this.Controls.Add(this.checkBoxDecimal);
            this.Controls.Add(this.textBoxHexadecimal);
            this.Controls.Add(this.checkBoxHexadecimal);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.labelPropertyDescriptionText);
            this.Controls.Add(this.labelChoiseText);
            this.Controls.Add(this.comboBoxPropertyList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MultiSelectEditorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multi Select Editor";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MultiSelectEditor_KeyDown);
            this.groupBoxProgressiveSum.ResumeLayout(false);
            this.groupBoxProgressiveSum.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSumValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDecimal)).EndInit();
            this.groupBoxCurrentValuePlusValueToAdd.ResumeLayout(false);
            this.groupBoxCurrentValuePlusValueToAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownValueToAdd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPropertyList;
        private System.Windows.Forms.Label labelChoiseText;
        private System.Windows.Forms.Label labelPropertyDescriptionText;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.CheckBox checkBoxHexadecimal;
        private System.Windows.Forms.TextBox textBoxHexadecimal;
        private System.Windows.Forms.CheckBox checkBoxDecimal;
        private System.Windows.Forms.GroupBox groupBoxProgressiveSum;
        private System.Windows.Forms.NumericUpDown numericUpDownSumValue;
        private System.Windows.Forms.Label labelValueSumText;
        private System.Windows.Forms.CheckBox checkBoxProgressiveSum;
        private System.Windows.Forms.NumericUpDown numericUpDownDecimal;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSetValue;
        private System.Windows.Forms.CheckBox checkBoxCurrentValuePlusValueToAdd;
        private System.Windows.Forms.GroupBox groupBoxCurrentValuePlusValueToAdd;
        private System.Windows.Forms.NumericUpDown numericUpDownValueToAdd;
        private System.Windows.Forms.Label labelValueSumText2;
    }
}