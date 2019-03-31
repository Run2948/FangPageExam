using System;
using System.Collections.Generic;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000033 RID: 51
	public class channeladd : SuperController
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00009BAC File Offset: 0x00007DAC
		protected override void Controller()
		{
			if (this.id > 0)
			{
				this.channelinfo = DbHelper.ExecuteModel<ChannelInfo>(this.id);
			}
			else
			{
				this.channelinfo.display = FPUtils.StrToInt(DbHelper.ExecuteMax<ChannelInfo>("display").ToString()) + 1;
			}
			if (this.ispost)
			{
				this.backurl = "channelmanage.aspx";
				this.channelinfo.sortapps = "";
				this.channelinfo = FPRequest.GetModel<ChannelInfo>(this.channelinfo);
				if (this.channelinfo.id > 0)
				{
					DbHelper.ExecuteUpdate<ChannelInfo>(this.channelinfo);
					base.AddMsg("更新频道成功!");
				}
				else
				{
					this.channelinfo.id = DbHelper.ExecuteInsert<ChannelInfo>(this.channelinfo);
					if (this.channelinfo.id > 0 && FPRequest.GetInt("ismenu") == 1)
					{
						MenuInfo menuInfo = new MenuInfo();
						menuInfo.name = this.channelinfo.name;
						SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", menuInfo.parentid);
						menuInfo.display = DbHelper.ExecuteCount<MenuInfo>(new SqlParam[]
						{
							sqlParam
						}) + 1;
						menuInfo.lefturl = "sorttree.aspx?channelid=" + this.channelinfo.id;
						menuInfo.id = DbHelper.ExecuteInsert<MenuInfo>(menuInfo);
						RoleInfo mapRoleInfo = RoleBll.GetMapRoleInfo(1);
						RoleInfo roleInfo = mapRoleInfo;
						roleInfo.menus += ((mapRoleInfo.menus == "") ? menuInfo.id.ToString() : ("," + menuInfo.id));
						DbHelper.ExecuteUpdate<RoleInfo>(mapRoleInfo);
					}
					base.AddMsg("添加频道成功!");
				}
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC);
		}

		// Token: 0x04000081 RID: 129
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000082 RID: 130
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000083 RID: 131
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();
	}
}
