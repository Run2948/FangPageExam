using System;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000026 RID: 38
	public class channeladd : SuperController
	{
		// Token: 0x0600005A RID: 90 RVA: 0x0000820C File Offset: 0x0000640C
		protected override void View()
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
				this.link = "channelmanage.aspx";
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
						RoleInfo roleInfo = RoleBll.GetRoleInfo(1);
						RoleInfo roleInfo2 = roleInfo;
						roleInfo2.menus += ((roleInfo.menus == "") ? menuInfo.id.ToString() : ("," + menuInfo.id));
						DbHelper.ExecuteUpdate<RoleInfo>(roleInfo);
					}
					base.AddMsg("添加频道成功!");
				}
			}
			base.SaveRightURL();
		}

		// Token: 0x0400004B RID: 75
		protected int id = FPRequest.GetInt("id");

		// Token: 0x0400004C RID: 76
		protected ChannelInfo channelinfo = new ChannelInfo();
	}
}
