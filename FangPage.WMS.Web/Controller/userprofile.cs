using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;

namespace FangPage.WMS.Web.Controller
{
	// Token: 0x0200000F RID: 15
	public class userprofile : LoginController
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00003704 File Offset: 0x00001904
		protected override void Controller()
		{
			this.fulluserinfo = DbHelper.ExecuteModel<UserInfo>(this.userid);
			if (this.fulluserinfo.id == 0)
			{
				this.ShowErr("对不起，该用户不存在或已被删除。");
				return;
			}
			if (this.ispost)
			{
				this.fulluserinfo = FPRequest.GetModel<UserInfo>(this.fulluserinfo);
				DbHelper.ExecuteUpdate<UserInfo>(this.fulluserinfo);
				base.ResetUser();
				base.AddMsg("信息更新成功！");
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			this.departlist = DbHelper.ExecuteList<Department>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003798 File Offset: 0x00001998
		protected string GetChildDepartment(int parentid, string tree)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			List<Department> list = DbHelper.ExecuteList<Department>(OrderBy.ASC, new SqlParam[]
			{
				sqlParam
			});
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (Department department in list)
			{
				stringBuilder.Append("  <option value=\"" + department.id + "\"");
				if (department.id == this.fulluserinfo.departid)
				{
					stringBuilder.Append("   selected=\"selected\"   ");
				}
				stringBuilder.AppendFormat(">{0}</option>", tree + department.name);
				stringBuilder.AppendLine(this.GetChildDepartment(department.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000006 RID: 6
		protected UserInfo fulluserinfo = new UserInfo();

		// Token: 0x04000007 RID: 7
		protected List<Department> departlist = new List<Department>();
	}
}
