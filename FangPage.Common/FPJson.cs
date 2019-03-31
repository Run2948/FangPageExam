using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FangPage.Common
{
	// Token: 0x02000010 RID: 16
	public class FPJson
	{
		// Token: 0x060000BF RID: 191 RVA: 0x0000539C File Offset: 0x0000359C
		public static T LoadModel<T>(string filename) where T : new()
		{
			string json = FPFile.ReadFile(filename);
			return FPJson.ToModel<T>(json);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000053C0 File Offset: 0x000035C0
		public static List<T> LoadList<T>(string filename) where T : new()
		{
			string json = FPFile.ReadFile(filename);
			return FPJson.ToList<T>(json);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000053E4 File Offset: 0x000035E4
		public static void SaveJson<T>(T model, string filename)
		{
			string content = FPJson.ToJson(model);
			FPFile.WriteFile(filename, content);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00005408 File Offset: 0x00003608
		public static void SaveJson<T>(List<T> list, string filename)
		{
			string content = FPJson.ToJson(list);
			FPFile.WriteFile(filename, content);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00005428 File Offset: 0x00003628
		public static string ToJson(object obj)
		{
			return JsonConvert.SerializeObject(obj, new JsonConverter[]
			{
				new IsoDateTimeConverter
				{
					DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
				}
			});
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00005460 File Offset: 0x00003660
		public static T ToModel<T>(string json) where T : new()
		{
			T result;
			try
			{
				result = JsonConvert.DeserializeObject<T>(json);
			}
			catch
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005498 File Offset: 0x00003698
		public static List<T> ToList<T>(string json) where T : new()
		{
			List<T> result;
			try
			{
				result = JsonConvert.DeserializeObject<List<T>>(json);
			}
			catch
			{
				result = new List<T>();
			}
			return result;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000054CC File Offset: 0x000036CC
		public static List<T> ToList<T>(string json, int count) where T : new()
		{
			List<T> list = FPJson.ToList<T>(json);
			int count2 = list.Count;
			bool flag = count > count2;
			if (flag)
			{
				for (int i = 0; i < count - count2; i++)
				{
					list.Add(Activator.CreateInstance<T>());
				}
			}
			return list;
		}
	}
}
