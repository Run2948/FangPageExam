using System;
using FangPage.MVC;
using FangPage.WMS.Config;

namespace FP_SSO
{
	// Token: 0x02000004 RID: 4
	public class SSOController : FPController
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021A2 File Offset: 0x000003A2
		protected override void Init()
		{
			base.Init();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AA File Offset: 0x000003AA
		protected void WriteErr(string errmsg)
		{
			FPResponse.WriteJson(new
			{
				errcode = 1,
				errmsg = errmsg
			});
		}

		// Token: 0x04000003 RID: 3
		protected SysConfig sysconfig = SysConfigs.GetConfig();

		// Token: 0x04000004 RID: 4
		protected string token = FPRequest.GetString("token");
	}
}
