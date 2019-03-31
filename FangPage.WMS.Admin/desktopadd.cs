using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200000B RID: 11
	public class desktopadd : SuperController
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00003214 File Offset: 0x00001414
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.desktopinfo = DbHelper.ExecuteModel<DesktopInfo>(this.id);
				this.parentid = this.desktopinfo.parentid;
			}
			else
			{
				this.desktopinfo.platform = this.platform;
				if (this.parentid > 0)
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", this.parentid);
					int desk = FPUtils.StrToInt(DbHelper.ExecuteMax<DesktopInfo>("desk", new SqlParam[]
					{
						sqlParam
					}));
					this.desktopinfo.desk = desk;
				}
			}
			if (this.desktopinfo.attach_icon == "")
			{
				this.desktopinfo.attach_icon = FPRandom.CreateCode(20);
			}
			if (this.ispost)
			{
				this.desktopinfo.hidden = 0;
				this.desktopinfo = FPRequest.GetModel<DesktopInfo>(this.desktopinfo);
				this.desktopinfo.platform = FPRequest.GetString("m_platform");
				if (this.desktopinfo.id == 0)
				{
					this.desktopinfo.uid = this.userid;
				}
				if (this.isfile)
				{
					AttachInfo attachInfo = AttachBll.UploadImg(FPRequest.Files["uploadicon"], this.desktopinfo.attach_icon, this.userid, "admin_desktop", 64, 64);
					if (attachInfo.id > 0)
					{
						this.desktopinfo.icon = attachInfo.filename;
					}
				}
				if (this.desktopinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<DesktopInfo>(this.desktopinfo);
				}
				else
				{
					this.desktopinfo.platform = this.sysconfig.platform;
					SqlParam sqlParam2 = DbHelper.MakeAndWhere("parentid", this.parentid);
					int num = DbHelper.ExecuteCount<DesktopInfo>(new SqlParam[]
					{
						sqlParam2
					});
					this.desktopinfo.display = num + 1;
					DbHelper.ExecuteInsert<DesktopInfo>(this.desktopinfo);
				}
				if (this.desktopinfo.icon != "")
				{
					AttachBll.UpdateOnceAttach(this.desktopinfo.attach_icon, this.desktopinfo.id);
				}
				else
				{
					AttachBll.Delete(this.desktopinfo.attach_icon);
				}
				FPCache.Remove("FP_DESKTOPLIST");
				base.Response.Redirect("desktopmanage.aspx");
			}
			foreach (SiteConfig siteconfig in SiteConfigs.GetSysSiteList())
			{
				this.setuplist.Add(SetupBll.GetSetupInfo(siteconfig));
			}
			foreach (AppConfig appconfig in AppConfigs.GetMapAppList())
			{
				this.setuplist.Add(SetupBll.GetSetupInfo(appconfig));
			}
			this.desklist = DesktopBll.GetDesktopList(0);
		}

		// Token: 0x04000017 RID: 23
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000018 RID: 24
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000019 RID: 25
		protected DesktopInfo desktopinfo = new DesktopInfo();

		// Token: 0x0400001A RID: 26
		protected List<SetupInfo> setuplist = new List<SetupInfo>();

		// Token: 0x0400001B RID: 27
		protected List<DesktopInfo> desklist = new List<DesktopInfo>();
	}
}
