using System;
using System.IO;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000B RID: 11
	public class desktopadd : SuperController
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000336C File Offset: 0x0000156C
		protected override void View()
		{
			if (this.id > 0)
			{
				this.desktopinfo = DbHelper.ExecuteModel<DesktopInfo>(this.id);
			}
			if (this.desktopinfo.system == 1 && !this.isperm)
			{
				this.ShowErr("对不起，您没有权限更改系统图标");
			}
			else
			{
				if (this.ispost)
				{
					this.desktopinfo = FPRequest.GetModel<DesktopInfo>();
					this.desktopinfo.system = (this.isperm ? FPRequest.GetInt("system") : 0);
					if (this.desktopinfo.id == 0)
					{
						this.desktopinfo.uid = this.userid;
					}
					if (this.isfile)
					{
						HttpPostedFile postedFile = FPRequest.Files["uploadicon"];
						UpLoad upLoad = new UpLoad();
						string json = upLoad.FileSaveAs(postedFile, "image", this.user, false, false, 32, 32);
						JsonData jsonData = JsonMapper.ToObject(json);
						if (jsonData["error"].ToString() == "")
						{
							if (this.desktopinfo.icon != "")
							{
								if (File.Exists(FPUtils.GetMapPath(this.desktopinfo.icon)))
								{
									File.Delete(FPUtils.GetMapPath(this.desktopinfo.icon));
								}
							}
							this.desktopinfo.icon = jsonData["filename"].ToString();
						}
					}
					if (this.desktopinfo.id > 0)
					{
						DbHelper.ExecuteUpdate<DesktopInfo>(this.desktopinfo);
					}
					else
					{
						DbHelper.ExecuteInsert<DesktopInfo>(this.desktopinfo);
					}
					base.Response.Redirect("desktopmanage.aspx");
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x04000011 RID: 17
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000012 RID: 18
		protected DesktopInfo desktopinfo = new DesktopInfo();
	}
}
