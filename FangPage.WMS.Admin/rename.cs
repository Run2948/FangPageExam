using System;
using System.Collections;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000011 RID: 17
	public class rename : SuperController
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00004224 File Offset: 0x00002424
		protected override void Controller()
		{
			if (this.filename == "")
			{
				this.ShowErrMsg("原文件名不能为空。");
			}
			if (this.newfilename == "")
			{
				this.ShowErrMsg("文件名不能为空。");
			}
			string mapPath = FPFile.GetMapPath(this.filename);
			string destDirName = Path.GetDirectoryName(mapPath) + "\\" + this.newfilename;
			string value = "";
			try
			{
				new DirectoryInfo(mapPath).MoveTo(destDirName);
			}
			catch
			{
				value = "文件更名错误，请检查目录是否有读写权限。";
			}
			Hashtable hashtable = new Hashtable();
			hashtable["message"] = value;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(FPJson.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004300 File Offset: 0x00002500
		protected void ShowErrMsg(string content)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["message"] = content;
			base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
			base.Response.Write(FPJson.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x04000028 RID: 40
		protected string newfilename = FPRequest.GetString("newfilename");

		// Token: 0x04000029 RID: 41
		protected string filename = FPRequest.GetString("filename");
	}
}
