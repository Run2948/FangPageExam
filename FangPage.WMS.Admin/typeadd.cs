using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000033 RID: 51
	public class typeadd : SuperController
	{
		// Token: 0x0600007B RID: 123 RVA: 0x0000B078 File Offset: 0x00009278
		protected override void View()
		{
			this.link = "typemanage.aspx";
			if (this.id > 0)
			{
				this.typeinfo = DbHelper.ExecuteModel<TypeInfo>(this.id);
				this.parentid = this.typeinfo.parentid;
			}
			if (this.ispost)
			{
				this.typeinfo = FPRequest.GetModel<TypeInfo>(this.typeinfo);
				if (this.typeinfo.id > 0)
				{
					if (DbHelper.ExecuteUpdate<TypeInfo>(this.typeinfo) > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						if (this.typeinfo.parentid != this.parentid)
						{
							stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=[subcounts]-1 WHERE [id]={1};", DbConfigs.Prefix, this.parentid);
							stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1};", DbConfigs.Prefix, this.typeinfo.parentid);
							stringBuilder.AppendFormat("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=0 WHERE [subcounts]<0", DbConfigs.Prefix);
							DbHelper.ExecuteSql(stringBuilder.ToString());
						}
					}
					base.AddMsg("更新分类成功！");
				}
				else
				{
					this.typeinfo.display = FPUtils.StrToInt(DbHelper.ExecuteMax<TypeInfo>("display").ToString()) + 1;
					if (DbHelper.ExecuteInsert<TypeInfo>(this.typeinfo) > 0)
					{
						string sqlstring = string.Format("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1}", DbConfigs.Prefix, this.typeinfo.parentid);
						DbHelper.ExecuteSql(sqlstring);
					}
					base.AddMsg("添加分类成功！");
				}
				CacheBll.RemoveSortCache();
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeAndWhere("parentid", 0)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.parenttypelist = DbHelper.ExecuteList<TypeInfo>(orderby, sqlparams);
			base.SaveRightURL();
		}

		// Token: 0x04000076 RID: 118
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000077 RID: 119
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000078 RID: 120
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000079 RID: 121
		protected TypeInfo typeinfo = new TypeInfo();

		// Token: 0x0400007A RID: 122
		protected List<TypeInfo> parenttypelist = new List<TypeInfo>();
	}
}
