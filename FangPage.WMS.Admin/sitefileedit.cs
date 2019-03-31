using System;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000036 RID: 54
	public class sitefileedit : SuperController
	{
		// Token: 0x0600007D RID: 125 RVA: 0x0000A3AC File Offset: 0x000085AC
		protected override void Controller()
		{
			if (this.m_path.StartsWith("/"))
			{
				this.filename = this.m_sitepath + this.m_path;
			}
			else
			{
				this.filename = this.m_sitepath + "/" + this.m_path;
			}
			string mapPath = FPFile.GetMapPath(this.webpath + "sites/" + this.filename);
			if (!File.Exists(mapPath))
			{
				this.ShowErr("对不起，该文件已被删除。");
				return;
			}
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				this.content = HttpUtility.HtmlDecode(FPRequest.GetString("content"));
				using (FileStream fileStream = new FileStream(mapPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
				{
					byte[] bytes = Encoding.UTF8.GetBytes(this.content);
					fileStream.Write(bytes, 0, bytes.Length);
					fileStream.Close();
				}
				base.Response.Redirect("sitefileedit.aspx?sitepath=" + this.m_sitepath + "&path=" + this.m_path);
			}
			using (StreamReader streamReader = new StreamReader(mapPath, Encoding.UTF8))
			{
				this.content = streamReader.ReadToEnd();
				streamReader.Close();
			}
			this.m_path = Path.GetDirectoryName(this.m_path).Replace("\\", "/");
			this.content = HttpUtility.HtmlEncode(this.content);
		}

		// Token: 0x0400008A RID: 138
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x0400008B RID: 139
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x0400008C RID: 140
		protected string filename = "";

		// Token: 0x0400008D RID: 141
		protected string content = "";
	}
}
