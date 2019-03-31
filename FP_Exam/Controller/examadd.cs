using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FP_Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000007 RID: 7
	public class examadd : AdminController
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00004444 File Offset: 0x00002644
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			if (this.id > 0)
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.id);
				if (this.examinfo.id == 0)
				{
					this.ShowErr("对不起，该试卷不存在或已被删除。");
					return;
				}
				this.sortid = this.examinfo.sortid;
			}
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.sortinfo.id == 0)
			{
				this.ShowErr("对不起，该栏目不存在或已被删除。");
			}
			else
			{
				if (this.examinfo.islimit == 0)
				{
					this.examinfo.starttime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
					this.examinfo.endtime = this.examinfo.starttime.AddMinutes((double)this.examinfo.examtime);
				}
				if (this.ispost)
				{
					if (this.action == "saveas")
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
						foreach (ExpInfo expInfo in list)
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
						foreach (ExamTopic examTopic in list2)
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
						this.examinfo.iscopy = 0;
						this.examinfo.examtype = 0;
						this.examinfo.channelid = this.sortinfo.channelid;
						this.examinfo.examroles = "";
						this.examinfo = FPRequest.GetModel<ExamInfo>(this.examinfo);
						if (this.examinfo.name == "")
						{
							this.ShowErr("考试名称不能为空。");
							return;
						}
						if (this.examinfo.papers <= 0)
						{
							this.examinfo.papers = 1;
						}
						if (this.examinfo.islimit == 1)
						{
							if (this.examinfo.endtime <= this.examinfo.starttime)
							{
								this.ShowErr("对不起，考试结束时间必须大于开始时间。");
								return;
							}
							TimeSpan timeSpan = new TimeSpan(this.examinfo.endtime.Ticks);
							TimeSpan ts = new TimeSpan(this.examinfo.starttime.Ticks);
							TimeSpan timeSpan2 = timeSpan.Subtract(ts).Duration();
							this.examinfo.examtime = Convert.ToInt32(timeSpan2.TotalMinutes);
						}
						if (this.examinfo.id > 0)
						{
							DbHelper.ExecuteUpdate<ExamInfo>(this.examinfo);
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [examname]='{1}' WHERE [examid]={2}|", DbConfigs.Prefix, DbUtils.RegEsc(this.examinfo.name), this.examinfo.id);
							if (DbConfigs.DbType == DbType.Access)
							{
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [passmark]={1},[examtime]={2},[examtype]={3},[showanswer]={4},[allowdelete]={5},[islimit]={6},[endtime]=#{7}# WHERE [examid]={8} AND [status]=0", new object[]
								{
									DbConfigs.Prefix,
									this.examinfo.passmark,
									this.examinfo.examtime,
									this.examinfo.examtype,
									this.examinfo.showanswer,
									this.examinfo.allowdelete,
									this.examinfo.islimit,
									this.examinfo.endtime,
									this.examinfo.id
								});
							}
							else
							{
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamResult] SET [passmark]={1},[examtime]={2},[examtype]={3},[showanswer]={4},[allowdelete]={5},[islimit]={6},[endtime]='{7}' WHERE [examid]={8} AND [status]=0", new object[]
								{
									DbConfigs.Prefix,
									this.examinfo.passmark,
									this.examinfo.examtime,
									this.examinfo.examtype,
									this.examinfo.showanswer,
									this.examinfo.allowdelete,
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
						double num3;
						if (this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "")
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
							if (this.examinfo.examroles != "")
							{
								SqlParam sqlParam3 = DbHelper.MakeAndWhere("roleid", WhereType.In, this.examinfo.examroles);
								List<UserInfo> list3 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam3
								});
								foreach (UserInfo userInfo in list3)
								{
									if (!FPUtils.InArray(userInfo.id, text))
									{
										if (text != "")
										{
											text += ",";
										}
										text += userInfo.id;
										num2++;
									}
								}
							}
							if (this.examinfo.examdeparts != "")
							{
								SqlParam sqlParam3 = DbHelper.MakeAndWhere("departid", WhereType.In, this.examinfo.examdeparts);
								List<UserInfo> list3 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam3
								});
								foreach (UserInfo userInfo in list3)
								{
									if (!FPUtils.InArray(userInfo.id, text))
									{
										if (text != "")
										{
											text += ",";
										}
										text += userInfo.id;
										num2++;
									}
								}
							}
							if (this.examinfo.examuser != "")
							{
								SqlParam sqlParam3 = DbHelper.MakeAndWhere("id", WhereType.In, this.examinfo.examuser);
								List<UserInfo> list3 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
								{
									sqlParam3
								});
								foreach (UserInfo userInfo in list3)
								{
									if (!FPUtils.InArray(userInfo.id, text))
									{
										if (text != "")
										{
											text += ",";
										}
										text += userInfo.id;
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
					if (this.tabactive == 1)
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
					SqlParam sqlParam4 = DbHelper.MakeAndWhere("id", WhereType.In, this.sortinfo.types);
					OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
					this.typelist = DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
					{
						sqlParam4
					});
					this.zNodes = this.GetDepartTree(0);
					base.SaveRightURL();
				}
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00005028 File Offset: 0x00003228
		private string GetDepartTree(int parentid)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", parentid);
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<Department> list = DbHelper.ExecuteList<Department>(orderby, new SqlParam[]
			{
				sqlParam
			});
			string text = "";
			foreach (Department department in list)
			{
				if (text != "")
				{
					text += ",";
				}
				string text2 = "";
				if (base.ischecked(department.id, this.examinfo.examdeparts))
				{
					text2 = "checked:true,";
				}
				if (department.subcounts > 0)
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"{ id: ",
						department.id,
						", pId: ",
						parentid,
						", name: \"",
						department.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.webpath,
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/usergroups.gif\" }"
					});
					text = text + "," + this.GetDepartTree(department.id);
				}
				else
				{
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"{ id: ",
						department.id,
						", pId: ",
						parentid,
						", name: \"",
						department.name,
						"\",",
						text2,
						"open:true, icon: \"",
						this.webpath,
						(this.sysconfig.adminpath == "") ? "" : (this.sysconfig.adminpath + "/"),
						"images/users.gif\" }"
					});
				}
			}
			return text;
		}

		// Token: 0x04000007 RID: 7
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000008 RID: 8
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000009 RID: 9
		protected int tabactive = FPRequest.GetInt("tabactive", 1);

		// Token: 0x0400000A RID: 10
		protected ExamConfig examconfig = new ExamConfig();

		// Token: 0x0400000B RID: 11
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400000C RID: 12
		protected List<TypeInfo> typelist = new List<TypeInfo>();

		// Token: 0x0400000D RID: 13
		protected List<RoleInfo> rolelist = new List<RoleInfo>();

		// Token: 0x0400000E RID: 14
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x0400000F RID: 15
		protected string zNodes = "";
	}
}
