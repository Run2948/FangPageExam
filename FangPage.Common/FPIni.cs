using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace FangPage.Common
{
	// Token: 0x0200000F RID: 15
	public class FPIni
	{
		// Token: 0x060000B6 RID: 182
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		// Token: 0x060000B7 RID: 183
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		// Token: 0x060000B8 RID: 184 RVA: 0x00004F78 File Offset: 0x00003178
		public static void WriteIni(string section, string key, string value, string filename)
		{
			bool flag = !Directory.Exists(Path.GetDirectoryName(filename));
			if (flag)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			bool flag2 = File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1;
			if (flag2)
			{
				File.SetAttributes(filename, FileAttributes.Normal);
			}
			FPIni.WritePrivateProfileString(section, key, value, filename);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FF4 File Offset: 0x000031F4
		public static void WriteIni<T>(T model, string filename)
		{
			Type typeFromHandle = typeof(T);
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				bool flag = propertyInfo == null || !propertyInfo.CanWrite;
				if (!flag)
				{
					string value = string.Empty;
					bool flag2 = propertyInfo.GetValue(model, null) != null;
					if (flag2)
					{
						value = propertyInfo.GetValue(model, null).ToString();
					}
					FPIni.WriteIni(typeFromHandle.Name, propertyInfo.Name, value, filename);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000508C File Offset: 0x0000328C
		public static void WriteIni<T>(string key, string value, string filename)
		{
			Type typeFromHandle = typeof(T);
			FPIni.WriteIni(typeFromHandle.Name, key, value, filename);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000050B4 File Offset: 0x000032B4
		public static string ReadIni(string section, string key, string filename)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			FPIni.GetPrivateProfileString(section, key, "", stringBuilder, 255, filename);
			return stringBuilder.ToString();
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000050EC File Offset: 0x000032EC
		public static string ReadIni<T>(string key, string filename)
		{
			Type typeFromHandle = typeof(T);
			return FPIni.ReadIni(typeFromHandle.Name, key, filename);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005118 File Offset: 0x00003318
		public static T ReadIni<T>(string filename) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			bool flag = !File.Exists(filename);
			T result;
			if (flag)
			{
				FPIni.WriteIni<T>(t, filename);
				result = t;
			}
			else
			{
				Type typeFromHandle = typeof(T);
				foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
				{
					bool flag2 = propertyInfo == null || !propertyInfo.CanWrite;
					if (!flag2)
					{
						string text = FPIni.ReadIni(typeFromHandle.Name, propertyInfo.Name, filename);
						bool flag3 = propertyInfo.PropertyType == typeof(string);
						if (flag3)
						{
							propertyInfo.SetValue(t, text, null);
						}
						else
						{
							bool flag4 = propertyInfo.PropertyType == typeof(int);
							if (flag4)
							{
								propertyInfo.SetValue(t, FPUtils.StrToInt(text), null);
							}
							else
							{
								bool flag5 = propertyInfo.PropertyType == typeof(short);
								if (flag5)
								{
									propertyInfo.SetValue(t, short.Parse(FPUtils.StrToInt(text).ToString()), null);
								}
								else
								{
									bool flag6 = propertyInfo.PropertyType == typeof(DateTime);
									if (flag6)
									{
										propertyInfo.SetValue(t, FPUtils.StrToDateTime(text), null);
									}
									else
									{
										bool flag7 = propertyInfo.PropertyType == typeof(decimal);
										if (flag7)
										{
											propertyInfo.SetValue(t, FPUtils.StrToDecimal(text), null);
										}
										else
										{
											bool flag8 = propertyInfo.PropertyType == typeof(float);
											if (flag8)
											{
												propertyInfo.SetValue(t, FPUtils.StrToFloat(text), null);
											}
											else
											{
												bool flag9 = propertyInfo.PropertyType == typeof(double);
												if (flag9)
												{
													propertyInfo.SetValue(t, FPUtils.StrToDouble(text), null);
												}
												else
												{
													bool flag10 = propertyInfo.PropertyType == typeof(DateTime?);
													if (flag10)
													{
														bool flag11 = propertyInfo.PropertyType != null;
														if (flag11)
														{
															propertyInfo.SetValue(t, FPUtils.StrToDateTime(text), null);
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
				result = t;
			}
			return result;
		}
	}
}
