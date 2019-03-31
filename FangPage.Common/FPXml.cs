using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace FangPage.Common
{
	// Token: 0x02000014 RID: 20
	public class FPXml
	{
		// Token: 0x06000109 RID: 265 RVA: 0x00006C84 File Offset: 0x00004E84
		public static void CreateXml<T>(string filename)
		{
			object obj = FPXml.lockHelper;
			lock (obj)
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement newChild = xmlDocument.CreateElement(typeof(T).Name + "s");
				xmlDocument.AppendChild(newChild);
				FPFile.WriteFile(filename, xmlDocument.InnerXml);
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006CF8 File Offset: 0x00004EF8
		public static T LoadModel<T>(string filename) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(filename);
			}
			catch
			{
				return default(T);
			}
			PropertyInfo[] properties = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] array = properties;
			int i = 0;
			while (i < array.Length)
			{
				PropertyInfo propertyInfo = array[i];
				bool flag = propertyInfo != null && propertyInfo.CanWrite;
				if (flag)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode(typeFromHandle.Name + "/" + propertyInfo.Name);
					bool flag2 = xmlNode == null;
					if (!flag2)
					{
						Type propertyType = propertyInfo.PropertyType;
						bool flag3 = propertyType == typeof(string);
						if (flag3)
						{
							propertyInfo.SetValue(t, xmlNode.InnerText, null);
						}
						else
						{
							bool flag4 = propertyType == typeof(int);
							if (flag4)
							{
								propertyInfo.SetValue(t, FPUtils.StrToInt(xmlNode.InnerText), null);
							}
							else
							{
								bool flag5 = propertyType == typeof(DateTime) || propertyType == typeof(DateTime?);
								if (flag5)
								{
									propertyInfo.SetValue(t, FPUtils.StrToDateTime(xmlNode.InnerText), null);
								}
								else
								{
									bool flag6 = propertyType == typeof(decimal);
									if (flag6)
									{
										propertyInfo.SetValue(t, FPUtils.StrToDecimal(xmlNode.InnerText), null);
									}
									else
									{
										bool flag7 = propertyType == typeof(float);
										if (flag7)
										{
											propertyInfo.SetValue(t, FPUtils.StrToFloat(xmlNode.InnerText), null);
										}
										else
										{
											bool flag8 = propertyType == typeof(double);
											if (flag8)
											{
												propertyInfo.SetValue(t, FPUtils.StrToDouble(xmlNode.InnerText), null);
											}
											else
											{
												bool flag9 = propertyType == typeof(bool);
												if (flag9)
												{
													propertyInfo.SetValue(t, FPUtils.StrToBool(xmlNode.InnerText, false), null);
												}
												else
												{
													bool flag10 = propertyType == typeof(short);
													if (flag10)
													{
														propertyInfo.SetValue(t, short.Parse(FPUtils.StrToInt(xmlNode.InnerText).ToString()), null);
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
				//IL_28A:
				i++;
				//continue;
				//goto IL_28A;
			}
			return t;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006FB8 File Offset: 0x000051B8
		public static List<T> LoadList<T>(string filename) where T : new()
		{
			List<T> list = new List<T>();
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.Load(filename);
			}
			catch
			{
				return list;
			}
			bool flag = xmlDocument.ChildNodes.Count == 0;
			List<T> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				bool flag2 = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1].Name.ToLower() != typeof(T).Name.ToLower() + "s";
				if (flag2)
				{
					result = list;
				}
				else
				{
					XmlNode xmlNode = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1];
					foreach (object obj in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						bool flag3 = xmlNode2.Name.ToLower() == typeof(T).Name.ToLower();
						if (flag3)
						{
							list.Add(FPXml.NodeToModel<T>(xmlNode2));
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000710C File Offset: 0x0000530C
		public static void SaveXml<T>(T model, string filename)
		{
			object obj = FPXml.lockHelper;
			lock (obj)
			{
				Type typeFromHandle = typeof(T);
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(typeFromHandle.Name);
				PropertyInfo[] properties = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				foreach (PropertyInfo propertyInfo in properties)
				{
					bool flag = propertyInfo != null && propertyInfo.CanWrite;
					if (flag)
					{
						XmlElement xmlElement2 = xmlDocument.CreateElement(propertyInfo.Name);
						string innerText = string.Empty;
						bool flag2 = propertyInfo.GetValue(model, null) != null;
						if (flag2)
						{
							innerText = propertyInfo.GetValue(model, null).ToString();
						}
						xmlElement2.InnerText = innerText;
						xmlElement.AppendChild(xmlElement2);
					}
				}
				xmlDocument.AppendChild(xmlElement);
				xmlDocument.Save(filename);
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000720C File Offset: 0x0000540C
		public static void SaveXml<T>(List<T> list, string filename)
		{
			object obj = FPXml.lockHelper;
			lock (obj)
			{
				XmlDocument xmlDocument = new XmlDocument();
				XmlElement xmlElement = xmlDocument.CreateElement(typeof(T).Name + "s");
				foreach (T model in list)
				{
					FPXml.ModelToNode<T>(xmlDocument, xmlElement, model);
				}
				xmlDocument.AppendChild(xmlElement);
				xmlDocument.Save(filename);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000072C0 File Offset: 0x000054C0
		public static T ToModel<T>(string xml) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.LoadXml(xml);
			}
			catch (Exception)
			{
				return default(T);
			}
			PropertyInfo[] properties = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			PropertyInfo[] array = properties;
			int i = 0;
			while (i < array.Length)
			{
				PropertyInfo propertyInfo = array[i];
				bool flag = propertyInfo != null && propertyInfo.CanWrite;
				if (flag)
				{
					XmlNode xmlNode = xmlDocument.SelectSingleNode(typeFromHandle.Name + "/" + propertyInfo.Name);
					bool flag2 = xmlNode == null;
					if (!flag2)
					{
						string text = propertyInfo.Name.ToLower();
						Type propertyType = propertyInfo.PropertyType;
						bool flag3 = propertyType == typeof(string);
						if (flag3)
						{
							propertyInfo.SetValue(t, xmlNode.InnerText, null);
						}
						else
						{
							bool flag4 = propertyType == typeof(int);
							if (flag4)
							{
								propertyInfo.SetValue(t, FPUtils.StrToInt(xmlNode.InnerText), null);
							}
							else
							{
								bool flag5 = propertyType == typeof(DateTime) || propertyType == typeof(DateTime?);
								if (flag5)
								{
									propertyInfo.SetValue(t, FPUtils.StrToDateTime(xmlNode.InnerText), null);
								}
								else
								{
									bool flag6 = propertyType == typeof(decimal);
									if (flag6)
									{
										propertyInfo.SetValue(t, FPUtils.StrToDecimal(xmlNode.InnerText), null);
									}
									else
									{
										bool flag7 = propertyType == typeof(float);
										if (flag7)
										{
											propertyInfo.SetValue(t, FPUtils.StrToFloat(xmlNode.InnerText), null);
										}
										else
										{
											bool flag8 = propertyType == typeof(double);
											if (flag8)
											{
												propertyInfo.SetValue(t, FPUtils.StrToDouble(xmlNode.InnerText), null);
											}
											else
											{
												bool flag9 = propertyType == typeof(bool);
												if (flag9)
												{
													propertyInfo.SetValue(t, FPUtils.StrToBool(xmlNode.InnerText, false), null);
												}
												else
												{
													bool flag10 = propertyType == typeof(short);
													if (flag10)
													{
														propertyInfo.SetValue(t, short.Parse(FPUtils.StrToInt(xmlNode.InnerText).ToString()), null);
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
				//IL_298:
				i++;
				//continue;
				//goto IL_298;
			}
			return t;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00007590 File Offset: 0x00005790
		public static string ToXml<T>(T model)
		{
			Type typeFromHandle = typeof(T);
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(typeFromHandle.Name);
			PropertyInfo[] properties = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				bool flag = propertyInfo != null && propertyInfo.CanWrite;
				if (flag)
				{
					XmlElement xmlElement2 = xmlDocument.CreateElement(propertyInfo.Name);
					string innerText = string.Empty;
					bool flag2 = propertyInfo.GetValue(model, null) != null;
					if (flag2)
					{
						innerText = propertyInfo.GetValue(model, null).ToString();
					}
					xmlElement2.InnerText = innerText;
					xmlElement.AppendChild(xmlElement2);
				}
			}
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.InnerXml;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000766C File Offset: 0x0000586C
		public static string ToXml<T>(List<T> list)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlElement xmlElement = xmlDocument.CreateElement(typeof(T).Name + "s");
			foreach (T model in list)
			{
				FPXml.ModelToNode<T>(xmlDocument, xmlElement, model);
			}
			xmlDocument.AppendChild(xmlElement);
			return xmlDocument.InnerXml;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x000076FC File Offset: 0x000058FC
		public static List<T> ToList<T>(string xml) where T : new()
		{
			List<T> list = new List<T>();
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				xmlDocument.LoadXml(xml);
			}
			catch
			{
				return list;
			}
			bool flag = xmlDocument.ChildNodes.Count == 0;
			List<T> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				bool flag2 = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1].Name.ToLower() != typeof(T).Name.ToLower() + "s";
				if (flag2)
				{
					result = list;
				}
				else
				{
					XmlNode xmlNode = xmlDocument.ChildNodes[xmlDocument.ChildNodes.Count - 1];
					foreach (object obj in xmlNode.ChildNodes)
					{
						XmlNode xmlNode2 = (XmlNode)obj;
						bool flag3 = xmlNode2.Name.ToLower() == typeof(T).Name.ToLower();
						if (flag3)
						{
							list.Add(FPXml.NodeToModel<T>(xmlNode2));
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00007850 File Offset: 0x00005A50
		private static void ModelToNode<T>(XmlDocument doc, XmlElement root, T model)
		{
			Type typeFromHandle = typeof(T);
			XmlElement xmlElement = doc.CreateElement(typeFromHandle.Name);
			PropertyInfo[] properties = typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			foreach (PropertyInfo propertyInfo in properties)
			{
				bool flag = propertyInfo != null && propertyInfo.CanWrite;
				if (flag)
				{
					string name = propertyInfo.Name;
					string value = string.Empty;
					bool flag2 = propertyInfo.GetValue(model, null) != null;
					if (flag2)
					{
						value = propertyInfo.GetValue(model, null).ToString();
					}
					xmlElement.SetAttribute(name, value);
				}
			}
			root.AppendChild(xmlElement);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007904 File Offset: 0x00005B04
		private static T NodeToModel<T>(XmlNode node) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			bool flag = node.NodeType == XmlNodeType.Element;
			if (flag)
			{
				XmlElement xmlElement = (XmlElement)node;
				PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
				foreach (object obj in xmlElement.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					string b = xmlAttribute.Name.ToLower();
					string text = xmlAttribute.Value.ToString();
					PropertyInfo[] array = properties;
					int i = 0;
					while (i < array.Length)
					{
						PropertyInfo propertyInfo = array[i];
						bool flag2 = propertyInfo != null && propertyInfo.CanWrite;
						if (flag2)
						{
							string a = propertyInfo.Name.ToLower();
							bool flag3 = a == b;
							if (flag3)
							{
								bool flag4 = string.IsNullOrEmpty(text);
								if (!flag4)
								{
									Type propertyType = propertyInfo.PropertyType;
									bool flag5 = propertyType == typeof(string);
									if (flag5)
									{
										propertyInfo.SetValue(t, text, null);
									}
									else
									{
										bool flag6 = propertyType == typeof(int);
										if (flag6)
										{
											propertyInfo.SetValue(t, FPUtils.StrToInt(text), null);
										}
										else
										{
											bool flag7 = propertyType == typeof(DateTime) || propertyType == typeof(DateTime?);
											if (flag7)
											{
												propertyInfo.SetValue(t, FPUtils.StrToDateTime(text), null);
											}
											else
											{
												bool flag8 = propertyType == typeof(decimal);
												if (flag8)
												{
													propertyInfo.SetValue(t, FPUtils.StrToDecimal(text), null);
												}
												else
												{
													bool flag9 = propertyType == typeof(float);
													if (flag9)
													{
														propertyInfo.SetValue(t, FPUtils.StrToFloat(text), null);
													}
													else
													{
														bool flag10 = propertyType == typeof(double);
														if (flag10)
														{
															propertyInfo.SetValue(t, FPUtils.StrToDouble(text), null);
														}
														else
														{
															bool flag11 = propertyType == typeof(bool);
															if (flag11)
															{
																propertyInfo.SetValue(t, FPUtils.StrToBool(text, false), null);
															}
															else
															{
																bool flag12 = propertyType == typeof(short);
																if (flag12)
																{
																	propertyInfo.SetValue(t, short.Parse(FPUtils.StrToInt(text).ToString()), null);
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
						//IL_296:
						i++;
						//continue;
						//goto IL_296;
					}
				}
			}
			return t;
		}

		// Token: 0x04000029 RID: 41
		private static object lockHelper = new object();
	}
}
