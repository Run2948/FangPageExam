using System;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000005 RID: 5
	public class runsql : SuperController
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002934 File Offset: 0x00000B34
		protected override void View()
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
			base.SaveRightURL();
		}

		// Token: 0x04000006 RID: 6
		protected string sqlstring = FPRequest.GetString("sqlstring");
	}
}
