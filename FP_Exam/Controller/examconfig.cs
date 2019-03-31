using System;
using System.Collections.Generic;
using FangPage.Data;
using FP_Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000008 RID: 8
	public class examconfig : AdminController
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00005364 File Offset: 0x00003564
		protected override void View()
		{
			this.examconfiginfo = ExamConifgs.GetExamConfig();
			if (this.ispost)
			{
				if (this.action == "save")
				{
					this.examconfiginfo.showanswer = 0;
					this.examconfiginfo = FPRequest.GetModel<ExamConfig>(this.examconfiginfo);
					if (this.examconfiginfo.testcount == 0)
					{
						this.examconfiginfo.testcount = 80;
					}
					if (this.examconfiginfo.testtime == 0)
					{
						this.examconfiginfo.testtime = 60;
					}
					ExamConifgs.SaveConfig(this.examconfiginfo);
					base.AddMsg("考试配置保存成功。");
				}
				else if (this.action == "reset" || this.action == "clear")
				{
					ChannelInfo channelInfo = new ChannelInfo();
					channelInfo = ChannelBll.GetChannelInfo("exam_question");
					if (channelInfo.id > 0)
					{
						SortAppInfo sortAppInfo = SortBll.GetSortAppInfo("exam_question");
						if (sortAppInfo.id > 0)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("appid", sortAppInfo.id);
							List<SortInfo> sortlist = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
							{
								sqlParam
							});
							SortBll.ResetSortPosts<ExamQuestion>(sortlist);
							sqlParam = DbHelper.MakeAndWhere("type", WhereType.In, "1,2");
							List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
							{
								sqlParam
							});
							for (int i = 0; i < list.Count; i++)
							{
								string text = "";
								int num = 0;
								foreach (string text2 in FPUtils.SplitString(list[i].content, "§"))
								{
									if (text2 != "")
									{
										if (text != "")
										{
											text += "§";
										}
										text += text2;
										num++;
									}
								}
								list[i].content = text;
								list[i].ascount = num;
								DbHelper.ExecuteUpdate<ExamQuestion>(list[i]);
							}
							base.AddMsg("题库统计重置成功。");
						}
					}
				}
				else if (this.action == "clear")
				{
					DbHelper.ExecuteDelete<ExamQuestion>(new SqlParam[0]);
					SortAppInfo sortAppInfo = SortBll.GetSortAppInfo("exam_question");
					if (sortAppInfo.id > 0)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("appid", sortAppInfo.id);
						List<SortInfo> sortlist = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
						{
							sqlParam
						});
						SortBll.ResetSortPosts<ExamQuestion>(sortlist);
					}
					base.AddMsg("题库清空成功。");
				}
			}
		}

		// Token: 0x04000010 RID: 16
		protected ExamConfig examconfiginfo = new ExamConfig();
	}
}
