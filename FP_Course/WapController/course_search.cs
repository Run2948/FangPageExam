using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.API;
using FP_Course.Model;

namespace FP_Course.WapController
{
	// Token: 0x02000008 RID: 8
	public class course_search : APIController
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002D18 File Offset: 0x00000F18
		protected override void Controller()
		{
			List<SqlParam> list = new List<SqlParam>();
			int @int = FPRequest.GetInt("sortid");
			bool flag = @int > 0;
			if (flag)
			{
				list.Add(DbHelper.MakeAndWhere("sortid", @int));
			}
			List<CourseInfo> courselist = DbHelper.ExecuteList<CourseInfo>(this.pager, list.ToArray());
			var o = new
			{
				this.pager,
				courselist
			};
			FPResponse.WriteJson(o);
		}

		// Token: 0x04000032 RID: 50
		protected Pager pager = FPRequest.GetModel<Pager>();
	}
}
