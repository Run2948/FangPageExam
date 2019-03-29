using System;
using System.IO;
using System.Web.UI;
using FangPage.MVC;

namespace FangPage.WMS.Tools
{
	// Token: 0x02000048 RID: 72
	public class verify : Page
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000BB78 File Offset: 0x00009D78
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			FPVCode fpvcode = new FPVCode();
			string text = fpvcode.CreateValidateCode(4);
			this.Session["FP_VERIFY"] = text;
			byte[] buffer = fpvcode.CreateValidateGraphic(text);
			MemoryStream memoryStream = new MemoryStream();
			base.Response.ClearContent();
			base.Response.ContentType = "image/Gif";
			base.Response.BinaryWrite(buffer);
		}
	}
}
