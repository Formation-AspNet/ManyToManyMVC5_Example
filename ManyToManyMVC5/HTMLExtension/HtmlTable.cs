using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ManyToManyMVC5.HTMLExtension
{
	public static class Extension
	{

		public static MvcHtmlString Grid(this HtmlHelper helper, string name, IEnumerable list, string[] columnNames,bool showHeader =false)
		{
			var table = new HtmlTable { ID = name };

			// add header rows
			if (showHeader)
			{
				table.Rows.Add(CreateHtmlTableHeaderRow(columnNames));
			}
			

			foreach (var row in CreateRowsWithList(list, columnNames))
			{
				table.Rows.Add(row);
			}

			return WriteToHtml(table);
		}

		public static TagBuilder FluentTextBox(this HtmlHelper htmlHelper, string name, string value)
		{
			var tagBuilder = new TagBuilder("input");
			tagBuilder.Attributes["name"] = name;
			tagBuilder.Attributes["id"] = name;
			tagBuilder.Attributes["value"] = value;
			return tagBuilder;
		}

		public static TagBuilder OnFocus(this TagBuilder tagBuilder, string script)
		{
			tagBuilder.MergeAttribute("onfocus", script);
			return tagBuilder;
		}

		public static TagBuilder OnBlur(this TagBuilder tagBuilder, string script)
		{
			tagBuilder.MergeAttribute("onblur", script);
			return tagBuilder;
		}

		

		private static HtmlTableRow CreateHtmlTableRow()
		{
			return new HtmlTableRow();
		}

		private static HtmlTableCell CreateHtmlTableCell(string innerHtml)
		{
			return new HtmlTableCell() { InnerHtml = innerHtml };
		}

		private static HtmlTableRow CreateHtmlTableHeaderRow(string[] columnNames)
		{
			var row = new HtmlTableRow();

			foreach (var columnName in columnNames)
			{
				var cell = new HtmlTableCell() { InnerHtml = columnName };
				row.Cells.Add(cell);
			}

			return row;
		}

		private static object GetPropertyValue(this object obj, string propertyName)
		{
			var property = obj.GetType().GetProperty(propertyName);
			if (property == null) throw new ArgumentNullException(String.Format("{0} is invalid", propertyName));

			return property.GetValue(obj, null);
		}

		private static HtmlTableRow[] CreateRowsWithList(IEnumerable list, string[] columnNames)
		{
			var rows = new List<HtmlTableRow>();

			foreach (var item in list)
			{
				var newRow = CreateHtmlTableRow();

				for (var i = 0; i < columnNames.Count(); i++)
				{
					var cell = CreateHtmlTableCell(item.GetPropertyValue(columnNames[i]) as String);
					newRow.Cells.Add(cell);
				}

				rows.Add(newRow);
			}

			return rows.ToArray();
		}

		private static MvcHtmlString WriteToHtml(HtmlTable table)
		{
			var sw = new StringWriter(new StringBuilder());
			var htw = new HtmlTextWriter(sw);

			table.RenderControl(htw);
			return new MvcHtmlString(sw.ToString());
		}

	
	}


}