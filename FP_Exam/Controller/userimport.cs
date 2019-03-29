using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using LitJson;

namespace FP_Exam.Controller
{
	// Token: 0x0200001C RID: 28
	public class userimport : AdminController
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		protected override void View()
		{
			if (this.ispost)
			{
				if (!this.isfile)
				{
					this.ShowErr("请选择要导入的本地Excel表文件");
				}
				else
				{
					string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
					string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(fileName).ToLower();
					if (a != ".xls")
					{
						this.ShowErr("该文件不是Excel表文件类型");
					}
					else
					{
						if (!Directory.Exists(mapPath))
						{
							Directory.CreateDirectory(mapPath);
						}
						if (File.Exists(mapPath + "\\" + fileName))
						{
							File.Delete(mapPath + "\\" + fileName);
						}
						FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
						DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
						string text = "";
						string text2 = "";
						string text3 = "";
						if (excelTable.Rows.Count > 0)
						{
							int num = excelTable.Rows.Count - 1;
							for (int i = 0; i < num; i++)
							{
								DataRow dataRow = excelTable.Rows[num - i];
								string text4 = dataRow.ItemArray[0].ToString().Trim();
								if (!(text4 == ""))
								{
									SqlParam sqlParam = DbHelper.MakeAndWhere("username", text4);
									UserInfo userInfo = DbHelper.ExecuteModel<UserInfo>(new SqlParam[]
									{
										sqlParam
									});
									if (userInfo.id == 0)
									{
										userInfo.username = text4;
										userInfo.realname = dataRow.ItemArray[1].ToString().Trim();
										userInfo.password = FPUtils.MD5(dataRow.ItemArray[2].ToString().Trim());
										userInfo.roleid = this.GetRoleId(dataRow.ItemArray[3].ToString().Trim());
										userInfo.departid = this.GetDepartId(dataRow.ItemArray[4].ToString().Trim());
										userInfo.nickname = dataRow.ItemArray[5].ToString().Trim();
										userInfo.id = DbHelper.ExecuteInsert<UserInfo>(userInfo);
									}
									else
									{
										if (!string.IsNullOrEmpty(dataRow.ItemArray[1].ToString().Trim()))
										{
											userInfo.realname = dataRow.ItemArray[1].ToString().Trim();
										}
										if (!string.IsNullOrEmpty(dataRow.ItemArray[2].ToString().Trim()))
										{
											userInfo.password = FPUtils.MD5(dataRow.ItemArray[2].ToString().Trim());
										}
										if (!string.IsNullOrEmpty(dataRow.ItemArray[3].ToString().Trim()))
										{
											userInfo.roleid = this.GetRoleId(dataRow.ItemArray[3].ToString().Trim());
										}
										if (!string.IsNullOrEmpty(dataRow.ItemArray[4].ToString().Trim()))
										{
											userInfo.departid = this.GetDepartId(dataRow.ItemArray[4].ToString().Trim());
										}
										if (!string.IsNullOrEmpty(dataRow.ItemArray[5].ToString().Trim()))
										{
											userInfo.nickname = dataRow.ItemArray[5].ToString().Trim();
										}
										DbHelper.ExecuteUpdate<UserInfo>(userInfo);
									}
									if (text != "")
									{
										text += ",";
									}
									text += userInfo.id;
								}
							}
							if (File.Exists(mapPath + "\\" + fileName))
							{
								File.Delete(mapPath + "\\" + fileName);
							}
							if (text != "")
							{
								if (this.examuser != "")
								{
									this.examuser += ",";
								}
								this.examuser += text;
							}
							SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, this.examuser);
							List<UserInfo> list = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
							{
								sqlParam2
							});
							foreach (int num2 in FPUtils.SplitInt(this.examuser))
							{
								foreach (UserInfo userInfo2 in list)
								{
									if (num2 == userInfo2.id && !FPUtils.InArray(num2, text3))
									{
										if (text2 != "")
										{
											text2 += ",";
										}
										if (userInfo2.realname != "")
										{
											text2 += userInfo2.realname;
										}
										else
										{
											text2 += userInfo2.username;
										}
										if (text3 != "")
										{
											text3 += ",";
										}
										text3 += num2;
									}
								}
							}
						}
						Hashtable hashtable = new Hashtable();
						hashtable["uname"] = text2;
						hashtable["examuser"] = text3;
						base.Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
						base.Response.Write(JsonMapper.ToJson(hashtable));
						base.Response.End();
					}
				}
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000D964 File Offset: 0x0000BB64
		private int GetRoleId(string rolename)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", rolename);
			RoleInfo roleInfo = DbHelper.ExecuteModel<RoleInfo>(new SqlParam[]
			{
				sqlParam
			});
			if (roleInfo.id == 0)
			{
				roleInfo.id = 5;
			}
			return roleInfo.id;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		private int GetDepartId(string departname)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", departname);
			Department department = DbHelper.ExecuteModel<Department>(new SqlParam[]
			{
				sqlParam
			});
			return department.id;
		}

		// Token: 0x04000089 RID: 137
		protected string examuser = FPRequest.GetString("examuser");
	}
}
