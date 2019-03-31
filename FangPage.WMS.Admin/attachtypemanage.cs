using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000008 RID: 8
	public class attachtypemanage : SuperController
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002B70 File Offset: 0x00000D70
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<AttachType>(FPRequest.GetString("chkdel"));
					FPCache.Remove("FP_ATTACHTYPE");
				}
				else if (this.action == "add")
				{
					AttachType model = FPRequest.GetModel<AttachType>();
					model.extension = model.extension.ToLower();
					if (model.extension == "")
					{
						this.ShowErr("上传附件类型后辍名不能为空。");
						return;
					}
					if (model.maxsize == 0)
					{
						model.maxsize = 2048;
					}
					if (model.extension.StartsWith(".") && model.extension.Length >= 1)
					{
						model.extension = model.extension.Substring(1, model.extension.Length - 1);
					}
					DbHelper.ExecuteInsert<AttachType>(model);
					FPCache.Remove("FP_ATTACHTYPE");
				}
				else if (this.action == "edit")
				{
					AttachType attachType = DbHelper.ExecuteModel<AttachType>(this.id);
					attachType.extension = attachType.extension.ToLower();
					attachType = FPRequest.GetModel<AttachType>(attachType, "edit_");
					if (attachType.extension == "")
					{
						this.ShowErr("上传附件类型后辍名不能为空。");
						return;
					}
					if (attachType.maxsize == 0)
					{
						attachType.maxsize = 2048;
					}
					if (attachType.extension.StartsWith(".") && attachType.extension.Length >= 1)
					{
						attachType.extension = attachType.extension.Substring(1, attachType.extension.Length - 1);
					}
					DbHelper.ExecuteUpdate<AttachType>(attachType);
					FPCache.Remove("FP_ATTACHTYPE");
				}
				base.Response.Redirect(this.pagename);
			}
			SqlParam sqlParam = DbHelper.MakeOrderBy("id", OrderBy.ASC);
			this.attachlist = DbHelper.ExecuteList<AttachType>(new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002D5C File Offset: 0x00000F5C
		protected string GetFileTypeIco(string type)
		{
			if (type.StartsWith("."))
			{
				type = type.Substring(1, type.Length - 1);
			}
			string text = this.webpath + "common/file/" + type + ".gif";
			if (File.Exists(FPFile.GetMapPath(text)))
			{
				return text;
			}
			text = this.webpath + "common/file/" + type + ".png";
			if (File.Exists(FPFile.GetMapPath(text)))
			{
				return text;
			}
			return this.webpath + "common/file/unknow.gif";
		}

		// Token: 0x04000011 RID: 17
		protected List<AttachType> attachlist = new List<AttachType>();

		// Token: 0x04000012 RID: 18
		protected int id = FPRequest.GetInt("id");
	}
}
