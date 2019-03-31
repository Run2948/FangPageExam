using System;
using System.Collections.Generic;
using FangPage.MVC;
using FangPage.WMS.API;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;

namespace FP_Course.WapController
{
	// Token: 0x02000009 RID: 9
	public class course_sortlist : APIController
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002D94 File Offset: 0x00000F94
		protected override void Controller()
		{
			List<SortInfo> sortList = SortBll.GetSortList(this.channelid, 0);
			List<object> list = new List<object>();
			foreach (SortInfo sortInfo in sortList)
			{
				var item = new
				{
					id = sortInfo.id,
					sortname = sortInfo.name,
					towitem = this.GetClassList(sortInfo.id)
				};
				list.Add(item);
			}
			FPResponse.WriteJson(list);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002E24 File Offset: 0x00001024
		private List<object> GetClassList(int parentid)
		{
			List<SortInfo> sortList = SortBll.GetSortList(parentid);
			List<object> list = new List<object>();
			foreach (SortInfo sortInfo in sortList)
			{
				var item = new
				{
					id = sortInfo.id,
					sortname = sortInfo.name
				};
				list.Add(item);
			}
			return list;
		}

		// Token: 0x04000033 RID: 51
		protected int channelid = FPRequest.GetInt("channelid");
	}
}
