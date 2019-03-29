using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000050 RID: 80
	public class msgtemplatemanage : SuperController
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000EC08 File Offset: 0x0000CE08
		protected override void View()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkid");
				DbHelper.ExecuteDelete<MsgTempInfo>(@string);
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
			base.SaveRightURL();
		}

		// Token: 0x040000C1 RID: 193
		protected int type = FPRequest.GetInt("type");

		// Token: 0x040000C2 RID: 194
		protected List<MsgTempInfo> msgtemplatelist = new List<MsgTempInfo>();
	}
}
