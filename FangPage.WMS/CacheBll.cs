using System;
using System.Collections.Generic;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000003 RID: 3
	public class CacheBll
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000216C File Offset: 0x0000036C
		public static void RemoveSortCache()
		{
			List<ChannelInfo> list = new List<ChannelInfo>();
			foreach (ChannelInfo channelInfo in list)
			{
				FPCache.Remove("FP_SORTTREE" + channelInfo.id);
			}
		}
	}
}
