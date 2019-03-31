using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000049 RID: 73
	public class msgtemplateadd : SuperController
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		protected override void Controller()
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
		}

		// Token: 0x040000D0 RID: 208
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000D1 RID: 209
		protected int type = FPRequest.GetInt("type");

		// Token: 0x040000D2 RID: 210
		protected MsgTempInfo msgtemplateinfo = new MsgTempInfo();
	}
}
