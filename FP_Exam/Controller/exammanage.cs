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
	// Token: 0x0200000A RID: 10
	public class exammanage : AdminController
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000059E8 File Offset: 0x00003BE8
		protected override void View()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.sortinfo.id == 0)
			{
				this.ShowErr("对不起，该试卷库不存在或已被删除。");
			}
			else
			{
				if (this.channelid == 0)
				{
					this.channelid = this.sortinfo.channelid;
				}
				if (this.ispost)
				{
					string @string = FPRequest.GetString("chkid");
					if (this.action == "delete")
					{
						string examSorts = ExamBll.GetExamSorts(@string);
						SortBll.UpdateSortPosts(examSorts, -1);
						DbHelper.ExecuteDelete<ExamInfo>(@string);
						SqlParam sqlParam = DbHelper.MakeAndWhere("examid", WhereType.In, @string);
						DbHelper.ExecuteDelete<ExamTopic>(new SqlParam[]
						{
							sqlParam
						});
					}
					else if (this.action == "sum")
					{
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, @string);
						List<ExamInfo> list = DbHelper.ExecuteList<ExamInfo>(new SqlParam[]
						{
							sqlParam2
						});
						string text = "";
						foreach (ExamInfo examInfo in list)
						{
							int num = 0;
							double num2 = 0.0;
							if (examInfo.examdeparts == "" && examInfo.examuser == "" && examInfo.examroles == "")
							{
								SqlParam sqlParam3 = DbHelper.MakeAndWhere("examid", examInfo.id);
								num = DbHelper.ExecuteCount<ExamResult>(new SqlParam[]
								{
									sqlParam3
								});
								num2 = FPUtils.StrToDouble(DbHelper.ExecuteSum<ExamResult>("score", new SqlParam[]
								{
									sqlParam3
								}));
							}
							else
							{
								string text2 = "";
								if (examInfo.examroles != "")
								{
									SqlParam sqlParam4 = DbHelper.MakeAndWhere("roleid", WhereType.In, examInfo.examroles);
									List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
									{
										sqlParam4
									});
									foreach (UserInfo userInfo in list2)
									{
										if (!FPUtils.InArray(userInfo.id, text2))
										{
											if (text2 != "")
											{
												text2 += ",";
											}
											text2 += userInfo.id;
											num++;
										}
									}
								}
								if (examInfo.examdeparts != "")
								{
									SqlParam sqlParam4 = DbHelper.MakeAndWhere("departid", WhereType.In, examInfo.examdeparts);
									List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
									{
										sqlParam4
									});
									foreach (UserInfo userInfo in list2)
									{
										if (!FPUtils.InArray(userInfo.id, text2))
										{
											if (text2 != "")
											{
												text2 += ",";
											}
											text2 += userInfo.id;
											num++;
										}
									}
								}
								if (examInfo.examuser != "")
								{
									SqlParam sqlParam4 = DbHelper.MakeAndWhere("id", WhereType.In, examInfo.examuser);
									List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
									{
										sqlParam4
									});
									foreach (UserInfo userInfo in list2)
									{
										if (!FPUtils.InArray(userInfo.id, text2))
										{
											if (text2 != "")
											{
												text2 += ",";
											}
											text2 += userInfo.id;
											num++;
										}
									}
								}
								string sqlstring = string.Format("DELETE FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [uid] NOT IN({2})", DbConfigs.Prefix, examInfo.id, text2);
								DbHelper.ExecuteSql(sqlstring);
								SqlParam[] sqlparams = new SqlParam[]
								{
									DbHelper.MakeAndWhere("examid", examInfo.id),
									DbHelper.MakeAndWhere("uid", WhereType.In, text2)
								};
								num2 = FPUtils.StrToDouble(DbHelper.ExecuteSum<ExamResult>("score", sqlparams));
							}
							List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(examInfo.id, 1);
							double num3 = 0.0;
							int num4 = 0;
							foreach (ExamTopic examTopic in examTopicList)
							{
								num3 += examTopic.perscore * (double)examTopic.questions;
								num4 += examTopic.questions;
							}
							if (text != "")
							{
								text += "|";
							}
							text += string.Format("UPDATE [{0}Exam_ExamInfo] SET [exams]={1},[score]={2},[questions]={3},[total]={4} WHERE [id]={5}", new object[]
							{
								DbConfigs.Prefix,
								num,
								num2,
								num4,
								num3,
								examInfo.id
							});
						}
						DbHelper.ExecuteSql(text);
					}
				}
				string childSorts = SortBll.GetChildSorts(this.sortinfo);
				List<SqlParam> list3 = new List<SqlParam>();
				list3.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
				if (this.keyword != "")
				{
					list3.Add(DbHelper.MakeAndWhere("name", WhereType.Like, this.keyword));
				}
				if (this.starttime != "" && this.isDate(this.starttime))
				{
					list3.Add(DbHelper.MakeAndWhere("islimit", 1));
					list3.Add(DbHelper.MakeAndWhere("starttime", WhereType.LessThanEqual, this.starttime));
				}
				if (this.endtime != "" && this.isDate(this.endtime))
				{
					list3.Add(DbHelper.MakeAndWhere("islimit", 1));
					list3.Add(DbHelper.MakeAndWhere("endtime", WhereType.GreaterThanEqual, this.endtime));
				}
				if (this.typeid > 0)
				{
					this.typeinfo = DbHelper.ExecuteModel<TypeInfo>(this.typeid);
					this.pagenav = "|" + this.typeinfo.name + "," + this.rawurl;
					list3.Add(DbHelper.MakeAndWhere(string.Format("(','+[typeid]+',') LIKE '%,{0},%'", this.typeid), WhereType.Custom, ""));
				}
				this.examlist = DbHelper.ExecuteList<ExamInfo>(this.pager, list3.ToArray());
				base.SaveRightURL();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00006220 File Offset: 0x00004420
		public bool isDate(string dateStr)
		{
			DateTime dateTime;
			return DateTime.TryParse(dateStr, out dateTime);
		}

		// Token: 0x04000018 RID: 24
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000019 RID: 25
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400001A RID: 26
		protected int typeid = FPRequest.GetInt("typeid");

		// Token: 0x0400001B RID: 27
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400001C RID: 28
		protected TypeInfo typeinfo = new TypeInfo();

		// Token: 0x0400001D RID: 29
		protected List<ExamInfo> examlist = new List<ExamInfo>();

		// Token: 0x0400001E RID: 30
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x0400001F RID: 31
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x04000020 RID: 32
		protected string starttime = FPRequest.GetString("starttime");

		// Token: 0x04000021 RID: 33
		protected string endtime = FPRequest.GetString("endtime");
	}
}
