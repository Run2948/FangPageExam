using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam.Controller
{
	public class examadd : AdminController
	{
		protected int sortid = FPRequest.GetInt("sortid");

		protected int id = FPRequest.GetInt("id");

		protected int tabactive = FPRequest.GetInt("tabactive", 1);

		protected ExamConfig examconfig = new ExamConfig();

		protected SortInfo sortinfo = new SortInfo();

		protected SortAppInfo sortappinfo = new SortAppInfo();

		protected List<TypeInfo> typelist = new List<TypeInfo>();

		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		protected ExamInfo examinfo = new ExamInfo();

		protected string zNodes = "";

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.id > 0;
			if (flag)
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.id);
				bool flag2 = this.examinfo.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该试卷不存在或已被删除。");
					return;
				}
				this.sortid = this.examinfo.sortid;
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool flag3 = this.sortinfo.id == 0;
			if (flag3)
			{
				this.ShowErr("对不起，该栏目不存在或已被删除。");
			}
			else
			{
				this.sortappinfo = SortBll.GetSortAppInfo(this.sortinfo.appid);
				bool flag4 = this.examinfo.islimit == 0;
				if (flag4)
				{
					this.examinfo.starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					this.examinfo.endtime = this.examinfo.starttime.AddMinutes((double)this.examinfo.examtime);
				}
				bool ispost = this.ispost;
				if (ispost)
				{
					bool flag5 = this.action == "saveas";
					if (flag5)
					{
						this.examinfo.name = this.examinfo.name + "_复制";
						this.examinfo.score = 0.0;
						this.examinfo.exams = 0;
						int examid = DbHelper.ExecuteInsert<ExamInfo>(this.examinfo);
						SqlParam sqlParam = DbHelper.MakeAndWhere("examid", this.id);
						List<ExpInfo> list = DbHelper.ExecuteList<ExpInfo>(OrderBy.ASC, new SqlParam[]
						{
							sqlParam
						});
						int num = 0;
						foreach (ExpInfo current in list)
						{
							list[num].examid = examid;
							DbHelper.ExecuteInsert<ExpInfo>(list[num]);
							num++;
						}
						List<ExamTopic> list2 = DbHelper.ExecuteList<ExamTopic>(new SqlParam[]
						{
							sqlParam
						});
						num = 0;
						foreach (ExamTopic current2 in list2)
						{
							list2[num].examid = examid;
							DbHelper.ExecuteInsert<ExamTopic>(list2[num]);
							num++;
						}
						base.AddMsg("考试另存成功！");
					}
					else
					{
						this.examinfo.islimit = 0;
						this.examinfo.showanswer = 0;
						this.examinfo.allowdelete = 0;
						this.examinfo.channelid = this.sortinfo.channelid;
						this.examinfo.examroles = "";
						this.examinfo = FPRequest.GetModel<ExamInfo>(this.examinfo);
						bool flag6 = this.examinfo.name == "";
						if (flag6)
						{
							this.ShowErr("考试名称不能为空。");
							return;
						}
						bool flag7 = this.isfree == 1;
						if (flag7)
						{
							bool flag8 = FPArray.SplitString(this.examinfo.examuser).Length > 30;
							if (flag8)
							{
								this.ShowErr("对不起，免费版考试人数不能超过30人。");
								return;
							}
						}
						bool flag9 = this.examinfo.papers <= 0;
						if (flag9)
						{
							this.examinfo.papers = 1;
						}
						bool flag10 = this.sortappinfo.markup == "exam_test";
						if (flag10)
						{
							this.examinfo.type = 1;
						}
						else
						{
							this.examinfo.type = 0;
						}
						bool flag11 = this.examinfo.islimit == 1;
						if (flag11)
						{
							bool flag12 = this.examinfo.endtime <= this.examinfo.starttime;
							if (flag12)
							{
								this.ShowErr("对不起，终止时间必须大于开始时间。");
								return;
							}
						}
						bool flag13 = this.examinfo.id > 0;
						if (flag13)
						{
							DbHelper.ExecuteUpdate<ExamInfo>(this.examinfo);
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [examname]='{1}',[passmark]={2},[showanswer]={3},[allowdelete]={4} WHERE [examid]={5}|", new object[]
							{
								DbConfigs.Prefix,
								DbUtils.RegEsc(this.examinfo.name),
								this.examinfo.passmark,
								this.examinfo.showanswer,
								this.examinfo.allowdelete,
								this.examinfo.id
							});
							bool flag14 = DbConfigs.DbType == DbType.Access;
							if (flag14)
							{
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [examtime]={1},[examtype]={2},[islimit]={3},[endtime]=#{4}# WHERE [examid]={5} AND [status]=0", new object[]
								{
									DbConfigs.Prefix,
									this.examinfo.examtime,
									this.examinfo.examtype,
									this.examinfo.islimit,
									this.examinfo.endtime,
									this.examinfo.id
								});
							}
							else
							{
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [examtime]={1},[examtype]={2},[islimit]={3},[endtime]='{4}' WHERE [examid]={5} AND [status]=0", new object[]
								{
									DbConfigs.Prefix,
									this.examinfo.examtime,
									this.examinfo.examtype,
									this.examinfo.islimit,
									this.examinfo.endtime,
									this.examinfo.id
								});
							}
							DbHelper.ExecuteSql(stringBuilder.ToString());
							base.AddMsg("更新考试成功！");
						}
						else
						{
							this.examinfo.uid = this.userid;
							this.examinfo.id = DbHelper.ExecuteInsert<ExamInfo>(this.examinfo);
							ExamBll.AddExamLarge(this.examinfo.id);
							base.AddMsg("添加考试成功！");
						}
						int num2 = 0;
						bool flag15 = this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "";
						double num3;
						if (flag15)
						{
							SqlParam sqlParam2 = DbHelper.MakeAndWhere("examid", this.examinfo.id);
							num2 = DbHelper.ExecuteCount<ExamResult>(new SqlParam[]
							{
								sqlParam2
							});
							num3 = FPUtils.StrToDouble(DbHelper.ExecuteSum<ExamResult>("score", new SqlParam[]
							{
								sqlParam2
							}));
						}
						else
						{
							string text = "";
							bool flag16 = this.examinfo.examroles != "";
							if (flag16)
							{
								SqlParam sqlParam3 = DbHelper.MakeAndWhere("roleid", WhereType.In, this.examinfo.examroles);
								List<UserInfo> list3 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam3
								});
								foreach (UserInfo current3 in list3)
								{
									bool flag17 = FPArray.InArray(current3.id, text) == -1;
									if (flag17)
									{
										bool flag18 = text != "";
										if (flag18)
										{
											text += ",";
										}
										text += current3.id;
										num2++;
									}
								}
							}
							bool flag19 = this.examinfo.examdeparts != "";
							if (flag19)
							{
								SqlParam sqlParam4 = DbHelper.MakeAndWhere("departid", WhereType.In, this.examinfo.examdeparts);
								List<UserInfo> list4 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam4
								});
								foreach (UserInfo current4 in list4)
								{
									bool flag20 = FPArray.InArray(current4.id, text) == -1;
									if (flag20)
									{
										bool flag21 = text != "";
										if (flag21)
										{
											text += ",";
										}
										text += current4.id;
										num2++;
									}
								}
							}
							bool flag22 = this.examinfo.examuser != "";
							if (flag22)
							{
								SqlParam sqlParam5 = DbHelper.MakeAndWhere("id", WhereType.In, this.examinfo.examuser);
								List<UserInfo> list5 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam5
								});
								foreach (UserInfo current5 in list5)
								{
									bool flag23 = FPArray.InArray(current5.id, text) == -1;
									if (flag23)
									{
										bool flag24 = text != "";
										if (flag24)
										{
											text += ",";
										}
										text += current5.id;
										num2++;
									}
								}
							}
							string sqlstring = string.Format("DELETE FROM [{0}Exam_ExamResult] WHERE [examid]={1} AND [uid] NOT IN({2})", DbConfigs.Prefix, this.examinfo.id, text);
							DbHelper.ExecuteSql(sqlstring);
							SqlParam[] sqlparams = new SqlParam[]
							{
								DbHelper.MakeAndWhere("examid", this.examinfo.id),
								DbHelper.MakeAndWhere("uid", WhereType.NotIn, text)
							};
							num3 = FPUtils.StrToDouble(DbHelper.ExecuteSum<ExamResult>("score", sqlparams));
						}
						string sqlstring2 = string.Format("UPDATE [{0}Exam_ExamInfo] SET [exams]={1},[score]={2} WHERE [id]={3}", new object[]
						{
							DbConfigs.Prefix,
							num2,
							num3,
							this.examinfo.id
						});
						DbHelper.ExecuteSql(sqlstring2);
					}
					object obj = FPCache.Get("ExamQuestionReset");
					bool flag25 = obj == null;
					if (flag25)
					{
						QuestionBll.ResetQuestionSort();
						FPCache.Insert("ExamQuestionReset", "已重置题库统计。");
					}
					bool flag26 = this.tabactive == 1;
					if (flag26)
					{
						this.link = "exammanage.aspx?sortid=" + this.sortid;
					}
					else
					{
						this.link = string.Concat(new object[]
						{
							"examadd.aspx?sortid=",
							this.sortid,
							"&tabactive=",
							this.tabactive,
							"&id=",
							this.examinfo.id
						});
					}
				}
				else
				{
					this.rolelist = DbHelper.ExecuteList<RoleInfo>();
					SqlParam[] sqlparams2 = new SqlParam[]
					{
						DbHelper.MakeAndWhere("id", WhereType.In, this.sortinfo.types),
						DbHelper.MakeOrderBy("display", OrderBy.ASC)
					};
					this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams2);
					this.zNodes = this.GetDepartTree(0);
				}
			}
		}

		private string GetDepartTree(int parentid)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			List<Department> list = DbHelper.ExecuteList<Department>(sqlparams);
			string text = "";
			foreach (Department current in list)
			{
				bool flag = text != "";
				if (flag)
				{
					text += ",";
				}
				string text2 = "";
				bool flag2 = FPArray.Contain(this.examinfo.examdeparts, current.id);
				if (flag2)
				{
					text2 = "checked:true,";
				}
				bool flag3 = current.subcounts > 0;
				if (flag3)
				{
					text = string.Concat(new object[]
					{
						text,
						"{ id: ",
						current.id,
						", pId: ",
						parentid,
						", name: \"",
						current.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.adminpath,
						"statics/images/usergroups.gif\" }"
					});
					text = text + "," + this.GetDepartTree(current.id);
				}
				else
				{
					text = string.Concat(new object[]
					{
						text,
						"{ id: ",
						current.id,
						", pId: ",
						parentid,
						", name: \"",
						current.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.adminpath,
						"statics/images/users.gif\" }"
					});
				}
			}
			return text;
		}
	}
}
