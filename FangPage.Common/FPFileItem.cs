using System;
using System.IO;

namespace FangPage.Common
{
	// Token: 0x02000004 RID: 4
	public class FPFileItem
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
		public FPFileItem(FileInfo fileInfo)
		{
			this.contract = new LocalContract(fileInfo);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		public FPFileItem(string filePath) : this(new FileInfo(filePath))
		{
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207F File Offset: 0x0000027F
		public FPFileItem(string fileName, byte[] content) : this(fileName, content, null)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208C File Offset: 0x0000028C
		public FPFileItem(string fileName, byte[] content, string mimeType)
		{
			this.contract = new ByteArrayContract(fileName, content, mimeType);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A4 File Offset: 0x000002A4
		public FPFileItem(string fileName, Stream stream) : this(fileName, stream, null)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B1 File Offset: 0x000002B1
		public FPFileItem(string fileName, Stream stream, string mimeType)
		{
			this.contract = new StreamContract(fileName, stream, mimeType);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020CC File Offset: 0x000002CC
		public bool IsValid()
		{
			return this.contract.IsValid();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020EC File Offset: 0x000002EC
		public long GetFileLength()
		{
			return this.contract.GetFileLength();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000210C File Offset: 0x0000030C
		public string GetFileName()
		{
			return this.contract.GetFileName();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000212C File Offset: 0x0000032C
		public string GetMimeType()
		{
			return this.contract.GetMimeType();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002149 File Offset: 0x00000349
		public void Write(Stream output)
		{
			this.contract.Write(output);
		}

		// Token: 0x0400001A RID: 26
		private Contract contract;
	}
}
