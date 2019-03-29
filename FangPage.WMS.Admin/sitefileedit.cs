using System;
using System.IO;
using System.Text;
using System.Web;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000029 RID: 41
	public class sitefileedit : SuperController
	{
		// Token: 0x06000060 RID: 96 RVA: 0x000087FC File Offset: 0x000069FC
		protected override void View()
		{
			this.filename = Path.GetFileName(this.m_path);
			string mapPath = FPUtils.GetMapPath(string.Concat(new string[]
			{
				this.webpath,
				"sites/",
				this.m_sitepath,
				"/",
				this.m_path
			}));
			if (File.Exists(mapPath))
			{
				if (this.ispost)
				{
					if (!this.isperm)
					{
						this.ShowErr("对不起，您没有权限操作。");
					}
					else
					{
						this.content = HttpUtility.HtmlDecode(FPRequest.GetString("content"));
						using (FileStream fileStream = new FileStream(mapPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
						{
							byte[] bytes = Encoding.UTF8.GetBytes(this.content);
							fileStream.Write(bytes, 0, bytes.Length);
							fileStream.Close();
						}
						base.Response.Redirect("sitefileedit.aspx?sitepath=" + this.m_sitepath + "&path=" + this.m_path);
					}
				}
				else
				{
					using (StreamReader streamReader = new StreamReader(mapPath, Encoding.UTF8))
					{
						this.content = streamReader.ReadToEnd();
						streamReader.Close();
					}
					this.m_path = Path.GetDirectoryName(this.m_path).Replace("\\", "/");
					this.content = HttpUtility.HtmlEncode(this.content);
					base.SaveRightURL();
				}
			}
		}

		// Token: 0x04000053 RID: 83
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x04000054 RID: 84
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x04000055 RID: 85
		protected string filename = "";

		// Token: 0x04000056 RID: 86
		protected string content = "";
	}
}
