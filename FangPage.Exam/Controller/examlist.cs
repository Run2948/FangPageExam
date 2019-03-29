using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000023 RID: 35
	public class examlist : LoginController
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000F068 File Offset: 0x0000D268
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("考试频道不存在或已被删除。");
			}
			else
			{
				this.sortlist = SortBll.GetSortList(this.channelid, 0);
				List<SqlParam> list = new List<SqlParam>();
				list.Add(DbHelper.MakeAndWhere("status", 1));
				if (this.channelid > 0)
				{
					list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
				}
				if (this.sortid > 0)
				{
					string childSorts = SortBll.GetChildSorts(this.sortid);
					list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
				}
				if (FPUtils.IsNumericArray(this.typeid))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (int num in FPUtils.SplitInt(this.typeid))
					{
						if (!string.IsNullOrEmpty(stringBuilder.ToString()))
						{
							stringBuilder.Append(" OR ");
						}
						stringBuilder.AppendFormat("(','+[typelist]+',') LIKE '%,{0},%'", num);
					}
					list.Add(DbHelper.MakeAndWhere("(" + stringBuilder.ToString() + ")", WhereType.Custom, ""));
				}
				list.Add(DbHelper.MakeAndWhere(string.Format("(([examroles]='' AND [examdeparts]='' AND [examuser]='') OR (','+[examroles]+',') LIKE '%,{0},%' OR (','+[examdeparts]+',') LIKE '%,{1},%' OR (','+[examuser]+',') LIKE '%,{2},%')", this.roleid, this.departid, this.userid), WhereType.Custom, ""));
				this._examlist = DbHelper.ExecuteList<ExamInfo>(this.pager, list.ToArray());
				this.pagenav = this.channelinfo.name;
			}
		}

		// Token: 0x040000AA RID: 170
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x040000AB RID: 171
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x040000AC RID: 172
		protected string typeid = FPRequest.GetString("typeid");

		// Token: 0x040000AD RID: 173
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x040000AE RID: 174
		protected List<ExamInfo> _examlist = new List<ExamInfo>();

		// Token: 0x040000AF RID: 175
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x040000B0 RID: 176
		protected List<SortInfo> sortlist = new List<SortInfo>();
	}
}
