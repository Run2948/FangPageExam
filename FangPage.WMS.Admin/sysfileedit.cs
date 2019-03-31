using System;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001C RID: 28
	public class sysfileedit : SuperController
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00005E14 File Offset: 0x00004014
		protected override void Controller()
		{
			this.m_path = Path.GetDirectoryName(this.filepath).Replace("\\", "/");
			this.filename = Path.GetFileName(this.filepath);
			string mapPath = FPFile.GetMapPath(this.webpath + this.filepath);
			if (!File.Exists(mapPath))
			{
				this.ShowErr("文件已被删除或不存在。");
				return;
			}
			if (!this.ispost)
			{
				using (StreamReader streamReader = new StreamReader(mapPath, Encoding.UTF8))
				{
					this.content = streamReader.ReadToEnd();
					streamReader.Close();
				}
				this.content = HttpUtility.HtmlEncode(this.content);
				return;
			}
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
			base.Response.Redirect("sysfileedit.aspx?path=" + this.filepath);
		}

		// Token: 0x0400003E RID: 62
		protected string filepath = FPRequest.GetString("path");

		// Token: 0x0400003F RID: 63
		protected string m_path = "";

		// Token: 0x04000040 RID: 64
		protected string filename = "";

		// Token: 0x04000041 RID: 65
		protected string content = "";
	}
}
