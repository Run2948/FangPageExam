using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000028 RID: 40
	public class siteadd : SuperController
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000084C4 File Offset: 0x000066C4
		protected override void View()
		{
			if (this.m_sitepath != "")
			{
				this.siteinfo = SiteBll.GetSiteConfig(this.m_sitepath);
			}
			if (this.siteinfo.version == "")
			{
				this.siteinfo.version = "V1.0";
			}
			if (this.ispost)
			{
				string text = FPRequest.GetString("dirpath").ToLower();
				if (text.Trim() == "")
				{
					base.AddErr("站点路径名称不能为空。");
				}
				string pattern = "^[a-zA-Z0-9_\\w]+$";
				if (this.err == 0 && !Regex.IsMatch(text.Trim(), pattern, RegexOptions.IgnoreCase))
				{
					base.AddErr("站点路径名称只能由数字、字母或下划线组成。");
				}
				if (this.err > 0)
				{
					if (!this.iscuserr)
					{
						MessageBox.Show(this.msg);
					}
					return;
				}
				this.siteinfo.roles = "";
				this.siteinfo = FPRequest.GetModel<SiteConfig>(this.siteinfo);
				if (this.siteinfo.sitepath != text)
				{
					if (Directory.Exists(FPUtils.GetMapPath(this.webpath + "sites/" + text)))
					{
						this.ShowErr("该站点路径已存在，请使用其他的名称");
						return;
					}
				}
				if (this.m_sitepath == "")
				{
					this.siteinfo.sitepath = text;
				}
				if (this.siteinfo.sitetitle == "")
				{
					this.siteinfo.sitetitle = this.siteinfo.name;
				}
				SiteBll.SaveSiteConfig(this.siteinfo);
				if (this.siteinfo.sitepath != text)
				{
					Directory.Move(FPUtils.GetMapPath(this.webpath + "sites/" + this.siteinfo.sitepath), FPUtils.GetMapPath(this.webpath + "sites/" + text));
				}
				if (this.tab == 0)
				{
					base.Response.Redirect("sitemanage.aspx");
				}
				else
				{
					base.Response.Redirect(string.Concat(new object[]
					{
						"siteadd.aspx?sitepath=",
						this.siteinfo.sitepath,
						"&tab=",
						this.tab
					}));
				}
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.NotIn, "2,3,4");
			this.rolelist = DbHelper.ExecuteList<RoleInfo>(new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x0400004E RID: 78
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x0400004F RID: 79
		protected SiteConfig siteinfo = new SiteConfig();

		// Token: 0x04000050 RID: 80
		protected string sysversion = "4.7";

		// Token: 0x04000051 RID: 81
		protected int tab = FPRequest.GetInt("tab");

		// Token: 0x04000052 RID: 82
		protected List<RoleInfo> rolelist = new List<RoleInfo>();
	}
}
