using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace FangPage.WMS
{
	// Token: 0x02000005 RID: 5
	public class WaterMark
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00003418 File Offset: 0x00001618
		public static void AddImageSignPic(string imgPath, string filename, string watermarkFilename, int watermarkStatus, int quality, int watermarkTransparency)
		{
			if (!File.Exists(imgPath))
			{
				return;
			}
			Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(imgPath)));
			if (!File.Exists(watermarkFilename))
			{
				return;
			}
			Graphics graphics = Graphics.FromImage(image);
			Image image2 = new Bitmap(watermarkFilename);
			if (image2.Height >= image.Height || image2.Width >= image.Width)
			{
				return;
			}
			ImageAttributes imageAttributes = new ImageAttributes();
			ColorMap[] map = new ColorMap[]
			{
				new ColorMap
				{
					OldColor = Color.FromArgb(255, 0, 255, 0),
					NewColor = Color.FromArgb(0, 0, 0, 0)
				}
			};
			imageAttributes.SetRemapTable(map, ColorAdjustType.Bitmap);
			float num = 0.5f;
			if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
			{
				num = (float)watermarkTransparency / 10f;
			}
			float[][] array = new float[5][];
			int num2 = 0;
			float[] array2 = new float[5];
			array2[0] = 1f;
			array[num2] = array2;
			int num3 = 1;
			float[] array3 = new float[5];
			array3[1] = 1f;
			array[num3] = array3;
			int num4 = 2;
			float[] array4 = new float[5];
			array4[2] = 1f;
			array[num4] = array4;
			int num5 = 3;
			float[] array5 = new float[5];
			array5[3] = num;
			array[num5] = array5;
			array[4] = new float[]
			{
				0f,
				0f,
				0f,
				0f,
				1f
			};
			ColorMatrix newColorMatrix = new ColorMatrix(array);
			imageAttributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
			int x = 0;
			int y = 0;
			switch (watermarkStatus)
			{
			case 1:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.01f);
				break;
			case 2:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.01f);
				break;
			case 3:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.01f);
				break;
			case 4:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 5:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 6:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.5f - (float)(image2.Height / 2));
				break;
			case 7:
				x = (int)((float)image.Width * 0.01f);
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			case 8:
				x = (int)((float)image.Width * 0.5f - (float)(image2.Width / 2));
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			case 9:
				x = (int)((float)image.Width * 0.99f - (float)image2.Width);
				y = (int)((float)image.Height * 0.99f - (float)image2.Height);
				break;
			}
			graphics.DrawImage(image2, new Rectangle(x, y, image2.Width, image2.Height), 0, 0, image2.Width, image2.Height, GraphicsUnit.Pixel, imageAttributes);
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;
			foreach (ImageCodecInfo imageCodecInfo2 in imageEncoders)
			{
				if (imageCodecInfo2.MimeType.IndexOf("jpeg") > -1)
				{
					imageCodecInfo = imageCodecInfo2;
				}
			}
			EncoderParameters encoderParameters = new EncoderParameters();
			long[] array7 = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			array7[0] = (long)quality;
			EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, array7);
			encoderParameters.Param[0] = encoderParameter;
			if (imageCodecInfo != null)
			{
				image.Save(filename, imageCodecInfo, encoderParameters);
			}
			else
			{
				image.Save(filename);
			}
			graphics.Dispose();
			image.Dispose();
			image2.Dispose();
			imageAttributes.Dispose();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003800 File Offset: 0x00001A00
		public static void AddImageSignText(string imgPath, string filename, string watermarkText, int watermarkStatus, int quality, string fontname, int fontsize)
		{
			if (!File.Exists(imgPath))
			{
				return;
			}
			Image image = Image.FromStream(new MemoryStream(File.ReadAllBytes(imgPath)));
			Graphics graphics = Graphics.FromImage(image);
			Font font = new Font(fontname, (float)fontsize, FontStyle.Regular, GraphicsUnit.Pixel);
			SizeF sizeF = graphics.MeasureString(watermarkText, font);
			float num = 0f;
			float num2 = 0f;
			switch (watermarkStatus)
			{
			case 1:
				num = (float)image.Width * 0.01f;
				num2 = (float)image.Height * 0.01f;
				break;
			case 2:
				num = (float)image.Width * 0.5f - sizeF.Width / 2f;
				num2 = (float)image.Height * 0.01f;
				break;
			case 3:
				num = (float)image.Width * 0.99f - sizeF.Width;
				num2 = (float)image.Height * 0.01f;
				break;
			case 4:
				num = (float)image.Width * 0.01f;
				num2 = (float)image.Height * 0.5f - sizeF.Height / 2f;
				break;
			case 5:
				num = (float)image.Width * 0.5f - sizeF.Width / 2f;
				num2 = (float)image.Height * 0.5f - sizeF.Height / 2f;
				break;
			case 6:
				num = (float)image.Width * 0.99f - sizeF.Width;
				num2 = (float)image.Height * 0.5f - sizeF.Height / 2f;
				break;
			case 7:
				num = (float)image.Width * 0.01f;
				num2 = (float)image.Height * 0.99f - sizeF.Height;
				break;
			case 8:
				num = (float)image.Width * 0.5f - sizeF.Width / 2f;
				num2 = (float)image.Height * 0.99f - sizeF.Height;
				break;
			case 9:
				num = (float)image.Width * 0.99f - sizeF.Width;
				num2 = (float)image.Height * 0.99f - sizeF.Height;
				break;
			}
			graphics.DrawString(watermarkText, font, new SolidBrush(Color.White), num + 1f, num2 + 1f);
			graphics.DrawString(watermarkText, font, new SolidBrush(Color.Black), num, num2);
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;
			foreach (ImageCodecInfo imageCodecInfo2 in imageEncoders)
			{
				if (imageCodecInfo2.MimeType.IndexOf("jpeg") > -1)
				{
					imageCodecInfo = imageCodecInfo2;
				}
			}
			EncoderParameters encoderParameters = new EncoderParameters();
			long[] array2 = new long[1];
			if (quality < 0 || quality > 100)
			{
				quality = 80;
			}
			array2[0] = (long)quality;
			EncoderParameter encoderParameter = new EncoderParameter(Encoder.Quality, array2);
			encoderParameters.Param[0] = encoderParameter;
			if (imageCodecInfo != null)
			{
				image.Save(filename, imageCodecInfo, encoderParameters);
			}
			else
			{
				image.Save(filename);
			}
			graphics.Dispose();
			image.Dispose();
		}
	}
}
