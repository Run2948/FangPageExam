using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using FangPage.Common;
using FangPage.MVC;

namespace FP_Code.Controller
{
	// Token: 0x02000002 RID: 2
	public class barcode : Page
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			int @int = FPRequest.GetInt("width");
			int int2 = FPRequest.GetInt("height");
			string @string = FPRequest.GetString("content");
			string string2 = FPRequest.GetString("type");
			Image image = FPCode.CreateBarCode(@string, string2, @int, int2);
			MemoryStream memoryStream = new MemoryStream();
			image.Save(memoryStream, ImageFormat.Png);
			byte[] buffer = memoryStream.ToArray();
			new MemoryStream();
			base.Response.ClearContent();
			base.Response.ContentType = "image/png";
			base.Response.BinaryWrite(buffer);
		}
	}
}
