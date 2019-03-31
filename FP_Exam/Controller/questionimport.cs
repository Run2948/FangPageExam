using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Saving;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Config;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FP_Exam.Controller
{
	public class questionimport : AdminController
	{
		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected override void Controller()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag = !this.isfile;
				if (flag)
				{
					this.ShowErr("请选择要导入的题库文件");
				}
				else
				{
					string mapPath = FPFile.GetMapPath(this.webpath + "cache");
					string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
					string a = Path.GetExtension(fileName).ToLower();
					bool flag2 = a != ".xls" && a != ".doc" && a != ".docx";
					if (flag2)
					{
						this.ShowErr("该文件不是题库文件类型");
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
						bool flag5 = a == ".xls";
						if (flag5)
						{
							DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
							bool flag6 = excelTable.Columns.Count < 12;
							if (flag6)
							{
								this.ShowErr("对不起，Excel表格列不得少于12。");
								return;
							}
							bool flag7 = excelTable.Rows.Count > 0;
							if (flag7)
							{
								int num = excelTable.Rows.Count - 1;
								for (int i = 0; i < excelTable.Rows.Count; i++)
								{
									DataRow dataRow = excelTable.Rows[num - i];
									ExamQuestion examQuestion = new ExamQuestion();
									examQuestion.uid = this.userid;
									examQuestion.channelid = this.sortinfo.channelid;
									examQuestion.type = this.TypeStr(dataRow.ItemArray[0].ToString());
									examQuestion.title = dataRow.ItemArray[1].ToString();
									bool flag8 = examQuestion.type == "" || examQuestion.title == "";
									if (!flag8)
									{
										for (int j = 0; j < 8; j++)
										{
											bool flag9 = dataRow.ItemArray[2 + j].ToString() != "";
											if (flag9)
											{
												bool flag10 = examQuestion.content != "";
												if (flag10)
												{
													ExamQuestion expr_288 = examQuestion;
													expr_288.content += "§";
												}
												ExamQuestion expr_2A1 = examQuestion;
												expr_2A1.content += dataRow.ItemArray[2 + j].ToString();
											}
										}
										bool flag11 = examQuestion.type == "TYPE_RADIO";
										if (flag11)
										{
											examQuestion.display = 1;
											examQuestion.answer = dataRow.ItemArray[10].ToString().Trim().ToUpper();
											examQuestion.ascount = FPArray.SplitString(examQuestion.content, "§").Length;
										}
										else
										{
											bool flag12 = examQuestion.type == "TYPE_MULTIPLE";
											if (flag12)
											{
												examQuestion.display = 2;
												string text = dataRow.ItemArray[10].ToString().Trim().ToUpper();
												bool flag13 = text.IndexOf(",") >= 0 || text.IndexOf("，") >= 0 || text.IndexOf("、") >= 0;
												if (flag13)
												{
													string[] array = FPArray.SplitString(text, new string[]
													{
														",",
														"，",
														"、"
													});
													for (int k = 0; k < array.Length; k++)
													{
														string item = array[k];
														examQuestion.answer = FPArray.Push(examQuestion.answer, item);
													}
												}
												else
												{
													bool flag14 = text != "";
													if (flag14)
													{
														for (int l = 0; l < text.Length; l++)
														{
															examQuestion.answer = FPArray.Push(examQuestion.answer, text.Substring(l, 1));
														}
													}
												}
												examQuestion.ascount = FPArray.SplitString(examQuestion.content, "§").Length;
											}
											else
											{
												bool flag15 = examQuestion.type == "TYPE_TRUE_FALSE";
												if (flag15)
												{
													examQuestion.display = 3;
													bool flag16 = FPArray.InArray(dataRow.ItemArray[10].ToString().Trim(), "y,Y,正确,对,YES,yes,√") >= 0;
													if (flag16)
													{
														examQuestion.answer = "Y";
													}
													else
													{
														examQuestion.answer = "N";
													}
													examQuestion.ascount = 2;
												}
												else
												{
													bool flag17 = examQuestion.type == "TYPE_BLANK";
													if (flag17)
													{
														examQuestion.display = 4;
														string[] array2 = FPArray.SplitString(examQuestion.content, "§");
														for (int m = 0; m < array2.Length; m++)
														{
															string a2 = array2[m];
															bool flag18 = a2 == "区分大小写";
															if (flag18)
															{
																examQuestion.upperflg = 1;
															}
															else
															{
																bool flag19 = a2 == "区分顺序";
																if (flag19)
																{
																	examQuestion.orderflg = 1;
																}
															}
														}
														string text2 = dataRow.ItemArray[10].ToString().Trim();
														bool flag20 = text2.IndexOf(",") >= 0 || text2.IndexOf("，") >= 0 || text2.IndexOf("、") >= 0;
														if (flag20)
														{
															string[] array3 = FPArray.SplitString(text2, new string[]
															{
																",",
																"，",
																"、"
															});
															for (int n = 0; n < array3.Length; n++)
															{
																string item2 = array3[n];
																examQuestion.answer = FPArray.Push(examQuestion.answer, item2);
															}
														}
														else
														{
															examQuestion.answer = dataRow.ItemArray[10].ToString().Trim();
														}
														examQuestion.ascount = FPArray.SplitString(examQuestion.answer).Length;
													}
													else
													{
														bool flag21 = examQuestion.type == "TYPE_ANSWER";
														if (flag21)
														{
															examQuestion.display = 5;
															examQuestion.answer = dataRow.ItemArray[10].ToString().Trim();
															examQuestion.answerkey = dataRow.ItemArray[11].ToString();
															examQuestion.ascount = FPArray.SplitString(examQuestion.answerkey).Length;
														}
														else
														{
															bool flag22 = examQuestion.type == "TYPE_OPERATION";
															if (flag22)
															{
																examQuestion.display = 6;
																examQuestion.answer = dataRow.ItemArray[10].ToString().Trim();
																examQuestion.answerkey = dataRow.ItemArray[11].ToString();
																examQuestion.ascount = FPArray.SplitString(examQuestion.answerkey).Length;
															}
															else
															{
																bool flag23 = examQuestion.type == "TYPE_COMPREHEND";
																if (flag23)
																{
																	examQuestion.display = 7;
																	examQuestion.answer = dataRow.ItemArray[10].ToString().Trim();
																	examQuestion.answerkey = dataRow.ItemArray[11].ToString();
																	examQuestion.ascount = FPArray.SplitString(examQuestion.answerkey).Length;
																}
															}
														}
													}
												}
											}
										}
										bool flag24 = examQuestion.ascount == 0;
										if (flag24)
										{
											examQuestion.ascount = 1;
										}
										examQuestion.explain = dataRow.ItemArray[12].ToString();
										examQuestion.difficulty = this.DifficultyInt(dataRow.ItemArray[13].ToString().Trim());
										examQuestion.status = 1;
										examQuestion.sortid = this.sortid;
										DbHelper.ExecuteInsert<ExamQuestion>(examQuestion);
									}
								}
								SortBll.UpdateSortPosts(this.sortid, 1);
								QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid);
								bool flag25 = this.sortinfo.types != "";
								if (flag25)
								{
									QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid, this.sortinfo.types);
								}
							}
						}
						else
						{
							bool flag26 = a == ".doc" || a == ".docx";
							if (flag26)
							{
								string text3 = FPRandom.CreateCode(10);
								bool flag27 = Directory.Exists(mapPath + "\\" + text3);
								if (flag27)
								{
									Directory.Delete(mapPath + "\\" + text3, true);
								}
								Directory.CreateDirectory(mapPath + "\\" + text3);
								string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
								Document document = new Document(mapPath + "\\" + fileName);
								DocumentBuilder documentBuilder = new DocumentBuilder(document);
								NodeCollection childNodes = document.GetChildNodes(NodeType.Shape, true);
								int num2 = 0;
								using (IEnumerator<Node> enumerator = childNodes.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										Shape shape = (Shape)enumerator.Current;
										bool flag28 = shape.OleFormat != null;
										if (flag28)
										{
											AttachInfo attachInfo = new AttachInfo();
											attachInfo.attachid = text3;
											attachInfo.app = "exam";
											attachInfo.uid = this.userid;
											attachInfo.platform = SysConfigs.PlatForm;
											string suggestedExtension = shape.OleFormat.SuggestedExtension;
											AttachType attachType = AttachBll.GetAttachType(suggestedExtension);
											attachInfo.filetype = attachType.extension;
											attachInfo.description = attachType.type;
											string text4 = string.Concat(new string[]
											{
												WebConfig.WebPath,
												"upload/",
												DateTime.Now.ToString("yyyyMM"),
												"/",
												DateTime.Now.ToString("dd"),
												"/"
											});
											string mapPath2 = FPFile.GetMapPath(text4);
											bool flag29 = !Directory.Exists(mapPath2);
											if (flag29)
											{
												Directory.CreateDirectory(mapPath2);
											}
											string str = DateTime.Now.ToString("yyyyMMddHHmmss") + FPRandom.CreateCodeNum(4) + suggestedExtension;
											attachInfo.filename = text4 + str;
											attachInfo.name = fileNameWithoutExtension + "_附件_" + num2.ToString() + suggestedExtension;
											bool flag30 = !string.IsNullOrEmpty(shape.OleFormat.IconCaption);
											if (flag30)
											{
												attachInfo.name = shape.OleFormat.IconCaption;
											}
											MemoryStream memoryStream = new MemoryStream();
											shape.OleFormat.Save(memoryStream);
											Stream stream = new FileStream(mapPath2 + str, FileMode.Create);
											memoryStream.WriteTo(stream);
											attachInfo.filesize = memoryStream.Length;
											stream.Close();
											memoryStream.Close();
											stream.Dispose();
											memoryStream.Dispose();
											attachInfo.postid = this.sortid;
											DbHelper.ExecuteInsert<AttachInfo>(attachInfo);
											shape.HRef = attachInfo.filename;
											num2++;
										}
									}
								}
								SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Html);
								document.Save(string.Concat(new string[]
								{
									mapPath,
									"\\",
									text3,
									"\\",
									fileNameWithoutExtension,
									".html"
								}), saveOptions);
								HtmlDocument htmlDocument = new HtmlDocument();
								HtmlNode.ElementsFlags.Remove("option");
								htmlDocument.Load(string.Concat(new string[]
								{
									mapPath,
									"\\",
									text3,
									"\\",
									fileNameWithoutExtension,
									".html"
								}), Encoding.UTF8);
								HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
								bool flag31 = htmlNodeCollection != null;
								if (flag31)
								{
									num2 = 0;
									foreach (HtmlNode current in ((IEnumerable<HtmlNode>)htmlNodeCollection))
									{
										string value = current.Attributes["src"].Value;
										bool flag32 = File.Exists(string.Concat(new string[]
										{
											mapPath,
											"\\",
											text3,
											"\\",
											value
										}));
										if (flag32)
										{
											AttachInfo attachInfo2 = new AttachInfo();
											attachInfo2.attachid = text3;
											attachInfo2.app = "exam";
											attachInfo2.uid = this.userid;
											attachInfo2.platform = SysConfigs.PlatForm;
											string extension = Path.GetExtension(value);
											AttachType attachType2 = AttachBll.GetAttachType(extension);
											attachInfo2.filetype = attachType2.extension;
											attachInfo2.description = attachType2.type;
											string text5 = string.Concat(new string[]
											{
												WebConfig.WebPath,
												"upload/",
												DateTime.Now.ToString("yyyyMM"),
												"/",
												DateTime.Now.ToString("dd"),
												"/"
											});
											string mapPath3 = FPFile.GetMapPath(text5);
											bool flag33 = !Directory.Exists(mapPath3);
											if (flag33)
											{
												Directory.CreateDirectory(mapPath3);
											}
											string str2 = DateTime.Now.ToString("yyyyMMddHHmmss") + FPRandom.CreateCodeNum(4) + extension;
											attachInfo2.filename = text5 + str2;
											attachInfo2.name = string.Concat(new object[]
											{
												fileNameWithoutExtension,
												"_图片附件_",
												num2,
												extension
											});
											FileInfo fileInfo = new FileInfo(string.Concat(new string[]
											{
												mapPath,
												"\\",
												text3,
												"\\",
												value
											}));
											attachInfo2.filesize = fileInfo.Length;
											fileInfo.CopyTo(mapPath3 + str2, true);
											attachInfo2.postid = this.sortid;
											DbHelper.ExecuteInsert<AttachInfo>(attachInfo2);
											current.Attributes["src"].Value = attachInfo2.filename;
											num2++;
										}
									}
								}
								htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//p");
								bool flag34 = htmlNodeCollection != null;
								if (flag34)
								{
									string a3 = "";
									foreach (HtmlNode current2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
									{
										string text6 = questionimport.GetTextFromHTML(current2.InnerHtml);
										bool flag35 = text6 == "&#xa0;";
										if (flag35)
										{
											bool flag36 = a3 == "§";
											if (flag36)
											{
												continue;
											}
											text6 = "§";
										}
										a3 = text6;
										text6 = text6.Replace("&#xa0;", "");
										FPFile.AppendFile(string.Concat(new string[]
										{
											mapPath,
											"\\",
											text3,
											"\\",
											fileNameWithoutExtension,
											".txt"
										}), text6);
									}
								}
								string[] array4 = FPArray.SplitString(FPFile.ReadFile(string.Concat(new string[]
								{
									mapPath,
									"\\",
									text3,
									"\\",
									fileNameWithoutExtension,
									".txt"
								})), "§");
								string text7 = "";
								List<ExamQuestion> list = new List<ExamQuestion>();
								string[] array5 = array4;
								for (int num3 = 0; num3 < array5.Length; num3++)
								{
									string text8 = array5[num3];
									bool flag37 = text8 == "";
									if (!flag37)
									{
										bool flag38 = false;
										string[] array6 = FPArray.SplitString(text8, "\r\n");
										ExamQuestion examQuestion2 = new ExamQuestion();
										examQuestion2.kid = FPRandom.CreateGuid();
										examQuestion2.uid = this.userid;
										examQuestion2.channelid = this.sortinfo.channelid;
										examQuestion2.status = 1;
										examQuestion2.sortid = this.sortid;
										string a4 = "";
										bool flag39 = false;
										string[] array7 = array6;
										for (int num4 = 0; num4 < array7.Length; num4++)
										{
											string text9 = array7[num4];
											bool flag40 = FPUtils.IsContain(text9, "[单选题]");
											if (flag40)
											{
												a4 = "[单选题]";
												examQuestion2.type = this.TypeStr("单选题");
												examQuestion2.display = 1;
												int num5 = text9.IndexOf("[单选题]");
												examQuestion2.title = text9.Substring(num5 + 5);
											}
											else
											{
												bool flag41 = FPUtils.IsContain(text9, "[多选题]");
												if (flag41)
												{
													a4 = "[多选题]";
													examQuestion2.type = this.TypeStr("多选题");
													examQuestion2.display = 2;
													int num6 = text9.IndexOf("[多选题]");
													examQuestion2.title = text9.Substring(num6 + 5);
												}
												else
												{
													bool flag42 = FPUtils.IsContain(text9, "[判断题]");
													if (flag42)
													{
														a4 = "[判断题]";
														examQuestion2.type = this.TypeStr("判断题");
														examQuestion2.display = 3;
														int num7 = text9.IndexOf("[判断题]");
														examQuestion2.title = text9.Substring(num7 + 5);
													}
													else
													{
														bool flag43 = FPUtils.IsContain(text9, "[填空题]");
														if (flag43)
														{
															a4 = "[填空题]";
															examQuestion2.type = this.TypeStr("填空题");
															examQuestion2.display = 4;
															int num8 = text9.IndexOf("[填空题]");
															examQuestion2.title = text9.Substring(num8 + 5);
														}
														else
														{
															bool flag44 = FPUtils.IsContain(text9, "[问答题]");
															if (flag44)
															{
																a4 = "[问答题]";
																examQuestion2.type = this.TypeStr("问答题");
																examQuestion2.display = 5;
																int num9 = text9.IndexOf("[问答题]");
																examQuestion2.title = text9.Substring(num9 + 5);
															}
															else
															{
																bool flag45 = FPUtils.IsContain(text9, "[操作题]");
																if (flag45)
																{
																	a4 = "[操作题]";
																	examQuestion2.type = this.TypeStr("操作题");
																	examQuestion2.display = 6;
																	int num10 = text9.IndexOf("[操作题]");
																	examQuestion2.title = text9.Substring(num10 + 5);
																}
																else
																{
																	bool flag46 = FPUtils.IsContain(text9, "[理解题]");
																	if (flag46)
																	{
																		a4 = "[理解题]";
																		examQuestion2.type = this.TypeStr("理解题");
																		examQuestion2.display = 7;
																		int num11 = text9.IndexOf("[理解题]");
																		examQuestion2.title = text9.Substring(num11 + 5);
																	}
																	else
																	{
																		bool flag47 = FPUtils.IsContain(text9, "[参考答案]");
																		if (flag47)
																		{
																			a4 = "[参考答案]";
																			bool flag48 = examQuestion2.type == "TYPE_RADIO";
																			if (flag48)
																			{
																				examQuestion2.answer = text9.Trim().Substring(6).ToUpper();
																			}
																			else
																			{
																				bool flag49 = examQuestion2.type == "TYPE_MULTIPLE";
																				if (flag49)
																				{
																					string text10 = text9.Trim().Substring(6).ToUpper();
																					bool flag50 = text10.IndexOf(",") >= 0 || text10.IndexOf("，") >= 0 || text10.IndexOf("、") >= 0;
																					if (flag50)
																					{
																						string[] array8 = FPArray.SplitString(text10, new string[]
																						{
																							",",
																							"，",
																							"、"
																						});
																						for (int num12 = 0; num12 < array8.Length; num12++)
																						{
																							string item3 = array8[num12];
																							examQuestion2.answer = FPArray.Push(examQuestion2.answer, item3);
																						}
																					}
																					else
																					{
																						bool flag51 = text10 != "";
																						if (flag51)
																						{
																							for (int num13 = 0; num13 < text10.Length; num13++)
																							{
																								examQuestion2.answer = FPArray.Push(examQuestion2.answer, text10.Substring(num13, 1));
																							}
																						}
																					}
																				}
																				else
																				{
																					bool flag52 = examQuestion2.type == "TYPE_TRUE_FALSE";
																					if (flag52)
																					{
																						bool flag53 = FPArray.InArray(text9.Trim().Substring(6), "y,Y,正确,对,YES,yes,√") >= 0;
																						if (flag53)
																						{
																							examQuestion2.answer = "Y";
																						}
																						else
																						{
																							examQuestion2.answer = "N";
																						}
																						examQuestion2.ascount = 2;
																					}
																					else
																					{
																						bool flag54 = examQuestion2.type == "TYPE_BLANK";
																						if (flag54)
																						{
																							string text11 = text9.Trim().Substring(6);
																							bool flag55 = text11.IndexOf(",") >= 0 || text11.IndexOf("，") >= 0 || text11.IndexOf("、") >= 0;
																							if (flag55)
																							{
																								string[] array9 = FPArray.SplitString(text11, new string[]
																								{
																									",",
																									"，",
																									"、"
																								});
																								for (int num14 = 0; num14 < array9.Length; num14++)
																								{
																									string item4 = array9[num14];
																									examQuestion2.answer = FPArray.Push(examQuestion2.answer, item4);
																								}
																							}
																							else
																							{
																								examQuestion2.answer = text11;
																							}
																							examQuestion2.ascount = FPArray.SplitString(examQuestion2.answer).Length;
																						}
																						else
																						{
																							examQuestion2.answer = text9.Trim().Substring(6);
																							examQuestion2.ascount = FPArray.SplitString(examQuestion2.answerkey).Length;
																						}
																					}
																				}
																			}
																		}
																		else
																		{
																			bool flag56 = FPUtils.IsContain(text9, "[答案解析]");
																			if (flag56)
																			{
																				a4 = "[答案解析]";
																				examQuestion2.explain = text9.Trim().Substring(6);
																			}
																			else
																			{
																				bool flag57 = FPUtils.IsContain(text9, "[难易程度]");
																				if (flag57)
																				{
																					a4 = "";
																					examQuestion2.difficulty = this.DifficultyInt(text9.Trim().Substring(6));
																				}
																				else
																				{
																					bool flag58 = FPUtils.IsContain(text9, "[答案关键词]");
																					if (flag58)
																					{
																						a4 = "";
																						examQuestion2.answerkey = text9.Trim().Substring(7);
																					}
																					else
																					{
																						bool flag59 = FPUtils.IsContain(text9, "[区分大小写]");
																						if (flag59)
																						{
																							a4 = "";
																							examQuestion2.upperflg = 1;
																						}
																						else
																						{
																							bool flag60 = FPUtils.IsContain(text9, "[区分顺序]");
																							if (flag60)
																							{
																								a4 = "";
																								examQuestion2.orderflg = 1;
																							}
																							else
																							{
																								bool flag61 = FPUtils.IsContain(text9, "[理解题结束]");
																								if (flag61)
																								{
																									a4 = "";
																									flag38 = true;
																								}
																								else
																								{
																									bool flag62 = a4 == "[单选题]" || a4 == "[多选题]";
																									if (flag62)
																									{
																										bool flag63 = false;
																										string[] array10 = FPArray.SplitString("A,B,C,D,E,F,G,H");
																										for (int num15 = 0; num15 < array10.Length; num15++)
																										{
																											string text12 = array10[num15];
																											bool flag64 = text9.Trim().StartsWith(text12 + ".") || text9.Trim().StartsWith(text12.ToLower() + "a.");
																											if (flag64)
																											{
																												bool flag65 = examQuestion2.content != "";
																												if (flag65)
																												{
																													ExamQuestion expr_17CD = examQuestion2;
																													expr_17CD.content += "§";
																												}
																												ExamQuestion expr_17E6 = examQuestion2;
																												expr_17E6.content += text9.Trim().Substring(2);
																												flag39 = true;
																												flag63 = true;
																											}
																										}
																										bool flag66 = flag39;
																										if (flag66)
																										{
																											bool flag67 = !flag63;
																											if (flag67)
																											{
																												ExamQuestion expr_1834 = examQuestion2;
																												expr_1834.content += text9.Trim();
																											}
																										}
																										else
																										{
																											examQuestion2.title = examQuestion2.title + "<br/>" + text9;
																										}
																									}
																									else
																									{
																										bool flag68 = a4 == "[判断题]" || a4 == "[填空题]" || a4 == "[问答题]" || a4 == "[操作题]" || a4 == "[理解题]";
																										if (flag68)
																										{
																											examQuestion2.title = examQuestion2.title + "<br/>" + text9;
																										}
																										else
																										{
																											bool flag69 = a4 == "[参考答案]";
																											if (flag69)
																											{
																												examQuestion2.answer = examQuestion2.answer + "<br/>" + text9;
																											}
																											else
																											{
																												bool flag70 = a4 == "[答案解析]";
																												if (flag70)
																												{
																													examQuestion2.explain = examQuestion2.explain + "<br/>" + text9;
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										bool flag71 = examQuestion2.type == "" || examQuestion2.title == "";
										if (flag71)
										{
											bool flag72 = flag38;
											if (flag72)
											{
												text7 = "";
											}
										}
										else
										{
											bool flag73 = examQuestion2.type == "TYPE_RADIO" || examQuestion2.type == "TYPE_MULTIPLE";
											if (flag73)
											{
												examQuestion2.ascount = FPArray.SplitString(examQuestion2.content, "§").Length;
											}
											bool flag74 = text7 != "";
											if (flag74)
											{
												examQuestion2.parentid = text7;
												DbHelper.ExecuteInsert<ExamQuestion>(examQuestion2);
												bool flag75 = flag38;
												if (flag75)
												{
													text7 = "";
												}
											}
											else
											{
												list.Add(examQuestion2);
											}
											bool flag76 = examQuestion2.type == "TYPE_COMPREHEND";
											if (flag76)
											{
												text7 = examQuestion2.kid;
											}
										}
									}
								}
								for (int num16 = 0; num16 < list.Count; num16++)
								{
									DbHelper.ExecuteInsert<ExamQuestion>(list[list.Count - 1 - num16]);
								}
								SortBll.UpdateSortPosts(this.sortid, 1);
								QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid);
								bool flag77 = this.sortinfo.types != "";
								if (flag77)
								{
									QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid, this.sortinfo.types);
								}
								bool flag78 = Directory.Exists(mapPath + "\\" + text3);
								if (flag78)
								{
									Directory.Delete(mapPath + "\\" + text3, true);
								}
							}
						}
						bool flag79 = File.Exists(mapPath + "\\" + fileName);
						if (flag79)
						{
							File.Delete(mapPath + "\\" + fileName);
						}
						base.Response.Redirect("questionmanage.aspx?sortid=" + this.sortid);
					}
				}
			}
		}

		public static string GetTextFromHTML(string HTML)
		{
			Regex regex = new Regex("</?(?!img|u|/u|a|/a|br)[^>]*>", RegexOptions.IgnoreCase);
			string html = regex.Replace(HTML, "");
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(html);
			HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@name]");
			bool flag = htmlNodeCollection != null;
			if (flag)
			{
				foreach (HtmlNode current in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					bool flag2 = current.Attributes["href"] == null;
					if (flag2)
					{
						current.Remove();
					}
				}
			}
			return htmlDocument.DocumentNode.InnerHtml;
		}

		protected string TypeStr(string _type)
		{
			bool flag = _type == "单选题" || _type == "单选" || _type == "选择题";
			string result;
			if (flag)
			{
				result = "TYPE_RADIO";
			}
			else
			{
				bool flag2 = _type == "多选题" || _type == "多选";
				if (flag2)
				{
					result = "TYPE_MULTIPLE";
				}
				else
				{
					bool flag3 = _type == "判断题" || _type == "判断" || _type == "对错题";
					if (flag3)
					{
						result = "TYPE_TRUE_FALSE";
					}
					else
					{
						bool flag4 = _type == "填空题" || _type == "填空";
						if (flag4)
						{
							result = "TYPE_BLANK";
						}
						else
						{
							bool flag5 = _type == "问答题" || _type == "问答";
							if (flag5)
							{
								result = "TYPE_ANSWER";
							}
							else
							{
								bool flag6 = _type == "操作题" || _type == "操作";
								if (flag6)
								{
									result = "TYPE_OPERATION";
								}
								else
								{
									bool flag7 = _type == "理解题" || _type == "阅读理解";
									if (flag7)
									{
										result = "TYPE_COMPREHEND";
									}
									else
									{
										result = "";
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		protected int DifficultyInt(string difficulty)
		{
			int result;
			if (!(difficulty == "易"))
			{
				if (!(difficulty == "较易"))
				{
					if (!(difficulty == "一般"))
					{
						if (!(difficulty == "较难"))
						{
							if (!(difficulty == "难"))
							{
								result = 2;
							}
							else
							{
								result = 4;
							}
						}
						else
						{
							result = 3;
						}
					}
					else
					{
						result = 2;
					}
				}
				else
				{
					result = 1;
				}
			}
			else
			{
				result = 0;
			}
			return result;
		}

		private string GetFileExt(string _filepath)
		{
			bool flag = string.IsNullOrEmpty(_filepath);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = _filepath.LastIndexOf(".") > 0;
				if (flag2)
				{
					result = _filepath.Substring(_filepath.LastIndexOf(".") + 1);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}
	}
}
