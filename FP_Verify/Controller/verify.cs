using System;
using System.IO;
using System.Web.UI;

namespace FP_Verify.Controller
{
	// Token: 0x02000005 RID: 5
	public class verify : Page
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000025EC File Offset: 0x000007EC
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			FPVCode fpvcode = new FPVCode();
			string text = fpvcode.CreateValidateCode(4);
			this.Session["FP_VERIFY"] = text;
			byte[] buffer = fpvcode.CreateValidateGraphic(text);
			new MemoryStream();
			base.Response.ClearContent();
			base.Response.ContentType = "image/Gif";
			base.Response.BinaryWrite(buffer);
		}
	}
}
