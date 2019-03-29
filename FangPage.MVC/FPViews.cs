using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using HtmlAgilityPack;

namespace FangPage.MVC
{
	// Token: 0x0200000C RID: 12
	public class FPViews
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00003AC8 File Offset: 0x00001CC8
		static FPViews()
		{
			FPViews.r[0] = new Regex("<%include\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[1] = new Regex("<%loop ((\\(([^\\[\\]\\{\\}\\s]+)\\) )?)([^\\[\\]\\{\\}\\s]+) ([^\\s]+)%>", FPViews.options);
			FPViews.r[2] = new Regex("<%\\/loop%>", FPViews.options);
			FPViews.r[3] = new Regex("<%while\\(([^\\[\\]\\{\\}\\s]+)\\)%>", FPViews.options);
			FPViews.r[4] = new Regex("<%\\/while\\(([^\\[\\]\\{\\}\\s]+)\\)%>", FPViews.options);
			FPViews.r[5] = new Regex("<%if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?)(?:\\s*)%>", FPViews.options);
			FPViews.r[6] = new Regex("<%else(( (?:\\s*)if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?))?)(?:\\s*)%>", FPViews.options);
			FPViews.r[7] = new Regex("<%\\/if%>", FPViews.options);
			FPViews.r[8] = new Regex("(\\{int\\(([^\\s]+?)\\)\\})", FPViews.options);
			FPViews.r[9] = new Regex("(\\${urlencode\\(([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[10] = new Regex("(\\${fmdate\\(([^\\s]+?),(.*?)\\)})", FPViews.options);
			FPViews.r[11] = new Regex("(\\$\\{([^\\.\\{\\}\\s]+)\\.([^\\[\\]\\{\\}\\s]+)\\})", FPViews.options);
			FPViews.r[12] = new Regex("(\\{request\\((?:\"?)([^\\[\\]\\{\\}\\s]+)\\)\\}(?:\"?))", FPViews.options);
			FPViews.r[13] = new Regex("(\\$\\{([^\\[\\]\\{\\}\\s]+)\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", FPViews.options);
			FPViews.r[14] = new Regex("(\\${([^\\[\\]/\\{\\}='\\s]+)})", FPViews.options);
			FPViews.r[15] = new Regex("(\\${([^\\[\\]/\\{\\}='\\s]+)})", FPViews.options);
			FPViews.r[16] = new Regex("(([=|>|<|!]=)\\\\\"([^\\s]*)\\\\\")", FPViews.options);
			FPViews.r[17] = new Regex("<%using\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[18] = new Regex("<%#([\\s\\S]+?)%>", FPViews.options);
			FPViews.r[19] = new Regex("<%set ((\\(([a-zA-Z]+)\\))?)(?:\\s*)\\{([^\\s]+)\\}(?:\\s*)=(?:\\s*)(.*?)(?:\\s*)%>", FPViews.options);
			FPViews.r[20] = new Regex("(\\${substr\\(([^\\s]+?),(.\\d*?)\\)})", FPViews.options);
			FPViews.r[21] = new Regex("(\\${thumb\\(([^\\s]+?),([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[22] = new Regex("(<%--(.*?)--%>)", FPViews.options);
			FPViews.r[23] = new Regex("<%controller\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[24] = new Regex("<%repeat\\(([^\\s]+?)(?:\\s*),(?:\\s*)([^\\s]+?)\\)%>", FPViews.options);
			FPViews.r[25] = new Regex("<%continue%>");
			FPViews.r[26] = new Regex("<%break%>");
			FPViews.r[27] = new Regex("(\\${([a-zA-Z]+)\\(([^\\[\\]/='\\s]+)\\)})", FPViews.options);
			FPViews.r[28] = new Regex("<%=([\\s\\S]+?)%>", FPViews.options);
			FPViews.r[29] = new Regex("(\\${txt\\(([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[30] = new Regex("(<%plugin\\(([^\\s]+?)\\)%>)", FPViews.options);
			FPViews.t[0] = new Regex("({([a-zA-Z]+)\\(([^\\[\\]/='\\s]+)\\)})", FPViews.options);
			FPViews.t[1] = new Regex("(\\{([^\\[\\]\\{\\}\\s]+)\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", FPViews.options);
			FPViews.t[2] = new Regex("(\\{([^\\.\\{\\}\\s]+)\\.([^\\[\\]\\{\\}\\s]+)\\})", FPViews.options);
			FPViews.t[3] = new Regex("({([^\\[\\]/\\{\\}='\\s]+)})", FPViews.options);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003E04 File Offset: 0x00002004
		public static string CreateView(SiteConfig siteconfig, string webpath, string viewpath, string aspxpath, int nest, string linkpath, out string includefile, out string includeimport)
		{
			includefile = "";
			includeimport = "";
			if (nest < 1)
			{
				nest = 1;
			}
			else if (nest > 5)
			{
				return "";
			}
			StringBuilder stringBuilder = new StringBuilder();
			string mapPath = FPUtils.GetMapPath(webpath + viewpath);
			string fileName = Path.GetFileName(viewpath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(viewpath);
			string result;
			if (!File.Exists(mapPath))
			{
				result = "";
			}
			else
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				HtmlNode.ElementsFlags.Remove("option");
				htmlDocument.Load(mapPath, Encoding.UTF8);
				if (siteconfig.urltype > 0)
				{
					HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["src"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["src"].Value, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//input[@src]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["src"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["src"].Value, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//link[@href]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["href"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["href"].Value, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//style[@type='text/css']");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.InnerHtml = FPViews.FormatPageCSS(htmlNode.InnerHtml, viewpath, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@style]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["style"].Value = FPViews.FormatPageCSS(htmlNode.Attributes["style"].Value, viewpath, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//script[@src]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["src"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["src"].Value, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@background]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["background"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["background"].Value, siteconfig.urltype);
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//param[@name='movie']");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							if (htmlNode.Attributes["value"] != null)
							{
								htmlNode.Attributes["value"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["value"].Value, siteconfig.urltype);
							}
						}
					}
					htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//embed[@src]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["src"].Value = FPViews.FormatPath(viewpath, htmlNode.Attributes["src"].Value, siteconfig.urltype);
						}
					}
				}
				if (linkpath != "")
				{
					HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
					if (htmlNodeCollection != null)
					{
						foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
						{
							htmlNode.Attributes["href"].Value = FPViews.FormatHref(htmlNode.Attributes["href"].Value, linkpath);
						}
					}
				}
				string text = htmlDocument.DocumentNode.InnerHtml;
				StringBuilder stringBuilder2 = new StringBuilder();
				foreach (object obj in FPViews.r[22].Matches(text))
				{
					Match match = (Match)obj;
					text = text.Replace(match.Groups[0].ToString(), "");
				}
				text = FPViews.IncludeTag(text);
				stringBuilder2.Append(text);
				for (int i = 0; i < 3; i++)
				{
					stringBuilder2.Replace("<% ", "<%");
					stringBuilder2.Replace(" %>", "%>");
				}
				for (int i = 0; i < 3; i++)
				{
					stringBuilder2.Replace("<%Controller ", "<%Controller");
					stringBuilder2.Replace("<%controller ", "<%controller");
					stringBuilder2.Replace("<%Using ", "<%Using");
					stringBuilder2.Replace("<%using ", "<%using");
					stringBuilder2.Replace("<%Include ", "<%Include");
					stringBuilder2.Replace("<%include ", "<%include");
					stringBuilder2.Replace("<%plugin ", "<%plugin");
					stringBuilder2.Replace("<%loop  ", "<%loop ");
					stringBuilder2.Replace("<%set  ", "<%set ");
				}
				stringBuilder2.Replace("<%loop(", "<%loop (");
				stringBuilder2.Replace("<%set(", "<%set (");
				stringBuilder2.Replace("{webpath}/", "{webpath}");
				stringBuilder2.Replace("{rawpath}/", "{rawpath}");
				stringBuilder2.Replace("{curpath}/", "{curpath}");
				stringBuilder2.Replace("{plupath}/", "{plupath}");
				stringBuilder2.Replace("{adminpath}/", "{adminpath}");
				string text2 = "FangPage.MVC.FPController";
				string text3 = "<%@ Import namespace=\"FangPage.MVC\" %>\r\n";
				if (nest == 1)
				{
					if (siteconfig.inherits != "")
					{
						text2 = siteconfig.inherits + "." + ((fileNameWithoutExtension.ToLower() == "default") ? ("_" + fileNameWithoutExtension) : fileNameWithoutExtension);
					}

                    IEnumerator enumerator2 = r[23].Matches(stringBuilder2.ToString()).GetEnumerator();
                    if (enumerator2.MoveNext())
                    {
                        Match match = (Match)enumerator2.Current;
                        text2 = match.Groups[1].ToString();
                        if (text2 == "*")
                        {
                            text2 = "FangPage.WMS.Controller.index";
                        }
                        else if (text2.Substring(text2.LastIndexOf(".") + 1) == "*")
                        {
                            text2 = text2.Substring(0, text2.LastIndexOf(".")) + "." + fileNameWithoutExtension;
                            if (text2.StartsWith("*."))
                            {
                                text2 = text2.Replace("*.", "FangPage.WMS.Controller.");
                            }
                            if (text2.StartsWith("#."))
                            {
                                text2 = text2.Replace("#.", (siteconfig.inherits != "") ? (siteconfig.inherits + ".") : "FangPage.WMS.Controller.");
                            }
                        }
                        else if (text2.StartsWith("*."))
                        {
                            text2 = text2.Replace("*.", "FangPage.WMS.Controller.");
                        }
                        else if (text2.StartsWith("#."))
                        {
                            text2 = text2.Replace("#.", (siteconfig.inherits != "") ? (siteconfig.inherits + ".") : "FangPage.WMS.Controller.");
                        }
                        stringBuilder2.Replace(match.Groups[0].ToString(), string.Empty);
                    }
                    if ("\"".Equals(text2))
					{
						text2 = "FangPage.MVC.FPController";
					}
					else
					{
						string text4 = text2.Substring(text2.LastIndexOf(".") + 1);
						if (text4.EndsWith("list") || text4.EndsWith("info"))
						{
							stringBuilder2.Replace(text4 + "%>", "_" + text4 + "%>");
							stringBuilder2.Replace("{" + text4, "{_" + text4);
							stringBuilder2.Replace(" " + text4, " _" + text4);
						}
					}
					if (text2 != "FangPage.MVC.FPController")
					{
						text3 += FPViews.AddExtImport(text2.Substring(0, text2.LastIndexOf('.')) + ".#");
					}
					if (siteconfig.import != "")
					{
						foreach (string text5 in siteconfig.import.Split(new string[]
						{
							"\r\n",
							";",
							",",
							"|"
						}, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text5 != "")
							{
								string text6 = text5;
								if (text6.LastIndexOf(".dll") >= 0)
								{
									text6 = text6.Substring(0, text6.LastIndexOf("."));
								}
								if (text6.LastIndexOf(".*") > 0 || text6.LastIndexOf(".#") > 0)
								{
									text3 += FPViews.AddExtImport(text6);
								}
								else
								{
									text3 = text3 + "<%@ Import namespace=\"" + text6 + "\" %>\r\n";
								}
							}
						}
					}
				}
				foreach (object obj2 in FPViews.r[17].Matches(stringBuilder2.ToString()))
				{
					Match match = (Match)obj2;
					string text7 = match.Groups[1].ToString();
					if (text7.LastIndexOf(".*") > 0 || text7.LastIndexOf(".#") > 0)
					{
						includeimport += FPViews.AddExtImport(text7);
					}
					else
					{
						includeimport = includeimport + "<%@ Import namespace=\"" + text7 + "\" %>\r\n";
					}
					stringBuilder2.Replace(match.Groups[0].ToString(), string.Empty);
				}
				foreach (object obj3 in FPViews.r[18].Matches(stringBuilder2.ToString()))
				{
					Match match = (Match)obj3;
					stringBuilder2.Replace(match.Groups[0].ToString(), match.Groups[0].ToString().Replace("\r\n", "\r\t\r"));
				}
				stringBuilder2.Replace("\r\n", "\r\r\r");
				stringBuilder2.Replace("<%", "\r\r\n<%");
				stringBuilder2.Replace("%>", "%>\r\r\n");
				stringBuilder2.Replace("<%#%>\r\r\n", "<%#%>").Replace("\r\r\n<%/#%>", "<%/#%>");
				string[] array2 = FPUtils.SplitString(stringBuilder2.ToString(), "\r\r\n");
				int upperBound = array2.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					string text8 = "";
					string str = "";
					stringBuilder.Append(FPViews.ConvertTags(nest, siteconfig, webpath, viewpath, array2[i], out text8, out str));
					if (text8 != "")
					{
						includefile += ((includefile == "") ? text8 : ("," + text8));
					}
					includeimport += str;
				}
				if (nest == 1)
				{
					text3 += includeimport;
					text3 = FPViews.DelSameImport(text3);
					string content = string.Format("<%@ Page language=\"c#\" AutoEventWireup=\"false\" EnableViewState=\"false\" Inherits=\"{0}\" %>\r\n{1}<script runat=\"server\">\r\noverride protected void OnInitComplete(EventArgs e)\r\n{{\r\n\t/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：{3}*/\r\n\tbase.OnInitComplete(e);\r\n\tint loop__id=0;\r\n{2}\r\n\tResponse.Write(ViewBuilder.ToString());\r\n}}\r\n</script>\r\n", new object[]
					{
						text2,
						text3,
						stringBuilder.ToString(),
						siteconfig.version
					});
					string mapPath2 = FPUtils.GetMapPath(webpath + aspxpath);
					FPFile.WriteFile(mapPath2, content);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004FBC File Offset: 0x000031BC
		private static string ConvertTags(int nest, SiteConfig siteconfig, string webpath, string templatepath, string inputStr, out string includefile, out string includeimport)
		{
			includefile = "";
			includeimport = "";
			string result = "";
			string text = inputStr;
			text = text.Replace("\\", "\\\\");
			text = text.Replace("\"", "\\\"");
			text = text.Replace("<SCRIPT", "<script");
			text = text.Replace("</SCRIPT>", "</script>");
			text = text.Replace("</script>", "</\");\r\n\tViewBuilder.Append(\"script>");
			bool flag = false;
			foreach (object obj in FPViews.r[0].Matches(text))
			{
				Match match = (Match)obj;
				flag = true;
				string text2 = match.Groups[1].ToString().Replace("\\", "").Replace("\"", "");
				string text3 = Path.GetDirectoryName(templatepath).Replace("\\", "/");
				string text4 = "";
				while (text2.StartsWith("../"))
				{
					if (text3 != "")
					{
						text3 = Path.GetDirectoryName(text3).Replace("\\", "/");
					}
					text2 = text2.Substring(3);
					text4 += "../";
				}
				includefile = ((text3 == "") ? "" : (text3 + "/")) + text2;
				string text5 = "";
				string text6 = "";
				text = text.Replace(match.Groups[0].ToString(), "\r\n" + FPViews.CreateView(siteconfig, webpath, ((text3 == "") ? "" : (text3 + "/")) + text2, "", nest + 1, text4, out text5, out text6) + "\r\n");
				if (text5 != "")
				{
					includefile += ((includefile == "") ? text5 : ("," + text5));
				}
				includeimport = text6;
			}
			foreach (object obj2 in FPViews.r[1].Matches(text))
			{
				Match match = (Match)obj2;
				flag = true;
				string text7 = FPViews.ReplaceTemplateFuntion(match.Groups[5].ToString());
				string text8 = "loop__id";
				string text9 = "";
				if (match.Groups[4].ToString().StartsWith("_"))
				{
					text9 = "int ";
					text8 = "loop__id" + match.Groups[4].ToString();
				}
				if (match.Groups[3].ToString() == "")
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\r\n\t{3}{2}=0;\r\n\tforeach(DataRow {0} in {1}.Rows)\r\n\t{{\r\n\t{2}++;\r\n", new object[]
					{
						match.Groups[4].ToString(),
						text7,
						text8,
						text9
					}));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\r\n\t{4}{3}=0;\r\n\tforeach({0} {1} in {2})\r\n\t{{\r\n\t{3}++;\r\n", new object[]
					{
						match.Groups[3].ToString(),
						match.Groups[4].ToString(),
						text7,
						text8,
						text9
					}));
				}
			}
			foreach (object obj3 in FPViews.r[2].Matches(text))
			{
				Match match = (Match)obj3;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "\r\n\t}\t//end loop\r\n");
			}
			foreach (object obj4 in FPViews.r[3].Matches(text))
			{
				Match match = (Match)obj4;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), string.Format("\r\n\tloop__id=0;\r\n\twhile({0}.Read())\r\n\t{{\r\n\tloop__id++;\r\n", FPViews.ReplaceTemplateFuntion(match.Groups[1].ToString())));
			}
			foreach (object obj5 in FPViews.r[4].Matches(text))
			{
				Match match = (Match)obj5;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "\r\n\t}\t//end while\r\n\t" + FPViews.ReplaceTemplateFuntion(match.Groups[1].ToString()) + ".Close();\r\n");
			}
			foreach (object obj6 in FPViews.r[5].Matches(text))
			{
				Match match = (Match)obj6;
				flag = true;
				string text10 = FPViews.ReplaceTemplateFuntion(match.Groups[1].ToString().Replace("\\\"", "\""));
				text = text.Replace(match.Groups[0].ToString(), "\r\n\tif (" + text10 + ")\r\n\t{\r\n");
			}
			foreach (object obj7 in FPViews.r[6].Matches(text))
			{
				Match match = (Match)obj7;
				flag = true;
				if (match.Groups[1].ToString() == string.Empty)
				{
					text = text.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse\r\n\t{\r\n");
				}
				else
				{
					string text10 = FPViews.ReplaceTemplateFuntion(match.Groups[3].ToString().Replace("\\\"", "\""));
					text = text.Replace(match.Groups[0].ToString(), "\r\n\t}\r\n\telse if (" + text10 + ")\r\n\t{\r\n");
				}
			}
			foreach (object obj8 in FPViews.r[7].Matches(text))
			{
				Match match = (Match)obj8;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "\r\n\t}\t//end if\r\n");
			}
			foreach (object obj9 in FPViews.r[19].Matches(text.ToString()))
			{
				Match match = (Match)obj9;
				flag = true;
				string arg = "";
				if (match.Groups[3].ToString() != string.Empty)
				{
					arg = match.Groups[3].ToString();
				}
				text = text.Replace(match.Groups[0].ToString(), string.Format("\t{0} {1} = {2};\r\n\t", arg, match.Groups[4].ToString(), FPViews.ReplaceTemplateFuntion(match.Groups[5].ToString()).Replace("\\\"", "\"")));
			}
			foreach (object obj10 in FPViews.r[24].Matches(text))
			{
				Match match = (Match)obj10;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), string.Concat(new object[]
				{
					"\tfor (int i = 0; i < ",
					match.Groups[2],
					"; i++)\r\n\t{\r\n\t\tViewBuilder.Append(",
					match.Groups[1].ToString().Replace("\\\"", "\"").Replace("\\\\", "\\"),
					");\r\n\t}\r\n"
				}));
			}
			foreach (object obj11 in FPViews.r[25].Matches(text))
			{
				Match match = (Match)obj11;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "\tcontinue;\r\n");
			}
			foreach (object obj12 in FPViews.r[26].Matches(text))
			{
				Match match = (Match)obj12;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "\tbreak;\r\n");
			}
			foreach (object obj13 in FPViews.r[8].Matches(text))
			{
				Match match = (Match)obj13;
				text = text.Replace(match.Groups[0].ToString(), "FangPage.MVC.FPUtils.StrToInt(" + FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()) + ", 0)");
			}
			foreach (object obj14 in FPViews.r[9].Matches(text))
			{
				Match match = (Match)obj14;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), "ViewBuilder.Append(FangPage.MVC.FPUtils.UrlEncode(" + match.Groups[2].ToString() + "));");
			}
			foreach (object obj15 in FPViews.r[10].Matches(text))
			{
				Match match = (Match)obj15;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("FangPage.MVC.FPUtils.GetDate({0},\"{1}\")", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString().Replace("\\\"", string.Empty)));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + FangPage.MVC.FPUtils.GetDate({0},\"{1}\") + \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString().Replace("\\\"", string.Empty)));
				}
			}
			foreach (object obj16 in FPViews.r[30].Matches(text))
			{
				Match match = (Match)obj16;
				text = text.Replace(match.Groups[0].ToString(), string.Format("\" + plugins(\"{0}\") + \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString())));
			}
			foreach (object obj17 in FPViews.r[29].Matches(text))
			{
				Match match = (Match)obj17;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("FangPage.MVC.FPUtils.RemoveHtml(\"{0}\")", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString())));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + FangPage.MVC.FPUtils.RemoveHtml(\"{0}\") + \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString())));
				}
			}
			foreach (object obj18 in FPViews.r[20].Matches(text))
			{
				Match match = (Match)obj18;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("FangPage.MVC.FPUtils.CutString({0},{1})", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString()));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + FangPage.MVC.FPUtils.CutString({0},{1})+ \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString()));
				}
			}
			foreach (object obj19 in FPViews.r[21].Matches(text))
			{
				Match match = (Match)obj19;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("FangPage.MVC.FPThumb.GetThumbnail({0},{1})", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), FPViews.ReplaceTemplateFuntion(match.Groups[3].ToString())));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + FangPage.MVC.FPThumb.GetThumbnail({0},{1})+ \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), FPViews.ReplaceTemplateFuntion(match.Groups[3].ToString())));
				}
			}
			foreach (object obj20 in FPViews.r[27].Matches(text))
			{
				Match match = (Match)obj20;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("{0}({1}).ToString()", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString()));
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0}({1}).ToString() + \"", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString()));
				}
			}
			foreach (object obj21 in FPViews.r[11].Matches(text))
			{
				Match match = (Match)obj21;
				string text11 = match.Groups[2].ToString();
				if (text11.IndexOf("(") >= 0 && text11.IndexOf(")") >= 0)
				{
					text11 = match.Groups[2].ToString();
				}
				if (flag)
				{
					if (match.Groups[3].ToString().ToLower() == "_id")
					{
						if (text11.StartsWith("_"))
						{
							text = text.Replace(match.Groups[0].ToString(), "loop__id" + text11);
						}
						else
						{
							text = text.Replace(match.Groups[0].ToString(), "loop__id");
						}
					}
					else
					{
						text = text.Replace(match.Groups[0].ToString(), string.Format("{0}.{1}", FPViews.ReplaceTemplateFuntion(text11), match.Groups[3].ToString()));
					}
				}
				else if (match.Groups[3].ToString().ToLower() == "_id")
				{
					if (text11.StartsWith("_"))
					{
						text = text.Replace(match.Groups[0].ToString(), string.Format("\" + loop__id{0}.ToString() + \"", text11));
					}
					else
					{
						text = text.Replace(match.Groups[0].ToString(), "\" + loop__id.ToString() + \"");
					}
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0}.{1}.ToString().Trim() + \"", FPViews.ReplaceTemplateFuntion(text11), match.Groups[3].ToString()));
				}
			}
			foreach (object obj22 in FPViews.r[12].Matches(text))
			{
				Match match = (Match)obj22;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), "FangPage.MVC.FPRequest.GetString(\"" + match.Groups[2].ToString() + "\")");
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + FangPage.MVC.FPRequest.GetString(\"{0}\") + \"", match.Groups[2].ToString()));
				}
			}
			foreach (object obj23 in FPViews.r[13].Matches(text))
			{
				Match match = (Match)obj23;
				if (flag)
				{
					if (FPUtils.IsNumeric(match.Groups[3].ToString()))
					{
						text = text.Replace(match.Groups[0].ToString(), match.Groups[2].ToString() + "[" + match.Groups[3].ToString() + "].ToString().Trim()");
					}
					else if (match.Groups[3].ToString().ToLower() == "_id")
					{
						text = text.Replace(match.Groups[0].ToString(), "loop__id");
					}
					else
					{
						text = text.Replace(match.Groups[0].ToString(), match.Groups[2].ToString() + "[\"" + match.Groups[3].ToString() + "\"].ToString().Trim()");
					}
				}
				else if (FPUtils.IsNumeric(match.Groups[3].ToString()))
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0}[{1}].ToString().Trim() + \"", match.Groups[2].ToString(), match.Groups[3].ToString()));
				}
				else if (match.Groups[3].ToString().ToLower() == "_id")
				{
					text = text.Replace(match.Groups[0].ToString(), "\" + loop__id.ToString() + \"");
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0}[\"{1}\"].ToString().Trim() + \"", match.Groups[2].ToString(), match.Groups[3].ToString()));
				}
			}
			foreach (object obj24 in FPViews.r[14].Matches(text))
			{
				Match match = (Match)obj24;
				if (match.Groups[0].ToString().ToLower() == "{fangpage}")
				{
					text = text.Replace(match.Groups[0].ToString(), "\\r\\n\" + 方配视图引擎 + \"");
				}
			}
			foreach (object obj25 in FPViews.r[15].Matches(text))
			{
				Match match = (Match)obj25;
				string text10 = FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString());
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), text10);
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0}.ToString() + \"", text10));
				}
			}
			foreach (object obj26 in FPViews.r[16].Matches(text))
			{
				Match match = (Match)obj26;
				text = text.Replace(match.Groups[0].ToString(), match.Groups[2].ToString() + "\"" + match.Groups[3].ToString() + "\"");
			}
			foreach (object obj27 in FPViews.r[18].Matches(text))
			{
				Match match = (Match)obj27;
				flag = true;
				text = text.Replace(match.Groups[0].ToString(), match.Groups[1].ToString().Replace("\r\t\r", "\r\n\t").Replace("\\\"", "\""));
			}
			foreach (object obj28 in FPViews.r[28].Matches(text))
			{
				Match match = (Match)obj28;
				if (flag)
				{
					text = text.Replace(match.Groups[0].ToString(), match.Groups[1].ToString());
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("\" + {0} + \"", match.Groups[1].ToString()));
				}
			}
			if (flag)
			{
				result = text + "\r\n";
			}
			else if (text.Trim() != "")
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text12 in FPUtils.SplitString(text, "\r\r\r"))
				{
					if (!(text12.Trim() == ""))
					{
						stringBuilder.Append("\tViewBuilder.Append(\"" + text12 + "\\r\\n\");\r\n");
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00006D20 File Offset: 0x00004F20
		private static string IncludeTag(string content)
		{
			content.Replace("<!--#include  ", "<!--#include ");
			content.Replace("<!--#include   ", "<!--#include ");
			content.Replace("<!--#include    ", "<!--#include ");
			content.Replace("<!--#include     ", "<!--#include ");
			Regex[] array = new Regex[]
			{
				new Regex("(<!--#include ([^>]*?)-->)", FPViews.options),
				new Regex("(<title>([^>]*?)<\\/title>)", FPViews.options)
			};
			foreach (object obj in array[0].Matches(content))
			{
				Match match = (Match)obj;
				string str = FPViews.IncludeFileTag(match.Groups[2].ToString());
				content = content.Replace(match.Groups[0].ToString(), "<%include(" + str + ")%>");
			}
			foreach (object obj2 in array[1].Matches(content))
			{
				Match match = (Match)obj2;
				if (content.IndexOf("<meta name=\"keywords\"") < 0 && content.IndexOf("<meta name=\"description\"") < 0)
				{
					content = content.Replace(match.Groups[0].ToString(), match.Groups[0].ToString() + "\r\n\t${meta}");
				}
			}
			return content;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00006EFC File Offset: 0x000050FC
		private static string IncludeFileTag(string attributes)
		{
			Regex regex = new Regex("(file=[\"|']([\\s\\S]*?)[\"|'|])", FPViews.options);
			Match match = regex.Match(attributes);
			string result = attributes;
			if (match != null)
			{
				result = match.Groups[2].ToString();
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006F48 File Offset: 0x00005148
		private static string ReplaceTemplateFuntion(string FuntionName)
		{
			string text = FuntionName;
			text = text.Replace("\\", "");
			foreach (object obj in FPViews.r[8].Matches(text))
			{
				Match match = (Match)obj;
				text = text.Replace(match.Groups[0].ToString(), "FPUtils.StrToInt(" + match.Groups[2].ToString() + ")");
			}
			foreach (object obj2 in FPViews.r[12].Matches(text))
			{
				Match match = (Match)obj2;
				text = text.Replace(match.Groups[0].ToString(), string.Format("FangPage.MVC.FPRequest.GetString(\"{0}\")", match.Groups[2].ToString()));
			}
			foreach (object obj3 in FPViews.t[0].Matches(text))
			{
				Match match = (Match)obj3;
				text = text.Replace(match.Groups[0].ToString(), string.Format("{0}({1})", FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString()), match.Groups[3].ToString()));
			}
			foreach (object obj4 in FPViews.t[1].Matches(text))
			{
				Match match = (Match)obj4;
				if (FPUtils.IsNumeric(match.Groups[3].ToString()))
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("{0}[{1}].ToString()", match.Groups[2].ToString(), match.Groups[3].ToString()));
				}
				else if (match.Groups[3].ToString().ToLower() == "_id")
				{
					text = text.Replace(match.Groups[0].ToString(), "loop__id");
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("{0}[\"{1}\"].ToString()", match.Groups[2].ToString(), match.Groups[3].ToString()));
				}
			}
			foreach (object obj5 in FPViews.t[2].Matches(text))
			{
				Match match = (Match)obj5;
				if (match.Groups[3].ToString().ToLower() == "_id")
				{
					text = text.Replace(match.Groups[0].ToString(), "loop__id");
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Format("{0}.{1}", match.Groups[2].ToString(), match.Groups[3].ToString()));
				}
			}
			foreach (object obj6 in FPViews.t[3].Matches(text))
			{
				Match match = (Match)obj6;
				string newValue = FPViews.ReplaceTemplateFuntion(match.Groups[2].ToString());
				text = text.Replace(match.Groups[0].ToString(), newValue);
			}
			return text;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00007474 File Offset: 0x00005674
		private static string FormatPageCSS(string content, string viewpath, int urltype)
		{
			Regex regex = new Regex("(url\\(([^>]*?)\\))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
			foreach (object obj in regex.Matches(content))
			{
				Match match = (Match)obj;
				content = content.Replace(match.Groups[0].ToString(), "url(" + FPViews.FormatPath(viewpath, match.Groups[2].ToString(), urltype) + ")");
			}
			return content;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00007530 File Offset: 0x00005730
		private static string FormatPath(string viewpath, string linkpath, int urltype)
		{
			string result;
			if (linkpath.StartsWith("/") || linkpath.StartsWith("\\") || linkpath.StartsWith("${") || linkpath.StartsWith("http://") || linkpath.StartsWith("https://"))
			{
				result = linkpath;
			}
			else
			{
				viewpath = Path.GetDirectoryName(viewpath).Replace("\\", "/");
				while (linkpath.StartsWith("../"))
				{
					if (viewpath != "")
					{
						viewpath = Path.GetDirectoryName(viewpath).Replace("\\", "/");
					}
					linkpath = linkpath.Substring(3);
				}
				string text;
				if (urltype == 1)
				{
					text = ((viewpath == "") ? linkpath.Replace("\\", "/") : (viewpath + "/" + linkpath.Replace("\\", "/")));
				}
				else
				{
					text = ((viewpath == "") ? ("${webpath}" + linkpath.Replace("\\", "/")) : ("${webpath}" + viewpath + "/" + linkpath.Replace("\\", "/")));
				}
				result = text;
			}
			return result;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000076A0 File Offset: 0x000058A0
		private static string FormatHref(string href, string linkpath)
		{
			string result;
			if (string.IsNullOrEmpty(href) || href.StartsWith("/") || href.StartsWith("\\") || href.StartsWith("#") || href.StartsWith("${") || href.StartsWith("http://") || href.StartsWith("https://"))
			{
				result = href;
			}
			else
			{
				result = linkpath + href;
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00007720 File Offset: 0x00005920
		public static Hashtable GetViewInclude()
		{
			Hashtable hashtable = FPCache.Get<Hashtable>("FP_VIEW_INCLUDE");
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "cache/views.config");
				if (File.Exists(mapPath))
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(mapPath);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("views");
					foreach (object obj in xmlNode.ChildNodes)
					{
						XmlElement xmlElement = (XmlElement)obj;
						if (xmlElement.NodeType != XmlNodeType.Comment && xmlElement.Name.ToLower() == "view")
						{
							if (xmlElement.Attributes["path"] != null && xmlElement.Attributes["include"] != null)
							{
								if (!hashtable.Contains(xmlElement.Attributes["path"].Value))
								{
									hashtable.Add(xmlElement.Attributes["path"].Value, xmlElement.Attributes["include"].Value);
								}
							}
						}
					}
					FPCache.Insert("FP_VIEW_INCLUDE", hashtable, mapPath);
					return hashtable;
				}
			}
			return hashtable;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000078CC File Offset: 0x00005ACC
		public static bool AddViewInclude(string viewpath, string include)
		{
			bool result;
			try
			{
				string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "cache/views.config");
				if (!Directory.Exists(Path.GetDirectoryName(mapPath)))
				{
					Directory.CreateDirectory(Path.GetDirectoryName(mapPath));
				}
				if (!File.Exists(mapPath))
				{
					string content = "<?xml version=\"1.0\"?>\r\n<views>\r\n</views>";
					FPFile.WriteFile(mapPath, content);
				}
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(mapPath);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("views");
				XmlElement xmlElement = xmlDocument.CreateElement("view");
				if (!string.IsNullOrEmpty(viewpath))
				{
					xmlElement.SetAttribute("path", viewpath);
					xmlElement.SetAttribute("include", include);
				}
				xmlNode.AppendChild(xmlElement);
				xmlDocument.Save(mapPath);
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000079AC File Offset: 0x00005BAC
		public static bool UpdateViewInclude(string viewpath, string include)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + "cache/views.config");
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(mapPath);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("views");
			XmlNodeList childNodes = xmlNode.ChildNodes;
			if (childNodes.Count > 0)
			{
				foreach (object obj in childNodes)
				{
					XmlElement xmlElement = (XmlElement)obj;
					if (xmlElement.Attributes["path"].Value.ToLower() == viewpath)
					{
						xmlElement.SetAttribute("path", viewpath);
						xmlElement.SetAttribute("include", include);
						xmlDocument.Save(mapPath);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00007ABC File Offset: 0x00005CBC
		private static string DelSameImport(string strIm)
		{
			string[] array = FPUtils.DelArraySame(strIm.Split(new char[]
			{
				'\n'
			}));
			string text = "";
			foreach (string str in array)
			{
				text = text + str + "\n";
			}
			return text;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00007B24 File Offset: 0x00005D24
		private static string AddExtImport(string strIm)
		{
			if (!strIm.EndsWith(".*") && !strIm.EndsWith(".#"))
			{
				strIm = strIm.Substring(0, strIm.LastIndexOf("."));
			}
			string text = FPViews.FindImportFile(strIm.Substring(0, strIm.LastIndexOf(".")));
			string text2 = "";
			if (text != "")
			{
				Assembly assembly = Assembly.LoadFrom(FPUtils.GetMapPath("/bin/" + text + ".dll"));
				foreach (Type type in assembly.GetTypes())
				{
					if (type.Namespace != null)
					{
						if (strIm.EndsWith(".#"))
						{
							if (type.Namespace.EndsWith(".Model") || type.Namespace == text)
							{
								text2 = text2 + "<%@ Import namespace=\"" + type.Namespace + "\" %>\r\n";
							}
						}
						else
						{
							text2 = text2 + "<%@ Import namespace=\"" + type.Namespace + "\" %>\r\n";
						}
					}
				}
				text2 = FPViews.DelSameImport(text2);
			}
			return text2;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00007C80 File Offset: 0x00005E80
		private static string FindImportFile(string strIm)
		{
			string text = strIm;
			while (!File.Exists(FPUtils.GetMapPath("/bin/" + text + ".dll")))
			{
				if (text.LastIndexOf(".") <= 0)
				{
					return "";
				}
				text = text.Substring(0, text.LastIndexOf("."));
			}
			return text;
		}

		// Token: 0x04000018 RID: 24
		private static Regex[] r = new Regex[31];

		// Token: 0x04000019 RID: 25
		private static Regex[] t = new Regex[4];

		// Token: 0x0400001A RID: 26
		private static RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
	}
}
