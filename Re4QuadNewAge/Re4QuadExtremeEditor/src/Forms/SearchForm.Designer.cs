
namespace Re4QuadExtremeEditor.src.Forms
{
    partial class SearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchForm));
            this.listBoxCont = new System.Windows.Forms.ListBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.checkBoxFilterMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBoxCont
            // 
            this.listBoxCont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxCont.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxCont.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxCont.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxCont.FormattingEnabled = true;
            this.listBoxCont.ItemHeight = 15;
            this.listBoxCont.Location = new System.Drawing.Point(2, 30);
            this.listBoxCont.Name = "listBoxCont";
            this.listBoxCont.Size = new System.Drawing.Size(786, 405);
            this.listBoxCont.TabIndex = 1;
            this.listBoxCont.SelectedIndexChanged += new System.EventHandler(this.listBoxCont_SelectedIndexChanged);
            this.listBoxCont.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            this.listBoxCont.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxCont_MouseDoubleClick);
            this.listBoxCont.MouseUp += new System.Windows.Forms.MouseEventHandler(this.listBoxCont_MouseUp);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearch.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSearch.Location = new System.Drawing.Point(2, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(664, 21);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.WordWrap = false;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Control_KeyDown);
            // 
            // checkBoxFilterMode
            // 
            this.checkBoxFilterMode.AutoSize = true;
            this.checkBoxFilterMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxFilterMode.Location = new System.Drawing.Point(670, 5);
            this.checkBoxFilterMode.Name = "checkBoxFilterMode";
            this.checkBoxFilterMode.Size = new System.Drawing.Size(99, 19);
            this.checkBoxFilterMode.TabIndex = 2;
            this.checkBoxFilterMode.Text = "Filter Mode";
            this.checkBoxFilterMode.UseVisualStyleBackColor = true;
            this.checkBoxFilterMode.CheckedChanged += new System.EventHandler(this.checkBoxFilterMode_CheckedChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 443);
            this.Controls.Add(this.checkBoxFilterMode);
            this.Controls.Add(this.listBoxCont);
            this.Controls.Add(this.textBoxSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 274);
            this.Name = "SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search";
            this.TopMost = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxCont;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.CheckBox checkBoxFilterMode;
    }
}