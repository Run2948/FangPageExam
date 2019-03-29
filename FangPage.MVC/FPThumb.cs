using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace FangPage.MVC
{
	// Token: 0x0200000F RID: 15
	public class FPThumb
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00009788 File Offset: 0x00007988
		public bool SetImage(string FileName)
		{
			this.srcFileName = FPUtils.GetMapPath(FileName);
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

		// Token: 0x060000A8 RID: 168 RVA: 0x000097D4 File Offset: 0x000079D4
		public bool ThumbnailCallback()
		{
			return false;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000097E8 File Offset: 0x000079E8
		public static string GetThumbnail(string imgpath, int maxsize)
		{
			int num = maxsize;
			if (num <= 0)
			{
				num = 600;
			}
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath);
			string text = mapPath + imgpath;
			string result;
			if (!File.Exists(text))
			{
				result = "";
			}
			else
			{
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
					string mapPath2 = FPUtils.GetMapPath(text4);
					if (!File.Exists(mapPath2))
					{
						string mapPath3 = FPUtils.GetMapPath(WebConfig.WebPath + "cache/thumbnail/");
						if (!Directory.Exists(mapPath3))
						{
							try
							{
								Directory.CreateDirectory(mapPath3);
							}
							catch
							{
								throw new Exception("请检查程序目录下cache文件夹的用户权限！");
							}
						}
						FPThumb.CreateThumbnail(mapPath2, text, num);
					}
					result = text4;
				}
				else if (text3 == ".gif")
				{
					result = imgpath;
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000995C File Offset: 0x00007B5C
		public static void CreateThumbnail(string attPhyCachePath, string attPhyPath, int theMaxsize)
		{
			if (File.Exists(attPhyPath))
			{
				try
				{
					FPThumb.MakeThumbnailImage(attPhyPath, attPhyCachePath, theMaxsize, theMaxsize);
				}
				catch
				{
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000999C File Offset: 0x00007B9C
		public Image GetImage(int Width, int Height)
		{
			Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(this.ThumbnailCallback);
			return this.srcImage.GetThumbnailImage(Width, Height, callback, IntPtr.Zero);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000099D0 File Offset: 0x00007BD0
		public void SaveThumbnailImage(int Width, int Height)
		{
			string text = Path.GetExtension(this.srcFileName).ToLower();
			if (text != null)
			{
				if (text == ".png")
				{
					this.SaveImage(Width, Height, ImageFormat.Png);
					return;
				}
				if (text == ".gif")
				{
					this.SaveImage(Width, Height, ImageFormat.Gif);
					return;
				}
			}
			this.SaveImage(Width, Height, ImageFormat.Jpeg);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009A40 File Offset: 0x00007C40
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

		// Token: 0x060000AE RID: 174 RVA: 0x00009AC4 File Offset: 0x00007CC4
		private static void SaveImage(Image image, string savePath, ImageCodecInfo ici)
		{
			EncoderParameters encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
			image.Save(savePath, ici, encoderParameters);
			encoderParameters.Dispose();
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009B00 File Offset: 0x00007D00
		private static ImageCodecInfo GetCodecInfo(string mimeType)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			foreach (ImageCodecInfo imageCodecInfo in imageEncoders)
			{
				if (imageCodecInfo.MimeType == mimeType)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00009B58 File Offset: 0x00007D58
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

		// Token: 0x060000B1 RID: 177 RVA: 0x00009C50 File Offset: 0x00007E50
		public static ImageFormat GetFormat(string name)
		{
			string text = name.Substring(name.LastIndexOf(".") + 1);
			string text2 = text.ToLower();
			if (text2 != null)
			{
				if (text2 == "jpg" || text2 == "jpeg")
				{
					return ImageFormat.Jpeg;
				}
				if (text2 == "bmp")
				{
					return ImageFormat.Bmp;
				}
				if (text2 == "png")
				{
					return ImageFormat.Png;
				}
				if (text2 == "gif")
				{
					return ImageFormat.Gif;
				}
			}
			return ImageFormat.Jpeg;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00009CE8 File Offset: 0x00007EE8
		public static void MakeSquareImage(Image image, string newFileName, int newSize)
		{
			int width = image.Width;
			int height = image.Height;
			if (width > height)
			{
			}
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

		// Token: 0x060000B3 RID: 179 RVA: 0x00009DF0 File Offset: 0x00007FF0
		public static void MakeSquareImage(string fileName, string newFileName, int newSize)
		{
			FPThumb.MakeSquareImage(Image.FromFile(fileName), newFileName, newSize);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00009E04 File Offset: 0x00008004
		public static void MakeRemoteSquareImage(string url, string newFileName, int newSize)
		{
			Stream remoteImage = FPThumb.GetRemoteImage(url);
			if (remoteImage != null)
			{
				Image image = Image.FromStream(remoteImage);
				remoteImage.Close();
				FPThumb.MakeSquareImage(image, newFileName, newSize);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00009E40 File Offset: 0x00008040
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

		// Token: 0x060000B6 RID: 182 RVA: 0x00009EB8 File Offset: 0x000080B8
		public static void MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight)
		{
			byte[] buffer = File.ReadAllBytes(fileName);
			Image original = Image.FromStream(new MemoryStream(buffer));
			FPThumb.MakeThumbnailImage(original, newFileName, maxWidth, maxHeight);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00009EE4 File Offset: 0x000080E4
		public static void MakeThumbnailImage(string fileName, string newFileName, int width, int height, string mode)
		{
			Image image = Image.FromFile(fileName);
			int num = width;
			int num2 = height;
			int x = 0;
			int y = 0;
			int num3 = image.Width;
			int num4 = image.Height;
			if (mode != null)
			{
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

		// Token: 0x060000B8 RID: 184 RVA: 0x0000A0B4 File Offset: 0x000082B4
		public static bool MakeThumbnailImage(string fileName, string newFileName, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
		{
			byte[] buffer = File.ReadAllBytes(fileName);
			Image image = Image.FromStream(new MemoryStream(buffer));
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
					Image image2 = new Bitmap(bitmap, maxWidth, maxHeight);
					FPThumb.SaveImage(image2, newFileName, FPThumb.GetCodecInfo("image/" + FPThumb.GetFormat(newFileName).ToString().ToLower()));
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

		// Token: 0x060000B9 RID: 185 RVA: 0x0000A1B4 File Offset: 0x000083B4
		public static void MakeRemoteThumbnailImage(string url, string newFileName, int maxWidth, int maxHeight)
		{
			Stream remoteImage = FPThumb.GetRemoteImage(url);
			if (remoteImage != null)
			{
				Image original = Image.FromStream(remoteImage);
				remoteImage.Close();
				FPThumb.MakeThumbnailImage(original, newFileName, maxWidth, maxHeight);
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000A1F0 File Offset: 0x000083F0
		private static Stream GetRemoteImage(string url)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "GET";
			httpWebRequest.ContentLength = 0L;
			httpWebRequest.Timeout = 20000;
			Stream result;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				result = httpWebResponse.GetResponseStream();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000039 RID: 57
		private Image srcImage;

		// Token: 0x0400003A RID: 58
		private string srcFileName;
	}
}
