using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

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
	/// Mnemonics are not implemented.
	/// </summary>
	public class MWCheckBox : System.Windows.Forms.CheckBox
	{
		#region Variables

		private StringFormat strfmt = StringFormat.GenericTypographic;
		private MWCommon.StringFormatEnum sfe = MWCommon.StringFormatEnum.GenericTypographic;
		private MWCommon.CheckBoxPaintOrder cbpoPaintOrder = MWCommon.CheckBoxPaintOrder.ImageCheckText;
		private MWCommon.TextDir tdTextDir = MWCommon.TextDir.Normal;

		private System.ComponentModel.Container components = null;

		#endregion Variables



		#region Constructor and Dispose

		/// <summary>
		/// Standard Constructor.
		/// </summary>
		public MWCheckBox()
		{
			//Set a few ControlStyles (note that not all are used/necessary, AllPaintingInWmPaint, DoubleBuffer and UserPaint are though).
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);

			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion Constructor and Dispose



		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		#endregion Component Designer generated code



		#region Overridden EventHandlers

		/// <summary>
		/// Overridden OnPaint EventHandler that draws the Text and the Image.
		/// </summary>
		/// <param name="e">Standard PaintEventArgs object.</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			if(this.FlatStyle != FlatStyle.System)
			{
				e.Graphics.Clear(this.BackColor);

				switch(this.CheckBoxPaintOrder)
				{
					case MWCommon.CheckBoxPaintOrder.CheckImageText:
						PaintCheckBoxControl(e.Graphics);
						PaintImage(e.Graphics);
						PaintText(e.Graphics);
						break;

					case MWCommon.CheckBoxPaintOrder.CheckTextImage:
						PaintCheckBoxControl(e.Graphics);
						PaintText(e.Graphics);
						PaintImage(e.Graphics);
						break;

					case MWCommon.CheckBoxPaintOrder.ImageCheckText:
						PaintImage(e.Graphics);
						PaintCheckBoxControl(e.Graphics);
						PaintText(e.Graphics);
						break;

					case MWCommon.CheckBoxPaintOrder.ImageTextCheck:
						PaintImage(e.Graphics);
						PaintText(e.Graphics);
						PaintCheckBoxControl(e.Graphics);
						break;

					case MWCommon.CheckBoxPaintOrder.TextCheckImage:
						PaintText(e.Graphics);
						PaintCheckBoxControl(e.Graphics);
						PaintImage(e.Graphics);
						break;

					case MWCommon.CheckBoxPaintOrder.TextImageCheck:
						PaintText(e.Graphics);
						PaintImage(e.Graphics);
						PaintCheckBoxControl(e.Graphics);
						break;

				}
			}
		}

		#endregion Overridden EventHandlers



		#region Paint Methods

		#region PaintAll Method

		/// <summary>
		/// Gets the Graphics object for this Control and paints the Image and the Text.
		/// </summary>
		private void PaintAll()
		{
			if(this.FlatStyle != FlatStyle.System)
			{
				Graphics g = this.CreateGraphics();
				g.Clear(this.BackColor);

				switch(this.CheckBoxPaintOrder)
				{
					case MWCommon.CheckBoxPaintOrder.CheckImageText:
						PaintCheckBoxControl(g);
						PaintImage(g);
						PaintText(g);
						break;

					case MWCommon.CheckBoxPaintOrder.CheckTextImage:
						PaintCheckBoxControl(g);
						PaintText(g);
						PaintImage(g);
						break;

					case MWCommon.CheckBoxPaintOrder.ImageCheckText:
						PaintImage(g);
						PaintCheckBoxControl(g);
						PaintText(g);
						break;

					case MWCommon.CheckBoxPaintOrder.ImageTextCheck:
						PaintImage(g);
						PaintText(g);
						PaintCheckBoxControl(g);
						break;

					case MWCommon.CheckBoxPaintOrder.TextCheckImage:
						PaintText(g);
						PaintCheckBoxControl(g);
						PaintImage(g);
						break;

					case MWCommon.CheckBoxPaintOrder.TextImageCheck:
						PaintText(g);
						PaintImage(g);
						PaintCheckBoxControl(g);
						break;

				}
			}
		}

		#endregion PaintAll Method



		#region PaintCheckBoxControl Method

		/// <summary>
		/// Returns true if Mouse is over Control or false otherwise.
		/// </summary>
		private bool IsMouseOverControl
		{
			get
			{
				Point ptMouseClient = this.PointToClient(Control.MousePosition);
				if(	ptMouseClient.X >= this.ClientRectangle.X && ptMouseClient.X <= this.ClientRectangle.Width &&
					ptMouseClient.Y >= this.ClientRectangle.Y && ptMouseClient.Y <= this.ClientRectangle.Height)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		private void PaintCheckBoxControl(Graphics g)
		{
			ButtonState bs = ButtonState.Normal;

			if(this.FlatStyle == FlatStyle.Standard)
			{
				if(this.CheckState == CheckState.Checked)
				{
					bs = ButtonState.Normal | ButtonState.Checked;
				}
				else if(this.CheckState == CheckState.Unchecked)
				{
					bs = ButtonState.Normal;
				}
				else if(this.CheckState == CheckState.Indeterminate)
				{
					bs = ButtonState.Normal | ButtonState.Checked | ButtonState.Inactive;
				}
			}
			else if(this.FlatStyle == FlatStyle.Flat)
			{
				if(this.CheckState == CheckState.Checked)
				{
					bs = ButtonState.Flat | ButtonState.Checked | ButtonState.Pushed;
				}
				else if(this.CheckState == CheckState.Unchecked)
				{
					bs = ButtonState.Flat;
				}
				else if(this.CheckState == CheckState.Indeterminate)
				{
					bs = ButtonState.Flat | ButtonState.Checked | ButtonState.Inactive;
				}
			}
			else if(this.FlatStyle == FlatStyle.Popup)
			{
				if(this.CheckState == CheckState.Checked)
				{
					if(IsMouseOverControl)
					{
						bs = ButtonState.Normal | ButtonState.Checked | ButtonState.Pushed;
					}
					else
					{
						bs = ButtonState.Flat | ButtonState.Checked | ButtonState.Pushed;
					}
				}
				else if(this.CheckState == CheckState.Unchecked)
				{
					if(IsMouseOverControl)
					{
						bs = ButtonState.Normal;
					}
					else
					{
						bs = ButtonState.Flat;
					}
				}
				else if(this.CheckState == CheckState.Indeterminate)
				{
					if(IsMouseOverControl)
					{
						bs = ButtonState.Normal | ButtonState.Checked | ButtonState.Inactive;
					}
					else
					{
						bs = ButtonState.Flat | ButtonState.Checked | ButtonState.Inactive;
					}
				}
			}

			if(this.Appearance == Appearance.Normal)
			{
				PaintCheckBox(g, bs);
			}
			else
			{
				ControlPaint.DrawButton(g, this.ClientRectangle, bs);
			}
		}

		#endregion PaintCheckBoxControl Method



		#region PaintCheckBox Method

		public void PaintCheckBox(Graphics g, ButtonState bs)
		{
			Bitmap bmp = null;

			int iCheckHeight = SystemInformation.MenuCheckSize.Height;
			int iCheckWidth = SystemInformation.MenuCheckSize.Width;

			ContentAlignment caCheck = this.CheckAlign;

			if(this.RightToLeft == RightToLeft.Yes)
			{
				if(caCheck == ContentAlignment.BottomLeft)
				{
					caCheck = ContentAlignment.BottomRight;
				}
				else if(caCheck == ContentAlignment.BottomRight)
				{
					caCheck = ContentAlignment.BottomLeft;
				}
				else if(caCheck == ContentAlignment.MiddleLeft)
				{
					caCheck = ContentAlignment.MiddleRight;
				}
				else if(caCheck == ContentAlignment.MiddleRight)
				{
					caCheck = ContentAlignment.MiddleLeft;
				}
				else if(caCheck == ContentAlignment.TopLeft)
				{
					caCheck = ContentAlignment.TopRight;
				}
				else if(caCheck == ContentAlignment.TopRight)
				{
					caCheck = ContentAlignment.TopLeft;
				}
			}

			if(!this.Enabled)
			{
				bmp = new Bitmap(iCheckWidth, iCheckHeight, g);
				Graphics gCheck = Graphics.FromImage(bmp);
				ControlPaint.DrawCheckBox(gCheck, 0, 0, bmp.Width, bmp.Height, bs);
			}

			switch(caCheck)
			{
				case ContentAlignment.BottomCenter:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, (this.Width - iCheckWidth) / 2, this.Height - iCheckHeight, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, (this.Width - iCheckWidth) / 2, this.Height - iCheckHeight, SystemColors.Control);
					}
					break;

				case ContentAlignment.BottomLeft:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, 0, this.Height - iCheckHeight, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, 0, this.Height - iCheckHeight, SystemColors.Control);
					}
					break;

				case ContentAlignment.BottomRight:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, this.Width - iCheckWidth, this.Height - iCheckHeight, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, this.Width - iCheckWidth, this.Height - iCheckHeight, SystemColors.Control);
					}
					break;

				case ContentAlignment.MiddleCenter:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, (this.Width - iCheckWidth) / 2, (this.Height - iCheckHeight) / 2, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, (this.Width - iCheckWidth) / 2, (this.Height - iCheckHeight) / 2, SystemColors.Control);
					}
					break;

				case ContentAlignment.MiddleLeft:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, 0, (this.Height - iCheckHeight) / 2, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, 0, (this.Height - iCheckHeight) / 2, SystemColors.Control);
					}
					break;

				case ContentAlignment.MiddleRight:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, this.Width - iCheckWidth, (this.Height - iCheckHeight) / 2, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, this.Width - iCheckWidth, (this.Height - iCheckHeight) / 2, SystemColors.Control);
					}
					break;

				case ContentAlignment.TopCenter:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, (this.Width - iCheckWidth) / 2, 0, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, (this.Width - iCheckWidth) / 2, 0, SystemColors.Control);
					}
					break;

				case ContentAlignment.TopLeft:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, 0, 0, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, 0, 0, SystemColors.Control);
					}
					break;

				case ContentAlignment.TopRight:
					if(this.Enabled)
					{
						ControlPaint.DrawCheckBox(g, this.Width - iCheckWidth, 0, iCheckWidth, iCheckHeight, bs);
					}
					else
					{
						ControlPaint.DrawImageDisabled(g, bmp, this.Width - iCheckWidth, 0, SystemColors.Control);
					}
					break;
			}

		}

		#endregion PaintCheckBox Method



		#region PaintImage Method

		private void PaintImage(Graphics g)
		{
			Image img = null;

			int iCheckHeight = SystemInformation.MenuCheckSize.Height;
			int iCheckWidth = SystemInformation.MenuCheckSize.Width;

			ContentAlignment caImage = this.ImageAlign;

			if(this.RightToLeft == RightToLeft.Yes)
			{
				if(caImage == ContentAlignment.BottomLeft)
				{
					caImage = ContentAlignment.BottomRight;
				}
				else if(caImage == ContentAlignment.BottomRight)
				{
					caImage = ContentAlignment.BottomLeft;
				}
				else if(caImage == ContentAlignment.MiddleLeft)
				{
					caImage = ContentAlignment.MiddleRight;
				}
				else if(caImage == ContentAlignment.MiddleRight)
				{
					caImage = ContentAlignment.MiddleLeft;
				}
				else if(caImage == ContentAlignment.TopLeft)
				{
					caImage = ContentAlignment.TopRight;
				}
				else if(caImage == ContentAlignment.TopRight)
				{
					caImage = ContentAlignment.TopLeft;
				}
			}

			if(this.Image != null)
			{
				try
				{
					img = this.Image;
				}
				catch
				{
				}
			}
			else if(this.ImageList != null)
			{
				try
				{
					img = this.ImageList.Images[this.ImageIndex];
				}
				catch
				{
				}
			}

			Rectangle rct = this.ClientRectangle;

			ContentAlignment caCheck = this.CheckAlign;

			if(this.RightToLeft == RightToLeft.Yes)
			{
				if(caCheck == ContentAlignment.BottomLeft)
				{
					caCheck = ContentAlignment.BottomRight;
				}
				else if(caCheck == ContentAlignment.BottomRight)
				{
					caCheck = ContentAlignment.BottomLeft;
				}
				else if(caCheck == ContentAlignment.MiddleLeft)
				{
					caCheck = ContentAlignment.MiddleRight;
				}
				else if(caCheck == ContentAlignment.MiddleRight)
				{
					caCheck = ContentAlignment.MiddleLeft;
				}
				else if(caCheck == ContentAlignment.TopLeft)
				{
					caCheck = ContentAlignment.TopRight;
				}
				else if(caCheck == ContentAlignment.TopRight)
				{
					caCheck = ContentAlignment.TopLeft;
				}
			}

			if(img != null)
			{
				switch(caCheck)
				{
					case ContentAlignment.BottomCenter:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckHeight);
						break;

					case ContentAlignment.BottomLeft:
						rct = new Rectangle(this.ClientRectangle.Left + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;

					case ContentAlignment.BottomRight:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;

					case ContentAlignment.MiddleCenter:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
						break;

					case ContentAlignment.MiddleLeft:
						rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;

					case ContentAlignment.MiddleRight:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;

					case ContentAlignment.TopCenter:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Top + iCheckHeight, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckWidth);
						break;

					case ContentAlignment.TopLeft:
						rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;

					case ContentAlignment.TopRight:
						rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
						break;
				}

				switch(caImage)
				{
					case ContentAlignment.BottomCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + (rct.Width - img.Width) / 2, rct.Top + rct.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + (rct.Width - img.Width) / 2, rct.Top + rct.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.BottomLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left, rct.Top + rct.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left, rct.Top + rct.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.BottomRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + rct.Width - img.Width, rct.Top + rct.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + rct.Width - img.Width, rct.Top + rct.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + (rct.Width - img.Width) / 2, rct.Top + (rct.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + (rct.Width - img.Width) / 2, rct.Top + (rct.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left, rct.Top + (rct.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left, rct.Top + (rct.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + rct.Width - img.Width, rct.Top + (rct.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + rct.Width - img.Width, rct.Top + (rct.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.TopCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + (rct.Width - img.Width) / 2, rct.Top);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + (rct.Width - img.Width) / 2, rct.Top, this.BackColor);
						}
						break;

					case ContentAlignment.TopLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left, rct.Top);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left, rct.Top, this.BackColor);
						}
						break;

					case ContentAlignment.TopRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, rct.Left + rct.Width - img.Width, rct.Top);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, rct.Left + rct.Width - img.Width, rct.Top, this.BackColor);
						}
						break;
				}
			}
		}

		#endregion PaintImage Method



		#region PaintText Method

		private void PaintText(Graphics g)
		{
			SetStringFormat();

			if(tdTextDir == MWCommon.TextDir.UpsideDown)
			{
				g.RotateTransform(180);
				g.TranslateTransform(-this.ClientRectangle.Width, -this.ClientRectangle.Height);
			}
			else if(tdTextDir == MWCommon.TextDir.Left)
			{
				g.RotateTransform(270);
				g.TranslateTransform(-this.ClientRectangle.Height, 0);
			}
			else if(tdTextDir == MWCommon.TextDir.Right)
			{
				g.RotateTransform(90);
				g.TranslateTransform(0, -this.ClientRectangle.Width);
			}

			#region Enabled/Disabled

			if(this.Enabled)
			{
				Rectangle rct = GetModifiedClientRectangle();

				g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rct, strfmt);
			}
			else
			{
				//This is not the method used by either Label or CheckBox! It should be though shouldn't it! By both of them in fact.
				//ControlPaint.DrawStringDisabled(e.Graphics, strText, this.Font, Color.White, this.ClientRectangle, strfmt);

				//Looks exactly like a CheckBox's Text (pixel by pixel and color in fact).
				Rectangle rct1 = GetModifiedClientRectangle(true);
				Rectangle rct2 = GetModifiedClientRectangle(false);
				g.DrawString(this.Text, this.Font, new SolidBrush(ControlPaint.LightLight(this.BackColor)), rct1, strfmt);
				if(this.BackColor == SystemColors.Control)
				{
					g.DrawString(this.Text, this.Font, new SolidBrush(ControlPaint.ContrastControlDark), rct2, strfmt);
				}
				else
				{
					g.DrawString(this.Text, this.Font, new SolidBrush(ControlPaint.Dark(this.BackColor)), rct2, strfmt);
				}
			}

			#endregion Enabled/Disabled

			g.ResetTransform();
		}

		#endregion PaintText Method



		#region Paint Help Methods

		#region GetModifiedClientRectangle Method

		/// <summary>
		/// Gets the Rectangle needed to draw the Text correctly.
		/// </summary>
		/// <returns>Rectangle needed to draw the Text correctly.</returns>
		private Rectangle GetModifiedClientRectangle()
		{
			Rectangle rct = this.ClientRectangle;

			int iCheckHeight = SystemInformation.MenuCheckSize.Height;
			int iCheckWidth = SystemInformation.MenuCheckSize.Width;

			ContentAlignment caCheck = this.CheckAlign;

			#region RightToLeft

			if(this.RightToLeft == RightToLeft.Yes)
			{
				if(caCheck == ContentAlignment.BottomLeft)
				{
					caCheck = ContentAlignment.BottomRight;
				}
				else if(caCheck == ContentAlignment.BottomRight)
				{
					caCheck = ContentAlignment.BottomLeft;
				}
				else if(caCheck == ContentAlignment.MiddleLeft)
				{
					caCheck = ContentAlignment.MiddleRight;
				}
				else if(caCheck == ContentAlignment.MiddleRight)
				{
					caCheck = ContentAlignment.MiddleLeft;
				}
				else if(caCheck == ContentAlignment.TopLeft)
				{
					caCheck = ContentAlignment.TopRight;
				}
				else if(caCheck == ContentAlignment.TopRight)
				{
					caCheck = ContentAlignment.TopLeft;
				}
			}

			#endregion RightToLeft

			switch(tdTextDir)
			{
				case MWCommon.TextDir.Normal:
					#region Normal

					switch(caCheck)
					{
						case ContentAlignment.BottomCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckHeight);
							break;

						case ContentAlignment.BottomLeft:
							rct = new Rectangle(this.ClientRectangle.Left + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.BottomRight:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleLeft:
							rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleRight:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.TopCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Top + iCheckHeight, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckWidth);
							break;

						case ContentAlignment.TopLeft:
							rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.TopRight:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;
					}

					return rct;

					#endregion Normal
				case MWCommon.TextDir.UpsideDown:
					#region UpsideDown

					switch(caCheck)
					{
						case ContentAlignment.BottomCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckHeight);
							break;

						case ContentAlignment.BottomLeft:
							rct = new Rectangle(this.ClientRectangle.Left, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.BottomRight:
							rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleLeft:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.MiddleRight:
							rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.TopCenter:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Top, this.ClientRectangle.Width, this.ClientRectangle.Height - iCheckHeight);
							break;

						case ContentAlignment.TopLeft:
							rct = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;

						case ContentAlignment.TopRight:
							rct = new Rectangle(this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Y, this.ClientRectangle.Width - iCheckWidth, this.ClientRectangle.Height);
							break;
					}

					return rct;

					#endregion UpsideDown
				case MWCommon.TextDir.Left:
					#region Left

					switch(caCheck)
					{
						case ContentAlignment.BottomCenter:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.BottomLeft:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.BottomRight:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.MiddleCenter:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width);
							break;

						case ContentAlignment.MiddleLeft:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Height, this.ClientRectangle.Width - iCheckWidth);
							break;

						case ContentAlignment.MiddleRight:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width - iCheckWidth);
							break;

						case ContentAlignment.TopCenter:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.TopLeft:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.TopRight:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;
					}

					return rct;

					#endregion Left
				case MWCommon.TextDir.Right:
					#region Right

					switch(caCheck)
					{
						case ContentAlignment.BottomCenter:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.BottomLeft:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.BottomRight:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.MiddleCenter:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width);
							break;

						case ContentAlignment.MiddleLeft:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width - iCheckWidth);
							break;

						case ContentAlignment.MiddleRight:
							rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X + iCheckWidth, this.ClientRectangle.Height, this.ClientRectangle.Width - iCheckWidth);
							break;

						case ContentAlignment.TopCenter:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.TopLeft:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;

						case ContentAlignment.TopRight:
							rct = new Rectangle(this.ClientRectangle.Y + iCheckHeight, this.ClientRectangle.X, this.ClientRectangle.Height - iCheckHeight, this.ClientRectangle.Width);
							break;
					}

					return rct;

					#endregion Right

				default:
					return rct;
			}
		}

		/// <summary>
		/// Gets the Rectangle needed to draw the Text correctly modified by a certain number of pixels.
		///	Used for drawing disabled/embossed Text (this.Enabled = false).
		/// </summary>
		/// <param name="bTop">True if embossed text (top left) or false for highlighted text (bottom right).</param>
		/// <returns>Rectangle needed to draw the Text correctly.</returns>
		private Rectangle GetModifiedClientRectangle(bool bTop)
		{
			Rectangle rct;

			int iTopXMod = 0;
			int iBottomXMod = 0;
			int iTopYMod = 0;
			int iBottomYMod = 0;

			if(strfmt.Alignment == StringAlignment.Near)
			{
				iTopXMod = 1;
				iBottomXMod = 0;
			}
			else if(strfmt.Alignment == StringAlignment.Center)
			{
				iTopXMod = 1;
				iBottomXMod = 1;
			}
			else if(strfmt.Alignment == StringAlignment.Far)
			{
				iTopXMod = 2;
				iBottomXMod = 1;
			}

			if(strfmt.LineAlignment == StringAlignment.Near)
			{
				iTopYMod = 1;
				iBottomYMod = 0;
			}
			else if(strfmt.LineAlignment == StringAlignment.Center)
			{
				iTopYMod = 2;
				iBottomYMod = 0;
			}
			else if(strfmt.LineAlignment == StringAlignment.Far)
			{
				iTopYMod = 2;
				iBottomYMod = 1;
			}

			if(tdTextDir == MWCommon.TextDir.Normal || tdTextDir == MWCommon.TextDir.UpsideDown)
			{
				rct = GetModifiedClientRectangle();
			}
			else
			{
				rct = GetModifiedClientRectangle();
			}

			if(bTop)
			{
				return new Rectangle(rct.X + iTopXMod, rct.Y + iTopYMod, rct.Width - 1, rct.Height - 1);
			}
			else
			{
				return new Rectangle(rct.X - iBottomXMod, rct.Y - iBottomYMod, rct.Width + 1, rct.Height + 1);
			}
		}

		#endregion GetModifiedClientRectangle Method



		#region SetStringFormat Method

		/// <summary>
		/// Sets the StringFormat so that it can be used throughout the Class.
		/// </summary>
		private void SetStringFormat()
		{
			strfmt = StringFormat.GenericTypographic;
			if(sfe == MWCommon.StringFormatEnum.GenericDefault)
			{
				strfmt = StringFormat.GenericDefault;
			}

			//This cannot be used since it changes the StringFormat for all MWLabels AND all MWScrollLabels etc!!!
			//if(tdTextDir == TextDir.Left || tdTextDir == TextDir.Right)
			//{
			//	strfmt.FormatFlags |= StringFormatFlags.DirectionVertical;
			//}

			ContentAlignment caText = this.TextAlign;

			if(this.RightToLeft == RightToLeft.Yes)
			{
				if(caText == ContentAlignment.BottomLeft)
				{
					caText = ContentAlignment.BottomRight;
				}
				else if(caText == ContentAlignment.BottomRight)
				{
					caText = ContentAlignment.BottomLeft;
				}
				else if(caText == ContentAlignment.MiddleLeft)
				{
					caText = ContentAlignment.MiddleRight;
				}
				else if(caText == ContentAlignment.MiddleRight)
				{
					caText = ContentAlignment.MiddleLeft;
				}
				else if(caText == ContentAlignment.TopLeft)
				{
					caText = ContentAlignment.TopRight;
				}
				else if(caText == ContentAlignment.TopRight)
				{
					caText = ContentAlignment.TopLeft;
				}
			}

			switch(caText)
			{
				case ContentAlignment.BottomCenter:
					strfmt.Alignment = StringAlignment.Center;
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.BottomLeft:
					strfmt.Alignment = StringAlignment.Near;
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.BottomRight:
					strfmt.Alignment = StringAlignment.Far;
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.MiddleCenter:
					strfmt.Alignment = StringAlignment.Center;
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.MiddleLeft:
					strfmt.Alignment = StringAlignment.Near;
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.MiddleRight:
					strfmt.Alignment = StringAlignment.Far;
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.TopCenter:
					strfmt.Alignment = StringAlignment.Center;
					strfmt.LineAlignment = StringAlignment.Near;
					break;

				case ContentAlignment.TopLeft:
					strfmt.Alignment = StringAlignment.Near;
					strfmt.LineAlignment = StringAlignment.Near;
					break;

				case ContentAlignment.TopRight:
					strfmt.Alignment = StringAlignment.Far;
					strfmt.LineAlignment = StringAlignment.Near;
					break;
			}
		}

		#endregion SetStringFormat Method

		#endregion Paint Help Methods

		#endregion Paint Methods



		#region Properties and their EventHandlers

		#region CheckBoxPaintOrder

		/// <summary>
		/// Decides which order to Paint the Check, the Image and the Text for MWCheckBoxes (last is topmost).
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Decides which order to Paint the Check, the Image and the Text for MWCheckBoxes (last is topmost)."),
		DefaultValue(MWCommon.CheckBoxPaintOrder.ImageCheckText)
		]
		public MWCommon.CheckBoxPaintOrder CheckBoxPaintOrder
		{
			get
			{
				return cbpoPaintOrder;
			}
			set
			{
				if(cbpoPaintOrder != value)
				{
					cbpoPaintOrder = value;
					OnCheckBoxPaintOrderChanged(new EventArgs());
					PaintAll();
				}
			}
		}

		/// <summary>
		/// Occurs when the CheckBoxPaintOrder property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Occurs when the CheckBoxPaintOrder property changes.")
		]
		public event EventHandler CheckBoxPaintOrderChanged;

		/// <summary>
		/// Raises the CheckBoxPaintOrderChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnCheckBoxPaintOrderChanged(EventArgs e)
		{
			if(CheckBoxPaintOrderChanged != null)
			{
				CheckBoxPaintOrderChanged(this, e);
			}
		}

		#endregion CheckBoxPaintOrder



		#region TextDir

		/// <summary>
		/// Decides which direction the Text should be painted.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Direction of the Text."),
		DefaultValue(MWCommon.TextDir.Normal),
		Editor(typeof(EditorTextDir), typeof(UITypeEditor))
		]
		public MWCommon.TextDir TextDir
		{
			get
			{
				return tdTextDir;
			}
			set
			{
				MWCommon.TextDir tdTextDirOld = tdTextDir;

				if(tdTextDir != value)
				{
					tdTextDir = value;
					OnTextDirChanged(new MWCommon.TextDirEventArgs(tdTextDirOld, tdTextDir));
					this.PaintAll();
				}
			}
		}

		/// <summary>
		/// A delegate for event TextDirEventHandler.
		/// </summary>
		public delegate void TextDirEventHandler(object sender, MWCommon.TextDirEventArgs e);

		/// <summary>
		/// Occurs when the TextDir property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Occurs when the TextDir property changes.")
		]
		public event MWCommon.TextDirEventHandler TextDirChanged;

		/// <summary>
		/// Raises the TextDirChanged Event.
		/// </summary>
		/// <param name="e">Standard TextDirEventArgs object.</param>
		public virtual void OnTextDirChanged(MWCommon.TextDirEventArgs e)
		{
			if(TextDirChanged != null)
			{
				TextDirChanged(this, e);
			}
		}

		#endregion TextDir



		#region StringFrmt

		/// <summary>
		/// Decides which StringFormatEnum the Text should use.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("StringFormatEnum of the Text.")
		]
		public MWCommon.StringFormatEnum StringFrmt
		{
			get
			{
				return sfe;
			}
			set
			{
				MWCommon.StringFormatEnum sfeOld = sfe;
				if(sfe != value)
				{
					sfe = value;
					OnStringFrmtChanged(new MWCommon.StringFormatEnumEventArgs(sfeOld, sfe));
					//This calls PaintText which calls SetStringFormat.
					this.PaintAll();
				}
			}
		}

		/// <summary>
		/// A delegate for event StringFormatEnumEventHandler.
		/// </summary>
		public delegate void StringFormatEnumEventHandler(object sender, MWCommon.StringFormatEnumEventArgs e);

		/// <summary>
		/// Occurs when the StringFrmt property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Occurs when the StringFrmt property changes.")
		]
		public event MWCommon.StringFormatEnumEventHandler StringFrmtChanged;

		/// <summary>
		/// Raises the StringFrmtChanged Event.
		/// </summary>
		/// <param name="e">Standard StringFormatEnumEventArgs object.</param>
		public virtual void OnStringFrmtChanged(MWCommon.StringFormatEnumEventArgs e)
		{
			if(StringFrmtChanged != null)
			{
				StringFrmtChanged(this, e);
			}
		}

		#endregion StringFrmt

		#endregion Properties and their EventHandlers

		protected override void OnMouseEnter(System.EventArgs eventargs)
		{
			PaintAll();
		}

		protected override void OnMouseLeave(System.EventArgs eventargs)
		{
			PaintAll();
		}

	}
}
