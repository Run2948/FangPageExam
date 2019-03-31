using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002C RID: 44
	public class extend : SuperController
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00007E10 File Offset: 0x00006010
		protected override void Controller()
		{
			string mapPath = FPFile.GetMapPath(this.webpath + "config/user_extend.config");
			this.extendlist = FPXml.LoadList<UserExtend>(mapPath);
			if (this.ispost)
			{
				int @int = FPRequest.GetInt("chkid");
				if (@int > 0 && @int < this.extendlist.Count + 1)
				{
					this.extendlist.RemoveAt(@int - 1);
				}
				FPXml.SaveXml<UserExtend>(this.extendlist, mapPath);
			}
			FPXml.SaveXml<UserExtend>(this.extendlist, mapPath);
		}

		// Token: 0x04000062 RID: 98
		protected List<UserExtend> extendlist = new List<UserExtend>();
	}
}
