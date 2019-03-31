using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000042 RID: 66
	public class typemanage : AdminController
	{
		// Token: 0x0600009C RID: 156 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		protected override void Controller()
		{
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				int @int = FPRequest.GetInt("id");
				if (this.action.Equals("delete"))
				{
					TypeInfo typeInfo = DbHelper.ExecuteModel<TypeInfo>(@int);
					if (DbHelper.ExecuteDelete<TypeInfo>(@int) > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("DELETE FROM [{0}WMS_TypeInfo] WHERE [id] IN (SELECT [id] FROM [{0}WMS_TypeInfo]  WHERE [parentid]={1};", DbConfigs.Prefix, typeInfo.id);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=[subcounts]-1 WHERE [id]={1};", DbConfigs.Prefix, typeInfo.parentid);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=0 WHERE [subcounts]<0", DbConfigs.Prefix);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
				}
				FPCache.Remove("FP_TYPELIST");
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		protected List<TypeInfo> GetChildType(int parentid)
		{
			return DbHelper.ExecuteList<TypeInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
		}

		// Token: 0x040000B7 RID: 183
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
