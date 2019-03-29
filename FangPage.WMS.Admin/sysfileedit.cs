using System;
using System.IO;
using System.Text;
using System.Web;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000015 RID: 21
	public class sysfileedit : SuperController
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00004F2C File Offset: 0x0000312C
		protected override void View()
		{
			this.m_path = Path.GetDirectoryName(this.filepath).Replace("\\", "/");
			this.filename = Path.GetFileName(this.filepath);
			string mapPath = FPUtils.GetMapPath(this.webpath + this.filepath);
			if (!File.Exists(mapPath))
			{
				this.ShowErr("文件已被删除或不存在。");
			}
			else if (this.ispost)
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
					base.Response.Redirect("sysfileedit.aspx?path=" + this.filepath);
				}
			}
			else
			{
				using (StreamReader streamReader = new StreamReader(mapPath, Encoding.UTF8))
				{
					this.content = streamReader.ReadToEnd();
					streamReader.Close();
				}
				this.content = HttpUtility.HtmlEncode(this.content);
				base.SaveRightURL();
			}
		}

		// Token: 0x04000025 RID: 37
		protected string filepath = FPRequest.GetString("path");

		// Token: 0x04000026 RID: 38
		protected string m_path = "";

		// Token: 0x04000027 RID: 39
		protected string filename = "";

		// Token: 0x04000028 RID: 40
		protected string content = "";
	}
}
