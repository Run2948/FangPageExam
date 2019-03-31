using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class exammanage : AdminController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected int typeid = FPRequest.GetInt("typeid");

		protected TypeInfo typeinfo = new TypeInfo();

		protected List<ExamInfo> examlist = new List<ExamInfo>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected string keyword = FPRequest.GetString("keyword");

		protected string starttime = FPRequest.GetString("starttime");

		protected string endtime = FPRequest.GetString("endtime");

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			bool flag = this.channelinfo.id == 0 && this.channelid > 0;
			if (flag)
			{
				this.ShowErr("对不起，该频道不存在或已被删除。");
			}
			else
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool flag2 = this.sortinfo.id == 0 && this.sortid > 0;
				if (flag2)
				{
					this.ShowErr("对不起，该试卷库不存在或已被删除。");
				}
				else
				{
					bool flag3 = this.channelid == 0 && this.sortinfo.id > 0;
					if (flag3)
					{
						this.channelid = this.sortinfo.channelid;
					}
					bool flag4 = this.sortinfo.id == 0;
					if (flag4)
					{
						this.sortinfo.name = this.channelinfo.name;
					}
					bool ispost = this.ispost;
					if (ispost)
					{
						string @string = FPRequest.GetString("chkid");
						bool flag5 = this.action == "delete";
						if (flag5)
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
						else
						{
							bool flag6 = this.action == "sum";
							if (flag6)
							{
								SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, @string);
								List<ExamInfo> list = DbHelper.ExecuteList<ExamInfo>(new SqlParam[]
								{
									sqlParam2
								});
								string text = "";
								foreach (ExamInfo current in list)
								{
									int num = 0;
									double num2 = 0.0;
									bool flag7 = current.examdeparts == "" && current.examuser == "" && current.examroles == "";
									if (flag7)
									{
										SqlParam sqlParam3 = DbHelper.MakeAndWhere("examid", current.id);
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
										bool flag8 = current.examroles != "";
										if (flag8)
										{
											SqlParam sqlParam4 = DbHelper.MakeAndWhere("roleid", WhereType.In, current.examroles);
											List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
											{
												sqlParam4
											});
											foreach (UserInfo current2 in list2)
											{
												bool flag9 = FPArray.InArray(current2.id, text2) == -1;
												if (flag9)
												{
													bool flag10 = text2 != "";
													if (flag10)
													{
														text2 += ",";
													}
													text2 += current2.id;
													num++;
												}
											}
										}
										bool flag11 = current.examdeparts != "";
										if (flag11)
										{
											SqlParam sqlParam5 = DbHelper.MakeAndWhere("departid", WhereType.In, current.examdeparts);
											List<UserInfo> list3 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
											{
												sqlParam5
											});
											foreach (UserInfo current3 in list3)
											{
												bool flag12 = FPArray.InArray(current3.id, text2) == -1;
												if (flag12)
												{
													bool flag13 = text2 != "";
													if (flag13)
													{
														text2 += ",";
													}
													text2 += current3.id;
													num++;
												}
											}
										}
										bool flag14 = current.examuser != "";
										if (flag14)
										{
											SqlParam sqlParam6 = DbHelper.MakeAndWhere("id", WhereType.In, current.examuser);
											List<UserInfo> list4 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
											{
												sqlParam6
											});
											foreach (UserInfo current4 in list4)
											{
												bool flag15 = FPArray.InArray(current4.id, text2) == -1;
												if (flag15)
												{
													bool flag16 = text2 != "";
													if (flag16)
													{
														text2 += ",";
													}
													text2 += current4.id;
													num++;
												}
											}
										}
										string sqlstring = string.Format("DELETE FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [uid] NOT IN({2})", DbConfigs.Prefix, current.id, text2);
										DbHelper.ExecuteSql(sqlstring);
										SqlParam[] sqlparams = new SqlParam[]
										{
											DbHelper.MakeAndWhere("examid", current.id),
											DbHelper.MakeAndWhere("uid", WhereType.In, text2)
										};
										num2 = FPUtils.StrToDouble(DbHelper.ExecuteSum<ExamResult>("score", sqlparams));
									}
									List<ExamTopic> examTopicList = ExamBll.GetExamTopicList(current.id, 1);
									double num3 = 0.0;
									int num4 = 0;
									foreach (ExamTopic current5 in examTopicList)
									{
										num3 += current5.perscore * (double)current5.questions;
										num4 += current5.questions;
									}
									bool flag17 = text != "";
									if (flag17)
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
										current.id
									});
								}
								DbHelper.ExecuteSql(text);
							}
						}
					}
					List<SqlParam> list5 = new List<SqlParam>();
					bool flag18 = this.channelid > 0;
					if (flag18)
					{
						list5.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
					}
					bool flag19 = this.sortid > 0;
					if (flag19)
					{
						string childSorts = SortBll.GetChildSorts(this.sortinfo);
						list5.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
					}
					bool flag20 = this.keyword != "";
					if (flag20)
					{
						list5.Add(DbHelper.MakeAndWhere("name", WhereType.Like, this.keyword));
					}
					bool flag21 = this.starttime != "" && this.isDate(this.starttime);
					if (flag21)
					{
						list5.Add(DbHelper.MakeAndWhere("islimit", 1));
						list5.Add(DbHelper.MakeAndWhere("starttime", WhereType.LessThanEqual, this.starttime));
					}
					bool flag22 = this.endtime != "" && this.isDate(this.endtime);
					if (flag22)
					{
						list5.Add(DbHelper.MakeAndWhere("islimit", 1));
						list5.Add(DbHelper.MakeAndWhere("endtime", WhereType.GreaterThanEqual, this.endtime));
					}
					bool flag23 = this.typeid > 0;
					if (flag23)
					{
						this.typeinfo = DbHelper.ExecuteModel<TypeInfo>(this.typeid);
						this.pagenav = "|" + this.typeinfo.name + "," + this.rawurl;
						list5.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, this.typeid));
					}
					this.examlist = DbHelper.ExecuteList<ExamInfo>(this.pager, list5.ToArray());
				}
			}
		}

		public bool isDate(string dateStr)
		{
			DateTime dateTime;
			return DateTime.TryParse(dateStr, out dateTime);
		}
	}
}
