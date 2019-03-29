using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000035 RID: 53
	public class typemanage : AdminController
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000B47C File Offset: 0x0000967C
		protected override void View()
		{
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
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
			}
			this.typelist = DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000B59C File Offset: 0x0000979C
		protected List<TypeInfo> GetChildType(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			return DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x0400007F RID: 127
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
