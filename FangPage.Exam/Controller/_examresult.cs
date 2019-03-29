using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x0200002A RID: 42
	public class _examresult : LoginController
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000130BC File Offset: 0x000112BC
		protected override void View()
		{
			this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
			if (this.examresult.id == 0)
			{
				this.ShowErr("该考生的试卷不存在或已被删除。");
			}
			else if (this.examresult.status == 0)
			{
				this.ShowErr("对不起，该考试尚未完成。");
			}
			else
			{
				this.examresult.passmark = this.examresult.passmark * this.examresult.total / 100.0;
				string commandText = string.Format("SELECT MAX([score]) AS [maxscore] FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [status]>0", DbConfigs.Prefix, this.examresult.examid);
				this.maxscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f), 1);
				commandText = string.Format("SELECT AVG([score]) AS [avgscore] FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [status]>0", DbConfigs.Prefix, this.examresult.examid);
				this.avgscore = Math.Round((double)FPUtils.StrToFloat(DbHelper.ExecuteScalar(commandText).ToString(), 0f));
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("examid", this.examresult.examid),
					DbHelper.MakeAndWhere("status", WhereType.GreaterThan, 0)
				};
				this.testers = DbHelper.ExecuteCount<ExamResult>(sqlparams);
				commandText = string.Format("SELECT COUNT(*) FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [score]>{2} AND [status]>0", DbConfigs.Prefix, this.examresult.examid, this.examresult.score);
				if (this.examresult.score > 0.0)
				{
					this.display = FPUtils.StrToInt(DbHelper.ExecuteScalar(commandText).ToString(), 0) + 1;
				}
				SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", this.resultid);
				OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
				this.examtopiclist = DbHelper.ExecuteList<ExamResultTopic>(orderby, new SqlParam[]
				{
					sqlParam
				});
				int num = Convert.ToInt32(this.examresult.total) / 5;
				int num2 = num / 2;
				if (num % 2 != 0)
				{
					num2++;
				}
				for (int i = 1; i < 10; i++)
				{
					if (i % 2 == 0)
					{
						this.bcklist.Add(i / 2 * num);
					}
					else
					{
						this.bcklist.Add(i * num2);
					}
				}
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00013350 File Offset: 0x00011550
		protected string CalRate(double myscore, double total)
		{
			return (myscore / total * 100.0).ToString("0.0");
		}

		// Token: 0x040000DC RID: 220
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x040000DD RID: 221
		protected ExamResult examresult = new ExamResult();

		// Token: 0x040000DE RID: 222
		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		// Token: 0x040000DF RID: 223
		protected double maxscore = 0.0;

		// Token: 0x040000E0 RID: 224
		protected double avgscore = 0.0;

		// Token: 0x040000E1 RID: 225
		protected int testers = 0;

		// Token: 0x040000E2 RID: 226
		protected int display = 0;

		// Token: 0x040000E3 RID: 227
		protected List<int> bcklist = new List<int>();
	}
}
