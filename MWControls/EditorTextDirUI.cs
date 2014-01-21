using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
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
	/// EditorTextDirUI is used in conjunction with the EditorTextDir UITypeEditor.
	/// </summary>
	public class EditorTextDirUI : System.Windows.Forms.UserControl
	{
		#region Variables

		private ITypeDescriptorContext itdc = null;
		private IWindowsFormsEditorService iwfes = null;
		private MWCommon.TextDir tdTextDir = MWCommon.TextDir.Normal;

		private MWControls.MWLabel mwlblTDN;
		private MWControls.MWLabel mwlblTDL;
		private MWControls.MWLabel mwlblTDR;
		private MWControls.MWLabel mwlblTDU;
		private System.Windows.Forms.Label lblDescription;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		#endregion Variables



		#region Constructor and Dispose

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public EditorTextDirUI()
		{
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
			this.mwlblTDN = new MWControls.MWLabel();
			this.mwlblTDL = new MWControls.MWLabel();
			this.mwlblTDR = new MWControls.MWLabel();
			this.mwlblTDU = new MWControls.MWLabel();
			this.lblDescription = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// mwlblTDN
			// 
			this.mwlblTDN.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.mwlblTDN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mwlblTDN.ForeColor = System.Drawing.SystemColors.HotTrack;
			this.mwlblTDN.Location = new System.Drawing.Point(24, 0);
			this.mwlblTDN.Name = "mwlblTDN";
			this.mwlblTDN.Size = new System.Drawing.Size(80, 24);
			this.mwlblTDN.TabIndex = 0;
			this.mwlblTDN.Text = "Text";
			this.mwlblTDN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mwlblTDN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mwlblTDN_MouseUp);
			// 
			// mwlblTDL
			// 
			this.mwlblTDL.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.mwlblTDL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mwlblTDL.ForeColor = System.Drawing.SystemColors.ControlText;
			this.mwlblTDL.Location = new System.Drawing.Point(0, 24);
			this.mwlblTDL.Name = "mwlblTDL";
			this.mwlblTDL.Size = new System.Drawing.Size(24, 80);
			this.mwlblTDL.TabIndex = 1;
			this.mwlblTDL.Text = "Text";
			this.mwlblTDL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mwlblTDL.TextDir = MWCommon.TextDir.Left;
			this.mwlblTDL.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mwlblTDL_MouseUp);
			// 
			// mwlblTDR
			// 
			this.mwlblTDR.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.mwlblTDR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mwlblTDR.ForeColor = System.Drawing.SystemColors.ControlText;
			this.mwlblTDR.Location = new System.Drawing.Point(104, 24);
			this.mwlblTDR.Name = "mwlblTDR";
			this.mwlblTDR.Size = new System.Drawing.Size(24, 80);
			this.mwlblTDR.TabIndex = 2;
			this.mwlblTDR.Text = "Text";
			this.mwlblTDR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mwlblTDR.TextDir = MWCommon.TextDir.Right;
			this.mwlblTDR.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mwlblTDR_MouseUp);
			// 
			// mwlblTDU
			// 
			this.mwlblTDU.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.mwlblTDU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.mwlblTDU.ForeColor = System.Drawing.SystemColors.ControlText;
			this.mwlblTDU.Location = new System.Drawing.Point(24, 104);
			this.mwlblTDU.Name = "mwlblTDU";
			this.mwlblTDU.Size = new System.Drawing.Size(80, 24);
			this.mwlblTDU.TabIndex = 3;
			this.mwlblTDU.Text = "Text";
			this.mwlblTDU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.mwlblTDU.TextDir = MWCommon.TextDir.UpsideDown;
			this.mwlblTDU.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mwlblTDU_MouseUp);
			// 
			// lblDescription
			// 
			this.lblDescription.Location = new System.Drawing.Point(24, 24);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(80, 80);
			this.lblDescription.TabIndex = 4;
			this.lblDescription.Text = "Text\nDirection:";
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblDescription.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblDescription_MouseUp);
			// 
			// EditorTextDirUI
			// 
			this.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.lblDescription,
																		  this.mwlblTDN,
																		  this.mwlblTDU,
																		  this.mwlblTDR,
																		  this.mwlblTDL});
			this.Name = "EditorTextDirUI";
			this.Size = new System.Drawing.Size(128, 128);
			this.Resize += new System.EventHandler(this.EditorTextDirUI_Resize);
			this.ResumeLayout(false);

		}

		#endregion Component Designer generated code



		#region Properties and EventHandlers

		#region TextDir

		/// <summary>
		/// Changes the TextDir and sets the new value for this ITDC.
		/// </summary>
		[
		Browsable(false),
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

				lblDescription.Text = "Text\nDirection: " + value.ToString();

				if(tdTextDir != value)
				{
					tdTextDir = value;

					switch(tdTextDir)
					{
						case MWCommon.TextDir.Normal:
							mwlblTDN.ForeColor = SystemColors.HotTrack;
							mwlblTDL.ForeColor = SystemColors.ControlText;
							mwlblTDR.ForeColor = SystemColors.ControlText;
							mwlblTDU.ForeColor = SystemColors.ControlText;
							break;

						case MWCommon.TextDir.Left:
							mwlblTDN.ForeColor = SystemColors.ControlText;
							mwlblTDL.ForeColor = SystemColors.HotTrack;
							mwlblTDR.ForeColor = SystemColors.ControlText;
							mwlblTDU.ForeColor = SystemColors.ControlText;
							break;

						case MWCommon.TextDir.Right:
							mwlblTDN.ForeColor = SystemColors.ControlText;
							mwlblTDL.ForeColor = SystemColors.ControlText;
							mwlblTDR.ForeColor = SystemColors.HotTrack;
							mwlblTDU.ForeColor = SystemColors.ControlText;
							break;

						case MWCommon.TextDir.UpsideDown:
							mwlblTDN.ForeColor = SystemColors.ControlText;
							mwlblTDL.ForeColor = SystemColors.ControlText;
							mwlblTDR.ForeColor = SystemColors.ControlText;
							mwlblTDU.ForeColor = SystemColors.HotTrack;
							break;
					}

					OnTextDirChanged(new MWCommon.TextDirEventArgs(tdTextDirOld, tdTextDir));

					this.ITDC.PropertyDescriptor.SetValue(this.ITDC.Instance, tdTextDir);
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
		Browsable(false),
		Category("Appearance"),
		Description("Occurs when the TextDir property changes.")
		]
		public event TextDirEventHandler TextDirChanged;

		/// <summary>
		/// Raises the TextDirChanged Event.
		/// </summary>
		/// <param name="e">Standard EventArgs object.</param>
		protected virtual void OnTextDirChanged(MWCommon.TextDirEventArgs e)
		{
			if(TextDirChanged != null)
			{
				TextDirChanged(this, e);
			}
		}

		#endregion TextDir



		#region ITDC

		/// <summary>
		/// The ITypeDescriptorContext of this Control.
		/// Used at design time.
		/// </summary>
		[
		Browsable(false),
		Category("Design Time"),
		Description("ITypeDescriptorContext of this Control."),
		DefaultValue(null)
		]
		public ITypeDescriptorContext ITDC
		{
			get
			{
				return itdc;
			}
			set
			{
				itdc = value;
			}
		}

		#endregion ITDC



		#region IWFES

		/// <summary>
		/// The IWindowsFormsEditorService of this Control.
		/// Used at design time.
		/// </summary>
		[
		Browsable(false),
		Category("Design Time"),
		Description("IWindowsFormsEditorService of this Control."),
		DefaultValue(null)
		]
		public IWindowsFormsEditorService IWFES
		{
			get
			{
				return iwfes;
			}
			set
			{
				iwfes = value;
			}
		}

		#endregion IWFES

		#endregion Properties and EventHandlers



		#region EventHandlers

		#region EditorTextDirUI

		/// <summary>
		/// Always display as same size.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard EventArgs object.</param>
		private void EditorTextDirUI_Resize(object sender, System.EventArgs e)
		{
			this.Size = new Size(128, 128);
		}

		#endregion EditorTextDirUI



		#region Labels

		/// <summary>
		/// Select TextDir.Normal if this Control is clicked.
		/// If it is clicked with the Left MouseButton also close it.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard MouseEventArgs object.</param>
		private void mwlblTDN_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.TextDir = MWCommon.TextDir.Normal;
			this.Refresh();

			if(e.Button == MouseButtons.Left)
			{
				this.IWFES.CloseDropDown();
			}
		}

		/// <summary>
		/// Select TextDir.UpsideDown if this Control is clicked.
		/// If it is clicked with the Left MouseButton also close it.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard MouseEventArgs object.</param>
		private void mwlblTDU_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.TextDir = MWCommon.TextDir.UpsideDown;
			this.Refresh();

			if(e.Button == MouseButtons.Left)
			{
				this.IWFES.CloseDropDown();
			}
		}

		/// <summary>
		/// Select TextDir.Left if this Control is clicked.
		/// If it is clicked with the Left MouseButton also close it.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard MouseEventArgs object.</param>
		private void mwlblTDL_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.TextDir = MWCommon.TextDir.Left;
			this.Refresh();

			if(e.Button == MouseButtons.Left)
			{
				this.IWFES.CloseDropDown();
			}
		}

		/// <summary>
		/// Select TextDir.Right if this Control is clicked.
		/// If it is clicked with the Left MouseButton also close it.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard MouseEventArgs object.</param>
		private void mwlblTDR_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.TextDir = MWCommon.TextDir.Right;
			this.Refresh();

			if(e.Button == MouseButtons.Left)
			{
				this.IWFES.CloseDropDown();
			}
		}

		/// <summary>
		/// Close this Control if the Right MouseButton is clicked.
		/// </summary>
		/// <param name="sender">Standard sender object.</param>
		/// <param name="e">Standard MouseEventArgs object.</param>
		private void lblDescription_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Right)
			{
				this.IWFES.CloseDropDown();
			}
		}

		#endregion Labels

		#endregion EventHandlers

	}
}
