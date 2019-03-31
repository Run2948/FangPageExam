using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000009 RID: 9
	public class cachemanage : SuperController
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00002E08 File Offset: 0x00001008
		protected override void Controller()
		{
			if (this.ispost)
			{
				string @string = FPRequest.GetString("chkid");
				if (this.action == "remove")
				{
					SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, @string);
					this.cachelist = DbHelper.ExecuteList<CacheInfo>(new SqlParam[]
					{
						sqlParam
					});
					using (List<CacheInfo>.Enumerator enumerator = this.cachelist.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CacheInfo cacheInfo = enumerator.Current;
							FPCache.Remove(cacheInfo.key);
						}
						goto IL_103;
					}
				}
				if (this.action == "clear")
				{
					try
					{
						ViewConfigs.ReSetViewConfig();
						FPFile.ClearDir(FPFile.GetMapPath(this.webpath + "cache"));
						FPCache.Clear();
						goto IL_103;
					}
					catch (Exception ex)
					{
						this.ShowErr(ex.Message);
						goto IL_103;
					}
				}
				if (this.action == "delete")
				{
					int @int = FPRequest.GetInt("cacheid");
					FPCache.Remove(DbHelper.ExecuteModel<CacheInfo>(@int).key);
					DbHelper.ExecuteDelete<CacheInfo>(@int);
				}
				IL_103:
				base.Response.Redirect("cachemanage.aspx");
			}
			this.cachelist = DbHelper.ExecuteList<CacheInfo>();
			IDictionaryEnumerator enumerator2 = this.Context.Cache.GetEnumerator();
			string text = "";
			while (enumerator2.MoveNext())
			{
				if (text != "")
				{
					text += ",";
				}
				text += enumerator2.Key.ToString();
			}
			int num = 0;
			using (List<CacheInfo>.Enumerator enumerator = this.cachelist.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (FPArray.InArray(enumerator.Current.key, text) >= 0)
					{
						this.cachelist[num].status = 1;
						if (this.cachelist[num].file != "" && File.Exists(FPFile.GetMapPath(this.cachelist[num].file)))
						{
							this.cachelist[num].cachedatetime = File.GetLastWriteTime(FPFile.GetMapPath(this.cachelist[num].file));
							DbHelper.ExecuteUpdate<CacheInfo>(this.cachelist[num]);
						}
					}
					else
					{
						this.cachelist[num].status = 0;
					}
					num++;
				}
			}
			this.cachelist = this.cachelist.FindAll((CacheInfo item) => item.status == 1);
		}

		// Token: 0x04000013 RID: 19
		protected List<CacheInfo> cachelist = new List<CacheInfo>();
	}
}
