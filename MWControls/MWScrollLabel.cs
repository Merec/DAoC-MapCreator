using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using MWCommon;

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
	/// A Label Control that scrolls to display its Text when there is too much to fit.
	/// Note that this Control does not display its Text in multiline - only one line. If the Text does not fit it paints it to the edge - this is intentional.
	/// Note that the Image does NOT scroll. An Image in a Label should be small enough to fit. Scrolling an Image could be done in a ScrollPictureBox Control
	///		or something like that - not in a Label Control.
	/// See the MWLabel Control for other comments (no point inheriting from this though as it is too different in some aspects, like the whole
	///		OnPaint EventHandler having to be rewritten - which really is the whole MWLabel).
	/// </summary>
	public class MWScrollLabel : System.Windows.Forms.Label
	{
		#region Variables

		private StringFormat strfmt = StringFormat.GenericTypographic;
		private MWCommon.StringFormatEnum sfe = MWCommon.StringFormatEnum.GenericTypographic;
		private bool bImageOverText = false;
		private MWCommon.TextDir tdTextDir = MWCommon.TextDir.Normal;

		private int iTextPos = 0;
		private int iTextPosMax = 0;
		private bool bTextPositive = true;
		private int iScrollStep = 2;
		private bool bScrollState = false;
		private int iScrollInterval = 100;
		private int iScrollEndPointPause = 500;
		private int iScrollStartPause = 1000;

		private System.Windows.Forms.Timer tScroll;
		private System.ComponentModel.IContainer components;

		#endregion Variables



		#region Constructor and Dispose

		public MWScrollLabel()
		{
			//Set a few ControlStyles (note that not all are used/necessary, AllPaintingInWmPaint, DoubleBuffer and UserPaint are though).
			this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.Selectable, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
			this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);

			InitializeComponent();

			SetStringFormat();

			SetTextPosMaxVariable();

			//Enable the next line if Control should start its life scrolling - even scrolls in Visual Studio.
			//ScrollStart();
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
			this.components = new System.ComponentModel.Container();
			this.tScroll = new System.Windows.Forms.Timer(this.components);
			// 
			// tScroll
			// 
			this.tScroll.Interval = 100;
			this.tScroll.Tick += new System.EventHandler(this.tScroll_Tick);

		}

		#endregion Component Designer generated code



		#region Overridden EventHandlers

		/// <summary>
		/// Overridden OnPaint EventHandler that draws the Text and the Image.
		/// </summary>
		/// <param name="e">Standard PaintEventArgs object.</param>
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.Clear(this.BackColor);

			if(bImageOverText)
			{
				PaintText(e.Graphics);
				PaintImage(e.Graphics);
			}
			else
			{
				PaintImage(e.Graphics);
				PaintText(e.Graphics);
			}
		}

		/// <summary>
		/// Overridden OnTextChanged EventHandler that updates the maximum scrolling position when the Text changes.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		protected override void OnTextChanged(System.EventArgs e)
		{
			SetTextPosMaxVariable();

			base.OnTextChanged(e);
		}

		/// <summary>
		/// Overridden OnSizeChanged EventHandler that updates the maximum scrolling position when the Size changes.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		protected override void OnSizeChanged(System.EventArgs e)
		{
			SetTextPosMaxVariable();

			base.OnSizeChanged(e);
		}

		/// <summary>
		/// Overridden OnFontChanged EventHandler that updates the maximum scrolling position when the Font changes.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		protected override void OnFontChanged(System.EventArgs e)
		{
			SetTextPosMaxVariable();

			base.OnFontChanged(e);
		}

		#endregion Overridden EventHandlers



		#region EventHandlers for Controls/Components

		/// <summary>
		/// Standard Timer Tick EventHandler.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard EventArgs object.</param>
		private void tScroll_Tick(object sender, System.EventArgs e)
		{
			if(iTextPosMax > 0)
				//if(Math.Abs(iTextPosMax) > 0)
			{
				bool bPaintAll = true;

				if(iTextPos > iTextPosMax)
				{
					bTextPositive = false;
					TextPos = iTextPosMax;
				}
				else if(iTextPos == iTextPosMax)
				{
					bTextPositive = false;
					TextPos -= iScrollStep;
					tScroll.Interval = iScrollInterval + iScrollEndPointPause;
					bPaintAll = false;
				}
				else if(iTextPos < 0)
				{
					bTextPositive = true;
					TextPos = 0;
				}
				else if(iTextPos == 0)
				{
					bTextPositive = true;
					TextPos += iScrollStep;
					tScroll.Interval = iScrollInterval + iScrollEndPointPause;
					bPaintAll = false;
				}
				else
				{
					tScroll.Interval = iScrollInterval;

					if(bTextPositive)
					{
						TextPos += iScrollStep;
					}
					else
					{
						TextPos -= iScrollStep;
					}
				}

				if(bPaintAll)
				{
					PaintAll();
				}
			}
		}

		#endregion EventHandlers for Controls/Components



		#region Paint Methods

		#region PaintAll Method

		/// <summary>
		/// Gets the Graphics object for this Control and paints the Image and the Text.
		/// Note that the Image does not scroll.
		/// </summary>
		private void PaintAll()
		{
			Graphics g = this.CreateGraphics();
			g.Clear(this.BackColor);

			if(bImageOverText)
			{
				PaintText(g);
				PaintImage(g);
			}
			else
			{
				PaintImage(g);
				PaintText(g);
			}
		}

		#endregion PaintAll Method



		#region PaintImage Method

		/// <summary>
		/// Paints the Image.
		/// Note that the Image does not scroll.
		/// </summary>
		/// <param name="g">Graphics object for this Control.</param>
		private void PaintImage(Graphics g)
		{
			Image img = null;

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

			if(img != null)
			{
				switch(caImage)
				{
					case ContentAlignment.BottomCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, (this.Width - img.Width) / 2, this.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, (this.Width - img.Width) / 2, this.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.BottomLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, 0, this.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, 0, this.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.BottomRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, this.Width - img.Width, this.Height - img.Height);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, this.Width - img.Width, this.Height - img.Height, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, (this.Width - img.Width) / 2, (this.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, (this.Width - img.Width) / 2, (this.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, 0, (this.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, 0, (this.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.MiddleRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, this.Width - img.Width, (this.Height - img.Height) / 2);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, this.Width - img.Width, (this.Height - img.Height) / 2, this.BackColor);
						}
						break;

					case ContentAlignment.TopCenter:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, (this.Width - img.Width) / 2, 0);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, (this.Width - img.Width) / 2, 0, this.BackColor);
						}
						break;

					case ContentAlignment.TopLeft:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, 0, 0);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, 0, 0, this.BackColor);
						}
						break;

					case ContentAlignment.TopRight:
						if(this.Enabled)
						{
							g.DrawImageUnscaled(img, this.Width - img.Width, 0);
						}
						else
						{
							ControlPaint.DrawImageDisabled(g, img, this.Width - img.Width, 0, this.BackColor);
						}
						break;
				}
			}
		}

		#endregion PaintImage Method



		#region PaintText Method

		/// <summary>
		/// Paints the Text.
		/// </summary>
		/// <param name="g">Graphics object for this Control.</param>
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

			if(this.Enabled)
			{
				g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), GetModifiedClientRectangle(), strfmt);
			}
			else
			{
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

			g.ResetTransform();
		}

		#endregion PaintText Method



		#region Paint Help Methods

		#region GetModifiedClientRectangle Methods

		/// <summary>
		/// Gets the Rectangle needed to draw the Text correctly.
		/// Used for drawing enabled Text (this.Enabled = true).
		/// </summary>
		/// <returns>Rectangle needed to draw the Text correctly.</returns>
		private Rectangle GetModifiedClientRectangle()
		{
			Rectangle rct;

			if(tdTextDir == MWCommon.TextDir.Normal || tdTextDir == MWCommon.TextDir.UpsideDown)
			{
				rct = this.ClientRectangle;
			}
			else
			{
				rct = new Rectangle(this.ClientRectangle.Y, this.ClientRectangle.X, this.ClientRectangle.Height, this.ClientRectangle.Width);
			}

			if(bScrollState)
			{
				return new Rectangle(rct.X - iTextPos, rct.Y, rct.Width + iTextPos, rct.Height);
			}
			else
			{
				return rct;
			}
		}

		/// <summary>
		/// Gets the Rectangle needed to draw the Text correctly modified by a certain number of pixels.
		///	Used for drawing disabled/embossed Text (this.Enabled = false).
		/// </summary>
		/// <param name="bTop">True if embossed text (top left) or false for highlighted text (bottom right).</param>
		/// <returns>Rectangle needed to draw the Text properly.</returns>
		private Rectangle GetModifiedClientRectangle(bool bTop)
		{
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

			if(bTop)
			{
				return new Rectangle(this.ClientRectangle.X + iTopXMod, this.ClientRectangle.Y + iTopYMod, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
			}
			else
			{
				return new Rectangle(this.ClientRectangle.X - iBottomXMod, this.ClientRectangle.Y - iBottomYMod, this.ClientRectangle.Width + 1, this.ClientRectangle.Height + 1);
			}
		}

		#endregion GetModifiedClientRectangle Methods



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

			strfmt.FormatFlags |= StringFormatFlags.NoWrap;

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
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Center;
					}
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.BottomLeft:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Near;
					}
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.BottomRight:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Far;
					}
					strfmt.LineAlignment = StringAlignment.Far;
					break;

				case ContentAlignment.MiddleCenter:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Center;
					}
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.MiddleLeft:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Near;
					}
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.MiddleRight:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Far;
					}
					strfmt.LineAlignment = StringAlignment.Center;
					break;

				case ContentAlignment.TopCenter:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Center;
					}
					strfmt.LineAlignment = StringAlignment.Near;
					break;

				case ContentAlignment.TopLeft:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Near;
					}
					strfmt.LineAlignment = StringAlignment.Near;
					break;

				case ContentAlignment.TopRight:
					if(!bScrollState)
					{
						strfmt.Alignment = StringAlignment.Far;
					}
					strfmt.LineAlignment = StringAlignment.Near;
					break;
			}

			if(bScrollState)
			{
				strfmt.Alignment = StringAlignment.Near;
			}
		}

		#endregion SetStringFormat Method

		#endregion Paint Help Methods

		#endregion Paint Methods



		#region Properties and their EventHandlers

		#region TextPos

		/// <summary>
		/// Keeps track of the position of the Text when scrolling.
		/// Allowed values are 0 to difference in width between Text and this Control.
		/// </summary>
		[
		Browsable(false),
		Category("Scrolling"),
		Description("Keeps track of the position of the Text when scrolling."),
		DefaultValue(0)
		]
		protected int TextPos
		{
			get
			{
				return iTextPos;
			}
			set
			{
				if(iTextPos != value)
				{
					//Don't allow invalid values.
					if(value > iTextPosMax)
					{
						iTextPos = iTextPosMax;
					}
					else if(value < 0)
					{
						iTextPos = 0;
					}
					else
					{
						iTextPos = value;
					}

					OnTextPosChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the TextPos property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the TextPos property changes.")
		]
		public event EventHandler TextPosChanged;

		/// <summary>
		/// Raises the TextPosChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnTextPosChanged(EventArgs e)
		{
			if(TextPosChanged != null)
			{
				TextPosChanged(this, e);
			}
		}

		#endregion TextPos



		#region ScrollState

		/// <summary>
		/// True if Control is scrolling or false otherwise.
		/// </summary>
		[
		Browsable(false),
		Category("Scrolling"),
		Description("True if Control is scrolling or false otherwise."),
		DefaultValue(false)
		]
		public bool ScrollState
		{
			get
			{
				return bScrollState;
			}
		}

		/// <summary>
		/// True if Control is scrolling or false otherwise.
		/// </summary>
		[
		Browsable(false),
		Category("Scrolling"),
		Description("True if Control is scrolling or false otherwise."),
		DefaultValue(false)
		]
		protected bool ScrollStateInternal
		{
			set
			{
				if(bScrollState != value)
				{
					bScrollState = value;
					OnScrollStateChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the ScrollState (ScrollStateInternal really since the ScrollState Property cannot be set) property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the ScrollState (ScrollStateInternal really since the ScrollState Property cannot be set) property changes.")
		]
		public event EventHandler ScrollStateChanged;

		/// <summary>
		/// Raises the ScrollStateChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnScrollStateChanged(EventArgs e)
		{
			if(ScrollStateChanged != null)
			{
				ScrollStateChanged(this, e);
			}
		}

		#endregion ScrollState



		#region ScrollStep

		/// <summary>
		/// The number of pixels the Text should scroll each time the Scroll timer is triggered (based on ScrollInterval).
		/// Only positive values are allowed, negative values are converted into positive ones (Abs).
		/// </summary>
		[
		Browsable(true),
		Category("Scrolling"),
		Description("The number of pixels the Text should scroll each time the Scroll timer is triggered (based on ScrollInterval)."),
		DefaultValue(2)
		]
		public int ScrollStep
		{
			get
			{
				return iScrollStep;
			}
			set
			{
				if(iScrollStep != Math.Abs(value))
				{
					iScrollStep = Math.Abs(value);
					OnScrollStepChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the ScrollStep property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the ScrollStep property changes.")
		]
		public event EventHandler ScrollStepChanged;

		/// <summary>
		/// Raises the ScrollStepChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnScrollStepChanged(EventArgs e)
		{
			if(ScrollStepChanged != null)
			{
				ScrollStepChanged(this, e);
			}
		}

		#endregion ScrollStep



		#region ScrollInterval

		/// <summary>
		/// The number of milliseconds between scrolling the Text a certain number of pixels (based on ScrollStep).
		/// Only positive values are allowed, negative values are converted into positive ones (Abs).
		/// </summary>
		[
		Browsable(true),
		Category("Scrolling"),
		Description("The number of milliseconds between scrolling the Text a certain number of pixels (based on ScrollStep)."),
		DefaultValue(100)
		]
		public int ScrollInterval
		{
			get
			{
				return iScrollInterval;
			}
			set
			{
				if(iScrollInterval != Math.Abs(value))
				{
					iScrollInterval = Math.Abs(value);
					tScroll.Interval = iScrollInterval;
					OnScrollStartPauseChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the ScrollInterval property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the ScrollInterval property changes.")
		]
		public event EventHandler ScrollIntervalChanged;

		/// <summary>
		/// Raises the ScrollIntervalChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnScrollIntervalChanged(EventArgs e)
		{
			if(ScrollIntervalChanged != null)
			{
				ScrollIntervalChanged(this, e);
			}
		}

		#endregion ScrollInterval



		#region ScrollEndPointPause

		/// <summary>
		/// The number of milliseconds that the Text pauses its scrolling at the end points.
		/// Only positive values are allowed, negative values are converted into positive ones (Abs).
		/// </summary>
		[
		Browsable(true),
		Category("Scrolling"),
		Description("The number of milliseconds that the Text pauses its scrolling at the end points."),
		DefaultValue(500)
		]
		public int ScrollEndPointPause
		{
			get
			{
				return iScrollEndPointPause;
			}
			set
			{
				if(iScrollEndPointPause != Math.Abs(value))
				{
					iScrollEndPointPause = Math.Abs(value);
					OnScrollEndPointPauseChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the ScrollEndPointPause property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the ScrollEndPointPause property changes.")
		]
		public event EventHandler ScrollEndPointPauseChanged;

		/// <summary>
		/// Raises the ScrollEndPointPauseChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnScrollEndPointPauseChanged(EventArgs e)
		{
			if(ScrollEndPointPauseChanged != null)
			{
				ScrollEndPointPauseChanged(this, e);
			}
		}

		#endregion ScrollEndPointPause



		#region ScrollStartPause

		/// <summary>
		/// The number of milliseconds that the Text pauses before starting to scroll.
		/// Only positive values are allowed, negative values are converted into positive ones (Abs).
		/// </summary>
		[
		Browsable(true),
		Category("Scrolling"),
		Description("The number of milliseconds that the Text pauses before starting to scroll."),
		DefaultValue(1000)
		]
		public int ScrollStartPause
		{
			get
			{
				return iScrollStartPause;
			}
			set
			{
				if(iScrollStartPause != Math.Abs(value))
				{
					iScrollStartPause = Math.Abs(value);
					OnScrollStartPauseChanged(new EventArgs());
				}
			}
		}

		/// <summary>
		/// Occurs when the ScrollStartPause property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Property Changed"),
		Description("Occurs when the ScrollStartPause property changes.")
		]
		public event EventHandler ScrollStartPauseChanged;

		/// <summary>
		/// Raises the ScrollStartPauseChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnScrollStartPauseChanged(EventArgs e)
		{
			if(ScrollStartPauseChanged != null)
			{
				ScrollStartPauseChanged(this, e);
			}
		}

		#endregion ScrollStartPause



		#region ImageOverText

		/// <summary>
		/// Decides whether the Image should be painted above the Text or not.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("True if the Image should be painted above the Text."),
		DefaultValue(false)
		]
		public bool ImageOverText
		{
			get
			{
				return bImageOverText;
			}
			set
			{
				if(bImageOverText != value)
				{
					bImageOverText = value;
					OnImageOverTextChanged(new EventArgs());
					PaintAll();
				}
			}
		}

		/// <summary>
		/// Occurs when the ImageOverText property changes.
		/// </summary>
		[
		Browsable(true),
		Category("Appearance"),
		Description("Occurs when the ImageOverText property changes.")
		]
		public event EventHandler ImageOverTextChanged;

		/// <summary>
		/// Raises the ImageOverTextChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		public virtual void OnImageOverTextChanged(EventArgs e)
		{
			if(ImageOverTextChanged != null)
			{
				ImageOverTextChanged(this, e);
			}
		}

		#endregion ImageOverText



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
		/// <param name="e">Standard EventArgs object.</param>
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



		#region Scroll Methods

		/// <summary>
		/// Start scrolling using the ScrollStartPause property.
		/// If already scrolling, do nothing.
		/// </summary>
		public void ScrollStart()
		{
			if(!bScrollState)
			{
				ScrollStateInternal = true;
				SetTextPosVariable();
				tScroll.Interval = iScrollInterval + iScrollStartPause;
				tScroll.Start();
			}
		}

		/// <summary>
		/// Start scrolling using the supplied DelayTime instead of the ScrollStartPause property.
		/// </summary>
		/// <param name="iDelayTime">DelayTime in milliseconds until scrolling starts.</param>
		public void ScrollDelayedStart(int iDelayTime)
		{
			if(!bScrollState)
			{
				ScrollStateInternal = true;
				tScroll.Stop();

				SetTextPosVariable();
				tScroll.Interval = iScrollInterval + iDelayTime;
				tScroll.Start();
			}
		}

		/// <summary>
		/// Stop scrolling.
		/// </summary>
		public void ScrollStop()
		{
			if(bScrollState)
			{
				ScrollStateInternal = false;
				tScroll.Stop();
				SetTextPosVariable();
				PaintAll();
			}
		}

		/// <summary>
		/// Toggle scrolling - if scrolling stop scrolling, if not scrolling start scrolling.
		/// </summary>
		public void ScrollToggle()
		{
			if(bScrollState)
			{
				ScrollStop();
			}
			else
			{
				ScrollStart();
			}
		}

		/// <summary>
		/// Pause the scrolling without redrawing the Control in its initial state (TextAlign etc).
		/// Also see ScrollContinue.
		/// </summary>
		public void ScrollPause()
		{
			if(bScrollState)
			{
				ScrollStateInternal = false;
				tScroll.Stop();
			}
		}

		/// <summary>
		/// Continue scrolling from current position.
		/// Also see ScrollPause.
		/// </summary>
		public void ScrollContinue()
		{
			if(!bScrollState)
			{
				ScrollStateInternal = true;
				tScroll.Start();
			}
		}

		/// <summary>
		/// Reset the Text of the Control to its initial unscrolled state.
		/// </summary>
		public void ScrollReset()
		{
			if(bScrollState)
			{
				tScroll.Stop();
				tScroll.Interval = iScrollInterval + iScrollStartPause;
				tScroll.Start();
			}
			SetTextPosVariable();
			PaintAll();
		}

		#endregion Scroll Methods



		#region Help Methods

		/// <summary>
		/// Set the TextPos variable to its starting value depending on TextAlign.
		/// </summary>
		private void SetTextPosVariable()
		{
			if(this.TextAlign == ContentAlignment.BottomLeft || this.TextAlign == ContentAlignment.MiddleLeft || this.TextAlign == ContentAlignment.TopLeft)
			{
				iTextPos = 0;
			}
			else if(this.TextAlign == ContentAlignment.BottomRight || this.TextAlign == ContentAlignment.MiddleRight || this.TextAlign == ContentAlignment.TopRight)
			{
				iTextPos = iTextPosMax;
			}
			else if(this.TextAlign == ContentAlignment.BottomCenter || this.TextAlign == ContentAlignment.MiddleCenter || this.TextAlign == ContentAlignment.TopCenter)
			{
				iTextPos = iTextPosMax / 2;
			}
		}

		/// <summary>
		/// Set the TextPosMax variable to its correct value depending on StringFormat.
		/// </summary>
		private void SetTextPosMaxVariable()
		{
			if(sfe == MWCommon.StringFormatEnum.GenericTypographic)
			{
				iTextPosMax = MWCommon.MWCommon.GetStringFormattedStringWidth(this, strfmt) + 2 - this.Width;
			}
			else
			{
				iTextPosMax = MWCommon.MWCommon.GetGraphicalStringWidth(this) + 2 - this.Width;
			}
		}

		#endregion Help Methods

	}
}
