using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000059 RID: 89
	public class msgtemplatemanage : SuperController
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x00010C10 File Offset: 0x0000EE10
		protected override void Controller()
		{
			if (this.ispost)
			{
				DbHelper.ExecuteDelete<MsgTempInfo>(FPRequest.GetString("chkid"));
			}
			if (this.type == 1)
			{
				this.pagenav = "邮件模板管理";
			}
			else if (this.type == 2)
			{
				this.pagenav = "短信模板管理";
			}
			else
			{
				this.pagenav = "信息模板管理";
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("type", this.type);
			this.msgtemplatelist = DbHelper.ExecuteList<MsgTempInfo>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x040000F8 RID: 248
		protected int type = FPRequest.GetInt("type");

		// Token: 0x040000F9 RID: 249
		protected List<MsgTempInfo> msgtemplatelist = new List<MsgTempInfo>();
	}
}
