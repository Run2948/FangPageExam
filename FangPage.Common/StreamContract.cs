using System;
using System.IO;

namespace FangPage.Common
{
	// Token: 0x02000008 RID: 8
	internal class StreamContract : Contract
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002322 File Offset: 0x00000522
		public StreamContract(string fileName, Stream stream, string mimeType)
		{
			this.fileName = fileName;
			this.stream = stream;
			this.mimeType = mimeType;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002344 File Offset: 0x00000544
		public long GetFileLength()
		{
			return 0L;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002358 File Offset: 0x00000558
		public string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002370 File Offset: 0x00000570
		public string GetMimeType()
		{
			bool flag = string.IsNullOrEmpty(this.mimeType);
			string result;
			if (flag)
			{
				result = "application/octet-stream";
			}
			else
			{
				result = this.mimeType;
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000023A4 File Offset: 0x000005A4
		public bool IsValid()
		{
			return this.stream != null && this.fileName != null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000023CC File Offset: 0x000005CC
		public void Write(Stream output)
		{
			using (this.stream)
			{
				byte[] array = new byte[4096];
				int count;
				while ((count = this.stream.Read(array, 0, array.Length)) > 0)
				{
					output.Write(array, 0, count);
				}
			}
		}

		// Token: 0x0400001F RID: 31
		private string fileName;

		// Token: 0x04000020 RID: 32
		private Stream stream;

		// Token: 0x04000021 RID: 33
		private string mimeType;
	}
}
