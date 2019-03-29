using System;
using System.Collections.Generic;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000008 RID: 8
	public class attachmanage : SuperController
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002F40 File Offset: 0x00001140
		protected override void View()
		{
			if (this.ispost)
			{
				if (this.action == "delete")
				{
					string @string = FPRequest.GetString("chkdel");
					foreach (string text in @string.Split(new char[]
					{
						','
					}))
					{
						AttachInfo attachInfo = DbHelper.ExecuteModel<AttachInfo>(int.Parse(text));
						if (DbHelper.ExecuteDelete<AttachInfo>(text) > 0)
						{
							if (File.Exists(FPUtils.GetMapPath(attachInfo.filename)))
							{
								File.Delete(FPUtils.GetMapPath(attachInfo.filename));
							}
						}
					}
				}
				base.Response.Redirect(this.pagename);
			}
			this.attachlist = DbHelper.ExecuteList<AttachInfo>(this.pager);
			base.SaveRightURL();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003034 File Offset: 0x00001234
		protected string FormatSize(long filesize)
		{
			return FPUtils.FormatBytesStr(filesize);
		}

		// Token: 0x0400000C RID: 12
		protected List<AttachInfo> attachlist = new List<AttachInfo>();

		// Token: 0x0400000D RID: 13
		protected int id = FPRequest.GetInt("id");

		// Token: 0x0400000E RID: 14
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
