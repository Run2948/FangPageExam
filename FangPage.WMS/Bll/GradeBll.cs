using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x0200002C RID: 44
	public class GradeBll
	{
		// Token: 0x06000350 RID: 848 RVA: 0x000076A0 File Offset: 0x000058A0
		public static List<GradeInfo> GetGradeList()
		{
			object obj = FPCache.Get("FP_GRADELIST");
			List<GradeInfo> list;
			if (obj != null)
			{
				list = (obj as List<GradeInfo>);
			}
			else
			{
				SqlParam sqlParam = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				list = DbHelper.ExecuteList<GradeInfo>(new SqlParam[]
				{
					sqlParam
				});
				CacheBll.Insert("系统岗位信息缓存", "FP_GRADELIST", list);
			}
			return list;
		}
	}
}
