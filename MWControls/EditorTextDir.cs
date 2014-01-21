using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

/// <summary>
///	Mikael Wiberg 2003
///		mikwib@hotmail.com (usual HoTMaiL spam filters)
///		mick@ar.com.au (heavy spam filters on, harldy anything gets through, START the subject with C# and it will probably go through)
///		md5mw@mdstud.chalmers.se (heavy spam filters on, harldy anything gets through, START the subject with C# and it will probably go through)
///	
///	Feel free to use this code as you wish, as long as you do not take credit for it yourself.
///	If it is used in commercial projects or applications please mention my name.
///	Feel free to donate any amount of money if this code makes you happy ;)
///	Use this code at your own risk. If your machine blows up while using it - don't blame me.
/// </summary>
namespace MWControls
{
	/// <summary>
	/// EditorTextDir is used in conjunction with the EditorTextDirUI Control.
	/// </summary>
	public class EditorTextDir : System.Drawing.Design.UITypeEditor
	{
		#region Variables

		private PropertyDescriptor pd = null;
		private object oInstance = null;

		private IWindowsFormsEditorService iwfes = null;

		#endregion Variables



		#region Overridden Methods and EventHandlers for UITypeEditor

		#region Methods for setting the value

		/// <summary>
		/// This enables the button for the dropdown to appear in the properties window.
		/// </summary>
		/// <param name="itdc">Standard ITypeDescriptorContext object.</param>
		/// <returns>The desired UITypeEditorEditStyle (in a DropDown).</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext itdc) 
		{
			if(itdc != null && itdc.Instance != null) 
			{
				return UITypeEditorEditStyle.DropDown;
			}

			return base.GetEditStyle(itdc);
		}

		/// <summary>
		/// This takes care of the actual value-change of the property.
		/// </summary>
		/// <param name="itdc">Standard ITypeDescriptorContext object.</param>
		/// <param name="isp">Standard IServiceProvider object.</param>
		/// <param name="value">The value as an object.</param>
		/// <returns>The new value as an object.</returns>
		public override object EditValue(ITypeDescriptorContext itdc, IServiceProvider isp, object value)
		{
			if(itdc != null && itdc.Instance != null && isp != null) 
			{
				iwfes = (IWindowsFormsEditorService)isp.GetService(typeof(IWindowsFormsEditorService));

				if(iwfes != null)
				{
					MWCommon.TextDir td = MWCommon.TextDir.Normal;

					if(value is MWCommon.TextDir)
					{
						td = (MWCommon.TextDir)itdc.PropertyDescriptor.GetValue(itdc.Instance);
						pd = itdc.PropertyDescriptor;
						oInstance = itdc.Instance;
					}

					EditorTextDirUI etdui = new EditorTextDirUI();
					etdui.IWFES = iwfes;
					etdui.ITDC = itdc;
					etdui.TextDir = (MWCommon.TextDir)value;
					etdui.TextDirChanged += new EditorTextDirUI.TextDirEventHandler(this.ValueChanged);

					iwfes.DropDownControl(etdui);
					value = etdui.TextDir;
				}
			}

			return value;
		}

		#endregion Methods for setting the value



		#region Methods for painting the value

		/// <summary>
		/// True if Custom Painting or false otherwise.
		/// </summary>
		/// <param name="itdc">Standard ITypeDescriptorContext object.</param>
		/// <returns>True if Custom Painting or false otherwise.</returns>
		public override bool GetPaintValueSupported(ITypeDescriptorContext itdc)
		{
			return true;
		}

		/// <summary>
		/// Paint the value in Visual Studio's (or wherever it is used) Property Window.
		/// </summary>
		/// <param name="e">Standard PaintValueEventArgs object.</param>
		public override void PaintValue(PaintValueEventArgs e)
		{
			//See e.Graphics.Restore further down.
			GraphicsState gs = e.Graphics.Save();

			MWCommon.TextDir td = (MWCommon.TextDir)e.Value;
			StringFormat strfmt = StringFormat.GenericDefault;
			strfmt.Alignment = StringAlignment.Center;
			strfmt.LineAlignment = StringAlignment.Center;

			switch(td)
			{
				case MWCommon.TextDir.Normal:
					break;

				case MWCommon.TextDir.UpsideDown:
					e.Graphics.RotateTransform(180);
					e.Graphics.TranslateTransform(-e.Bounds.Width, -e.Bounds.Height);
					break;

				case MWCommon.TextDir.Left:
					e.Graphics.RotateTransform(270);
					e.Graphics.TranslateTransform(-e.Bounds.Height, 0);
					break;

				case MWCommon.TextDir.Right:
					e.Graphics.RotateTransform(90);
					e.Graphics.TranslateTransform(0, -e.Bounds.Width);
					break;
			}

			e.Graphics.DrawString("A", new Font("Arial", 8), new SolidBrush(Color.Black), GetModifiedClientRectangle(td, e.Bounds), strfmt);

			//Important. Without this Visual Studio (or whatever) cannot draw its Property Window properly.
			e.Graphics.Restore(gs);

			base.PaintValue(e);
		}

		#endregion Methods for painting the value

		#endregion Overridden Methods and EventHandlers for UITypeEditor



		#region Help Methods for setting the value

		/// <summary>
		/// Standard ValueChanged EventHandler for EditorTextDirUI etdui.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard TextDirEventArgs object.</param>
		private void ValueChanged(object sender, MWCommon.TextDirEventArgs e)
		{
			if(pd != null && oInstance != null)
			{
				pd.SetValue(oInstance, e.NewTextDir);
			}
		}

		#endregion Help Methods for setting the value



		#region Help Methods for painting the value

		/// <summary>
		/// Gets a Rectangle that is sized for being used to paint the value properly in Visual Studio's
		///		(or wherever it is used) Property Window.
		/// </summary>
		/// <param name="td">TextDir to base the Rectangle size and position on.</param>
		/// <param name="rct">Starting Rectangle.</param>
		/// <returns></returns>
		private Rectangle GetModifiedClientRectangle(MWCommon.TextDir td, Rectangle rct)
		{
			switch(td)
			{
				case MWCommon.TextDir.Normal:
					return new Rectangle(rct.X + 1, rct.Y, rct.Width, rct.Height);
					break;

				case MWCommon.TextDir.UpsideDown:
					return new Rectangle(rct.X - 2, rct.Y - 1, rct.Width, rct.Height);
					break;

				case MWCommon.TextDir.Left:
					return new Rectangle(rct.X - 5, rct.Y + 4, rct.Width, rct.Height);
					break;

				case MWCommon.TextDir.Right:
					return new Rectangle(rct.X - 2, rct.Y + 1, rct.Width, rct.Height);
					break;

				default:
					return rct;
					break;
			}
		}

		#endregion Help Methods for painting the value

	}
}
