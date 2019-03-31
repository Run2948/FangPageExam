using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace FangPage.Common
{
	// Token: 0x02000016 RID: 22
	public class FPCode
	{
		// Token: 0x06000123 RID: 291 RVA: 0x00008208 File Offset: 0x00006408
		public static Image CreateBarCode(string source, int width, int height)
		{
			return FPCode.CreateBarCode(source, "CODE128", width, height);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00008228 File Offset: 0x00006428
		public static Image CreateBarCode(string source, string type, int width, int height)
		{
			bool flag = width == 0 && !string.IsNullOrEmpty(source);
			if (flag)
			{
				width = source.Length;
			}
			bool flag2 = width == 0;
			if (flag2)
			{
				width = 100;
			}
			bool flag3 = height == 0;
			if (flag3)
			{
				height = 80;
			}
			bool flag4 = string.IsNullOrEmpty(source);
			Image result;
			if (flag4)
			{
				result = new Bitmap(width, height);
			}
			else
			{
				BarcodeFormat format = BarcodeFormat.CODE_128;
				bool flag5 = type.ToUpper() == "EAN13";
				if (flag5)
				{
					format = BarcodeFormat.EAN_13;
				}
				else
				{
					bool flag6 = type.ToLower() == "EAN8";
					if (flag6)
					{
						format = BarcodeFormat.EAN_8;
					}
				}
				EncodingOptions encodingOptions = new EncodingOptions();
				encodingOptions.Height = height;
				encodingOptions.Width = width;
				Image image = new BarcodeWriter
				{
					Options = encodingOptions,
					Format = format
				}.Write(source);
				result = image;
			}
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008308 File Offset: 0x00006508
		public static Image CreateQRCode(string source, int width, int height, int margin)
		{
			bool flag = width == 0 && height > 0;
			if (flag)
			{
				width = height;
			}
			else
			{
				bool flag2 = width > 0 && height == 0;
				if (flag2)
				{
					height = width;
				}
				else
				{
					width = 200;
					height = 200;
				}
			}
			bool flag3 = string.IsNullOrEmpty(source);
			Image result;
			if (flag3)
			{
				result = new Bitmap(width, height);
			}
			else
			{
				QrCodeEncodingOptions qrCodeEncodingOptions = new QrCodeEncodingOptions();
				qrCodeEncodingOptions.CharacterSet = "UTF-8";
				qrCodeEncodingOptions.Height = height;
				qrCodeEncodingOptions.Width = width;
				qrCodeEncodingOptions.Margin = margin;
				Image image = new BarcodeWriter
				{
					Format = BarcodeFormat.QR_CODE,
					Options = qrCodeEncodingOptions
				}.Write(source);
				result = image;
			}
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000083C0 File Offset: 0x000065C0
		public static Image CreateQRCode(string source, string logo, int width, int height, int margin, int logoop)
		{
			bool flag = width == 0 && height > 0;
			if (flag)
			{
				width = height;
			}
			else
			{
				bool flag2 = width > 0 && height == 0;
				if (flag2)
				{
					height = width;
				}
				else
				{
					width = 200;
					height = 200;
				}
			}
			bool flag3 = logoop == 0;
			if (flag3)
			{
				logoop = 55;
			}
			bool flag4 = margin < 0;
			if (flag4)
			{
				margin = 1;
			}
			bool flag5 = string.IsNullOrEmpty(source);
			Image result;
			if (flag5)
			{
				result = new Bitmap(width, height);
			}
			else
			{
				QrCodeEncodingOptions qrCodeEncodingOptions = new QrCodeEncodingOptions();
				qrCodeEncodingOptions.CharacterSet = "UTF-8";
				qrCodeEncodingOptions.Height = height;
				qrCodeEncodingOptions.Width = width;
				qrCodeEncodingOptions.Margin = margin;
				Bitmap bitmap = new BarcodeWriter
				{
					Format = BarcodeFormat.QR_CODE,
					Options = qrCodeEncodingOptions
				}.Write(source);
				bool flag6 = File.Exists(logo);
				if (flag6)
				{
					Bitmap bitmap2 = Image.FromFile(logo) as Bitmap;
					int num = 2;
					float horizontalResolution = bitmap.HorizontalResolution;
					float verticalResolution = bitmap.VerticalResolution;
					float num2 = (float)(10 * bitmap.Width - 46 * num) * 1f / (float)logoop;
					Image image = FPCode.ZoomPic(bitmap2, (double)(num2 / (float)bitmap2.Width));
					int num3 = image.Width + num;
					Bitmap bitmap3 = new Bitmap(num3, num3);
					bitmap3.MakeTransparent();
					Graphics graphics = Graphics.FromImage(bitmap3);
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.SmoothingMode = SmoothingMode.HighQuality;
					graphics.Clear(Color.Transparent);
					Pen pen = new Pen(new SolidBrush(Color.White));
					Rectangle rect = new Rectangle(0, 0, num3 - 1, num3 - 1);
					using (GraphicsPath graphicsPath = FPCode.CreateRoundedRectanglePath(rect, 7))
					{
						graphics.DrawPath(pen, graphicsPath);
						graphics.FillPath(new SolidBrush(Color.White), graphicsPath);
					}
					Bitmap image2 = new Bitmap(image.Width, image.Width);
					Graphics graphics2 = Graphics.FromImage(image2);
					graphics2.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics2.SmoothingMode = SmoothingMode.HighQuality;
					graphics2.Clear(Color.Transparent);
					Pen pen2 = new Pen(new SolidBrush(Color.Gray));
					Rectangle rect2 = new Rectangle(0, 0, image.Width - 1, image.Width - 1);
					using (GraphicsPath graphicsPath2 = FPCode.CreateRoundedRectanglePath(rect2, 7))
					{
						graphics2.DrawPath(pen2, graphicsPath2);
						TextureBrush brush = new TextureBrush(image);
						graphics2.FillPath(brush, graphicsPath2);
					}
					graphics2.Dispose();
					PointF pointF = new PointF((float)((num3 - image.Width) / 2), (float)((num3 - image.Height) / 2));
					graphics.DrawImage(image2, pointF.X, pointF.Y, (float)image.Width, (float)image.Height);
					graphics.Dispose();
					Bitmap bitmap4 = new Bitmap(bitmap.Width, bitmap.Height);
					bitmap4.MakeTransparent();
					bitmap4.SetResolution(horizontalResolution, verticalResolution);
					bitmap3.SetResolution(horizontalResolution, verticalResolution);
					Graphics graphics3 = Graphics.FromImage(bitmap4);
					graphics3.Clear(Color.Transparent);
					graphics3.DrawImage(bitmap, 0, 0);
					PointF point = new PointF((float)((bitmap.Width - bitmap3.Width) / 2), (float)((bitmap.Height - bitmap3.Height) / 2));
					graphics3.DrawImage(bitmap3, point);
					graphics3.Dispose();
					result = bitmap4;
				}
				else
				{
					result = bitmap;
				}
			}
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008754 File Offset: 0x00006954
		public static string ReadBarCode(Image img)
		{
			DecodingOptions decodingOptions = new DecodingOptions();
			decodingOptions.PossibleFormats = new List<BarcodeFormat>
			{
				BarcodeFormat.EAN_13
			};
			Result result = new BarcodeReader
			{
				Options = decodingOptions
			}.Decode(img as Bitmap);
			bool flag = result == null;
			string result2;
			if (flag)
			{
				result2 = "";
			}
			else
			{
				result2 = result.Text;
			}
			return result2;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000087BC File Offset: 0x000069BC
		public static string ReadBarCode(Bitmap img)
		{
			DecodingOptions decodingOptions = new DecodingOptions();
			decodingOptions.PossibleFormats = new List<BarcodeFormat>
			{
				BarcodeFormat.EAN_13
			};
			Result result = new BarcodeReader
			{
				Options = decodingOptions
			}.Decode(img);
			bool flag = result == null;
			string result2;
			if (flag)
			{
				result2 = "";
			}
			else
			{
				result2 = result.Text;
			}
			return result2;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008820 File Offset: 0x00006A20
		public static string ReadBarCode(string imgpath)
		{
			bool flag = !File.Exists(imgpath);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Bitmap barcodeBitmap = new Bitmap(imgpath);
				DecodingOptions decodingOptions = new DecodingOptions();
				decodingOptions.PossibleFormats = new List<BarcodeFormat>
				{
					BarcodeFormat.EAN_13
				};
				Result result2 = new BarcodeReader
				{
					Options = decodingOptions
				}.Decode(barcodeBitmap);
				bool flag2 = result2 == null;
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = result2.Text;
				}
			}
			return result;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000088A4 File Offset: 0x00006AA4
		public static string ReadQRCode(Image img)
		{
			DecodingOptions decodingOptions = new DecodingOptions();
			decodingOptions.PossibleFormats = new List<BarcodeFormat>
			{
				BarcodeFormat.QR_CODE
			};
			Result result = new BarcodeReader
			{
				Options = decodingOptions
			}.Decode(img as Bitmap);
			bool flag = result == null;
			string result2;
			if (flag)
			{
				result2 = "";
			}
			else
			{
				result2 = result.Text;
			}
			return result2;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000890C File Offset: 0x00006B0C
		public static string ReadQRCode(Bitmap img)
		{
			DecodingOptions decodingOptions = new DecodingOptions();
			decodingOptions.PossibleFormats = new List<BarcodeFormat>
			{
				BarcodeFormat.QR_CODE
			};
			Result result = new BarcodeReader
			{
				Options = decodingOptions
			}.Decode(img);
			bool flag = result == null;
			string result2;
			if (flag)
			{
				result2 = "";
			}
			else
			{
				result2 = result.Text;
			}
			return result2;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008970 File Offset: 0x00006B70
		public static string ReadQRCode(string imgpath)
		{
			bool flag = !File.Exists(imgpath);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				Bitmap barcodeBitmap = new Bitmap(imgpath);
				DecodingOptions decodingOptions = new DecodingOptions();
				decodingOptions.PossibleFormats = new List<BarcodeFormat>
				{
					BarcodeFormat.QR_CODE
				};
				Result result2 = new BarcodeReader
				{
					Options = decodingOptions
				}.Decode(barcodeBitmap);
				bool flag2 = result2 == null;
				if (flag2)
				{
					result = "";
				}
				else
				{
					result = result2.Text;
				}
			}
			return result;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000089F4 File Offset: 0x00006BF4
		private static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180f, 90f);
			graphicsPath.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
			graphicsPath.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270f, 90f);
			graphicsPath.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
			graphicsPath.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0f, 90f);
			graphicsPath.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
			graphicsPath.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90f, 90f);
			graphicsPath.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008B84 File Offset: 0x00006D84
		private static Image ZoomPic(Image initImage, double n)
		{
			double num = (double)initImage.Width;
			double num2 = (double)initImage.Height;
			num = n * (double)initImage.Width;
			num2 = n * (double)initImage.Height;
			Image image = new Bitmap((int)num, (int)num2);
			Graphics graphics = Graphics.FromImage(image);
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.Clear(Color.Transparent);
			graphics.DrawImage(initImage, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, initImage.Width, initImage.Height), GraphicsUnit.Pixel);
			graphics.Dispose();
			return image;
		}
	}
}
