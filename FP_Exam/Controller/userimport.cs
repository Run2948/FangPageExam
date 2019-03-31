using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace FP_Exam.Controller
{
	public class userimport : AdminController
	{
		protected string examuser = FPRequest.GetString("examuser");

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag = !this.isfile;
				if (flag)
				{
					this.ShowErr("请选择要导入的本地Excel表文件");
				}
				else
				{
					string mapPath = FPFile.GetMapPath(this.webpath + "cache");
					string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(fileName).ToLower();
					bool flag2 = a != ".xls";
					if (flag2)
					{
						this.ShowErr("该文件不是Excel表文件类型");
					}
					else
					{
						bool flag3 = !Directory.Exists(mapPath);
						if (flag3)
						{
							Directory.CreateDirectory(mapPath);
						}
						bool flag4 = File.Exists(mapPath + "\\" + fileName);
						if (flag4)
						{
							File.Delete(mapPath + "\\" + fileName);
						}
						FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
						DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
						int @int = FPRequest.GetInt("isdepart");
						SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
						int num = FPUtils.StrToInt(DbHelper.ExecuteMax<Department>("display", new SqlParam[]
						{
							sqlParam
						}).ToString());
						string text = "";
						string text2 = "";
						string text3 = "";
						bool flag5 = excelTable.Rows.Count > 0;
						if (flag5)
						{
							int num2 = excelTable.Rows.Count - 1;
							for (int i = 0; i < excelTable.Rows.Count; i++)
							{
								DataRow dataRow = excelTable.Rows[num2 - i];
								UserInfo userInfo = new UserInfo();
								string text4 = "";
								for (int j = 0; j < excelTable.Columns.Count; j++)
								{
									string text5 = excelTable.Columns[j].ColumnName.Trim();
									uint num3 = FPUtils.ComputeStringHash(text5);
									if (num3 <= 403450561u)
									{
										if (num3 <= 108399595u)
										{
											if (num3 != 90080012u)
											{
												if (num3 == 108399595u)
												{
													if (text5 == "所属部门")
													{
														string text6 = dataRow.ItemArray[j].ToString();
														bool flag6 = !string.IsNullOrEmpty(text6);
														if (flag6)
														{
															Department department = DepartmentBll.CheckDepart(text6);
															department.name = text6;
															department.parentlist = "0";
															bool flag7 = @int == 1 && department.id == 0;
															if (flag7)
															{
																department.departlist = text6;
																department.display = num + 1;
																DbHelper.ExecuteInsert<Department>(department);
																department.parentlist = department.parentlist + "," + department.id;
																StringBuilder stringBuilder = new StringBuilder();
																stringBuilder.AppendFormat("UPDATE [{0}WMS_Department] SET [parentlist] = '{1}' WHERE [id]={2};", DbConfigs.Prefix, department.parentlist, department.id);
																DbHelper.ExecuteSql(stringBuilder.ToString());
															}
															bool flag8 = department.id > 0;
															if (flag8)
															{
																userInfo.departid = department.id;
																userInfo.departname = department.longname;
															}
															else
															{
																userInfo.departid = 0;
																userInfo.departname = department.longname;
															}
														}
													}
												}
											}
											else if (text5 == "用户角色")
											{
												string text7 = dataRow.ItemArray[j].ToString().Trim();
												bool flag9 = !string.IsNullOrEmpty(text7);
												if (flag9)
												{
													RoleInfo roleInfo = RoleBll.CheckRole(text7);
													bool flag10 = roleInfo.id > 0;
													if (flag10)
													{
														userInfo.roleid = roleInfo.id;
													}
												}
											}
										}
										else if (num3 != 205826092u)
										{
											if (num3 == 403450561u)
											{
												if (text5 == "电子邮箱")
												{
													userInfo.email = dataRow.ItemArray[j].ToString().Trim();
													bool flag11 = !string.IsNullOrEmpty(userInfo.email);
													if (flag11)
													{
														userInfo.isemail = 1;
													}
												}
											}
										}
										else if (text5 == "手机号码")
										{
											userInfo.mobile = dataRow.ItemArray[j].ToString().Trim();
											bool flag12 = !string.IsNullOrEmpty(userInfo.mobile);
											if (flag12)
											{
												userInfo.ismobile = 1;
											}
										}
									}
									else if (num3 <= 1830022295u)
									{
										if (num3 != 1593819024u)
										{
											if (num3 == 1830022295u)
											{
												if (text5 == "用户名")
												{
													userInfo.username = dataRow.ItemArray[j].ToString().Trim();
												}
											}
										}
										else if (text5 == "身份证号码")
										{
											userInfo.idcard = dataRow.ItemArray[j].ToString().Trim();
											bool flag13 = !string.IsNullOrEmpty(userInfo.idcard);
											if (flag13)
											{
												userInfo.isreal = 1;
											}
										}
									}
									else if (num3 != 1965088023u)
									{
										if (num3 != 2212667773u)
										{
											if (num3 == 2824235944u)
											{
												if (text5 == "密码")
												{
													text4 = dataRow.ItemArray[j].ToString().Trim();
												}
											}
										}
										else if (text5 == "姓名")
										{
											userInfo.realname = dataRow.ItemArray[j].ToString().Trim();
										}
									}
									else if (text5 == "性别")
									{
										userInfo.sex = dataRow.ItemArray[j].ToString().Trim();
									}
								}
								bool flag14 = userInfo.username == "";
								if (!flag14)
								{
									bool flag15 = string.IsNullOrEmpty(text4);
									if (flag15)
									{
										text4 = FPRequest.GetString("password");
										bool flag16 = text4 == "";
										if (flag16)
										{
											text4 = "123456";
										}
									}
									userInfo.password = FPUtils.MD5(text4);
									bool flag17 = userInfo.roleid == 0;
									if (flag17)
									{
										userInfo.roleid = 5;
									}
									userInfo.id = UserBll.CheckUserName(userInfo.username);
									bool flag18 = userInfo.id > 0;
									if (flag18)
									{
										DbHelper.ExecuteUpdate<UserInfo>(userInfo);
									}
									else
									{
										DbHelper.ExecuteInsert<UserInfo>(userInfo);
									}
									text = FPArray.Push(text, userInfo.id);
								}
							}
							bool flag19 = @int == 1;
							if (flag19)
							{
								FPCache.Remove("FP_DEPARTLIST");
							}
							bool flag20 = File.Exists(mapPath + "\\" + fileName);
							if (flag20)
							{
								File.Delete(mapPath + "\\" + fileName);
							}
							this.examuser = FPArray.Push(this.examuser, text);
							SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, this.examuser);
							List<UserInfo> list = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
							{
								sqlParam2
							});
							int[] array = FPArray.SplitInt(this.examuser);
							for (int k = 0; k < array.Length; k++)
							{
								int num4 = array[k];
								foreach (UserInfo current in list)
								{
									bool flag21 = num4 == current.id && FPArray.InArray(num4, text3) == -1;
									if (flag21)
									{
										bool flag22 = text2 != "";
										if (flag22)
										{
											text2 += ",";
										}
										bool flag23 = current.realname != "";
										if (flag23)
										{
											text2 += current.realname;
										}
										else
										{
											text2 += current.username;
										}
										bool flag24 = text3 != "";
										if (flag24)
										{
											text3 += ",";
										}
										text3 += num4;
									}
								}
							}
						}
						Hashtable hashtable = new Hashtable();
						hashtable["uname"] = text2;
						hashtable["examuser"] = text3;
						FPResponse.WriteJson(hashtable);
					}
				}
			}
		}

		private int GetRoleId(string rolename)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", rolename);
			RoleInfo roleInfo = DbHelper.ExecuteModel<RoleInfo>(new SqlParam[]
			{
				sqlParam
			});
			bool flag = roleInfo.id == 0;
			if (flag)
			{
				roleInfo.id = 5;
			}
			return roleInfo.id;
		}

		private int GetDepartId(string departname)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", departname);
			Department department = DbHelper.ExecuteModel<Department>(new SqlParam[]
			{
				sqlParam
			});
			return department.id;
		}
	}
}
