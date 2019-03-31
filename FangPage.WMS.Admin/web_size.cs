using System;
using System.Collections;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000026 RID: 38
	public class web_size : AdminController
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00007648 File Offset: 0x00005848
		protected override void Controller()
		{
			string value = FPFile.FormatBytesStr(DbBll.GetDbSize());
			Hashtable hashtable = new Hashtable();
			hashtable["dbsize"] = value;
			FPResponse.WriteJson(hashtable);
		}
	}
}
