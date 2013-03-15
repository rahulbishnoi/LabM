using System;
using System.Text;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;
using C1.Win.C1FlexGrid;


namespace LabManager
{

	//===============================================================================
	#region ** Multi-Column editor
	//
	// MultiColumnEditor
	// UITypeEditor implemented using a FlexGrid. This is easier than creating a
	// drop down control from scratch. The UITypeEditorControl class takes care
	// of turning this into a control that can be used as a grid editor.
	// 
	public class MultiColumnEditor : UITypeEditor
	{
		// ** fields
		private IWindowsFormsEditorService _edSvc;
		private C1FlexGrid	_flex;
		private string      _keyColumn;
		private bool		_cancel;
        private bool _ValueSelected = false;
        private bool _bTranslate = false;
        private bool _bSetValue = false;
        private FilterRow.FilterRow fr = null;
		// ** ctor
        public MultiColumnEditor(C1FlexGrid flex, string keyColumn, bool bTranslate = false, bool bSetValue = true)
		{
			// some sanity
			if (!flex.Cols.Contains(keyColumn))
				throw new ApplicationException(string.Format("Column '{0}' not found on the grid.", keyColumn));

			// column that contains the value being edited
			_keyColumn = keyColumn;
            _bTranslate = bTranslate;
            _bSetValue = bSetValue;
			// initialize drop-down flex control
			_flex = flex;
         //   _flex.Cols.Fixed = 1;
            _flex.Rows.Fixed = 1;
			_flex.Cols[0].Width = _flex.Font.Height;
			_flex.ShowCursor	= true;
            _flex.AllowFiltering = true;
			_flex.AllowEditing  = false;
			_flex.BorderStyle	= C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            _flex.VisualStyle = VisualStyle.Office2007Blue;
            _flex.TabStop = false;
            _flex.FocusRect = C1.Win.C1FlexGrid.FocusRectEnum.None;
			_flex.SelectionMode = SelectionModeEnum.Row;
			//_flex.FocusRect		= FocusRectEnum.None;
			_flex.AutoSearch	= AutoSearchEnum.FromCursor;
            _flex.EditOptions = EditFlags.None;
			//_flex.Click    += new EventHandler(_flex_Click);
            _flex.DoubleClick += new EventHandler(_flex_DoubleClick);
			_flex.KeyPress += new KeyPressEventHandler(_flex_KeyPress);
         
           // if (bTranslate) { 
                _flex.Cols[1].Visible = false;
           // }
                fr = new FilterRow.FilterRow(_flex);
              //  _flex.Select(1, _flex.Cols.Fixed);
              
		}

		// ** overrides
		override public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext ctx) 
		{
			return UITypeEditorEditStyle.DropDown;
		}
		override public object EditValue(ITypeDescriptorContext ctx, IServiceProvider provider, object value)
		{ 
			// initialize editor service
			_edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (_edSvc == null)
            {
                return value;
            }
			// size the grid
            int x = Math.Min(800, _flex.Cols[_flex.Cols.Count-1].Right);
            int y = Math.Max(100,  _flex.Rows[_flex.Rows.Count-1].Bottom);
            if(y>500) y=500; //max height

			_flex.ClientSize = new Size(x,y);

			// initialize selection
			int col = _flex.Cols[_keyColumn].Index;
			_flex.Col = col;
			_flex.Row = (value is string)
				? _flex.FindRow((string)value, _flex.Cols.Fixed, col, false, true, true)
				: -1;

			// show the grid
            _flex.Visible = true;
			_cancel = false;
			_edSvc.DropDownControl(_flex);

            if (_ValueSelected)
            {
                // get return value from selected item on the grid
                if (!_cancel && _flex.Row > 0)
                	//value = _flex[_flex.Row, _keyColumn];
                    if (_bTranslate) { value = _flex[_flex.Row, 1]; } else { value = _flex[_flex.Row, _keyColumn]; }

            }

			// done
			return value;
		}

		// ** event handlers
        public void  SelectFilterRowColumn(int nColToSelect)
        {
            fr.SelectFilterRowColumn(nColToSelect);
        }

		// close editor when the user picks a row with the mouse
		private void _flex_DoubleClick(object sender, EventArgs e)
		{
            if (_bSetValue)
            {
                if (_edSvc != null)
                {
                    _ValueSelected = true;
                    _edSvc.CloseDropDown();
                }
            }
		}

		// close editor if the user presses enter or escape
		private void _flex_KeyPress(object sender, KeyPressEventArgs e)
		{
			switch (e.KeyChar)
			{
                    // key ESC
				case (char)27:
					e.Handled = true;
					_cancel = true;
					_edSvc.CloseDropDown();
					break;

                    // key ENTER
				case (char)13:
					e.Handled = true;
					_edSvc.CloseDropDown();
					break;
			}
		}
	}
	#endregion

    //===============================================================================
    #region ** DropDown editor
    //
    // MultiColumnEditor
    // UITypeEditor implemented using a FlexGrid. This is easier than creating a
    // drop down control from scratch. The UITypeEditorControl class takes care
    // of turning this into a control that can be used as a grid editor.
    // 
    public class DropDownEditor : UITypeEditor
    {
        // ** fields
        private IWindowsFormsEditorService _edSvc;
        private C1FlexGrid _flex;
        private string _keyColumn;
        private bool _cancel;
        private bool _ValueSelected = false;

        // ** ctor
        public DropDownEditor(C1FlexGrid flex, string keyColumn)
        {
            // some sanity
            if (!flex.Cols.Contains(keyColumn))
                throw new ApplicationException(string.Format("Column '{0}' not found on the grid.", keyColumn));

            // column that contains the value being edited
            _keyColumn = keyColumn;

            // initialize drop-down flex control
            _flex = flex;
            _flex.Cols.Fixed = 1;
            _flex.Cols[0].Width = _flex.Font.Height;
            _flex.ShowCursor = true;
            _flex.AllowFiltering = true;
            _flex.AllowEditing = false;
            _flex.BorderStyle = C1.Win.C1FlexGrid.Util.BaseControls.BorderStyleEnum.FixedSingle;
            _flex.SelectionMode = SelectionModeEnum.Row;
            _flex.FocusRect		= FocusRectEnum.None;
            _flex.AutoSearch = AutoSearchEnum.FromCursor;
           // _flex.Click    += new EventHandler(_flex_Click);
            _flex.DoubleClick += new EventHandler(_flex_DoubleClick);
            _flex.KeyPress += new KeyPressEventHandler(_flex_KeyPress);
           
           
        }

        // ** overrides
        override public UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext ctx)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        override public object EditValue(ITypeDescriptorContext ctx, IServiceProvider provider, object value)
        {
            // initialize editor service
            _edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (_edSvc == null)
                return value;

            // size the grid
            _flex.ClientSize = new Size(
                Math.Min(800, _flex.Cols[_flex.Cols.Count - 1].Right),
                Math.Min(250, _flex.Rows[_flex.Rows.Count - 1].Bottom));

            // initialize selection
            int col = _flex.Cols[_keyColumn].Index;
            _flex.Col = col;
            _flex.Row = (value is string)
                ? _flex.FindRow((string)value, _flex.Cols.Fixed, col, false, true, true)
                : -1;

            // show the grid
            _flex.Visible = true;
            _cancel = false;
            _edSvc.DropDownControl(_flex);

            if (_ValueSelected)
            {
                // get return value from selected item on the grid
                if (!_cancel && _flex.Row > -1)
                    value = _flex[_flex.Row, _keyColumn];
            }

            // done
            return value;
        }

        // ** event handlers

        // close editor when the user picks a row with the mouse
        private void _flex_DoubleClick(object sender, EventArgs e)
        {
            if (_edSvc != null)
            {
                _ValueSelected = true;
                _edSvc.CloseDropDown();
            }
        }

        // close editor if the user presses enter or escape
        private void _flex_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)27:
                    e.Handled = true;
                    _cancel = true;
                    //		_edSvc.CloseDropDown();
                    break;
                case (char)13:
                    e.Handled = true;
                    //	_edSvc.CloseDropDown();
                    break;
            }
        }
    }
    #endregion
}
