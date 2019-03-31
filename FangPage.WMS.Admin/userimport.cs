using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000058 RID: 88
	public class userimport : SuperController
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00010214 File Offset: 0x0000E414
		protected override void Controller()
		{
			this.regconfig = RegConfigs.GetRegConfig();
			this.extendlist = FPXml.LoadList<UserExtend>(FPFile.GetMapPath(this.webpath + "config/user_extend.config"));
			if (this.ispost)
			{
				if (!this.isfile)
				{
					this.ShowErr("请选择要导入的本地Excel表文件");
					return;
				}
				int @int = FPRequest.GetInt("sms");
				int int2 = FPRequest.GetInt("email");
				int int3 = FPRequest.GetInt("isdepart");
				string mapPath = FPFile.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				if (Path.GetExtension(fileName).ToLower() != ".xls")
				{
					this.ShowErr("该文件不是Excel表文件类型");
					return;
				}
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				MsgTempInfo msgTempInfo = new MsgTempInfo();
				if (@int == 1)
				{
					msgTempInfo = MsgTempBll.GetMsgTemplate("sms_import");
				}
				MsgTempInfo msgTempInfo2 = new MsgTempInfo();
				if (int2 == 1)
				{
					msgTempInfo2 = MsgTempBll.GetMsgTemplate("email_import");
				}
				SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
				int num = FPUtils.StrToInt(DbHelper.ExecuteMax<Department>("display", new SqlParam[]
				{
					sqlParam
				}).ToString());
				FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
				DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
				if (excelTable.Rows.Count > 0)
				{
					int num2 = excelTable.Rows.Count - 1;
					for (int i = 0; i < excelTable.Rows.Count; i++)
					{
						DataRow dataRow = excelTable.Rows[num2 - i];
						UserInfo userInfo = new UserInfo();
						string text = "";
						for (int j = 0; j < excelTable.Columns.Count; j++)
						{
							string text2 = excelTable.Columns[j].ColumnName.Trim();
							if (text2 == "用户名" || text2 == "登录名")
							{
								userInfo.username = dataRow.ItemArray[j].ToString().Trim();
							}
							else if (text2 == "密码")
							{
								text = dataRow.ItemArray[j].ToString().Trim();
							}
							else if (text2 == "姓名" || text2 == "真实姓名")
							{
								userInfo.realname = dataRow.ItemArray[j].ToString().Trim();
							}
							else if (text2 == "手机号码" || text2 == "手机" || text2 == "手机号")
							{
								userInfo.mobile = dataRow.ItemArray[j].ToString().Trim();
								if (!string.IsNullOrEmpty(userInfo.mobile))
								{
									userInfo.ismobile = 1;
								}
							}
							else if (text2 == "电子邮箱" || text2 == "邮箱" || text2.ToLower() == "email")
							{
								userInfo.email = dataRow.ItemArray[j].ToString().Trim();
								if (!string.IsNullOrEmpty(userInfo.email))
								{
									userInfo.isemail = 1;
								}
							}
							else if (text2 == "用户角色" || text2 == "角色")
							{
								string text3 = dataRow.ItemArray[j].ToString().Trim();
								if (!string.IsNullOrEmpty(text3))
								{
									RoleInfo roleInfo = RoleBll.CheckRole(text3);
									if (roleInfo.id > 0)
									{
										userInfo.roleid = roleInfo.id;
									}
								}
							}
							else if (text2 == "所属部门" || text2 == "部门")
							{
								string text4 = dataRow.ItemArray[j].ToString();
								if (!string.IsNullOrEmpty(text4))
								{
									Department department = DepartmentBll.CheckDepart(text4);
									if (int3 == 1 && department.id == 0)
									{
										department.name = text4;
										department.parentlist = "0";
										department.departlist = text4;
										department.display = num + 1;
										DbHelper.ExecuteInsert<Department>(department);
										department.parentlist = department.parentlist + "," + department.id;
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist] = '{1}' WHERE [id]={2};", DbConfigs.Prefix, department.parentlist, department.id);
										DbHelper.ExecuteSql(stringBuilder.ToString());
									}
									userInfo.departname = department.longname;
									if (department.id > 0)
									{
										userInfo.departid = department.id;
									}
								}
							}
							else if (text2 == "岗位" || text2 == "职务")
							{
								string text5 = dataRow.ItemArray[j].ToString();
								if (!string.IsNullOrEmpty(text5))
								{
									SqlParam sqlParam2 = DbHelper.MakeAndWhere("name", text5);
									GradeInfo gradeInfo = DbHelper.ExecuteModel<GradeInfo>(new SqlParam[]
									{
										sqlParam2
									});
									if (gradeInfo.id == 0)
									{
										gradeInfo.name = text5;
										DbHelper.ExecuteInsert<GradeInfo>(gradeInfo);
									}
									userInfo.gradeid = gradeInfo.id;
								}
								else
								{
									userInfo.gradeid = 0;
								}
							}
							else if (text2 == "身份证号码" || text2 == "身份证" || text2 == "身份证号")
							{
								userInfo.idcard = dataRow.ItemArray[j].ToString().Trim();
								if (!string.IsNullOrEmpty(userInfo.idcard))
								{
									userInfo.isreal = 1;
								}
							}
							else if (text2 == "性别")
							{
								userInfo.sex = dataRow.ItemArray[j].ToString().Trim();
							}
							else
							{
								UserExtend extendInfo = this.GetExtendInfo(text2);
								if (extendInfo.name != "")
								{
									userInfo.extend[extendInfo.markup] = dataRow.ItemArray[j].ToString().Trim();
								}
							}
						}
						if (!(userInfo.username == "") && !this.InRestrictArray(userInfo.username, this.regconfig.restrict))
						{
							userInfo.id = UserBll.CheckUserName(userInfo.username);
							if (userInfo.id > 0)
							{
								UserInfo userInfo2 = UserBll.GetUserInfo(userInfo.id);
								if (string.IsNullOrEmpty(text))
								{
									text = FPRequest.GetString("password");
									if (text == "")
									{
										text = "123456";
									}
									userInfo.password = FPUtils.MD5(text);
								}
								else
								{
									userInfo.password = userInfo2.password;
								}
								if (userInfo.roleid == 0)
								{
									if (userInfo2.roleid == 0)
									{
										userInfo.roleid = 5;
									}
									else
									{
										userInfo.roleid = userInfo2.roleid;
									}
								}
								userInfo.issso = userInfo2.issso;
								DbHelper.ExecuteUpdate<UserInfo>(userInfo);
							}
							else
							{
								if (string.IsNullOrEmpty(text))
								{
									text = FPRequest.GetString("password");
									if (text == "")
									{
										text = "123456";
									}
								}
								userInfo.password = FPUtils.MD5(text);
								if (userInfo.roleid == 0)
								{
									userInfo.roleid = 5;
								}
								DbHelper.ExecuteInsert<UserInfo>(userInfo);
							}
							if (msgTempInfo.id > 0 && @int == 1 && userInfo.mobile != "")
							{
								msgTempInfo.content = msgTempInfo.content.Replace("【用户名】", userInfo.username);
								msgTempInfo.content = msgTempInfo.content.Replace("【密码】", dataRow.ItemArray[1].ToString().Trim());
								SMS.Send("", userInfo.mobile, msgTempInfo.content);
							}
							if (msgTempInfo2.id > 0 && int2 == 1 && userInfo.email != "")
							{
								msgTempInfo2.content = msgTempInfo2.content.Replace("【用户名】", userInfo.username).Replace("【密码】", dataRow.ItemArray[1].ToString().Trim());
								try
								{
									Email.Send(userInfo.email, msgTempInfo2.name, msgTempInfo2.content);
								}
								catch
								{
								}
							}
						}
					}
				}
				if (int3 == 1)
				{
					FPCache.Remove("FP_DEPARTLIST");
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				base.Response.Redirect("usermanage.aspx");
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00010B1C File Offset: 0x0000ED1C
		private bool InRestrictArray(string usernametxt, string restrict)
		{
			if (restrict == null || restrict == "")
			{
				return false;
			}
			restrict = Regex.Escape(restrict).Replace("\\*", "[\\w-]*");
			foreach (string text in FPArray.SplitString(restrict.ToLower(), "|"))
			{
				if (new Regex(string.Format("^{0}$", text)).IsMatch(usernametxt.ToLower()) && !text.Trim().Equals(""))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00010BA8 File Offset: 0x0000EDA8
		private UserExtend GetExtendInfo(string name)
		{
			UserExtend result = new UserExtend();
			List<UserExtend> list = this.extendlist.FindAll((UserExtend ext) => ext.name == name || ext.markup == name);
			if (list.Count > 0)
			{
				result = list[0];
			}
			return result;
		}

		// Token: 0x040000F6 RID: 246
		protected RegConfig regconfig = new RegConfig();

		// Token: 0x040000F7 RID: 247
		protected List<UserExtend> extendlist = new List<UserExtend>();
	}
}
