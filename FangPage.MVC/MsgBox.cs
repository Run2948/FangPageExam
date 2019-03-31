using System;
using System.Text;
using System.Web;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000007 RID: 7
	public class MsgBox
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public static void Show(string message)
		{
			MsgBox.Show(message, "");
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DE0 File Offset: 0x00000FE0
		public static void Show(string message, string title)
		{
			MsgBox.Show(message, title, "");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public static void Show(string message, string title, string btn)
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
			stringBuilder.Append("html, body, ul, h2, p{margin: 0; padding: 0; border: 0; outline: 0; font-size: 100%; vertical-align: baseline; background: transparent; }\r\n");
			stringBuilder.Append("body, button, input, textarea {font: 12px/1.5 Tahoma, Helvetica, Arial, 'Microsoft YaHei', sans-serif;}\r\n");
			stringBuilder.Append(".board {\r\n");
			stringBuilder.Append("\tborder: #a7c5e2 1px solid;\r\n");
			stringBuilder.Append("\tpadding: 1px;\r\n");
			stringBuilder.Append("\twidth: 470px;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".topinfo {\r\n");
			stringBuilder.Append("\ttext-align: left;\r\n");
			stringBuilder.Append("\tpadding-top: 12px ;\r\n");
			stringBuilder.Append("\tpadding-left:36px;\r\n");
			stringBuilder.Append("\tfont: bold 16px verdana;\r\n");
			stringBuilder.Append("\tbackground: #ebf3fb url(data:image/png;base64,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) no-repeat 10px center;\r\n");
			stringBuilder.Append("\tcolor: #4a8f00;\r\n");
			stringBuilder.Append("\theight:30px;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".tipcontent {\r\n");
			if (btn != "")
			{
				stringBuilder.Append("\tborder-bottom: #d2e2f4 1px solid;\r\n");
			}
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
			stringBuilder.Append(".butinfo.butinfo a{\r\n");
			stringBuilder.Append("\tdisplay: inline-block;\r\n");
			stringBuilder.Append("\theight: 33px;\r\n");
			stringBuilder.Append("\tline-height: 31px;\r\n");
			stringBuilder.Append("\tborder-radius: 2px;\r\n");
			stringBuilder.Append("\tbackground-color: #4bd252;\r\n");
			stringBuilder.Append("\tborder: solid 1px #36b148;\r\n");
			stringBuilder.Append("\ttext-align: center;\r\n");
			stringBuilder.Append("\tcolor: #fff;\r\n");
			stringBuilder.Append("\tfont-size: 14px;\r\n");
			stringBuilder.Append("\tbox-sizing:border-box;\r\n");
			stringBuilder.Append("\tcursor: pointer;\r\n");
			stringBuilder.Append("\t-webkit-user-select:none;\r\n");
			stringBuilder.Append("\ttext-decoration:none;\r\n");
			stringBuilder.Append("\tpadding:0 15px;\r\n");
			stringBuilder.Append("}\r\n");
			stringBuilder.Append(".butinfo .button:hover{background-color: #4fe256;border: solid 1px #49c74f;}\r\n");
			stringBuilder.Append(".butinfo .button:active{background-color: #40bf46;border: solid 1px #31b945;}\r\n");
			stringBuilder.Append("</style>\r\n");
			stringBuilder.Append("</head>\r\n");
			stringBuilder.Append("<body style=\"margin-top: 80px\">\r\n");
			stringBuilder.Append("<center>\r\n");
			stringBuilder.Append("  <div class=board>\r\n");
			stringBuilder.Append("    <div class=topinfo>" + title.ToString() + "</div>\r\n");
			stringBuilder.Append("    <div class=tipcontent>\r\n");
			stringBuilder.Append("      " + message.ToString() + "\r\n");
			stringBuilder.Append("    </div>\r\n");
			if (btn != "")
			{
				stringBuilder.Append("<div class=butinfo>\r\n");
				string[] array = FPArray.SplitString(btn, ",");
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = FPArray.SplitString(array[i], "|", 2);
					if (array2[0] == "返回")
					{
						stringBuilder.Append("<a class=\"button\" href=\"javascript:history.back();\">" + array2[0] + "</a>&nbsp;&nbsp;\r\n");
					}
					else if (array2[0] == "关闭")
					{
						stringBuilder.Append("<a class=\"button\" href=\"javascript:window.close();\">" + array2[0] + "</a>&nbsp;&nbsp;\r\n");
					}
					else
					{
						stringBuilder.Append(string.Concat(new string[]
						{
							"<a class=\"button\" target=\"_top\" href=\"",
							array2[1],
							"\">",
							array2[0],
							"</a>&nbsp;&nbsp;\r\n"
						}));
					}
				}
				stringBuilder.Append("</div>\r\n");
			}
			stringBuilder.Append("  </div>\r\n");
			stringBuilder.Append("</center>\r\n");
			stringBuilder.Append("</body>\r\n");
			stringBuilder.Append("</html>\r\n");
			HttpContext.Current.Response.Clear();
			HttpContext.Current.Response.Write(stringBuilder.ToString());
			HttpContext.Current.Response.End();
		}
	}
}
