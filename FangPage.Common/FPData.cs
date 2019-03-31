using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace FangPage.Common
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public class FPData : ISerializable
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000035E0 File Offset: 0x000017E0
		public FPData()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000035F5 File Offset: 0x000017F5
		public FPData(string json)
		{
			this.m_data = FPJson.ToModel<Dictionary<string, string>>(json);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003618 File Offset: 0x00001818
		public FPData(object obj)
		{
			try
			{
				Type type = obj.GetType();
				bool flag = type.GetProperties().Length != 0;
				if (flag)
				{
					foreach (PropertyInfo propertyInfo in type.GetProperties())
					{
						this.m_data[propertyInfo.Name] = propertyInfo.GetValue(obj, null).ToString();
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000036A8 File Offset: 0x000018A8
		protected FPData(SerializationInfo info, StreamingContext context)
		{
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				this.m_data[enumerator.Name] = enumerator.Value.ToString();
			}
		}

		// Token: 0x17000001 RID: 1
		public string this[string key]
		{
			get
			{
				bool flag = !this.m_data.ContainsKey(key);
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = this.m_data[key];
				}
				return result;
			}
			set
			{
				this.m_data[key] = value;
			}
		}

		// Token: 0x17000002 RID: 2
		public string this[int index]
		{
			get
			{
				bool flag = !this.m_data.ContainsKey(index.ToString());
				string result;
				if (flag)
				{
					result = "";
				}
				else
				{
					result = this.m_data[index.ToString()];
				}
				return result;
			}
			set
			{
				this.m_data[index.ToString()] = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000037A8 File Offset: 0x000019A8
		public int Count
		{
			get
			{
				return this.m_data.Count;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000037C8 File Offset: 0x000019C8
		public Dictionary<string, string> Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000037E0 File Offset: 0x000019E0
		public string Json
		{
			get
			{
				return FPJson.ToJson(this.m_data);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003800 File Offset: 0x00001A00
		public string[] Keys
		{
			get
			{
				return new List<string>(this.m_data.Keys).ToArray();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003828 File Offset: 0x00001A28
		public string[] Values
		{
			get
			{
				return new List<string>(this.m_data.Values).ToArray();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003850 File Offset: 0x00001A50
		public bool ContainsKey(string key)
		{
			return this.m_data.ContainsKey(key);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003870 File Offset: 0x00001A70
		public bool ContainsValue(string value)
		{
			return this.m_data.ContainsValue(value);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000388E File Offset: 0x00001A8E
		public void Add(string key, string value)
		{
			this.m_data.Add(key, value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000038A0 File Offset: 0x00001AA0
		public bool Remove(string key)
		{
			return this.m_data.Remove(key);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038BE File Offset: 0x00001ABE
		public void Clear()
		{
			this.m_data.Clear();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000038D0 File Offset: 0x00001AD0
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.m_data)
			{
				info.AddValue(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003938 File Offset: 0x00001B38
		public override string ToString()
		{
			return FPJson.ToJson(this.m_data);
		}

		// Token: 0x04000022 RID: 34
		private Dictionary<string, string> m_data = new Dictionary<string, string>();
	}
}
