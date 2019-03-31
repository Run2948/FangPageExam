using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002D RID: 45
	public class extend_add : SuperController
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00007EA0 File Offset: 0x000060A0
		protected override void Controller()
		{
			string mapPath = FPFile.GetMapPath(this.webpath + "config/user_extend.config");
			this.extendlist = FPXml.LoadList<UserExtend>(mapPath);
			if (this.id < this.extendlist.Count + 1 && this.id > 0)
			{
				this.extendinfo = this.extendlist[this.id - 1];
			}
			if (this.ispost)
			{
				if (this.id > 0)
				{
					this.extendlist[this.id - 1] = FPRequest.GetModel<UserExtend>(this.extendinfo);
				}
				else
				{
					this.extendinfo = FPRequest.GetModel<UserExtend>(this.extendinfo);
					this.extendlist.Add(this.extendinfo);
				}
				FPXml.SaveXml<UserExtend>(this.extendlist, mapPath);
				FPResponse.Redirect("extend.aspx");
			}
		}

		// Token: 0x04000063 RID: 99
		protected UserExtend extendinfo = new UserExtend();

		// Token: 0x04000064 RID: 100
		protected List<UserExtend> extendlist = new List<UserExtend>();

		// Token: 0x04000065 RID: 101
		protected int id = FPRequest.GetInt("id");
	}
}
