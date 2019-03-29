using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FangPage.MVC
{
	// Token: 0x02000006 RID: 6
	public class FPVCode
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002D4C File Offset: 0x00000F4C
		public int MaxLength
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002D60 File Offset: 0x00000F60
		public int MinLength
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002D74 File Offset: 0x00000F74
		public string CreateValidateCode(int length)
		{
			int[] array = new int[length];
			int[] array2 = new int[length];
			string text = "";
			int seed = (int)DateTime.Now.Ticks;
			Random random = new Random(seed);
			int num = random.Next(0, int.MaxValue - length * 10000);
			int[] array3 = new int[length];
			for (int i = 0; i < length; i++)
			{
				num += 10000;
				array3[i] = num;
			}
			for (int i = 0; i < length; i++)
			{
				Random random2 = new Random(array3[i]);
				int minValue = (int)Math.Pow(10.0, (double)length);
				array[i] = random2.Next(minValue, int.MaxValue);
			}
			for (int i = 0; i < length; i++)
			{
				string text2 = array[i].ToString();
				int length2 = text2.Length;
				Random random2 = new Random();
				int startIndex = random2.Next(0, length2 - 1);
				array2[i] = int.Parse(text2.Substring(startIndex, 1));
			}
			for (int i = 0; i < length; i++)
			{
				text += array2[i].ToString();
			}
			return text;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002ECC File Offset: 0x000010CC
		public byte[] CreateValidateGraphic(string validateCode)
		{
			Bitmap bitmap = new Bitmap((int)Math.Ceiling((double)validateCode.Length * 12.0), 22);
			Graphics graphics = Graphics.FromImage(bitmap);
			byte[] result;
			try
			{
				Random random = new Random();
				graphics.Clear(Color.White);
				for (int i = 0; i < 25; i++)
				{
					int x = random.Next(bitmap.Width);
					int x2 = random.Next(bitmap.Width);
					int y = random.Next(bitmap.Height);
					int y2 = random.Next(bitmap.Height);
					graphics.DrawLine(new Pen(Color.Silver), x, y, x2, y2);
				}
				Font font = new Font("Arial", 12f, FontStyle.Bold | FontStyle.Italic);
				LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitmap.Width, bitmap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
				graphics.DrawString(validateCode, font, brush, 3f, 2f);
				for (int i = 0; i < 100; i++)
				{
					int x3 = random.Next(bitmap.Width);
					int y3 = random.Next(bitmap.Height);
					bitmap.SetPixel(x3, y3, Color.FromArgb(random.Next()));
				}
				graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Jpeg);
				result = memoryStream.ToArray();
			}
			finally
			{
				graphics.Dispose();
				bitmap.Dispose();
			}
			return result;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003088 File Offset: 0x00001288
		public static int GetImageWidth(int validateNumLength)
		{
			return (int)((double)validateNumLength * 12.0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000030A8 File Offset: 0x000012A8
		public static double GetImageHeight()
		{
			return 22.5;
		}
	}
}
