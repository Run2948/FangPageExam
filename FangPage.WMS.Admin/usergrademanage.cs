using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200004E RID: 78
	public class usergrademanage : SuperController
	{
		// Token: 0x060000BC RID: 188 RVA: 0x0000E600 File Offset: 0x0000C800
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					DbHelper.ExecuteDelete<UserGrade>(FPRequest.GetString("chkdel"));
				}
				else if (this.action == "reset")
				{
					this.gradelist = DbHelper.ExecuteList<UserGrade>(OrderBy.ASC);
					string text = "";
					foreach (UserGrade userGrade in this.gradelist)
					{
						if (text != "")
						{
							text += "|";
						}
						text += string.Format("UPDATE [{0}WMS_UserInfo] SET [gradeid]={1} WHERE [exp]>={2} AND [exp]<{3}", new object[]
						{
							DbConfigs.Prefix,
							userGrade.id,
							userGrade.explower,
							userGrade.expupper
						});
					}
					if (text != "")
					{
						text = DbHelper.ExecuteSql(text);
						if (text != "")
						{
							this.ShowErr(text);
							return;
						}
					}
					base.ResetUser();
					base.Response.Redirect(this.pagename);
				}
			}
			this.gradelist = DbHelper.ExecuteList<UserGrade>(OrderBy.ASC);
			base.SaveRightURL();
		}

		// Token: 0x040000BF RID: 191
		protected List<UserGrade> gradelist = new List<UserGrade>();
	}
}
