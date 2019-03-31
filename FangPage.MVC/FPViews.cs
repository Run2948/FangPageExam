using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using FangPage.Common;
using HtmlAgilityPack;

namespace FangPage.MVC
{
	// Token: 0x0200000D RID: 13
	public class FPViews
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003E30 File Offset: 0x00002030
		static FPViews()
		{
			FPViews.r[0] = new Regex("(<%--(.*?)--%>)", FPViews.options);
			FPViews.r[1] = new Regex("<%controller\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[2] = new Regex("<%using\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[3] = new Regex("<%#([\\s\\S]+?)%>", FPViews.options);
			FPViews.r[4] = new Regex("<%include\\((?:\"?)([\\s\\S]+?)(?:\"?)\\)%>", FPViews.options);
			FPViews.r[5] = new Regex("<%loop ((\\(([^\\[\\]\\{\\}\\s]+)\\) )?)([^\\[\\]\\{\\}\\s]+) ([^\\s]+)%>", FPViews.options);
			FPViews.r[6] = new Regex("<%\\/loop%>", FPViews.options);
			FPViews.r[7] = new Regex("<%while\\(([^\\[\\]\\{\\}\\s]+)\\)%>", FPViews.options);
			FPViews.r[8] = new Regex("<%\\/while\\(([^\\[\\]\\{\\}\\s]+)\\)%>", FPViews.options);
			FPViews.r[9] = new Regex("<%for\\(([^\\s]+?)(?:\\s*),(?:\\s*)([^\\s]+?)((,([a-zA-Z]+))?)\\)%>", FPViews.options);
			FPViews.r[10] = new Regex("<%\\/for%>", FPViews.options);
			FPViews.r[11] = new Regex("<%continue%>");
			FPViews.r[12] = new Regex("<%break%>");
			FPViews.r[13] = new Regex("<%if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?)(?:\\s*)%>", FPViews.options);
			FPViews.r[14] = new Regex("<%else(( (?:\\s*)if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?))?)(?:\\s*)%>", FPViews.options);
			FPViews.r[15] = new Regex("<%\\/if%>", FPViews.options);
			FPViews.r[16] = new Regex("(\\{request\\((?:\"?)([^\\[\\]\\{\\}\\s]+)\\)\\}(?:\"?))", FPViews.options);
			FPViews.r[17] = new Regex("<%set ((\\(?([\\w\\.<>]+)(?:\\)| ))?)(?:\\s*)\\{?([^\\s\\{\\}]+)\\}?(?:\\s*)=(?:\\s*)(.*?)(?:\\s*)%>", FPViews.options);
			FPViews.r[18] = new Regex("(\\${url\\((?:\\s*)(.*?)(?:\\s*)\\)})", FPViews.options);
			FPViews.r[19] = new Regex("(\\{int\\(([^\\s]+?)\\)\\})", FPViews.options);
			FPViews.r[20] = new Regex("(\\${urlencode\\(([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[21] = new Regex("(\\${thumb\\(([^\\s]+?),([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[22] = new Regex("(\\${txt\\(([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[23] = new Regex("(\\${substr\\(([^\\s]+?),(.\\d*?)\\)})", FPViews.options);
			FPViews.r[24] = new Regex("(\\${fmdate\\(([^\\s]+?),(.*?)\\)})", FPViews.options);
			FPViews.r[25] = new Regex("(\\${([a-zA-Z]+)\\(([^\\[\\]/=\\s]+)\\)})", FPViews.options);
			FPViews.r[26] = new Regex("(\\$\\{([^\\.\\{\\}\\s]+)\\.([^\\[\\]\\{\\}\\s]+)\\})", FPViews.options);
			FPViews.r[27] = new Regex("(\\$\\{([^\\[\\]\\{\\}\\s]+)\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", FPViews.options);
			FPViews.r[28] = new Regex("(\\${([^\\[\\]/\\{\\}='\\s]+)})", FPViews.options);
			FPViews.r[29] = new Regex("(\\${console\\(([^\\s]+?)\\)})", FPViews.options);
			FPViews.r[30] = new Regex("(\\${([^\\s\\{\\}\\$]+?),(.*?)})", FPViews.options);
			FPViews.r[31] = new Regex("<%\\/else(( (?:\\s*)if (?:\\s*)(([^\\s]+)((?:\\s*)(\\|\\||\\&\\&)(?:\\s*)([^\\s]+))?))?)(?:\\s*)%>", FPViews.options);
			FPViews.r[32] = new Regex("(\\${replace\\(([^\\s]+?),(.*?),(.*?)\\)})", FPViews.options);
			FPViews.r[33] = new Regex("(\\${light\\(([^\\s]+?),(.*?)\\)})", FPViews.options);
			FPViews.t[0] = new Regex("({([a-zA-Z]+)\\(([^\\[\\]/='\\s]+)\\)})", FPViews.options);
			FPViews.t[1] = new Regex("(\\{([^\\[\\]\\{\\}\\s]+)\\[([^\\[\\]\\{\\}\\s]+)\\]\\})", FPViews.options);
			FPViews.t[2] = new Regex("(\\{([^\\.\\{\\}\\s]+)\\.([^\\[\\]\\{\\}\\s]+)\\})", FPViews.options);
			FPViews.t[3] = new Regex("({([^\\[\\]/\\{\\}='\\s]+)})", FPViews.options);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000041B0 File Offset: 0x000023B0
		public static string CreateView(SiteConfig siteconfig, string viewpath, string aspxpath, int nest, string linkpath, out string includefile, out string includeimport)
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
			int num = siteconfig.urltype;
			if (viewpath.StartsWith("/"))
			{
				num = 2;
			}
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + viewpath.TrimStart(new char[]
			{
				'/'
			}));
			Path.GetFileName(viewpath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(viewpath);
			if (!File.Exists(mapPath))
			{
				return "";
			}
			HtmlDocument htmlDocument = new HtmlDocument();
			HtmlNode.ElementsFlags.Remove("option");
			htmlDocument.Load(mapPath, Encoding.UTF8);
			HtmlNodeCollection htmlNodeCollection;
			if (num > 0)
			{
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						if (!htmlNode.Attributes["src"].Value.StartsWith("data:image"))
						{
							htmlNode.Attributes["src"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode.Attributes["src"].Value, num);
						}
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//input[@src]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode2.Attributes["src"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode2.Attributes["src"].Value, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//link[@href]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode3 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode3.Attributes["href"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode3.Attributes["href"].Value, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//style[@type='text/css']");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode4 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode4.InnerHtml = FPViews.FormatPageCSS(htmlNode4.InnerHtml, viewpath, aspxpath, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@style]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode5 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode5.Attributes["style"].Value = FPViews.FormatPageCSS(htmlNode5.Attributes["style"].Value, viewpath, aspxpath, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//script[@src]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode6 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode6.Attributes["src"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode6.Attributes["src"].Value, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@background]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode7 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode7.Attributes["background"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode7.Attributes["background"].Value, siteconfig.urltype);
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//param[@name='movie']");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode8 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						if (htmlNode8.Attributes["value"] != null)
						{
							htmlNode8.Attributes["value"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode8.Attributes["value"].Value, siteconfig.urltype);
						}
					}
				}
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//embed[@src]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode9 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode9.Attributes["src"].Value = FPViews.FormatPath(viewpath, aspxpath, htmlNode9.Attributes["src"].Value, siteconfig.urltype);
					}
				}
			}
			if (linkpath != "")
			{
				htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
				if (htmlNodeCollection != null)
				{
					foreach (HtmlNode htmlNode10 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
					{
						htmlNode10.Attributes["href"].Value = FPViews.FormatHref(htmlNode10.Attributes["href"].Value, linkpath);
					}
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@loop]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.LoopNodes(htmlnode);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@for]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.ForNodes(htmlnode2);
				}
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//input[@is-checked]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode11 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text = FPRandom.CreateCode(20);
					string text2 = htmlNode11.Attributes["is-checked"].Value;
					if (text2.StartsWith("${"))
					{
						text2 = FPUtils.TrimStart(text2, "${");
						text2 = FPUtils.TrimEnd(text2, "}");
					}
					text2 = "${" + text2 + ",\"checked\",\"\"}";
					dictionary.Add(text, text2);
					htmlNode11.Attributes["is-checked"].Value = text;
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//option[@is-selected]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode12 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text3 = FPRandom.CreateCode(20);
					string value = "${" + htmlNode12.Attributes["is-selected"].Value.Replace("${", "").Replace("}", "") + ",\"selected\",\"\"}";
					dictionary.Add(text3, value);
					htmlNode12.Attributes["is-selected"].Value = text3;
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//input[@is-disabled]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode13 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text4 = FPRandom.CreateCode(20);
					string text5 = htmlNode13.Attributes["is-disabled"].Value;
					if (text5.StartsWith("${"))
					{
						text5 = FPUtils.TrimStart(text5, "${");
						text5 = FPUtils.TrimEnd(text5, "}");
					}
					text5 = "${" + text5 + ",\"disabled\",\"\"}";
					dictionary.Add(text4, text5);
					htmlNode13.Attributes["is-disabled"].Value = text4;
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@is-show]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode14 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text6 = "${" + htmlNode14.Attributes["is-show"].Value.Replace("'", "\"") + ",\"\",\"display:none\"}";
					htmlNode14.Attributes["is-show"].Remove();
					if (htmlNode14.Attributes["style"] != null)
					{
						string value2 = htmlNode14.Attributes["style"].Value;
						text6 = text6 + ";" + value2;
					}
					htmlNode14.SetAttributeValue("style", text6);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@is-class]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode15 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string value3 = htmlNode15.Attributes["is-class"].Value;
					htmlNode15.Attributes["is-class"].Remove();
					string text7 = "";
					if (htmlNode15.Attributes["class"] != null)
					{
						text7 = htmlNode15.Attributes["class"].Value;
					}
					string[] array = FPArray.SplitString(value3);
					if (array.Length == 1)
					{
						text7 = string.Concat(new string[]
						{
							"${",
							array[0],
							",'",
							text7,
							"',''}"
						});
					}
					else if (array.Length == 2)
					{
						text7 = string.Concat(new string[]
						{
							text7,
							"${",
							array[0],
							",' '+",
							array[1],
							",''}"
						});
					}
					else if (array.Length == 3)
					{
						text7 = string.Concat(new string[]
						{
							text7,
							"${",
							array[0],
							",' '+",
							array[1],
							",' '+",
							array[2],
							"}"
						});
					}
					htmlNode15.SetAttributeValue("class", text7);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@is-style]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlNode16 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string value4 = htmlNode16.Attributes["is-style"].Value;
					htmlNode16.Attributes["is-style"].Remove();
					string text8 = "";
					if (htmlNode16.Attributes["style"] != null)
					{
						text8 = htmlNode16.Attributes["style"].Value;
					}
					string[] array2 = FPArray.SplitString(value4);
					if (array2.Length == 1)
					{
						text8 = string.Concat(new string[]
						{
							"${",
							array2[0],
							",'",
							text8,
							"',''}"
						});
					}
					else if (array2.Length == 2)
					{
						text8 = string.Concat(new string[]
						{
							text8,
							"${",
							array2[0],
							",';'+",
							array2[1],
							",''}"
						});
					}
					else if (array2.Length == 3)
					{
						text8 = string.Concat(new string[]
						{
							text8,
							"${",
							array2[0],
							",';'+",
							array2[1],
							",';'+",
							array2[2],
							"}"
						});
					}
					htmlNode16.SetAttributeValue("style", text8.Trim());
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@if-show]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode3 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.IfShowNodes(htmlnode3);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//*[@else-show]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode4 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.ElseShowNodes(htmlnode4);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//include[@src]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode5 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.IncldeNodes(htmlnode5);
				}
			}
			string text9 = htmlDocument.DocumentNode.InnerHtml;
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (object obj in FPViews.r[0].Matches(text9))
			{
				Match match = (Match)obj;
				text9 = text9.Replace(match.Groups[0].ToString(), "");
			}
			text9 = FPViews.IncludeTag(text9);
			stringBuilder2.Append(text9);
			if (dictionary.Count > 0)
			{
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					stringBuilder2.Replace("is-checked=\"" + keyValuePair.Key + "\"", keyValuePair.Value);
					stringBuilder2.Replace("is-selected=\"" + keyValuePair.Key + "\"", keyValuePair.Value);
					stringBuilder2.Replace("is-disabled=\"" + keyValuePair.Key + "\"", keyValuePair.Value);
					stringBuilder2.Replace("is-checked='" + keyValuePair.Key + "'", keyValuePair.Value);
					stringBuilder2.Replace("is-selected='" + keyValuePair.Key + "'", keyValuePair.Value);
					stringBuilder2.Replace("is-disabled='" + keyValuePair.Key + "'", keyValuePair.Value);
				}
			}
			for (int i = 0; i < 3; i++)
			{
				stringBuilder2.Replace("<% ", "<%");
				stringBuilder2.Replace(" %>", "%>");
			}
			for (int j = 0; j < 3; j++)
			{
				stringBuilder2.Replace("<%Controller ", "<%Controller");
				stringBuilder2.Replace("<%controller ", "<%controller");
				stringBuilder2.Replace("<%Using ", "<%Using");
				stringBuilder2.Replace("<%using ", "<%using");
				stringBuilder2.Replace("<%Include ", "<%Include");
				stringBuilder2.Replace("<%include ", "<%include");
				stringBuilder2.Replace("<%loop  ", "<%loop ");
				stringBuilder2.Replace("<%Loop  ", "<%Loop ");
				stringBuilder2.Replace("<%set  ", "<%set ");
				stringBuilder2.Replace("<%Set  ", "<%Set ");
				stringBuilder2.Replace("<%for ", "<%for");
				stringBuilder2.Replace("<%For ", "<%For");
			}
			stringBuilder2.Replace("<%loop(", "<%loop (");
			stringBuilder2.Replace("<%Loop(", "<%Loop (");
			stringBuilder2.Replace("<%set(", "<%set (");
			stringBuilder2.Replace("<%Set(", "<%Set (");
			stringBuilder2.Replace("~/", "${webpath}");
			stringBuilder2.Replace("{webpath}/", "{webpath}");
			stringBuilder2.Replace("{rawpath}/", "{rawpath}");
			stringBuilder2.Replace("{curpath}/", "{curpath}");
			stringBuilder2.Replace("{plupath}/", "{plupath}");
			stringBuilder2.Replace("{adminpath}/", "{adminpath}");
			stringBuilder2.Replace("{apppath}/", "{apppath}");
			string text10 = "FangPage.MVC.FPController";
			string text11 = "<%@ Import namespace=\"System.Collections.Generic\" %>\r\n<%@ Import namespace=\"FangPage.Common\" %>\r\n<%@ Import namespace=\"FangPage.MVC\" %>";
			string str = FPArray.SplitString(siteconfig.dll, 2)[0];
			if (nest == 1)
			{
                IEnumerator enumerator2 = r[1].Matches(stringBuilder2.ToString()).GetEnumerator();
                if (enumerator2.MoveNext())
                {
                    Match match2 = (Match)enumerator2.Current;
                    text10 = match2.Groups[1].ToString();
                    if (text10 == "*")
                    {
                        text10 = "FangPage.WMS.Web.WebController";
                    }
                    else if (text10 == "#")
                    {
                        text10 = "FangPage.WMS.Web.LoginController";
                    }
                    else if (text10 == "$.*")
                    {
                        text10 = str + "." + fileNameWithoutExtension;
                    }
                    else if (text10.StartsWith("$."))
                    {
                        text10 = text10.Replace("$.", str + ".");
                    }
                    else if (text10.EndsWith(".*"))
                    {
                        text10 = text10.Substring(0, text10.LastIndexOf(".")) + "." + fileNameWithoutExtension;
                        if (text10.StartsWith("*."))
                        {
                            text10 = text10.Replace("*.", "FangPage.WMS.Web.Controller.");
                        }
                    }
                    else if (text10.StartsWith("*."))
                    {
                        text10 = text10.Replace("*.", "FangPage.WMS.Web.Controller.");
                    }
                    stringBuilder2.Replace(match2.Groups[0].ToString(), string.Empty);
                }
                if ("\"".Equals(text10))
				{
					text10 = "FangPage.MVC.FPController";
				}
				if (text10 != "FangPage.MVC.FPController")
				{
					string text12 = FPViews.AddExtImport(text10.Substring(0, text10.LastIndexOf('.')) + ".#");
					if (text12 != "")
					{
						text11 = text11 + "\r\n" + text12;
					}
				}
				if (siteconfig.import != "")
				{
					foreach (string text13 in siteconfig.import.Split(new string[]
					{
						"\r\n",
						";",
						",",
						"|"
					}, StringSplitOptions.RemoveEmptyEntries))
					{
						if (text13 != "")
						{
							if (text11 != "")
							{
								text11 += "\r\n";
							}
							string text14 = text13;
							if (Path.GetExtension(text14).ToLower() == ".dll")
							{
								text14 = text14.Substring(0, text14.LastIndexOf(".")) + ".*";
							}
							if (text14.EndsWith(".*") || text14.EndsWith(".#"))
							{
								string text15 = FPViews.AddExtImport(text14);
								if (text15 != "")
								{
									text11 = text11 + "\r\n" + text15;
								}
							}
							else
							{
								text11 = text11 + "<%@ Import namespace=\"" + text14 + "\" %>";
							}
						}
					}
				}
			}
			foreach (object obj2 in FPViews.r[2].Matches(stringBuilder2.ToString()))
			{
				Match match3 = (Match)obj2;
				string text16 = match3.Groups[1].ToString();
				if (includeimport != "")
				{
					includeimport += "\r\n";
				}
				if (text16.EndsWith(".*") || text16.EndsWith(".#"))
				{
					includeimport += FPViews.AddExtImport(text16);
				}
				else
				{
					includeimport = includeimport + "<%@ Import namespace=\"" + text16 + "\" %>";
				}
				stringBuilder2.Replace(match3.Groups[0].ToString(), string.Empty);
			}
			foreach (object obj3 in FPViews.r[3].Matches(stringBuilder2.ToString()))
			{
				Match match4 = (Match)obj3;
				stringBuilder2.Replace(match4.Groups[0].ToString(), match4.Groups[0].ToString().Replace("\r\n", "\r\t\r"));
			}
			foreach (object obj4 in FPViews.r[29].Matches(stringBuilder2.ToString()))
			{
				Match match5 = (Match)obj4;
				stringBuilder2.Replace(match5.Groups[0].ToString(), "<script type=\"text/javascript\">\r\n\tconsole.log('${" + match5.Groups[2].ToString() + "}');\r\n</script>");
			}
			stringBuilder2.Replace("\r\n", "\r\r\r");
			stringBuilder2.Replace("<%", "\r\r\n<%");
			stringBuilder2.Replace("%>", "%>\r\r\n");
			string[] array4 = FPArray.SplitString(stringBuilder2.ToString(), "\r\r\n");
			int upperBound = array4.GetUpperBound(0);
			for (int l = 0; l <= upperBound; l++)
			{
                stringBuilder.Append(FPViews.ConvertTags(nest, siteconfig, viewpath, aspxpath, array4[l], out var item, out string text17));
                includefile = FPArray.Push(includefile, item, ";");
				if (text17 != "")
				{
					includeimport = includeimport + "\r\n" + text17;
				}
			}
			if (nest == 1)
			{
				if (includeimport != "")
				{
					text11 = text11 + "\r\n" + includeimport;
				}
				text11 = FPViews.DelSameImport(text11);
				string content;
				if (stringBuilder.ToString() != "")
				{
					content = string.Format("<%@ Page language=\"c#\" AutoEventWireup=\"false\" EnableViewState=\"false\" Inherits=\"{0}\" %>\r\n{1}\r\n<script runat=\"server\">\r\nprotected override void View()\r\n{{\r\n\tbase.View();\r\n{2}\tif(iswrite==0)\r\n\t{{\r\n\tResponse.Write(ViewBuilder.ToString());\r\n\t}}\r\n\telse if(iswrite==1)\r\n\t{{\r\n\tHashtable hash = new Hashtable();\r\n\thash[\"errcode\"] = 0;\r\n\thash[\"errmsg\"] =\"\";\r\n\thash[\"html\"]=ViewBuilder.ToString();\r\n\tFPResponse.WriteJson(hash);\r\n\t}}\r\n}}\r\n</script>\r\n", text10, text11, stringBuilder.ToString());
				}
				else
				{
					content = string.Format("<%@ Page language=\"c#\" AutoEventWireup=\"false\" EnableViewState=\"false\" Inherits=\"{0}\" %>\r\n</script>\r\n", text10);
				}
				FPFile.WriteFile(FPFile.GetMapPath(WebConfig.WebPath + aspxpath.TrimStart(new char[]
				{
					'/'
				})), content);
				ViewConfigs.SaveViewConfig(new ViewConfig
				{
					path = viewpath,
					include = includefile
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private static string ConvertTags(int nest, SiteConfig siteconfig, string viewpath, string aspxpath, string inputStr, out string includefile, out string includeimport)
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
			foreach (object obj in FPViews.r[4].Matches(text))
			{
				Match match = (Match)obj;
				flag = true;
				string text2 = match.Groups[1].ToString().Replace("\\", "").Replace("\"", "").Replace("\\", "/");
				string text3 = Path.GetDirectoryName(viewpath).Replace("\\", "/");
				string text4 = Path.GetDirectoryName(aspxpath).Replace("\\", "/");
				string text5 = "";
				while (text2.StartsWith("../"))
				{
					if (text3 != "")
					{
						text3 = Path.GetDirectoryName(text3).Replace("\\", "/");
					}
					if (text4 != "")
					{
						text4 = Path.GetDirectoryName(text4).Replace("\\", "/");
					}
					text2 = text2.Substring(3);
					text5 += "../";
				}
				if (!text2.StartsWith("/"))
				{
					text4 = FPFile.Combine(text4, text2);
					text2 = FPFile.Combine(text3, text2);
				}
				else
				{
					text4 = text2;
				}
				includefile = FPArray.Push(includefile, text2, ";");
                string text7 = FPViews.CreateView(siteconfig, text2, text4, nest + 1, text5, out var item, out var text6);
				if (text7 != "")
				{
					text = text.Replace(match.Groups[0].ToString(), text7);
				}
				else
				{
					text = text.Replace(match.Groups[0].ToString(), string.Empty);
				}
				includefile = FPArray.Push(includefile, item, ";");
				if (text6 != "")
				{
					includeimport = includeimport + "\r\n" + text6;
				}
			}
			foreach (object obj2 in FPViews.r[5].Matches(text))
			{
				Match match2 = (Match)obj2;
				flag = true;
				string text8 = FPViews.ReplaceTemplateFuntion(match2.Groups[5].ToString());
				string text9 = "loop__id";
				string text10 = "";
				if (match2.Groups[4].ToString().StartsWith("_"))
				{
					text10 = "int ";
					text9 = "loop__id" + match2.Groups[4].ToString();
				}
				if (match2.Groups[3].ToString() == "")
				{
					if (text8.EndsWith(".Data"))
					{
						text = text.Replace(match2.Groups[0].ToString(), string.Format("\r\n\t{3}{2}=0;\r\n\tforeach(KeyValuePair<string,string> {0} in {1})\r\n\t{{\r\n\t{2}++;\r\n", new object[]
						{
							match2.Groups[4].ToString(),
							text8,
							text9,
							text10
						}));
					}
					else
					{
						text = text.Replace(match2.Groups[0].ToString(), string.Format("\r\n\t{3}{2}=0;\r\n\tforeach(DataRow {0} in {1}.Rows)\r\n\t{{\r\n\t{2}++;\r\n", new object[]
						{
							match2.Groups[4].ToString(),
							text8,
							text9,
							text10
						}));
					}
				}
				else
				{
					text = text.Replace(match2.Groups[0].ToString(), string.Format("\r\n\t{4}{3}=0;\r\n\tforeach({0} {1} in {2})\r\n\t{{\r\n\t{3}++;\r\n", new object[]
					{
						match2.Groups[3].ToString(),
						match2.Groups[4].ToString(),
						text8,
						text9,
						text10
					}));
				}
			}
			foreach (object obj3 in FPViews.r[6].Matches(text))
			{
				Match match3 = (Match)obj3;
				flag = true;
				text = text.Replace(match3.Groups[0].ToString(), "\t}//end loop\r\n");
			}
			foreach (object obj4 in FPViews.r[7].Matches(text))
			{
				Match match4 = (Match)obj4;
				flag = true;
				text = text.Replace(match4.Groups[0].ToString(), string.Format("\r\n\tloop__id=0;\r\n\twhile({0}.Read())\r\n\t{{\r\n\tloop__id++;\r\n", FPViews.ReplaceTemplateFuntion(match4.Groups[1].ToString())));
			}
			foreach (object obj5 in FPViews.r[8].Matches(text))
			{
				Match match5 = (Match)obj5;
				flag = true;
				text = text.Replace(match5.Groups[0].ToString(), "\t}//end while\r\n\t" + FPViews.ReplaceTemplateFuntion(match5.Groups[1].ToString()) + ".Close();\r\n");
			}
			foreach (object obj6 in FPViews.r[9].Matches(text))
			{
				Match match6 = (Match)obj6;
				flag = true;
				string text11 = match6.Groups[5].ToString();
				if (text11 == "")
				{
					text11 = "i";
				}
				text = text.Replace(match6.Groups[0].ToString(), string.Concat(new object[]
				{
					"\tfor (int ",
					text11,
					" = ",
					match6.Groups[1],
					"; ",
					text11,
					" <= ",
					match6.Groups[2],
					"; ",
					text11,
					"++){\r\n"
				}));
			}
			foreach (object obj7 in FPViews.r[10].Matches(text))
			{
				Match match7 = (Match)obj7;
				flag = true;
				text = text.Replace(match7.Groups[0].ToString(), "\t}//end for\r\n");
			}
			foreach (object obj8 in FPViews.r[11].Matches(text))
			{
				Match match8 = (Match)obj8;
				flag = true;
				text = text.Replace(match8.Groups[0].ToString(), "\tcontinue;\r\n");
			}
			foreach (object obj9 in FPViews.r[12].Matches(text))
			{
				Match match9 = (Match)obj9;
				flag = true;
				text = text.Replace(match9.Groups[0].ToString(), "\tbreak;\r\n");
			}
			foreach (object obj10 in FPViews.r[13].Matches(text))
			{
				Match match10 = (Match)obj10;
				flag = true;
				string str = FPViews.ReplaceTemplateFuntion(match10.Groups[1].ToString().Replace("\\\"", "\""));
				text = text.Replace(match10.Groups[0].ToString(), "\r\n\tif (" + str + ")\r\n\t{\r\n");
			}
			foreach (object obj11 in FPViews.r[14].Matches(text))
			{
				Match match11 = (Match)obj11;
				flag = true;
				if (match11.Groups[1].ToString() == string.Empty)
				{
					text = text.Replace(match11.Groups[0].ToString(), "\t}\r\n\telse\r\n\t{\r\n");
				}
				else
				{
					string str2 = FPViews.ReplaceTemplateFuntion(match11.Groups[3].ToString().Replace("\\\"", "\""));
					text = text.Replace(match11.Groups[0].ToString(), "\t}\r\n\telse if (" + str2 + ")\r\n\t{\r\n");
				}
			}
			foreach (object obj12 in FPViews.r[31].Matches(text))
			{
				Match match12 = (Match)obj12;
				flag = true;
				if (match12.Groups[1].ToString() == string.Empty)
				{
					text = text.Replace(match12.Groups[0].ToString(), "\telse\r\n\t{\r\n");
				}
				else
				{
					string str3 = FPViews.ReplaceTemplateFuntion(match12.Groups[3].ToString().Replace("\\\"", "\""));
					text = text.Replace(match12.Groups[0].ToString(), "\telse if (" + str3 + ")\r\n\t{\r\n");
				}
			}
			foreach (object obj13 in FPViews.r[15].Matches(text))
			{
				Match match13 = (Match)obj13;
				flag = true;
				text = text.Replace(match13.Groups[0].ToString(), "\t}//end if\r\n");
			}
			foreach (object obj14 in FPViews.r[16].Matches(text))
			{
				Match match14 = (Match)obj14;
				text = text.Replace(match14.Groups[0].ToString(), "FPRequest.GetString(\"" + match14.Groups[2].ToString() + "\")");
			}
			foreach (object obj15 in FPViews.r[17].Matches(text.ToString()))
			{
				Match match15 = (Match)obj15;
				flag = true;
				string arg = "";
				if (match15.Groups[3].ToString() != string.Empty)
				{
					arg = match15.Groups[3].ToString();
				}
				text = text.Replace(match15.Groups[0].ToString(), string.Format("\t{0} {1} = {2};\r\n", arg, match15.Groups[4].ToString(), FPViews.ReplaceTemplateFuntion(match15.Groups[5].ToString()).Replace("\\\"", "\"")));
			}
			foreach (object obj16 in FPViews.r[18].Matches(text.ToString()))
			{
				Match match16 = (Match)obj16;
				text = text.Replace(match16.Groups[0].ToString(), string.Format("\" + seturl({0})+ \"", FPViews.ReplaceTemplateFuntion(match16.Groups[2].ToString().Trim()).Replace("'", "\"")));
			}
			foreach (object obj17 in FPViews.r[19].Matches(text))
			{
				Match match17 = (Match)obj17;
				text = text.Replace(match17.Groups[0].ToString(), "FPUtils.StrToInt(" + FPViews.ReplaceTemplateFuntion(match17.Groups[2].ToString()) + ", 0)");
			}
			foreach (object obj18 in FPViews.r[20].Matches(text))
			{
				Match match18 = (Match)obj18;
				text = text.Replace(match18.Groups[0].ToString(), "\"+FPUtils.UrlEncode(" + match18.Groups[2].ToString() + ")+\"");
			}
			foreach (object obj19 in FPViews.r[21].Matches(text))
			{
				Match match19 = (Match)obj19;
				if (flag)
				{
					text = text.Replace(match19.Groups[0].ToString(), string.Format("FPThumb.GetThumbnail({0},{1})", FPViews.ReplaceTemplateFuntion(match19.Groups[2].ToString()), FPViews.ReplaceTemplateFuntion(match19.Groups[3].ToString())));
				}
				else
				{
					text = text.Replace(match19.Groups[0].ToString(), string.Format("\" + FPThumb.GetThumbnail({0},{1})+ \"", FPViews.ReplaceTemplateFuntion(match19.Groups[2].ToString()), FPViews.ReplaceTemplateFuntion(match19.Groups[3].ToString())));
				}
			}
			foreach (object obj20 in FPViews.r[22].Matches(text))
			{
				Match match20 = (Match)obj20;
				if (flag)
				{
					text = text.Replace(match20.Groups[0].ToString(), string.Format("FPUtils.RemoveHtml({0})", FPViews.ReplaceTemplateFuntion(match20.Groups[2].ToString())));
				}
				else
				{
					text = text.Replace(match20.Groups[0].ToString(), string.Format("\" + FPUtils.RemoveHtml({0}) + \"", FPViews.ReplaceTemplateFuntion(match20.Groups[2].ToString())));
				}
			}
			foreach (object obj21 in FPViews.r[23].Matches(text))
			{
				Match match21 = (Match)obj21;
				if (flag)
				{
					text = text.Replace(match21.Groups[0].ToString(), string.Format("echo({0},{1})", FPViews.ReplaceTemplateFuntion(match21.Groups[2].ToString()), match21.Groups[3].ToString()));
				}
				else
				{
					text = text.Replace(match21.Groups[0].ToString(), string.Format("\" + echo({0},{1})+ \"", FPViews.ReplaceTemplateFuntion(match21.Groups[2].ToString()), match21.Groups[3].ToString()));
				}
			}
			foreach (object obj22 in FPViews.r[24].Matches(text))
			{
				Match match22 = (Match)obj22;
				if (flag)
				{
					text = text.Replace(match22.Groups[0].ToString(), string.Format("echo({0},\"{1}\")", FPViews.ReplaceTemplateFuntion(match22.Groups[2].ToString()), match22.Groups[3].ToString().Replace("\\\"", string.Empty)));
				}
				else
				{
					text = text.Replace(match22.Groups[0].ToString(), string.Format("\" + echo({0},\"{1}\") + \"", FPViews.ReplaceTemplateFuntion(match22.Groups[2].ToString()), match22.Groups[3].ToString().Replace("\\\"", string.Empty)));
				}
			}
			foreach (object obj23 in FPViews.r[32].Matches(text))
			{
				Match match23 = (Match)obj23;
				string text12 = match23.Groups[3].ToString().Replace("\\\"", "\"");
				if ((text12.StartsWith("\"") && text12.EndsWith("\"")) || (text12.StartsWith("'") && text12.EndsWith("'")))
				{
					string[] array = FPArray.SplitString(text12.TrimStart(new char[]
					{
						'"'
					}).TrimEnd(new char[]
					{
						'"'
					}).TrimStart(new char[]
					{
						'\''
					}).TrimEnd(new char[]
					{
						'\''
					}), "|");
					text12 = "";
					foreach (string text13 in array)
					{
						if (text13.StartsWith("{") && text13.EndsWith("}"))
						{
							text12 = FPArray.Append(text12, text13.TrimStart(new char[]
							{
								'{'
							}).TrimEnd(new char[]
							{
								'}'
							}), "+\"|\"+");
						}
						else
						{
							text12 = FPArray.Append(text12, "\"" + text13 + "\"", "+\"|\"+");
						}
					}
				}
				else
				{
					string[] array3 = FPArray.SplitString(text12, "|");
					text12 = "";
					foreach (string item2 in array3)
					{
						text12 = FPArray.Append(text12, item2, "+\"|\"+");
					}
				}
				string text14 = match23.Groups[4].ToString().Replace("\\\"", "\"");
				if ((text14.StartsWith("\"") && text14.EndsWith("\"")) || (text14.StartsWith("'") && text14.EndsWith("'")))
				{
					string[] array4 = FPArray.SplitString(text14.TrimStart(new char[]
					{
						'"'
					}).TrimEnd(new char[]
					{
						'"'
					}).TrimStart(new char[]
					{
						'\''
					}).TrimEnd(new char[]
					{
						'\''
					}), "|");
					text14 = "";
					foreach (string text15 in array4)
					{
						if (text15.StartsWith("{") && text15.EndsWith("}"))
						{
							text14 = FPArray.Append(text14, text15.TrimStart(new char[]
							{
								'{'
							}).TrimEnd(new char[]
							{
								'}'
							}), "+\"|\"+");
						}
						else
						{
							text14 = FPArray.Append(text14, "\"" + text15 + "\"", "+\"|\"+");
						}
					}
				}
				else
				{
					string[] array5 = FPArray.SplitString(text14, "|");
					text14 = "";
					foreach (string item3 in array5)
					{
						text14 = FPArray.Append(text14, item3, "+\"|\"+");
					}
				}
				text = text.Replace(match23.Groups[0].ToString(), string.Format("\" + echo({0},{1},{2}) + \"", FPViews.ReplaceTemplateFuntion(match23.Groups[2].ToString()), text12, text14));
			}
			foreach (object obj24 in FPViews.r[33].Matches(text))
			{
				Match match24 = (Match)obj24;
				string text16 = match24.Groups[3].ToString().Replace("\\\"", "\"");
				string text17 = "";
				if ((text16.StartsWith("\"") && text16.EndsWith("\"")) || (text16.StartsWith("'") && text16.EndsWith("'")))
				{
					string[] array6 = FPArray.SplitString(text16.TrimStart(new char[]
					{
						'"'
					}).TrimEnd(new char[]
					{
						'"'
					}).TrimStart(new char[]
					{
						'\''
					}).TrimEnd(new char[]
					{
						'\''
					}), "|");
					text16 = "";
					foreach (string text18 in array6)
					{
						if (text18.StartsWith("{") && text18.EndsWith("}"))
						{
							text16 = FPArray.Append(text16, text18.TrimStart(new char[]
							{
								'{'
							}).TrimEnd(new char[]
							{
								'}'
							}), "+\"|\"+");
							text17 = FPArray.Append(text17, "\"<span style=\\\"background-color:#ffd800;\\\">\"+" + text18.TrimStart(new char[]
							{
								'{'
							}).TrimEnd(new char[]
							{
								'}'
							}) + "+\"</span>\"", "+\"|\"+");
						}
						else
						{
							text16 = FPArray.Append(text16, "\"" + text18 + "\"", "+\"|\"+");
							text17 = FPArray.Append(text17, "\"<span style=\\\"background-color:#ffd800;\\\">" + text18 + "</span>\"", "+\"|\"+");
						}
					}
				}
				else
				{
					string[] array7 = FPArray.SplitString(text16, "|");
					text16 = "";
					foreach (string text19 in array7)
					{
						text16 = FPArray.Append(text16, text19, "+\"|\"+");
						text17 = FPArray.Append(text17, "\"<span style=\\\"background-color:#ffd800;\\\">\"+" + text19 + "+\"</span>\"", "+\"|\"+");
					}
				}
				text = text.Replace(match24.Groups[0].ToString(), string.Format("\" + echo({0},{1},{2}) + \"", FPViews.ReplaceTemplateFuntion(match24.Groups[2].ToString()), text16, text17));
			}
			foreach (object obj25 in FPViews.r[25].Matches(text))
			{
				Match match25 = (Match)obj25;
				if (flag)
				{
					text = text.Replace(match25.Groups[0].ToString(), string.Format("echo({0}({1}))", FPViews.ReplaceTemplateFuntion(match25.Groups[2].ToString()), match25.Groups[3].ToString().Replace("\\\"", "\"").Replace("'", "\"")));
				}
				else
				{
					text = text.Replace(match25.Groups[0].ToString(), string.Format("\" + echo({0}({1})) + \"", FPViews.ReplaceTemplateFuntion(match25.Groups[2].ToString()), match25.Groups[3].ToString().Replace("\\\"", "\"").Replace("'", "\"")));
				}
			}
			foreach (object obj26 in FPViews.r[30].Matches(text))
			{
				Match match26 = (Match)obj26;
				string text20 = FPViews.ReplaceTemplateFuntion(match26.Groups[2].ToString().Replace("\\\"", "\"").Replace("'", "\""));
				string[] array8 = FPArray.SplitString(match26.Groups[3].ToString().Replace("\\\"", "\"").Replace("'", "\""), 2);
				if (!text20.StartsWith("("))
				{
					if (array8[0] == "")
					{
						array8[0] = "\"\"";
					}
					if (array8[1] == "")
					{
						array8[1] = "\"\"";
					}
					text = text.Replace(match26.Groups[0].ToString(), string.Concat(new string[]
					{
						"\"+(",
						text20,
						"?echo(",
						array8[0],
						"):echo(",
						array8[1],
						"))+\""
					}));
				}
			}
			foreach (object obj27 in FPViews.r[26].Matches(text))
			{
				Match match27 = (Match)obj27;
				string text21 = match27.Groups[2].ToString();
				if (text21.IndexOf("(") >= 0 && text21.IndexOf(")") >= 0)
				{
					text21 = match27.Groups[2].ToString();
				}
				if (flag)
				{
					if (match27.Groups[3].ToString().ToLower() == "_id")
					{
						if (text21.StartsWith("_"))
						{
							text = text.Replace(match27.Groups[0].ToString(), "loop__id" + text21);
						}
						else
						{
							text = text.Replace(match27.Groups[0].ToString(), "loop__id");
						}
					}
					else
					{
						text = text.Replace(match27.Groups[0].ToString(), string.Format("{0}.{1}", FPViews.ReplaceTemplateFuntion(text21), match27.Groups[3].ToString()));
					}
				}
				else if (match27.Groups[3].ToString().ToLower() == "_id")
				{
					if (text21.StartsWith("_"))
					{
						text = text.Replace(match27.Groups[0].ToString(), string.Format("\" + loop__id{0}.ToString() + \"", text21));
					}
					else
					{
						text = text.Replace(match27.Groups[0].ToString(), "\" + loop__id.ToString() + \"");
					}
				}
				else
				{
					text = text.Replace(match27.Groups[0].ToString(), string.Format("\" + echo({0}.{1}) + \"", FPViews.ReplaceTemplateFuntion(text21), match27.Groups[3].ToString()));
				}
			}
			foreach (object obj28 in FPViews.r[27].Matches(text))
			{
				Match match28 = (Match)obj28;
				string text22 = match28.Groups[3].ToString();
				if (flag)
				{
					if (FPUtils.IsNumeric(text22))
					{
						text = text.Replace(match28.Groups[0].ToString(), string.Concat(new string[]
						{
							"echo(",
							match28.Groups[2].ToString(),
							"[",
							text22,
							"])"
						}));
					}
					else if (text22.ToLower() == "_id")
					{
						text = text.Replace(match28.Groups[0].ToString(), "loop__id");
					}
					else if (text22.ToLower() == "i" || text22.ToLower() == "j" || (text22.StartsWith("(") && text22.EndsWith(")")))
					{
						text = text.Replace(match28.Groups[0].ToString(), string.Concat(new string[]
						{
							"echo(",
							match28.Groups[2].ToString(),
							"[",
							text22.TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}),
							"])"
						}));
					}
					else
					{
						text = text.Replace(match28.Groups[0].ToString(), string.Concat(new string[]
						{
							"echo(",
							match28.Groups[2].ToString(),
							"[\"",
							text22,
							"\"])"
						}));
					}
				}
				else if (FPUtils.IsNumeric(text22))
				{
					text = text.Replace(match28.Groups[0].ToString(), string.Format("\" + echo({0}[{1}]) + \"", match28.Groups[2].ToString(), text22));
				}
				else if (text22.ToLower() == "_id")
				{
					text = text.Replace(match28.Groups[0].ToString(), "\" + loop__id.ToString() + \"");
				}
				else if (text22.ToLower() == "i" || text22.ToLower() == "j" || (text22.StartsWith("(") && text22.EndsWith(")")))
				{
					text = text.Replace(match28.Groups[0].ToString(), string.Format("\" + echo({0}[{1}]) + \"", match28.Groups[2].ToString(), text22.TrimStart(new char[]
					{
						'('
					}).TrimEnd(new char[]
					{
						')'
					})));
				}
				else
				{
					text = text.Replace(match28.Groups[0].ToString(), string.Format("\" + echo({0}[\"{1}\"]) + \"", match28.Groups[2].ToString(), match28.Groups[3].ToString()));
				}
			}
			foreach (object obj29 in FPViews.r[28].Matches(text))
			{
				Match match29 = (Match)obj29;
				string text23 = FPViews.ReplaceTemplateFuntion(match29.Groups[2].ToString());
				if (flag)
				{
					text = text.Replace(match29.Groups[0].ToString(), text23);
				}
				else
				{
					text = text.Replace(match29.Groups[0].ToString(), string.Format("\" + echo({0}) + \"", text23));
				}
			}
			foreach (object obj30 in FPViews.r[3].Matches(text))
			{
				Match match30 = (Match)obj30;
				flag = true;
				text = text.Replace(match30.Groups[0].ToString(), match30.Groups[1].ToString().Replace("\r\t\r", "\r\n\t").Replace("\\\"", "\""));
			}
			if (flag)
			{
				result = text;
			}
			else if (text.Trim() != "")
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text24 in FPArray.SplitString(text, "\r\r\r"))
				{
					if (!(text24.Trim() == ""))
					{
						stringBuilder.Append("\tViewBuilder.Append(\"" + text24 + "\\r\\n\");\r\n");
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00007F30 File Offset: 0x00006130
		public static void CreateSite(SiteConfig siteconfig)
		{
			FPViews.CreateSite(siteconfig, siteconfig.sitepath);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00007F40 File Offset: 0x00006140
		public static void CreateSite(SiteConfig siteinfo, string sitepath)
		{
			if (!Directory.Exists(FPFile.GetMapPath(WebConfig.WebPath + "sites/" + siteinfo.sitepath)))
			{
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(FPFile.GetMapPath(WebConfig.WebPath + "sites/" + sitepath));
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if (fileInfo.Extension.ToLower() == ".aspx" && !fileInfo.Name.StartsWith("_"))
				{
					string viewpath = "sites/" + sitepath + "/" + fileInfo.Name;
					string aspxpath = sitepath + "/" + fileInfo.Name;
                    FPViews.CreateView(siteinfo, viewpath, aspxpath, 1, "", out _, out _);
				}
				else if (fileInfo.Extension.ToLower() != ".aspx" && (siteinfo.urltype == 0 || siteinfo.urltype == 1))
				{
					string mapPath = FPFile.GetMapPath(WebConfig.WebPath + sitepath);
					if (!Directory.Exists(mapPath))
					{
						Directory.CreateDirectory(mapPath);
					}
					if (File.Exists(mapPath + "\\" + fileInfo.Name) && File.GetAttributes(mapPath + "\\" + fileInfo.Name).ToString().IndexOf("ReadOnly") != -1)
					{
						File.SetAttributes(mapPath + "\\" + fileInfo.Name, FileAttributes.Normal);
					}
					fileInfo.CopyTo(mapPath + "\\" + fileInfo.Name, true);
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				FPViews.CreateSite(siteinfo, sitepath + "/" + directoryInfo2.Name);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00008130 File Offset: 0x00006330
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
				Match match2 = (Match)obj2;
				if (content.IndexOf("<meta name=\"keywords\"") < 0 && content.IndexOf("<meta name=\"description\"") < 0)
				{
					content = content.Replace(match2.Groups[0].ToString(), match2.Groups[0].ToString() + "\r\n\t${meta}");
				}
			}
			return content;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000082D4 File Offset: 0x000064D4
		private static string IncludeFileTag(string attributes)
		{
			Match match = new Regex("(file=[\"|']([\\s\\S]*?)[\"|'|])", FPViews.options).Match(attributes);
			string result = attributes;
			if (match != null)
			{
				result = match.Groups[2].ToString();
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00008310 File Offset: 0x00006510
		private static string ReplaceTemplateFuntion(string FuntionName)
		{
			string text = FuntionName;
			text = text.Replace("\\", "");
			if (!text.StartsWith("{") && text.IndexOf("[") > 0 && text.IndexOf("]") > 0)
			{
				text = "{" + text;
				text = text.Replace("]", "]}");
			}
			foreach (object obj in FPViews.r[19].Matches(text))
			{
				Match match = (Match)obj;
				text = text.Replace(match.Groups[0].ToString(), "FPUtils.StrToInt(" + match.Groups[2].ToString() + ")");
			}
			foreach (object obj2 in FPViews.r[16].Matches(text))
			{
				Match match2 = (Match)obj2;
				text = text.Replace(match2.Groups[0].ToString(), string.Format("FPRequest.GetString(\"{0}\")", match2.Groups[2].ToString()));
			}
			foreach (object obj3 in FPViews.t[0].Matches(text))
			{
				Match match3 = (Match)obj3;
				text = text.Replace(match3.Groups[0].ToString(), string.Format("{0}({1})", FPViews.ReplaceTemplateFuntion(match3.Groups[2].ToString()), match3.Groups[3].ToString()));
			}
			foreach (object obj4 in FPViews.t[1].Matches(text))
			{
				Match match4 = (Match)obj4;
				if (FPUtils.IsNumeric(match4.Groups[3].ToString()))
				{
					text = text.Replace(match4.Groups[0].ToString(), string.Format("{0}[{1}].ToString()", match4.Groups[2].ToString(), match4.Groups[3].ToString()));
				}
				else
				{
					string text2 = match4.Groups[3].ToString();
					if (text2 == "_id")
					{
						text = text.Replace(match4.Groups[0].ToString(), "loop__id");
					}
					else if (text2.ToLower() == "i" || text2.ToLower() == "j" || (text2.StartsWith("(") && text2.EndsWith(")")))
					{
						text = text.Replace(match4.Groups[0].ToString(), string.Format("{0}[{1}]", match4.Groups[2].ToString(), text2.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						})));
					}
					else
					{
						text = text.Replace(match4.Groups[0].ToString(), string.Format("{0}[\"{1}\"].ToString()", match4.Groups[2].ToString(), text2));
					}
				}
			}
			foreach (object obj5 in FPViews.t[2].Matches(text))
			{
				Match match5 = (Match)obj5;
				if (match5.Groups[3].ToString().ToLower() == "_id")
				{
					text = text.Replace(match5.Groups[0].ToString(), "loop__id");
				}
				else
				{
					text = text.Replace(match5.Groups[0].ToString(), string.Format("{0}.{1}", match5.Groups[2].ToString(), match5.Groups[3].ToString()));
				}
			}
			foreach (object obj6 in FPViews.t[3].Matches(text))
			{
				Match match6 = (Match)obj6;
				string newValue = FPViews.ReplaceTemplateFuntion(match6.Groups[2].ToString());
				text = text.Replace(match6.Groups[0].ToString(), newValue);
			}
			if (text.IndexOf("!#") > 0)
			{
				foreach (string text3 in FPArray.SplitString(text, new string[]
				{
					"&&",
					"||"
				}))
				{
					if (text3.IndexOf("!#") > 0)
					{
						string[] array2 = FPArray.SplitString(text3, "!#", 2);
						text = text.Replace(text3, string.Concat(new string[]
						{
							"!FPArray.Contain(",
							array2[0],
							",",
							array2[1],
							")"
						}));
					}
				}
			}
			if (text.IndexOf("#") > 0)
			{
				foreach (string text4 in FPArray.SplitString(text, new string[]
				{
					"&&",
					"||"
				}))
				{
					if (text4.IndexOf("#") > 0)
					{
						string[] array3 = FPArray.SplitString(text4, "#", 2);
						text = text.Replace(text4, string.Concat(new string[]
						{
							"FPArray.Contain(",
							array3[0],
							",",
							array3[1],
							")"
						}));
					}
				}
			}
			return text;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000089BC File Offset: 0x00006BBC
		private static string FormatPageCSS(string content, string viewpath, string aspxpath, int urltype)
		{
			foreach (object obj in new Regex("(url\\(([^>]*?)\\))", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline).Matches(content))
			{
				Match match = (Match)obj;
				content = content.Replace(match.Groups[0].ToString(), "url(" + FPViews.FormatPath(viewpath, aspxpath, match.Groups[2].ToString(), urltype) + ")");
			}
			return content;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008A5C File Offset: 0x00006C5C
		private static string FormatPath(string viewpath, string aspxpath, string linkpath, int urltype)
		{
			linkpath = linkpath.TrimStart(new char[]
			{
				'"'
			}).TrimEnd(new char[]
			{
				'"'
			});
			linkpath = linkpath.TrimStart(new char[]
			{
				'\''
			}).TrimEnd(new char[]
			{
				'\''
			});
			if (linkpath.StartsWith("/") || linkpath.StartsWith("\\") || linkpath.StartsWith("~/") || linkpath.StartsWith("~\\") || linkpath.StartsWith("${") || linkpath.StartsWith("http://") || linkpath.StartsWith("https://"))
			{
				return linkpath;
			}
			string result;
			if (urltype == 1)
			{
				aspxpath = Path.GetDirectoryName(aspxpath).Replace("\\", "/").TrimStart(new char[]
				{
					'/'
				});
				while (linkpath.StartsWith("../"))
				{
					if (aspxpath != "")
					{
						aspxpath = Path.GetDirectoryName(aspxpath).Replace("\\", "/");
					}
					linkpath = linkpath.Substring(3);
				}
				if (aspxpath != "")
				{
					if (aspxpath.IndexOf("/") >= 0)
					{
						aspxpath = aspxpath.Substring(aspxpath.IndexOf("/") + 1);
					}
					else
					{
						aspxpath = "";
					}
				}
				result = "${webpath}/${sitepath}/" + FPFile.Combine(aspxpath, linkpath);
			}
			else
			{
				viewpath = Path.GetDirectoryName(viewpath).Replace("\\", "/").TrimStart(new char[]
				{
					'/'
				});
				while (linkpath.StartsWith("../"))
				{
					if (viewpath != "")
					{
						viewpath = Path.GetDirectoryName(viewpath).Replace("\\", "/");
					}
					linkpath = linkpath.Substring(3);
				}
				result = "${webpath}/" + FPFile.Combine(viewpath, linkpath);
			}
			return result;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00008C40 File Offset: 0x00006E40
		private static string FormatHref(string href, string linkpath)
		{
			if (string.IsNullOrEmpty(href) || href.StartsWith("/") || href.StartsWith("\\") || href.StartsWith("#") || href.StartsWith("${") || href.StartsWith("http://") || href.StartsWith("https://"))
			{
				return href;
			}
			return linkpath + href;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008CAC File Offset: 0x00006EAC
		private static string DelSameImport(string strIm)
		{
			string[] array = FPArray.RemoveSame(FPArray.SplitString(strIm, "\r\n"));
			string text = "";
			foreach (string str in array)
			{
				if (text != "")
				{
					text += "\r\n";
				}
				text += str;
			}
			return text;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008D04 File Offset: 0x00006F04
		private static string AddExtImport(string strIm)
		{
			string text = FPViews.FindImportFile(strIm);
			string text2 = "";
			if (text != "")
			{
				foreach (Type type in FPUtils.GetAssembly(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + text + ".dll")).GetTypes())
				{
					if (type.Namespace != null)
					{
						if (strIm.EndsWith(".#"))
						{
							if (type.Namespace.EndsWith(".Model") || type.Namespace == text)
							{
								if (text2 != "")
								{
									text2 += "\r\n";
								}
								text2 = text2 + "<%@ Import namespace=\"" + type.Namespace + "\" %>";
							}
							if (type.Namespace.EndsWith(".Bll") || type.Namespace == text)
							{
								if (text2 != "")
								{
									text2 += "\r\n";
								}
								text2 = text2 + "<%@ Import namespace=\"" + type.Namespace + "\" %>";
							}
						}
						else
						{
							if (text2 != "")
							{
								text2 += "\r\n";
							}
							text2 = text2 + "<%@ Import namespace=\"" + type.Namespace + "\" %>";
						}
					}
				}
				text2 = FPViews.DelSameImport(text2);
			}
			return text2;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00008E6C File Offset: 0x0000706C
		private static string FindImportFile(string strIm)
		{
			string text = strIm;
			while (!File.Exists(FPFile.GetMapPath(WebConfig.WebPath + "bin/" + text + ".dll")))
			{
				if (text.LastIndexOf(".") <= 0)
				{
					return "";
				}
				text = text.Substring(0, text.LastIndexOf("."));
			}
			return text;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008EC8 File Offset: 0x000070C8
		private static void IncldeNodes(HtmlNode htmlnode)
		{
			string value = htmlnode.Attributes["src"].Value;
			HtmlNode htmlNode = HtmlNode.CreateNode("<%include(" + value + ")%>\r\n");
			htmlnode.ParentNode.ReplaceChild(htmlNode.ParentNode, htmlnode);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00008F14 File Offset: 0x00007114
		private static void LoopNodes(HtmlNode htmlnode)
		{
			string text = htmlnode.Attributes["loop"].Value.Replace("'", "\"");
			htmlnode.Attributes["loop"].Remove();
			HtmlNode htmlNode = HtmlNode.CreateNode(string.Concat(new string[]
			{
				"<%loop ",
				text,
				"%>\r\n",
				htmlnode.OuterHtml,
				"<%/loop%>\r\n"
			}));
			HtmlNodeCollection htmlNodeCollection = htmlNode.SelectNodes("//*[@loop]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.LoopNodes(htmlnode2);
				}
			}
			htmlnode.ParentNode.ReplaceChild(htmlNode.ParentNode, htmlnode);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00008FEC File Offset: 0x000071EC
		private static void ForNodes(HtmlNode htmlnode)
		{
			string text = htmlnode.Attributes["for"].Value.Replace("'", "\"");
			if (text.IndexOf(',') <= 0)
			{
				return;
			}
			htmlnode.Attributes["for"].Remove();
			HtmlNode htmlNode = HtmlNode.CreateNode(string.Concat(new string[]
			{
				"<%for(",
				text.Trim(),
				")%>\r\n",
				htmlnode.OuterHtml,
				"<%/for%>\r\n"
			}));
			HtmlNodeCollection htmlNodeCollection = htmlNode.SelectNodes("//*[@for]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.ForNodes(htmlnode2);
				}
			}
			htmlnode.ParentNode.ReplaceChild(htmlNode.ParentNode, htmlnode);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000090D4 File Offset: 0x000072D4
		private static void IfShowNodes(HtmlNode htmlnode)
		{
			string text = htmlnode.Attributes["if-show"].Value.Replace("'", "\"");
			htmlnode.Attributes["if-show"].Remove();
			HtmlNode htmlNode = HtmlNode.CreateNode(string.Concat(new string[]
			{
				"<%if ",
				text,
				"%>\r\n",
				htmlnode.OuterHtml,
				"<%/if%>\r\n"
			}));
			HtmlNodeCollection htmlNodeCollection = htmlNode.SelectNodes("//*[@if-show]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.IfShowNodes(htmlnode2);
				}
			}
			htmlnode.ParentNode.ReplaceChild(htmlNode.ParentNode, htmlnode);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000091AC File Offset: 0x000073AC
		private static void ElseShowNodes(HtmlNode htmlnode)
		{
			string text = htmlnode.Attributes["else-show"].Value.Replace("'", "\"");
			htmlnode.Attributes["else-show"].Remove();
			string html;
			if (text != "")
			{
				html = string.Concat(new string[]
				{
					"<%/else if ",
					text,
					"%>\r\n",
					htmlnode.OuterHtml,
					"<%/if%>\r\n"
				});
			}
			else
			{
				html = "<%/else%>\r\n" + htmlnode.OuterHtml + "<%/if%>\r\n";
			}
			HtmlNode htmlNode = HtmlNode.CreateNode(html);
			HtmlNodeCollection htmlNodeCollection = htmlNode.SelectNodes("//*[@else-show]");
			if (htmlNodeCollection != null)
			{
				foreach (HtmlNode htmlnode2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					FPViews.ElseShowNodes(htmlnode2);
				}
			}
			htmlnode.ParentNode.ReplaceChild(htmlNode.ParentNode, htmlnode);
		}

		// Token: 0x04000025 RID: 37
		private static Regex[] r = new Regex[34];

		// Token: 0x04000026 RID: 38
		private static Regex[] t = new Regex[4];

		// Token: 0x04000027 RID: 39
		private static RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
	}
}
