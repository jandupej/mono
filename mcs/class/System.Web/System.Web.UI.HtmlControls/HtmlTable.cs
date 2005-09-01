//
// System.Web.UI.HtmlControls.HtmlTable.cs
//
// Author:
//	Sebastien Pouliot  <sebastien@ximian.com>
//
// Copyright (C) 2005 Novell, Inc (http://www.novell.com)
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls {

#if NET_2_0
	[ParseChildren (true, "Rows", ChildControlType = typeof(Control))]
#else
	[ParseChildren (true, "Rows")]
#endif		
	public class HtmlTable : HtmlContainerControl {

		private HtmlTableRowCollection _rows;


		public HtmlTable ()
			: base ("table")
		{
		}


		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Align {
			get {
				string s = Attributes ["align"];
				return (s == null) ? String.Empty : s;
			}
			set {
				if (value == null)
					Attributes.Remove ("align");
				else
					Attributes ["align"] = value;
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string BgColor {
			get {
				string s = Attributes ["bgcolor"];
				return (s == null) ? String.Empty : s;
			}
			set {
				if (value == null)
					Attributes.Remove ("bgcolor");
				else
					Attributes ["bgcolor"] = value;
				}
		}

		[DefaultValue (-1)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Border {
			get {
				string s = Attributes ["border"];
				return (s == null) ? -1 : Convert.ToInt32 (s);
			}
			set {
				if (value == -1)
					Attributes.Remove ("border");
				else
					Attributes ["border"] = value.ToString (CultureInfo.InvariantCulture);
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string BorderColor {
			get {
				string s = Attributes ["bordercolor"];
				return (s == null) ? String.Empty : s;
			}
			set {
				if (value == null)
					Attributes.Remove ("bordercolor");
				else
					Attributes ["bordercolor"] = value;
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int CellPadding {
			get {
				string s = Attributes ["cellpadding"];
				return (s == null) ? -1 : Convert.ToInt32 (s);
			}
			set {
				if (value == -1)
					Attributes.Remove ("cellpadding");
				else
					Attributes ["cellpadding"] = value.ToString (CultureInfo.InvariantCulture);
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int CellSpacing {
			get {
				string s = Attributes ["cellspacing"];
				return (s == null) ? -1 : Convert.ToInt32 (s);
			}
			set {
				if (value == -1)
					Attributes.Remove ("cellspacing");
				else
					Attributes ["cellspacing"] = value.ToString (CultureInfo.InvariantCulture);
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Height {
			get {
				string s = Attributes ["height"];
				return (s == null) ? String.Empty : s;
			}
			set {
				if (value == null)
					Attributes.Remove ("height");
				else
					Attributes ["height"] = value;
			}
		}

		public override string InnerHtml {
			get { throw new NotSupportedException (); }
			set { throw new NotSupportedException (); }
		}

		public override string InnerText {
			get { throw new NotSupportedException (); }
			set { throw new NotSupportedException (); }
		}

		[Browsable (false)]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		public virtual HtmlTableRowCollection Rows {
			get {
				if (_rows == null)
					_rows = new HtmlTableRowCollection (this);
				return _rows;
			}
		}

		[DefaultValue ("")]
		[DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public string Width {
			get {
				string s = Attributes ["width"];
				return (s == null) ? String.Empty : s;
			}
			set {
				if (value == null)
					Attributes.Remove ("width");
				else
					Attributes ["width"] = value;
			}
		}


		protected override ControlCollection CreateControlCollection ()
		{
			return new HtmlTableRowControlCollection (this);
		}

#if NET_2_0
		protected internal
#else		
		protected
#endif		
		override void RenderChildren (HtmlTextWriter writer)
		{
			int n = (_rows == null) ? 0 : _rows.Count;
			if (n > 0) {
				writer.Indent++;
				for (int i=0; i < n; i++) {
					writer.WriteLine ();
					_rows [i].RenderControl (writer);
				}
				writer.Indent--;
			}
		}

		protected override void RenderEndTag (HtmlTextWriter writer)
		{
			writer.WriteLine ();
			writer.WriteEndTag (TagName);
			writer.WriteLine ();
		}


		protected class HtmlTableRowControlCollection : ControlCollection {

			internal HtmlTableRowControlCollection (HtmlTable owner)
				: base (owner)
			{
			}

			public override void Add (Control child)
			{
				if (child == null)
					throw new NullReferenceException ("null");
				if (!(child is HtmlTableRow))
					throw new ArgumentException ("child", Locale.GetText ("Must be an HtmlTableRow instance."));

				base.Add (child);
			}

			public override void AddAt (int index, Control child)
			{
				if (child == null)
					throw new NullReferenceException ("null");
				if (!(child is HtmlTableRow))
					throw new ArgumentException ("child", Locale.GetText ("Must be an HtmlTableRow instance."));

				base.AddAt (index, child);
			}
		}
	}
}
