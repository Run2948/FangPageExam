using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000005 RID: 5
	public class runsql : SuperController
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002750 File Offset: 0x00000950
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (this.sqlstring.Trim() == "")
				{
					this.ShowErr("请输入SQL语句");
					return;
				}
				string text = DbHelper.ExecuteSql(this.sqlstring);
				if (text != string.Empty)
				{
					this.ShowErr(text);
					return;
				}
			}
		}

		// Token: 0x04000007 RID: 7
		protected string sqlstring = FPRequest.GetString("sqlstring");
	}
}
