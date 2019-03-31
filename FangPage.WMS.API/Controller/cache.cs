using System;
using FangPage.MVC;

namespace FangPage.WMS.API.Controller
{
	// Token: 0x02000008 RID: 8
	public class cache : FPController
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000025D3 File Offset: 0x000007D3
		protected override void Controller()
		{
			if (this.key != "")
			{
				FPCache.Remove(this.key);
				FPResponse.SendData("ok");
				return;
			}
			FPResponse.SendData("false");
		}

		// Token: 0x04000003 RID: 3
		protected string key = FPRequest.GetString("key");
	}
}
