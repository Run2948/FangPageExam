using System;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS.Bll
{
	// Token: 0x02000037 RID: 55
	public class MsgTempBll
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A3EC File Offset: 0x000085EC
		public static MsgTempInfo GetMsgTemplate(string markup)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("markup", markup);
			return DbHelper.ExecuteModel<MsgTempInfo>(new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
