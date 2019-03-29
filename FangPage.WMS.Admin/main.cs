using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000021 RID: 33
	public class main : AdminController
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00006AA4 File Offset: 0x00004CA4
		protected override void View()
		{
			long sysSize = SysBll.GetSysSize();
			this.websize = FPUtils.FormatBytesStr(sysSize);
			this.dbconfig = DbConfigs.GetDbConfig();
			this.dbsize = FPUtils.FormatBytesStr(DbBll.GetDbSize());
			if (this.role.desktop == "")
			{
				this.role.desktop = "0";
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere(string.Format("([hidden]=0 AND [uid]={0}) OR ([hidden]=0 AND [system]=1 AND [id] IN({1}))", this.userid, this.role.desktop), WhereType.Custom, "");
			this.desktoplist = DbHelper.ExecuteList<DesktopInfo>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL(this.pagename);
		}

		// Token: 0x0400003D RID: 61
		protected List<DesktopInfo> desktoplist = new List<DesktopInfo>();

		// Token: 0x0400003E RID: 62
		protected internal string websize;

		// Token: 0x0400003F RID: 63
		protected internal string dbsize;

		// Token: 0x04000040 RID: 64
		protected DbConfigInfo dbconfig = new DbConfigInfo();

		// Token: 0x04000041 RID: 65
		protected internal string linkurl;

		// Token: 0x04000042 RID: 66
		protected internal bool islink;
	}
}
