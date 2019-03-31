using System;
using System.IO;

namespace FangPage.Common
{
	// Token: 0x02000007 RID: 7
	internal class ByteArrayContract : Contract
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000225C File Offset: 0x0000045C
		public ByteArrayContract(string fileName, byte[] content, string mimeType)
		{
			this.fileName = fileName;
			this.content = content;
			this.mimeType = mimeType;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000227C File Offset: 0x0000047C
		public bool IsValid()
		{
			return this.content != null && this.fileName != null;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022A4 File Offset: 0x000004A4
		public long GetFileLength()
		{
			return (long)this.content.Length;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022C0 File Offset: 0x000004C0
		public string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022D8 File Offset: 0x000004D8
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

		// Token: 0x0600001E RID: 30 RVA: 0x00002309 File Offset: 0x00000509
		public void Write(Stream output)
		{
			output.Write(this.content, 0, this.content.Length);
		}

		// Token: 0x0400001C RID: 28
		private string fileName;

		// Token: 0x0400001D RID: 29
		private byte[] content;

		// Token: 0x0400001E RID: 30
		private string mimeType;
	}
}
