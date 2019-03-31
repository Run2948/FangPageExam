using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;

namespace FP_Exam.Wap.Controller
{
	public class examview : LoginController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected ExamInfo examinfo = new ExamInfo();

		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				base.AddErr("对不起，该考试不存在或已被删除。");
			}
			else
			{
				bool flag2 = this.examinfo.status == 0;
				if (flag2)
				{
					base.AddErr("对不起，该考试已关闭。");
				}
				else
				{
					bool flag3 = this.examconfig.teststatus == 0 && this.examinfo.type == 1;
					if (flag3)
					{
						base.AddErr("对不起，考试系统已关闭了用户练习。");
					}
					else
					{
						bool flag4 = this.examinfo.examroles != "";
						if (flag4)
						{
							bool flag5 = !FPArray.Contain(this.examinfo.examroles, this.roleid) && !this.isperm;
							if (flag5)
							{
								base.AddErr("对不起，您所在的角色不允许参加本场考试。");
								return;
							}
						}
						bool flag6 = this.examinfo.examdeparts != "";
						if (flag6)
						{
							bool flag7 = !FPArray.Contain(this.examinfo.examdeparts, this.user.departid) && !this.isperm;
							if (flag7)
							{
								base.AddErr("对不起，您所在的部门不允许参加本场考试。");
								return;
							}
						}
						bool flag8 = this.examinfo.examuser != "";
						if (flag8)
						{
							bool flag9 = !FPArray.Contain(this.examinfo.examuser, this.userid) && !this.isperm;
							if (flag9)
							{
								base.AddErr("对不起，您不允许参加本场考试。");
								return;
							}
						}
						bool flag10 = this.examinfo.islimit == 1;
						if (flag10)
						{
							bool flag11 = this.examinfo.starttime > DateTime.Now;
							if (flag11)
							{
								base.AddErr("对不起，本场考试尚未到开考时间。");
								return;
							}
							bool flag12 = this.examinfo.endtime < DateTime.Now;
							if (flag12)
							{
								base.AddErr("对不起，本场考试已超过了考试期限。");
								return;
							}
						}
						bool flag13 = this.examinfo.repeats > 0;
						if (flag13)
						{
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeAndWhere("examid", this.examid),
								DbHelper.MakeAndWhere("uid", this.userid),
								DbHelper.MakeAndWhere("status", WhereType.GreaterThanEqual, 1)
							};
							int num = DbHelper.ExecuteCount<ExamResult>(sqlparams);
							bool flag14 = num >= this.examinfo.repeats;
							if (flag14)
							{
								base.AddErr("对不起，本场考试限制次数为" + this.examinfo.repeats + "次，您已考完不能再考。");
							}
						}
					}
				}
			}
		}
	}
}
