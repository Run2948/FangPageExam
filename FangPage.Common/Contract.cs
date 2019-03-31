using System;
using System.IO;

namespace FangPage.Common
{
	// Token: 0x02000005 RID: 5
	internal interface Contract
	{
		// Token: 0x0600000E RID: 14
		bool IsValid();

		// Token: 0x0600000F RID: 15
		string GetFileName();

		// Token: 0x06000010 RID: 16
		string GetMimeType();

		// Token: 0x06000011 RID: 17
		long GetFileLength();

		// Token: 0x06000012 RID: 18
		void Write(Stream output);
	}
}
