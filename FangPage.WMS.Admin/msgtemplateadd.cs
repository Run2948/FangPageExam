using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003E RID: 62
	public class msgtemplateadd : SuperController
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000CBC0 File Offset: 0x0000ADC0
		protected override void View()
		{
			if (this.id > 0)
			{
				this.msgtemplateinfo = DbHelper.ExecuteModel<MsgTempInfo>(this.id);
				if (this.msgtemplateinfo.id == 0)
				{
					this.ShowErr("对不起，该模板不存在或已被删除。");
					return;
				}
				this.type = this.msgtemplateinfo.type;
			}
			if (this.ispost)
			{
				this.msgtemplateinfo = FPRequest.GetModel<MsgTempInfo>(this.msgtemplateinfo);
				if (this.msgtemplateinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<MsgTempInfo>(this.msgtemplateinfo);
				}
				else
				{
					DbHelper.ExecuteInsert<MsgTempInfo>(this.msgtemplateinfo);
				}
				base.Response.Redirect("msgtemplatemanage.aspx?type=" + this.type);
			}
			base.SaveRightURL();
		}

		// Token: 0x04000097 RID: 151
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000098 RID: 152
		protected int type = FPRequest.GetInt("type");

		// Token: 0x04000099 RID: 153
		protected MsgTempInfo msgtemplateinfo = new MsgTempInfo();
	}
}
