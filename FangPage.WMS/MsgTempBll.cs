using System;
using FangPage.Data;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x0200002B RID: 43
	public class MsgTempBll
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00007B78 File Offset: 0x00005D78
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
