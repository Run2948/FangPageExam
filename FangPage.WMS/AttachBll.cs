using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS
{
	// Token: 0x02000002 RID: 2
	public class AttachBll
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string GetAttachTypeArray(string type)
		{
			string text = FPCache.Get<string>("FP_ATTACHTYPE" + type);
			if (text == null)
			{
				SqlParam sqlParam = DbHelper.MakeAndWhere("type", type.ToLower());
				List<AttachType> list = DbHelper.ExecuteList<AttachType>(new SqlParam[]
				{
					sqlParam
				});
				StringBuilder stringBuilder = new StringBuilder();
				foreach (AttachType attachType in list)
				{
					if (stringBuilder.ToString() != "")
					{
						stringBuilder.Append("\r\n");
					}
					stringBuilder.Append(attachType.extension);
					stringBuilder.Append(",");
					stringBuilder.Append(attachType.maxsize);
				}
				text = stringBuilder.ToString();
				FPCache.Insert("FP_ATTACHTYPE" + type, text, 120);
			}
			return text;
		}
	}
}
