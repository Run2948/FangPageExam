using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000040 RID: 64
	public class typeadd : SuperController
	{
		// Token: 0x06000098 RID: 152 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		protected override void Controller()
		{
			this.backurl = "typemanage.aspx";
			if (this.id > 0)
			{
				this.typeinfo = DbHelper.ExecuteModel<TypeInfo>(this.id);
				if (this.typeinfo.id == 0)
				{
					FPCache.Remove("FP_TYPELIST");
					this.ShowErr("对不起，该类型已不存在。");
					return;
				}
				this.parentid = this.typeinfo.parentid;
			}
			if (this.ispost)
			{
				this.typeinfo.required = 0;
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
					base.AddMsg("更新类型成功！");
				}
				else
				{
					this.typeinfo.display = FPUtils.StrToInt(DbHelper.ExecuteMax<TypeInfo>("display").ToString()) + 1;
					if (DbHelper.ExecuteInsert<TypeInfo>(this.typeinfo) > 0)
					{
						DbHelper.ExecuteSql(string.Format("UPDATE [{0}WMS_TypeInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1}", DbConfigs.Prefix, this.typeinfo.parentid));
					}
					base.AddMsg("添加类型成功！");
				}
				if (this.typeinfo.img != "")
				{
					AttachBll.UpdateOnceAttach(this.typeinfo.attach_img, this.typeinfo.id);
				}
				else
				{
					AttachBll.Delete(this.typeinfo.attach_img);
				}
				CacheBll.RemoveSortCache();
				FPCache.Remove("FP_TYPELIST");
			}
			if (this.typeinfo.attach_img == "")
			{
				this.typeinfo.attach_img = FPRandom.CreateCode(20);
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.parenttypelist = DbHelper.ExecuteList<TypeInfo>(sqlparams);
		}

		// Token: 0x040000AE RID: 174
		protected int id = FPRequest.GetInt("id");

		// Token: 0x040000AF RID: 175
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000B0 RID: 176
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x040000B1 RID: 177
		protected TypeInfo typeinfo = new TypeInfo();

		// Token: 0x040000B2 RID: 178
		protected List<TypeInfo> parenttypelist = new List<TypeInfo>();
	}
}
