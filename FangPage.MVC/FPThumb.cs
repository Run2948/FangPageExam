using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using FangPage.Common;

namespace FangPage.MVC
{
	// Token: 0x02000010 RID: 16
	public class FPThumb
	{
		// Token: 0x06000109 RID: 265 RVA: 0x0000B914 File Offset: 0x00009B14
		public bool SetImage(string FileName)
		{
			this.srcFileName = FPFile.GetMapPath(FileName);
			try
			{
				this.srcImage = Image.FromFile(this.srcFileName);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000B958 File Offset: 0x00009B58
		public bool ThumbnailCallback()
		{
			return false;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000B95C File Offset: 0x00009B5C
		public static string GetThumbnail(string imgpath, int maxsize)
		{
			int num = maxsize;
			if (num <= 0)
			{
				num = 600;
			}
			string text = FPFile.GetMapPath(WebConfig.WebPath) + imgpath;
			if (!File.Exists(text))
			{
				return "";
			}
			string text2 = Path.GetFileName(imgpath);
			string text3 = Path.GetExtension(imgpath).ToLower();
			if (text3 == ".jpg" || text3 == ".bmp" || text3 == ".png")
			{
				text2 = Path.GetFileNameWithoutExtension(imgpath);
				string text4 = string.Format("{0}cache/thumbnail/{1}_{2}{3}", new object[]
				{
					WebConfig.WebPath,
					text2,
					num,
					text3
				});
				string mapPath = FPFile.GetMapPath(text4);
				if (!File.Exists(mapPath))
				{
					string mapPath2 = FPFile.GetMapPath(WebConfig.WebPath + "cache/thumbnail/");
					if (!Directory.Exists(mapPath2))
					{
						try
						{
							Directory.CreateDirectory(mapPath2);
						}
						catch
						{
							throw new Exception("请检查程序目录下cache文件夹的用户权限！");
						}
					}
					FPThumb.CreateThumbnail(mapPath, text, num);
				}
				return text4;
			}
			if (text3 == ".gif")
			{
				return imgpath;
			}
			return "";
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000BA7C File Offset: 0x00009C7C
		public static void CreateThumbnail(string attPhyCachePath, string attPhyPath, int theMaxsize)
		{
			if (!File.Exists(attPhyPath))
			{
				return;
			}
			try
			{
				FPThumb.MakeThumbnailImage(attPhyPath, attPhyCachePath, theMaxsize, theMaxsize);
			}
			catch
			{
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000BAB4 File Offset: 0x00009CB4
		public Image GetImage(int Width, int Height)
		{
			Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
			return this.srcImage.GetThumbnailImage(Width, Height, callback, IntPtr.Zero);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public void SaveThumbnailImage(int Width, int Height)
		{
			string a = Path.GetExtension(this.srcFileName).ToLower();
			if (a == ".png")
			{
				this.SaveImage(Width, Height, ImageFormat.Png);
				return;
			}
			if (!(a == ".gif"))
			{
				this.SaveImage(Width, Height, ImageFormat.Jpeg);
				return;
			}
			this.SaveImage(Width, Height, ImageFormat.Gif);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000BB48 File Offset: 0x00009D48
		public void SaveImage(int Width, int Height, ImageFormat imgformat)
		{
			if ((imgformat != ImageFormat.Gif && this.srcImage.Width > Width) || this.srcImage.Height > Height)
			{
				Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
				Image thumbnailImage = this.srcImage.GetThumbnailImage(Width, Height, callback, IntPtr.Zero);
				this.srcImage.Dispose();
				thumbnailImage.Save(this.srcFileName, imgformat);
				thumbnailImage.Dispose();
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000BBB8 File Offset: 0x00009DB8
		private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
		{
			EncoderParameters encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
			image.Save(savePath, ici, encoderParameters);
			encoderParameters.Dispose();
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000BBF0 File Offset: 0x00009DF0
		private static ImageCodecInfo GetCodecInfo(string mimeType)
		{
			foreach (ImageCodecInfo imageCodecInfo in ImageCodecInfo.GetImageEncoders())
			{
				if (imageCodecInfo.MimeType == mimeType)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000BC28 File Offset: 0x00009E28
		private static Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
		{
			if (maxWidth <= 0)
			{
				maxWidth = width;
			}
			if (maxHeight <= 0)
			{
				maxHeight = height;
			}
			decimal num = maxWidth;
			decimal d = maxHeight;
			decimal d2 = num / d;
			decimal d3 = width;
			decimal num2 = height;
			int width2;
			int height2;
			if (d3 > num || num2 > d)
			{
				if (d3 / num2 > d2)
				{
					decimal d4 = d3 / num;
					width2 = Convert.ToInt32(d3 / d4);
					height2 = Convert.ToInt32(num2 / d4);
				}
				else
				{
					decimal d4 = num2 / d;
					width2 = Convert.ToInt32(d3 / d4);
					height2 = Convert.ToInt32(num2 / d4);
				}
			}
			else
			{
				width2 = width;
				height2 = height;
			}
			return new Size(width2, height2);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000BCF4 File Offset: 0x00009EF4
		public static ImageFormat GetFormat(string name)
		{
			string a = name.Substring(name.LastIndexOf(".") + 1).ToLower();
			if (a == "jpg" || a == "jpeg")
			{
				return ImageFormat.Jpeg;
			}
			if (a == "bmp")
			{
				return ImageFormat.Bmp;
			}
			if (a == "png")
			{
				return ImageFormat.Png;
			}
			if (!(a == "gif"))
			{
				return ImageFormat.Jpeg;
			}
			return ImageFormat.Gif;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000BD7C File Offset: 0x00009F7C
		public static void MakeSquareImage(Image image, string newFileName, int newSize)
		{
			int width = image.Width;
			int height = image.Height;
			Bitmap bitmap = new Bitmap(newSize, newSize);
			try
			{
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.Clear(Color.Transparent);
				if (width < height)
				{
					graphics.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle(0, (height - width) / 2, width, width), GraphicsUnit.Pixel);
				}
				else
				{
					graphics.DrawImage(image, new Rectangle(0, 0, newSize, newSize), new Rectangle((width - height) / 2, 0, height, height), GraphicsUnit.Pixel);
				}
				FPThumb.SaveImage(bitmap, newFileName, FPThumb.GetCodecInfo("image/" + FPThumb.GetFormat(newFileName).ToString().ToLower()));
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000BE54 File Offset: 0x0000A054
		public static void MakeSquareImage(string fileName, string newFileName, int newSize)
		{
			FPThumb.MakeSquareImage(Image.FromFile(fileName), newFileName, newSize);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000BE64 File Offset: 0x0000A064
		public static void MakeRemoteSquareImage(string url, string newFileName, int newSize)
		{
			Stream remoteImage = FPThumb.GetRemoteImage(url);
			if (remoteImage == null)
			{
				return;
			}
			Image image = Image.FromStream(remoteImage);
			remoteImage.Close();
			FPThumb.MakeSquareImage(image, newFileName, newSize);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000BE94 File Offset: 0x0000A094
		public static void MakeThumbnailImage(Image original, string newFileName, int maxWidth, int maxHeight)
		{
			Size newSize = FPThumb.ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
			using (Image image = new Bitmap(original, newSize))
			{
				try
				{
					image.Save(newFileName, original.RawFormat);
				}
				finally
				{
					original.Dispose();
				}
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
		{
			FPThumb.MakeThumbnailImage(Image.FromStream(new MemoryStream(File.ReadAllBytes(fileName))), newFileName, maxWidth, maxHeight);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000BF18 File Offset: 0x0000A118
		public static void MakeThumbnailImage(string fileName, string newFileName, int width, int height, string mode)
		{
			Image image = Image.FromFile(fileName);
			int num = width;
			int num2 = height;
			int x = 0;
			int y = 0;
			int num3 = image.Width;
			int num4 = image.Height;
			if (!(mode == "HW"))
			{
				if (!(mode == "W"))
				{
					if (!(mode == "H"))
					{
						if (mode == "Cut")
						{
							if ((double)image.Width / (double)image.Height > (double)num / (double)num2)
							{
								num4 = image.Height;
								num3 = image.Height * num / num2;
								y = 0;
								x = (image.Width - num3) / 2;
							}
							else
							{
								num3 = image.Width;
								num4 = image.Width * height / num;
								x = 0;
								y = (image.Height - num4) / 2;
							}
						}
					}
					else
					{
						num = image.Width * height / image.Height;
					}
				}
				else
				{
					num2 = image.Height * width / image.Width;
				}
			}
			Bitmap bitmap = new Bitmap(num, num2);
			try
			{
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
				graphics.Clear(Color.Transparent);
				graphics.DrawImage(image, new Rectangle(0, 0, num, num2), new Rectangle(x, y, num3, num4), GraphicsUnit.Pixel);
				FPThumb.SaveImage(bitmap, newFileName, FPThumb.GetCodecInfo("image/" + FPThumb.GetFormat(newFileName).ToString().ToLower()));
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		public static bool MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
		{
			Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(fileName)));
			Bitmap bitmap = new Bitmap(cropWidth, cropHeight);
			bool result;
			try
			{
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
					graphics.SmoothingMode = SmoothingMode.AntiAlias;
					graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
					graphics.Clear(Color.Transparent);
					graphics.DrawImage(image, new Rectangle(0, 0, cropWidth, cropHeight), X, Y, cropWidth, cropHeight, GraphicsUnit.Pixel);
					FPThumb.SaveImage(new Bitmap(bitmap, maxWidth, maxHeight), newFileName, FPThumb.GetCodecInfo("image/" + FPThumb.GetFormat(newFileName).ToString().ToLower()));
					result = true;
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
			}
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000C184 File Offset: 0x0000A384
		public static void MakeRemoteThumbnailImage(string url, string newFileName, int maxWidth, int maxHeight)
		{
			Stream remoteImage = FPThumb.GetRemoteImage(url);
			if (remoteImage == null)
			{
				return;
			}
			Image original = Image.FromStream(remoteImage);
			remoteImage.Close();
			FPThumb.MakeThumbnailImage(original, newFileName, maxWidth, maxHeight);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		private static Stream GetRemoteImage(string url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentLength = 0L;
			httpWebRequest.Timeout = 20000;
			Stream result;
			try
			{
				result = ((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000053 RID: 83
		private Image srcImage;

		// Token: 0x04000054 RID: 84
		private string srcFileName;
	}
}
