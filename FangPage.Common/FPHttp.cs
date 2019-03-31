using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace FangPage.Common
{
	// Token: 0x0200000E RID: 14
	public sealed class FPHttp : IDisposable
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000045FC File Offset: 0x000027FC
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004614 File Offset: 0x00002814
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				this._timeout = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004620 File Offset: 0x00002820
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004638 File Offset: 0x00002838
		public int ReadWriteTimeout
		{
			get
			{
				return this._readWriteTimeout;
			}
			set
			{
				this._readWriteTimeout = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004644 File Offset: 0x00002844
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000465C File Offset: 0x0000285C
		public bool IgnoreSSLCheck
		{
			get
			{
				return this._ignoreSSLCheck;
			}
			set
			{
				this._ignoreSSLCheck = value;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004668 File Offset: 0x00002868
		public string Post(string url)
		{
			return this.Post(url, "");
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004688 File Offset: 0x00002888
		public string Post(string url, FPData query)
		{
			string result;
			try
			{
				string text = FPHttp.BuildQuery(query);
				HttpWebRequest webRequest = this.GetWebRequest(url, "POST", null);
				webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
				bool flag = !string.IsNullOrEmpty(text);
				if (flag)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					Stream requestStream = webRequest.GetRequestStream();
					requestStream.Write(bytes, 0, bytes.Length);
					requestStream.Flush();
					requestStream.Close();
				}
				else
				{
					webRequest.ContentLength = 0L;
				}
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004754 File Offset: 0x00002954
		public string Post(string url, string content)
		{
			string result;
			try
			{
				HttpWebRequest webRequest = this.GetWebRequest(url, "POST", null);
				webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
				bool flag = !string.IsNullOrEmpty(content);
				if (flag)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(content);
					Stream requestStream = webRequest.GetRequestStream();
					requestStream.Write(bytes, 0, bytes.Length);
					requestStream.Flush();
					requestStream.Close();
				}
				else
				{
					webRequest.ContentLength = 0L;
				}
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004818 File Offset: 0x00002A18
		public string PostJson(string url, object obj)
		{
			string result;
			try
			{
				string text = FPJson.ToJson(obj);
				HttpWebRequest webRequest = this.GetWebRequest(url, "POST", null);
				webRequest.ContentType = "application/json;charset=utf-8";
				bool flag = !string.IsNullOrEmpty(text);
				if (flag)
				{
					byte[] bytes = Encoding.UTF8.GetBytes(text);
					Stream requestStream = webRequest.GetRequestStream();
					requestStream.Write(bytes, 0, bytes.Length);
					requestStream.Flush();
					requestStream.Close();
				}
				else
				{
					webRequest.ContentLength = 0L;
				}
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000048E4 File Offset: 0x00002AE4
		public string PostModel<T>(string url, T model) where T : new()
		{
			Type typeFromHandle = typeof(T);
			FPData fpdata = new FPData();
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				bool flag = !propertyInfo.CanRead;
				if (!flag)
				{
					object value = propertyInfo.GetValue(model, null);
					bool flag2 = value == null;
					if (!flag2)
					{
						fpdata.Add(propertyInfo.Name, value.ToString());
					}
				}
			}
			return this.Post(url, fpdata);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004974 File Offset: 0x00002B74
		public T GetModel<T>(string url, FPData query) where T : new()
		{
			string json = this.Post(url, query);
			return FPJson.ToModel<T>(json);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004998 File Offset: 0x00002B98
		public List<T> GetList<T>(string url, FPData query) where T : new()
		{
			string json = this.Post(url, query);
			return FPJson.ToList<T>(json);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000049BC File Offset: 0x00002BBC
		public string Get(string url)
		{
			string result;
			try
			{
				HttpWebRequest webRequest = this.GetWebRequest(url, "GET", null);
				webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004A2C File Offset: 0x00002C2C
		public string Get(string url, FPData query)
		{
			string result;
			try
			{
				bool flag = query != null && query.Count > 0;
				if (flag)
				{
					url = FPHttp.BuildRequestUrl(url, query);
				}
				HttpWebRequest webRequest = this.GetWebRequest(url, "GET", null);
				webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004AC0 File Offset: 0x00002CC0
		public string Get(string url, string query)
		{
			string result;
			try
			{
				bool flag = !string.IsNullOrEmpty(query);
				if (flag)
				{
					url = FPHttp.BuildRequestUrl(url, new string[]
					{
						query
					});
				}
				HttpWebRequest webRequest = this.GetWebRequest(url, "GET", null);
				webRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
				HttpWebResponse rsp = (HttpWebResponse)webRequest.GetResponse();
				Encoding responseEncoding = this.GetResponseEncoding(rsp);
				result = this.GetResponseAsString(rsp, responseEncoding);
			}
			catch (Exception ex)
			{
				result = "Error:" + ex.Message;
			}
			return result;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004B54 File Offset: 0x00002D54
		public HttpWebRequest GetWebRequest(string url, string method, FPData headerParams)
		{
			HttpWebRequest httpWebRequest = null;
			bool flag = url.StartsWith("https", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				bool ignoreSSLCheck = this._ignoreSSLCheck;
				if (ignoreSSLCheck)
				{
					ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(FPHttp.TrustAllValidationCallback);
				}
				httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
			}
			else
			{
				httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			}
			bool flag2 = headerParams != null && headerParams.Count > 0;
			if (flag2)
			{
				foreach (string text in headerParams.Data.Keys)
				{
					httpWebRequest.Headers.Add(text, headerParams[text]);
				}
			}
			httpWebRequest.ServicePoint.Expect100Continue = false;
			httpWebRequest.Method = method;
			httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";
			httpWebRequest.Timeout = this._timeout;
			httpWebRequest.ReadWriteTimeout = this._readWriteTimeout;
			return httpWebRequest;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C6C File Offset: 0x00002E6C
		public string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
		{
			Stream stream = null;
			StreamReader streamReader = null;
			string result;
			try
			{
				stream = rsp.GetResponseStream();
				bool flag = "gzip".Equals(rsp.ContentEncoding, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					stream = new GZipStream(stream, CompressionMode.Decompress);
				}
				streamReader = new StreamReader(stream, encoding);
				result = streamReader.ReadToEnd();
			}
			finally
			{
				bool flag2 = streamReader != null;
				if (flag2)
				{
					streamReader.Close();
				}
				bool flag3 = stream != null;
				if (flag3)
				{
					stream.Close();
				}
				bool flag4 = rsp != null;
				if (flag4)
				{
					rsp.Close();
				}
			}
			return result;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004D00 File Offset: 0x00002F00
		public static string BuildRequestUrl(string url, FPData parameters)
		{
			bool flag = parameters != null && parameters.Count > 0;
			string result;
			if (flag)
			{
				result = FPHttp.BuildRequestUrl(url, new string[]
				{
					FPHttp.BuildQuery(parameters)
				});
			}
			else
			{
				result = url;
			}
			return result;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004D40 File Offset: 0x00002F40
		public static string BuildRequestUrl(string url, params string[] queries)
		{
			bool flag = queries == null || queries.Length == 0;
			string result;
			if (flag)
			{
				result = url;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(url);
				bool flag2 = url.Contains("?");
				bool flag3 = url.EndsWith("?") || url.EndsWith("&");
				foreach (string value in queries)
				{
					bool flag4 = !string.IsNullOrEmpty(value);
					if (flag4)
					{
						bool flag5 = !flag3;
						if (flag5)
						{
							bool flag6 = flag2;
							if (flag6)
							{
								stringBuilder.Append("&");
							}
							else
							{
								stringBuilder.Append("?");
								flag2 = true;
							}
						}
						stringBuilder.Append(value);
						flag3 = false;
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004E14 File Offset: 0x00003014
		public static string BuildQuery(FPData parameters)
		{
			bool flag = parameters == null || parameters.Count == 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag2 = false;
				foreach (KeyValuePair<string, string> keyValuePair in parameters.Data)
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					bool flag3 = !string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value);
					if (flag3)
					{
						bool flag4 = flag2;
						if (flag4)
						{
							stringBuilder.Append("&");
						}
						stringBuilder.Append(key);
						stringBuilder.Append("=");
						stringBuilder.Append(HttpUtility.UrlEncode(value, Encoding.UTF8));
						flag2 = true;
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004F08 File Offset: 0x00003108
		private Encoding GetResponseEncoding(HttpWebResponse rsp)
		{
			string text = rsp.CharacterSet;
			bool flag = string.IsNullOrEmpty(text);
			if (flag)
			{
				text = "utf-8";
			}
			return Encoding.GetEncoding(text);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004F3C File Offset: 0x0000313C
		private static bool TrustAllValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
		{
			return true;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000347F File Offset: 0x0000167F
		public void Dispose()
		{
		}

		// Token: 0x04000024 RID: 36
		private int _timeout = 20000;

		// Token: 0x04000025 RID: 37
		private int _readWriteTimeout = 60000;

		// Token: 0x04000026 RID: 38
		private bool _ignoreSSLCheck = true;
	}
}
