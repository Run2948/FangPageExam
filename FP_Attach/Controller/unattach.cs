using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Model;

namespace FP_Attach.Controller
{
	// Token: 0x0200000B RID: 11
	public class unattach : APIController
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002FA8 File Offset: 0x000011A8
		protected override void Controller()
		{
			List<object> list = new List<object>();
			AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(this.aid);
			if (attachInfo.id == 0)
			{
				base.WriteErr("该附件已被删除或不存在。");
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(attachInfo.filename);
			string text = FPFile.GetMapPath(this.webpath + "cache/attach/" + fileNameWithoutExtension);
			string extension = Path.GetExtension(attachInfo.filename);
			if (extension != ".zip" && extension != ".rar")
			{
				base.WriteErr("该附件不是压缩文件类型。");
			}
			if (!Directory.Exists(text))
			{
				FPFile.CreateDir(text);
				if (extension == ".zip")
				{
					FPZip.UnZip(FPFile.GetMapPath(attachInfo.filename), text);
				}
				else if (extension == ".rar")
				{
					FPZip.UnRar(FPFile.GetMapPath(attachInfo.filename), text);
				}
			}
			if (this.path != "")
			{
				text = FPFile.Combine(text, this.path);
				attachInfo.name = this.path;
			}
			if (!Directory.Exists(text))
			{
				base.WriteErr("该附件文件夹不存在。");
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(text);
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				string text2;
				if (this.path != "")
				{
					text2 = this.path + "\\" + directoryInfo2.Name;
				}
				else
				{
					text2 = directoryInfo2.Name;
				}
				var item = new
				{
					filetype = "folder",
					name = directoryInfo2.Name,
					aid = this.aid,
					path = FPUtils.UrlEncode(text2)
				};
				list.Add(item);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				string text3;
				if (this.path != "")
				{
					text3 = string.Concat(new string[]
					{
						this.webpath,
						"cache/attach/",
						fileNameWithoutExtension,
						"/",
						this.path,
						"/",
						fileInfo.Name
					});
				}
				else
				{
					text3 = string.Concat(new string[]
					{
						this.webpath,
						"cache/attach/",
						fileNameWithoutExtension,
						"/",
						fileInfo.Name
					});
				}
				var item2 = new
				{
					filetype = fileInfo.Extension.Replace(".", ""),
					name = fileInfo.Name,
					aid = this.aid,
					path = FPUtils.UrlEncode(text3)
				};
				list.Add(item2);
			}
			FPResponse.WriteJson(new
			{
				errcode = 0,
				errmsg = "",
				name = attachInfo.name,
				filelist = list
			});
		}

		// Token: 0x0400001C RID: 28
		protected int aid = FPRequest.GetInt("aid");

		// Token: 0x0400001D RID: 29
		protected string path = FPRequest.GetString("path");
	}
}
