using System;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000029 RID: 41
	public class avatarimport : SuperController
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000077F0 File Offset: 0x000059F0
		protected override void Controller()
		{
			if (this.ispost)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(FPFile.GetMapPath(this.webpath + "avatar"));
				if (!directoryInfo.Exists)
				{
					this.ShowErr("站点目录下没有发现有【avatar】文件夹，请先上传照片。");
					return;
				}
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (fileInfo != null && FPArray.InArray(fileInfo.Extension, ".jpg|.gif|.png", "|") != -1)
					{
						string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileInfo.Name);
						UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
						{
							DbHelper.MakeOrWhere("username", fileNameWithoutExtension),
							DbHelper.MakeOrWhere("realname", fileNameWithoutExtension)
						});
						if (userInfo.id > 0)
						{
							string text = FPFile.GetMapPath(this.webpath + "common/avatar");
							if (!Directory.Exists(text))
							{
								Directory.CreateDirectory(text);
							}
							text = text + "/" + userInfo.id.ToString() + fileInfo.Extension;
							FPThumb.MakeThumbnailImage(fileInfo.FullName, text, 150, 150);
							userInfo.avatar = "common/avatar/" + userInfo.id.ToString() + fileInfo.Extension;
							DbHelper.ExecuteUpdate<UserInfo>(new SqlParam[]
							{
								DbHelper.MakeUpdate("avatar", userInfo.avatar),
								DbHelper.MakeAndWhere("id", userInfo.id)
							});
						}
					}
				}
				base.AddMsg("导入用户照片成功！");
			}
		}
	}
}
