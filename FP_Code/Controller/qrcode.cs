using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using FangPage.Common;
using FangPage.MVC;

namespace FP_Code.Controller
{
	// Token: 0x02000003 RID: 3
	public class qrcode : Page
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020E8 File Offset: 0x000002E8
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			int @int = FPRequest.GetInt("width");
			int int2 = FPRequest.GetInt("height");
			string @string = FPRequest.GetString("content");
			string mapPath = FPFile.GetMapPath(FPRequest.GetString("logo"));
			int int3 = FPRequest.GetInt("margin", -1);
			int int4 = FPRequest.GetInt("logoop");
			Image image;
			if (@string != "")
			{
				image = FPCode.CreateQRCode(@string, mapPath, @int, int2, int3, int4);
			}
			else
			{
				image = new Bitmap(200, 200);
			}
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
