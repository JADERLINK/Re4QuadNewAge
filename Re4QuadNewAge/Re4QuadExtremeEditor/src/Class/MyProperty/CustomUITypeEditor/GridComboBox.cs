using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

// baseado em https://www.codeproject.com/Articles/23242/Property-Grid-Dynamic-List-ComboBox-Validation-and
// CustomControls.ComboBox.GridComboBox.cs
namespace Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor
{
    public abstract class GridComboBox : UITypeEditor
    {

		#region Data Members

		private IList _dataList;
		private readonly ListBox _listBox;
		private bool _escKeyPressed;
		private IWindowsFormsEditorService _editorService;

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public GridComboBox()
		{
			_listBox = new ListBox();

			// Properties
			_listBox.BorderStyle = BorderStyle.None;
			_listBox.BackColor = System.Drawing.SystemColors.Control;
			_listBox.Font = new System.Drawing.Font("Courier New", 8.25f);
			_listBox.DrawMode = DrawMode.OwnerDrawFixed;
			_listBox.HorizontalScrollbar = true;

            // Events
            _listBox.DrawItem += myListBox_DrawItem;
			_listBox.Click += myListBox_Click;
			_listBox.PreviewKeyDown += myListBox_PreviewKeyDown;

			onStart();
		}

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set for ListBox
        /// </summary>
        protected ListBox ListBox
		{
			get { return (_listBox); }
		}

		/// <summary>
		/// Get/Set for DataList
		/// </summary>
		protected IList DataList
		{
			get { return (_dataList); }
			set { _dataList = value; }
		}


		#endregion

		#region Methods - Public

		/// <summary>
		/// Close DropDown window to finish editing
		/// </summary>
		public void CloseDropDownWindow()
		{
			if (_editorService != null)
			  { _editorService.CloseDropDown(); }
		}

		#endregion

		#region Methods - Private

		/// <summary>
		/// Populate the ListBox with data items
		/// </summary>
		/// <param name="context"></param>
		/// <param name="currentValue"></param>
		private void PopulateListBox(ITypeDescriptorContext context, object currentValue)
		{
			// Clear List
			_listBox.Items.Clear();

			// Retrieve the reference to the items to be displayed in the list
			if (_dataList == null)
			  { RetrieveDataList(context); }

			if (_dataList != null)
			{
				int hzSize = 0;
				Graphics g = _listBox.CreateGraphics();

				// Add Items to the ListBox
				foreach (object obj in _dataList)
				{
					_listBox.Items.Add(obj);
					int temphzSize = (int)g.MeasureString(obj.ToString(), _listBox.Font).Width;
					if (temphzSize > hzSize)
					{
						hzSize = temphzSize;
					}
				}

				_listBox.HorizontalExtent = hzSize;

				// Select current item 
				if (currentValue != null)
				  { _listBox.SelectedItem = currentValue; }
			}

			// Set the height based on the Items in the ListBox
			_listBox.Height = _listBox.PreferredHeight;
			_listBox.Width = 150;

			if (_listBox.PreferredHeight > (35 * _listBox.ItemHeight))
			{
				_listBox.Height = (35 * _listBox.ItemHeight) - 7;
			}

		}


        #endregion

        #region Methods - Protected

        /// <summary>
        /// Get the object selected in the ComboBox
        /// </summary>
        /// <returns>Selected Object</returns>
        protected abstract object GetDataObjectSelected(ITypeDescriptorContext context);

		/// <summary>
		/// Find the list of data items to populate the ListBox
		/// </summary>
		/// <param name="context"></param>
		protected abstract void RetrieveDataList(ITypeDescriptorContext context);

		/// <summary>
		/// ao iniciar a classe;
		/// </summary>
		protected abstract void onStart();

		#endregion

		#region Event Handlers

		/// <summary>
		/// Preview Key Pressed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void myListBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{

			if (e.KeyCode == Keys.Escape)
			{ _escKeyPressed = true; }
		}

		/// <summary>
		/// ListBox Click Event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void myListBox_Click(object sender, EventArgs e)
		{
			//when user clicks on an item, the edit process is done.
			this.CloseDropDownWindow();
		}

		private void myListBox_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index > -1)
			{
				e.DrawBackground();
				if (e.State.HasFlag(DrawItemState.Selected))
				{
				   e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(0x70, 0xBB, 0xDB)), e.Bounds);
				}
				e.Graphics.DrawString(_listBox.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds.Left, e.Bounds.Top);
			}
		}

		#endregion

		#region Overrides

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if ((context != null) && (provider != null))
			{
				//Uses the IWindowsFormsEditorService to display a 
				// drop-down UI in the Properties window:
				_editorService = provider.GetService(
									 typeof(IWindowsFormsEditorService))
								 as IWindowsFormsEditorService;

				if (_editorService != null)
				{
					// Add Values to the ListBox
					PopulateListBox(context, value);

					// Set to false before showing the control
					_escKeyPressed = false;

					// Attach the ListBox to the DropDown Control
					_editorService.DropDownControl(_listBox);

					// User pressed the ESC key --> Return the Old Value
					if (!_escKeyPressed)
					{
						// Get the Selected Object
						object obj = GetDataObjectSelected(context);

						// If an Object is Selected --> Return it
						if (obj != null)
						  { return obj; }
					}
				}
			}

			return value;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return (UITypeEditorEditStyle.DropDown);
		}

		#endregion





	}
}
