using System;
using System.Text;
using System.Web;

namespace FangPage.MVC
{
	// Token: 0x02000005 RID: 5
	public class MessageBox
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000028F8 File Offset: 0x00000AF8
		public static void Show(string message)
		{
			MessageBox.Show(message, "");
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002908 File Offset: 0x00000B08
		public static void Show(string message, string title)
		{
			if (title == "")
			{
				title = "提示信息";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
			stringBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
			stringBuilder.Append("<head>\r\n");
			stringBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=Content-Type>\r\n");
			stringBuilder.AppendFormat("<title>{0}</title>\r\n", title);
			stringBuilder.Append("<style type=text/css>\r\n");
			stringBuilder.Append(".board {\r\n");
			stringBuilder.Append("\tborder: #a7c5e2 1px solid;\r\n");
			stringBuilder.Append("\tpadding: 1px;\r\n");
			stringBuilder.Append("\twidth: 470px;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".topinfo {\r\n");
			stringBuilder.Append("\ttext-align: left;\r\n");
			stringBuilder.Append("\tpadding: 12px;\r\n");
			stringBuilder.Append("\tfont: bold 16px verdana;\r\n");
			stringBuilder.Append("\tbackground: #ebf3fb;\r\n");
			stringBuilder.Append("\tcolor: #4a8f00;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".tipcontent {\r\n");
			stringBuilder.Append("\tborder-bottom: #d2e2f4 1px solid;\r\n");
			stringBuilder.Append("\ttext-align: left;\r\n");
			stringBuilder.Append("\tpadding: 15px;\r\n");
			stringBuilder.Append("\tline-height: 22px;\r\n");
			stringBuilder.Append("\ttext-indent: 26px;\r\n");
			stringBuilder.Append("\tmin-height: 120px;\r\n");
			stringBuilder.Append("\tbackground: #fff;\r\n");
			stringBuilder.Append("\tcolor: red;\r\n");
			stringBuilder.Append("\tmax-height: 300px;\r\n");
			stringBuilder.Append("\tfont-size: 15px;\r\n");
			stringBuilder.Append("\tborder-top: #d2e2f4 1px solid;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".butinfo {\r\n");
			stringBuilder.Append("\ttext-align: right;\r\n");
			stringBuilder.Append("\tpadding: 8px;\r\n");
			stringBuilder.Append("\tfont: bold 15px verdana;\r\n");
			stringBuilder.Append("\tbackground: #ebf3fb;\r\n");
			stringBuilder.Append("\tcolor: #4a8f00;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append("</style>\r\n");
			stringBuilder.Append("</head>\r\n");
			stringBuilder.Append("<body style=\"margin-top: 80px\">\r\n");
			stringBuilder.Append("<center>\r\n");
			stringBuilder.Append("  <div class=board>\r\n");
			stringBuilder.Append("    <div class=topinfo>" + title.ToString() + "</div>\r\n");
			stringBuilder.Append("    <div class=tipcontent>\r\n");
			stringBuilder.Append("      " + message.ToString() + "\r\n");
			stringBuilder.Append("    </div>\r\n");
			stringBuilder.Append("    <div class=butinfo>\r\n");
			stringBuilder.Append("    <a href=\"javascript:history.back();\">返回</a></div>\r\n");
			stringBuilder.Append("  </div>\r\n");
			stringBuilder.Append("</center>\r\n");
			stringBuilder.Append("</body>\r\n");
			stringBuilder.Append("</html>\r\n");
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.Write(stringBuilder.ToString());
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static void Alert(string message)
		{
			HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>alert('" + message + "')</script>");
			HttpContext.Current.Response.Write("<script>history.go(-1)</script>");
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C60 File Offset: 0x00000E60
		public static void Alert(string message, string src)
		{
			HttpContext.Current.Response.Write(string.Concat(new string[]
			{
				"<script language='javascript' type='text/javascript'>alert('",
				message,
				"');location.href='",
				src,
				"'</script>"
			}));
			HttpContext.Current.Response.End();
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static void Alert(string message, bool close)
		{
			if (close)
			{
				HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>alert('" + message + "');window.close()</script>");
				HttpContext.Current.Response.End();
			}
			else
			{
				HttpContext.Current.Response.Write("<script language='javascript' type='text/javascript'>alert('" + message + "')</script>");
				HttpContext.Current.Response.End();
			}
		}
	}
}
