using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace FP_Verify
{
	// Token: 0x02000002 RID: 2
	public class FPVCode
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public int MaxLength
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		public int MinLength
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002060 File Offset: 0x00000260
		public string CreateValidateCode(int length)
		{
			int[] array = new int[length];
			int[] array2 = new int[length];
			string text = "";
			int num = new Random((int)DateTime.Now.Ticks).Next(0, int.MaxValue - length * 10000);
			int[] array3 = new int[length];
			for (int i = 0; i < length; i++)
			{
				num += 10000;
				array3[i] = num;
			}
			for (int j = 0; j < length; j++)
			{
				Random random = new Random(array3[j]);
				int minValue = (int)Math.Pow(10.0, (double)length);
				array[j] = random.Next(minValue, int.MaxValue);
			}
			for (int k = 0; k < length; k++)
			{
				string text2 = array[k].ToString();
				int length2 = text2.Length;
				int startIndex = new Random().Next(0, length2 - 1);
				array2[k] = int.Parse(text2.Substring(startIndex, 1));
			}
			for (int l = 0; l < length; l++)
			{
				text += array2[l].ToString();
			}
			return text;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002184 File Offset: 0x00000384
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
				for (int j = 0; j < 100; j++)
				{
					int x3 = random.Next(bitmap.Width);
					int y3 = random.Next(bitmap.Height);
					bitmap.SetPixel(x3, y3, Color.FromArgb(random.Next()));
				}
				graphics.DrawRectangle(new Pen(Color.Silver), 0, 0, bitmap.Width - 1, bitmap.Height - 1);
				MemoryStream memoryStream = new MemoryStream();
				bitmap.Save(memoryStream, ImageFormat.Gif);
				result = memoryStream.ToArray();
			}
			finally
			{
				graphics.Dispose();
				bitmap.Dispose();
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002328 File Offset: 0x00000528
		public static int GetImageWidth(int validateNumLength)
		{
			return (int)((double)validateNumLength * 12.0);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002337 File Offset: 0x00000537
		public static double GetImageHeight()
		{
			return 22.5;
		}
	}
}
