using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using FangPage.Common;

namespace FangPage.Data
{
	// Token: 0x0200000A RID: 10
	public class DbHelper
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000278C File Offset: 0x0000098C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000027A4 File Offset: 0x000009A4
		public static DbConfigInfo DbConfig
		{
			get
			{
				if (DbHelper.m_dbconfig == null)
				{
					DbHelper.m_dbconfig = DbConfigs.GetDbConfig();
				}
				return DbHelper.m_dbconfig;
			}
			set
			{
				DbHelper.m_dbconfig = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000027AC File Offset: 0x000009AC
		public static IDbProvider DbProvider
		{
			get
			{
				if (DbHelper.m_dbprovider == null)
				{
					DbType dbtype = DbHelper.DbConfig.dbtype;
					if (dbtype != DbType.SqlServer)
					{
						if (dbtype == DbType.Access)
						{
							DbHelper.m_dbprovider = new AccessProvider();
						}
					}
					else
					{
						DbHelper.m_dbprovider = new SqlServerProvider();
					}
				}
				return DbHelper.m_dbprovider;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000027EF File Offset: 0x000009EF
		public static DbProviderFactory Factory
		{
			get
			{
				if (DbHelper.m_factory == null)
				{
					DbHelper.m_factory = DbHelper.DbProvider.Instance();
				}
				return DbHelper.m_factory;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000280C File Offset: 0x00000A0C
		public static void ResetDbProvider()
		{
			DbConfigs.ReSet();
			DbHelper.m_dbconfig = null;
			DbHelper.m_factory = null;
			DbHelper.m_dbprovider = null;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002828 File Offset: 0x00000A28
		private static void AttachParameters(DbCommand command, DbParameter[] commandParameters)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (commandParameters != null)
			{
				foreach (DbParameter dbParameter in commandParameters)
				{
					if (dbParameter != null)
					{
						if ((dbParameter.Direction == ParameterDirection.InputOutput || dbParameter.Direction == ParameterDirection.Input) && dbParameter.Value == null)
						{
							dbParameter.Value = DBNull.Value;
						}
						command.Parameters.Add(dbParameter);
					}
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002890 File Offset: 0x00000A90
		private static void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, out bool mustCloseConnection)
		{
			if (command == null)
			{
				throw new ArgumentNullException("command");
			}
			if (commandText == null || commandText.Length == 0)
			{
				throw new ArgumentNullException("commandText");
			}
			if (connection.State != ConnectionState.Open)
			{
				mustCloseConnection = true;
				connection.Open();
			}
			else
			{
				mustCloseConnection = false;
			}
			command.Connection = connection;
			command.CommandText = commandText;
			if (transaction != null)
			{
				if (transaction.Connection == null)
				{
					throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
				}
				command.Transaction = transaction;
			}
			command.CommandType = commandType;
			if (commandParameters != null)
			{
				DbHelper.AttachParameters(command, commandParameters);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002920 File Offset: 0x00000B20
		public static AttrInfo GetAttrInfo(PropertyInfo info)
		{
			AttrInfo attrInfo = new AttrInfo();
			if (info != null)
			{
				foreach (object obj in info.GetCustomAttributes(true))
				{
					if (obj is NText)
					{
						attrInfo.IsNtext = (obj as NText).IsNText;
					}
					else if (obj is PrimaryKey)
					{
						attrInfo.IsPrimaryKey = (obj as PrimaryKey).IsPrimaryKey;
					}
					else if (obj is Identity)
					{
						attrInfo.IsIdentity = (obj as Identity).IsIdentity;
					}
					else if (obj is TempField)
					{
						attrInfo.IsTempField = (obj as TempField).IsTemp;
					}
					else if (obj is BindField)
					{
						attrInfo.IsBindField = (obj as BindField).IsBind;
					}
					else if (obj is LeftJoin)
					{
						attrInfo.IsLeftJoin = true;
						LeftJoin leftJoin = obj as LeftJoin;
						attrInfo.TableName = leftJoin.TableName;
						attrInfo.Number = leftJoin.Number;
						attrInfo.ColName = leftJoin.Field;
					}
					else if (obj is Map)
					{
						attrInfo.IsMap = true;
						Map map = obj as Map;
						attrInfo.TableName = map.TableName;
						attrInfo.Number = map.Number;
						attrInfo.ColName = map.Field;
					}
				}
			}
			return attrInfo;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static List<PropertyInfo> GetIdentitys(Type type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (DbHelper.GetAttrInfo(propertyInfo).IsIdentity)
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public static List<PropertyInfo> GetPrimaryKeys(Type type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (DbHelper.GetAttrInfo(propertyInfo).IsPrimaryKey)
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public static Dictionary<string, AttrInfo> GetLeftJoinList(Type type)
		{
			Dictionary<string, AttrInfo> dictionary = new Dictionary<string, AttrInfo>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsMap)
				{
					dictionary.Add(propertyInfo.Name, attrInfo);
				}
			}
			return dictionary;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B40 File Offset: 0x00000D40
		public static string GetModelTable(Type type)
		{
			string text = type.Name;
			object[] customAttributes = type.GetCustomAttributes(true);
			ModelPrefix modelPrefix = null;
			BindTable bindTable = null;
			foreach (object obj in customAttributes)
			{
				if (obj is BindTable)
				{
					bindTable = (obj as BindTable);
				}
				else if (obj is ModelPrefix)
				{
					modelPrefix = (obj as ModelPrefix);
				}
				if (bindTable != null && modelPrefix != null)
				{
					break;
				}
			}
			if (modelPrefix != null && !string.IsNullOrEmpty(modelPrefix.Prefix))
			{
				text = modelPrefix.Prefix + "_" + text;
			}
			if (bindTable != null && !string.IsNullOrEmpty(bindTable.TableName))
			{
				text = bindTable.TableName;
			}
			return text;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BDC File Offset: 0x00000DDC
		public static DbParameter MakeInParam(string ParamName, object Value)
		{
			if (!ParamName.StartsWith("@"))
			{
				ParamName = "@" + ParamName;
			}
			Type type = Value.GetType();
			object value;
			if (type == typeof(FPData))
			{
				value = FPJson.ToJson(Value as FPData);
			}
			else if (type == typeof(List<FPData>))
			{
				value = FPJson.ToJson(Value as List<FPData>);
			}
			else
			{
				value = Value;
			}
			return DbHelper.DbProvider.MakeParam(ParamName, value);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002C50 File Offset: 0x00000E50
		public static DbParameter MakeInParam<T>(PropertyInfo propInfo, T targetObj)
		{
			string paramName = string.Format("@{0}", propInfo.Name);
			object value;
			if (propInfo.PropertyType == typeof(FPData))
			{
				value = FPJson.ToJson(propInfo.GetValue(targetObj, null) as FPData);
			}
			else if (propInfo.PropertyType == typeof(List<FPData>))
			{
				value = FPJson.ToJson(propInfo.GetValue(targetObj, null) as List<FPData>);
			}
			else
			{
				value = propInfo.GetValue(targetObj, null);
			}
			return DbHelper.DbProvider.MakeParam(paramName, value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002CE0 File Offset: 0x00000EE0
		public static DbParameter MakeInParam(string ParamName, DbType DbType, int Size, object Value)
		{
			return DbHelper.MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002CEC File Offset: 0x00000EEC
		public static DbParameter MakeOutParam(string ParamName, DbType DbType, int Size)
		{
			return DbHelper.MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public static DbParameter MakeParam(string ParamName, DbType DbType, int Size, ParameterDirection Direction, object Value)
		{
			if (!ParamName.StartsWith("@"))
			{
				ParamName = "@" + ParamName;
			}
			DbParameter dbParameter = DbHelper.DbProvider.MakeParam(ParamName, DbType, Size);
			dbParameter.Direction = Direction;
			if (Direction != ParameterDirection.Output || Value != null)
			{
				dbParameter.Value = Value;
			}
			return dbParameter;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002D45 File Offset: 0x00000F45
		public static SqlParam MakeAndWhere(string WhereStr)
		{
			return new SqlParam(SqlType.And, WhereStr, WhereType.Custom, null);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002D54 File Offset: 0x00000F54
		public static SqlParam MakeAndWhere(string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return new SqlParam(SqlType.And, ParamName, whereType, "");
			}
			return new SqlParam(SqlType.And, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public static SqlParam MakeAndWhere(char Brackets, string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeAndWhere(Brackets, ParamName, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.And, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E20 File Offset: 0x00001020
		public static SqlParam MakeAndWhere(string ParamName, char Brackets, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeAndWhere(ParamName, Brackets, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.And, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E97 File Offset: 0x00001097
		public static SqlParam MakeAndWhere(string ParamName, WhereType WhereType, object Value)
		{
			return new SqlParam(SqlType.And, ParamName, WhereType, Value);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002EA2 File Offset: 0x000010A2
		public static SqlParam MakeAndWhere(char Brackets, string ParamName, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.And, ParamName, WhereType, Value);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002ED3 File Offset: 0x000010D3
		public static SqlParam MakeAndWhere(string ParamName, char Brackets, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.And, ParamName, WhereType, Value);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002F04 File Offset: 0x00001104
		public static SqlParam MakeOrWhere(string WhereStr)
		{
			return new SqlParam(SqlType.Or, WhereStr, WhereType.Custom, null);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F10 File Offset: 0x00001110
		public static SqlParam MakeOrWhere(string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return new SqlParam(SqlType.Or, ParamName, whereType, "");
			}
			return new SqlParam(SqlType.Or, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F64 File Offset: 0x00001164
		public static SqlParam MakeOrWhere(char Brackets, string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeOrWhere(Brackets, ParamName, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.Or, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FDC File Offset: 0x000011DC
		public static SqlParam MakeOrWhere(string ParamName, char Brackets, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeOrWhere(Brackets, ParamName, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.Or, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003053 File Offset: 0x00001253
		public static SqlParam MakeOrWhere(string ParamName, WhereType WhereType, object Value)
		{
			return new SqlParam(SqlType.Or, ParamName, WhereType, Value);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000305E File Offset: 0x0000125E
		public static SqlParam MakeOrWhere(char Brackets, string ParamName, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.Or, ParamName, WhereType, Value);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000308F File Offset: 0x0000128F
		public static SqlParam MakeOrWhere(string ParamName, char Brackets, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.Or, ParamName, WhereType, Value);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000030C0 File Offset: 0x000012C0
		public static SqlParam MakeWhere(string WhereStr)
		{
			return new SqlParam(SqlType.None, WhereStr, WhereType.Custom, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000030CC File Offset: 0x000012CC
		public static SqlParam MakeWhere(string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return new SqlParam(SqlType.None, ParamName, whereType, "");
			}
			return new SqlParam(SqlType.None, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003120 File Offset: 0x00001320
		public static SqlParam MakeWhere(char Brackets, string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeWhere(Brackets, ParamName, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.None, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003198 File Offset: 0x00001398
		public static SqlParam MakeWhere(string ParamName, char Brackets, object Value)
		{
			if (Value.GetType() == typeof(WhereType))
			{
				WhereType whereType = (WhereType)Enum.Parse(typeof(WhereType), Value.ToString());
				return DbHelper.MakeWhere(Brackets, ParamName, whereType, "");
			}
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.None, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000320F File Offset: 0x0000140F
		public static SqlParam MakeWhere(string ParamName, WhereType WhereType, object Value)
		{
			return new SqlParam(SqlType.None, ParamName, WhereType, Value);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000321A File Offset: 0x0000141A
		public static SqlParam MakeWhere(char Brackets, string ParamName, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.None, ParamName, WhereType, Value);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000324B File Offset: 0x0000144B
		public static SqlParam MakeWhere(string ParamName, char Brackets, WhereType WhereType, object Value)
		{
			if (Brackets == '(')
			{
				ParamName = "(" + ParamName;
			}
			else if (Brackets == ')')
			{
				ParamName += ")";
			}
			return new SqlParam(SqlType.None, ParamName, WhereType, Value);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000327C File Offset: 0x0000147C
		public static SqlParam MakeUpdate(string ParamName, object Value)
		{
			if (Value.GetType() == typeof(WhereType) && Value.ToString() == "Custom")
			{
				return new SqlParam(SqlType.Update, "", WhereType.Custom, ParamName);
			}
			return new SqlParam(SqlType.Update, ParamName, Value);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032B9 File Offset: 0x000014B9
		public static SqlParam MakeUpdate(string ParamName, WhereType WhereType, object Value)
		{
			if (WhereType == WhereType.Custom)
			{
				return new SqlParam(SqlType.Update, ParamName, WhereType.Custom, Value);
			}
			return new SqlParam(SqlType.Update, ParamName, Value);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000032D3 File Offset: 0x000014D3
		public static SqlParam MakeSet(string ParamName, object Value)
		{
			return new SqlParam(SqlType.Set, ParamName, Value);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000032DD File Offset: 0x000014DD
		public static SqlParam MakeOrderBy(string FieldName, OrderBy OrderByType)
		{
			return new SqlParam(FieldName, OrderByType);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000032E8 File Offset: 0x000014E8
		public static string CreateWhere(params SqlParam[] sqlparams)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType != SqlType.Update && sqlParam.SqlType != SqlType.Set && sqlParam.SqlType != SqlType.Insert && sqlParam.SqlType != SqlType.OrderBy)
				{
					if (sqlParam.SqlType == SqlType.And && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" AND ");
					}
					else if (sqlParam.SqlType == SqlType.Or && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" OR ");
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						stringBuilder.Append(sqlParam.ParamName);
					}
					else
					{
						if (sqlParam.ParamName.StartsWith("("))
						{
							stringBuilder.Append("(");
						}
						bool flag = false;
						if (sqlParam.ParamName.EndsWith(")"))
						{
							flag = true;
						}
						string text = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						string text2 = "";
						if (text.IndexOf('[') > 0)
						{
							string[] array = FPArray.SplitString(text, "[");
							text = array[0];
							text2 = array[1];
						}
						string text3 = sqlParam.Value.ToString().Replace("'", "''");
						if (text3.IndexOf("[") >= 0 && text3.IndexOf("]") >= 0 && sqlParam.WhereType != WhereType.Like && sqlParam.WhereType != WhereType.NotLike)
						{
							stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
						}
						else
						{
							if (sqlParam.WhereType == WhereType.In)
							{
								string text4 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text4))
								{
									stringBuilder.AppendFormat("[{0}] IN({1})", text, text4);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotIn)
							{
								string text5 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text5))
								{
									stringBuilder.AppendFormat("([{0}] NOT IN({1}) OR [{0}] IS NULL)", text, text5);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Like)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array2 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text6 = "";
									foreach (string str in array2)
									{
										if (text6 != "")
										{
											text6 += " OR ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text6 += string.Format("[{0}] LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str));
											}
											else if (text2 == "#")
											{
												text6 += string.Format("[{0}] LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str));
											}
											else
											{
												text6 += string.Format("[{0}] LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str));
											}
										}
										else
										{
											text6 += string.Format("[{0}] LIKE '%{1}%'", text, DbUtils.RegEsc(str));
										}
									}
									if (array2.Length > 1)
									{
										text6 = "(" + text6 + ")";
									}
									stringBuilder.Append(text6);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotLike)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array4 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text7 = "";
									foreach (string str2 in array4)
									{
										if (text7 != "")
										{
											text7 += " AND ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str2));
											}
											else if (text2 == "#")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str2));
											}
											else
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str2));
											}
										}
										else
										{
											text7 += string.Format("[{0}] NOT LIKE '%{1}%'", text, DbUtils.RegEsc(str2));
										}
									}
									if (array4.Length > 1)
									{
										text7 = string.Format("(({0}) OR [{1}] IS NULL)", text7, text);
									}
									else
									{
										text7 = string.Format("({0} OR [{1}] IS NULL)", text7, text);
									}
									stringBuilder.Append(text7);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Contain)
							{
								string text8 = "";
								foreach (string str3 in DbUtils.SplitString(text3, ","))
								{
									if (text8 != "")
									{
										text8 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text8 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str3));
										}
										else if (text2 == "#")
										{
											text8 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str3));
										}
										else
										{
											text8 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str3));
										}
									}
									else
									{
										text8 += string.Format("(','+[{0}]+',') LIKE '%,{1},%'", text, DbUtils.RegEsc(str3));
									}
								}
								if (text8 != "")
								{
									stringBuilder.AppendFormat("({0})", text8);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.ContainOrEmpty)
							{
								string text9 = "";
								foreach (string str4 in DbUtils.SplitString(text3, ","))
								{
									if (text9 != "")
									{
										text9 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text9 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%' OR [{0}] LIKE '%\":\"\"%')", text, DbUtils.RegEsc(str4));
										}
										else if (text2 == "#")
										{
											text9 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str4));
										}
										else
										{
											text9 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%' OR [{0}] LIKE '%\"{1}\":\"\"%')", text, text2, DbUtils.RegEsc(str4));
										}
									}
									else
									{
										text9 += string.Format("((','+[{0}]+',') LIKE '%,{1},%' OR [{0}]='' OR [{0}] IS NULL)", text, DbUtils.RegEsc(str4));
									}
								}
								if (text9 != "")
								{
									stringBuilder.AppendFormat("({0})", text9);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.NotContain)
							{
								string text10 = "";
								foreach (string str5 in DbUtils.SplitString(text3, ","))
								{
									if (text10 != "")
									{
										text10 += " AND ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text10 += string.Format("([{0}] NOT LIKE '%\":\"%,{1},%\"%' AND [{0}] NOT LIKE '%\":\"{1},%\"%' AND [{0}] NOT LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str5));
										}
										else if (text2 == "#")
										{
											text10 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str5));
										}
										else
										{
											text10 += string.Format("([{0}] NOT LIKE '%\"{1}\":\"%,{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str5));
										}
									}
									else
									{
										text10 += string.Format("(','+[{0}]+',') NOT LIKE '%,{1},%'", text, DbUtils.RegEsc(str5));
									}
								}
								if (text10 != "")
								{
									stringBuilder.AppendFormat("(({0}) OR [{1}] IS NULL)", text10, text);
								}
								else
								{
									stringBuilder.AppendFormat("1=1", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNull)
							{
								string[] array5 = DbUtils.SplitString(text, ",");
								if (array5.Length > 1)
								{
									string text11 = "";
									foreach (string arg in array5)
									{
										if (text11 != "")
										{
											text11 += " AND ";
										}
										text11 += string.Format("[{0}] IS NULL ", arg);
									}
									stringBuilder.Append("(" + text11 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNotNull)
							{
								string[] array6 = DbUtils.SplitString(text, ",");
								if (array6.Length > 1)
								{
									string text12 = "";
									foreach (string arg2 in array6)
									{
										if (text12 != "")
										{
											text12 += " AND ";
										}
										text12 += string.Format("[{0}] IS NOT NULL ", arg2);
									}
									stringBuilder.Append("(" + text12 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NOT NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNullOrEmpty)
							{
								string[] array7 = DbUtils.SplitString(text, ",");
								if (array7.Length > 1)
								{
									string text13 = "";
									foreach (string arg3 in array7)
									{
										if (text13 != "")
										{
											text13 += " AND ";
										}
										text13 += string.Format("([{0}] IS NULL OR [{0}]='')", arg3);
									}
									stringBuilder.Append("(" + text13 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("([{0}] IS NULL OR [{0}]='')", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsEmpty)
							{
								string[] array8 = DbUtils.SplitString(text, ",");
								if (array8.Length > 1)
								{
									string text14 = "";
									foreach (string arg4 in array8)
									{
										if (text14 != "")
										{
											text14 += " AND ";
										}
										text14 += string.Format("[{0}]=''", arg4);
									}
									stringBuilder.Append("(" + text14 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}]=''", text);
								}
							}
							else if (text2 != "")
							{
								if (text2 == "*")
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\":\"{1}\"%'", text, DbUtils.RegEsc(text3));
								}
								else if (text2 == "#")
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(text3));
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"{2}\"%'", text, text2, DbUtils.RegEsc(text3));
								}
							}
							else
							{
								Type type = sqlParam.Value.GetType();
								if (type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTime?))
								{
									if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
									{
										stringBuilder.AppendFormat("([{0}]{1}'{2}' OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
									}
									else
									{
										stringBuilder.AppendFormat("[{0}]{1}'{2}'", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
									}
								}
								else if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
								{
									stringBuilder.AppendFormat("([{0}]{1}{2} OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
								}
								else
								{
									stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
								}
							}
							if (flag)
							{
								stringBuilder.Append(")");
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000403C File Offset: 0x0000223C
		public static string CreateWhere<T>(params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType != SqlType.Update && sqlParam.SqlType != SqlType.Set && sqlParam.SqlType != SqlType.Insert && sqlParam.SqlType != SqlType.OrderBy)
				{
					if (sqlParam.SqlType == SqlType.And && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" AND ");
					}
					else if (sqlParam.SqlType == SqlType.Or && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" OR ");
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						stringBuilder.Append(sqlParam.ParamName);
					}
					else
					{
						if (sqlParam.ParamName.StartsWith("("))
						{
							stringBuilder.Append("(");
						}
						bool flag = false;
						if (sqlParam.ParamName.EndsWith(")"))
						{
							flag = true;
						}
						string text = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						string text2 = "";
						if (text.IndexOf('[') > 0)
						{
							string[] array = FPArray.SplitString(text, "[");
							text = array[0];
							text2 = array[1];
						}
						string text3 = sqlParam.Value.ToString().Replace("'", "''");
						if (text3.IndexOf("[") >= 0 && text3.IndexOf("]") >= 0 && sqlParam.WhereType != WhereType.Like && sqlParam.WhereType != WhereType.NotLike)
						{
							stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
						}
						else
						{
							if (sqlParam.WhereType == WhereType.In)
							{
								string text4 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text4))
								{
									stringBuilder.AppendFormat("[{0}] IN({1})", text, text4);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotIn)
							{
								string text5 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text5))
								{
									stringBuilder.AppendFormat("([{0}] NOT IN({1}) OR [{0}] IS NULL)", text, text5);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Like)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array2 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text6 = "";
									foreach (string str in array2)
									{
										if (text6 != "")
										{
											text6 += " OR ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text6 += string.Format("[{0}] LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str));
											}
											else if (text2 == "#")
											{
												text6 += string.Format("[{0}] LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str));
											}
											else
											{
												text6 += string.Format("[{0}] LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str));
											}
										}
										else
										{
											text6 += string.Format("[{0}] LIKE '%{1}%'", text, DbUtils.RegEsc(str));
										}
									}
									if (array2.Length > 1)
									{
										text6 = "(" + text6 + ")";
									}
									stringBuilder.Append(text6);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotLike)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array4 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text7 = "";
									foreach (string str2 in array4)
									{
										if (text7 != "")
										{
											text7 += " AND ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str2));
											}
											else if (text2 == "#")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str2));
											}
											else
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str2));
											}
										}
										else
										{
											text7 += string.Format("[{0}] NOT LIKE '%{1}%'", text, DbUtils.RegEsc(str2));
										}
									}
									if (array4.Length > 1)
									{
										text7 = string.Format("(({0}) OR [{1}] IS NULL)", text7, text);
									}
									else
									{
										text7 = string.Format("({0} OR [{1}] IS NULL)", text7, text);
									}
									stringBuilder.Append(text7);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Contain)
							{
								string text8 = "";
								foreach (string str3 in DbUtils.SplitString(text3, ","))
								{
									if (text8 != "")
									{
										text8 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text8 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str3));
										}
										else if (text2 == "#")
										{
											text8 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str3));
										}
										else
										{
											text8 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str3));
										}
									}
									else
									{
										text8 += string.Format("(','+[{0}]+',') LIKE '%,{1},%'", text, DbUtils.RegEsc(str3));
									}
								}
								if (text8 != "")
								{
									stringBuilder.AppendFormat("({0})", text8);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.ContainOrEmpty)
							{
								string text9 = "";
								foreach (string str4 in DbUtils.SplitString(text3, ","))
								{
									if (text9 != "")
									{
										text9 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text9 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%' OR [{0}] LIKE '%\":\"\"%')", text, DbUtils.RegEsc(str4));
										}
										else if (text2 == "#")
										{
											text9 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str4));
										}
										else
										{
											text9 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%' OR [{0}] LIKE '%\"{1}\":\"\"%')", text, text2, DbUtils.RegEsc(str4));
										}
									}
									else
									{
										text9 += string.Format("((','+[{0}]+',') LIKE '%,{1},%' OR [{0}]='' OR [{0}] IS NULL)", text, DbUtils.RegEsc(str4));
									}
								}
								if (text9 != "")
								{
									stringBuilder.AppendFormat("({0})", text9);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.NotContain)
							{
								string text10 = "";
								foreach (string str5 in DbUtils.SplitString(text3, ","))
								{
									if (text10 != "")
									{
										text10 += " AND ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text10 += string.Format("([{0}] NOT LIKE '%\":\"%,{1},%\"%' AND [{0}] NOT LIKE '%\":\"{1},%\"%' AND [{0}] NOT LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str5));
										}
										else if (text2 == "#")
										{
											text10 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str5));
										}
										else
										{
											text10 += string.Format("([{0}] NOT LIKE '%\"{1}\":\"%,{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str5));
										}
									}
									else
									{
										text10 += string.Format("(','+[{0}]+',') NOT LIKE '%,{1},%'", text, DbUtils.RegEsc(str5));
									}
								}
								if (text10 != "")
								{
									stringBuilder.AppendFormat("(({0}) OR [{1}] IS NULL)", text10, text);
								}
								else
								{
									stringBuilder.AppendFormat("1=1", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNull)
							{
								string[] array5 = DbUtils.SplitString(text, ",");
								if (array5.Length > 1)
								{
									string text11 = "";
									foreach (string arg in array5)
									{
										if (text11 != "")
										{
											text11 += " AND ";
										}
										text11 += string.Format("[{0}] IS NULL ", arg);
									}
									stringBuilder.Append("(" + text11 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNotNull)
							{
								string[] array6 = DbUtils.SplitString(text, ",");
								if (array6.Length > 1)
								{
									string text12 = "";
									foreach (string arg2 in array6)
									{
										if (text12 != "")
										{
											text12 += " AND ";
										}
										text12 += string.Format("[{0}] IS NOT NULL ", arg2);
									}
									stringBuilder.Append("(" + text12 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NOT NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNullOrEmpty)
							{
								string[] array7 = DbUtils.SplitString(text, ",");
								if (array7.Length > 1)
								{
									string text13 = "";
									foreach (string arg3 in array7)
									{
										if (text13 != "")
										{
											text13 += " AND ";
										}
										text13 += string.Format("([{0}] IS NULL OR [{0}]='')", arg3);
									}
									stringBuilder.Append("(" + text13 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("([{0}] IS NULL OR [{0}]='')", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsEmpty)
							{
								string[] array8 = DbUtils.SplitString(text, ",");
								if (array8.Length > 1)
								{
									string text14 = "";
									foreach (string arg4 in array8)
									{
										if (text14 != "")
										{
											text14 += " AND ";
										}
										text14 += string.Format("[{0}]=''", arg4);
									}
									stringBuilder.Append("(" + text14 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}]=''", text);
								}
							}
							else
							{
								bool flag2 = false;
								foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
								{
									if (propertyInfo.Name == text)
									{
										flag2 = true;
										if (propertyInfo.PropertyType == typeof(string) || propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
										{
											if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
											{
												stringBuilder.AppendFormat("([{0}]{1}'{2}' OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
											}
											else
											{
												stringBuilder.AppendFormat("[{0}]{1}'{2}'", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
											}
										}
										else if (text2 != "")
										{
											if (text2 == "*")
											{
												stringBuilder.AppendFormat("[{0}] LIKE '%\":\"{1}\"%'", text, DbUtils.RegEsc(text3));
											}
											else if (text2 == "#")
											{
												stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(text3));
											}
											else
											{
												stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"{2}\"%'", text, text2, DbUtils.RegEsc(text3));
											}
										}
										else if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
										{
											stringBuilder.AppendFormat("([{0}]{1}{2} OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
										}
										else
										{
											stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
										}
									}
								}
								if (!flag2)
								{
									if (text2 != "")
									{
										if (text2 == "*")
										{
											stringBuilder.AppendFormat("[{0}] LIKE '%\":\"{1}\"%'", text, DbUtils.RegEsc(text3));
										}
										else if (text2 == "#")
										{
											stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(text3));
										}
										else
										{
											stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"{2}\"%'", text, text2, DbUtils.RegEsc(text3));
										}
									}
									else
									{
										Type type = sqlParam.Value.GetType();
										if (type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTime?))
										{
											if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
											{
												stringBuilder.AppendFormat("([{0}]{1}'{2}' OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
											}
											else
											{
												stringBuilder.AppendFormat("[{0}]{1}'{2}'", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
											}
										}
										else if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
										{
											stringBuilder.AppendFormat("([{0}]{1}{2} OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
										}
										else
										{
											stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
										}
									}
								}
							}
							if (flag)
							{
								stringBuilder.Append(")");
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004FB4 File Offset: 0x000031B4
		public static string CreateWhere(List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType != SqlType.Update && sqlParam.SqlType != SqlType.Set && sqlParam.SqlType != SqlType.Insert && sqlParam.SqlType != SqlType.OrderBy)
				{
					if (sqlParam.SqlType == SqlType.And && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" AND ");
					}
					else if (sqlParam.SqlType == SqlType.Or && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" OR ");
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						stringBuilder.Append(sqlParam.ParamName);
					}
					else
					{
						if (sqlParam.ParamName.StartsWith("("))
						{
							stringBuilder.Append("(");
						}
						bool flag = false;
						if (sqlParam.ParamName.EndsWith(")"))
						{
							flag = true;
						}
						string text = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						string text2 = "";
						if (text.IndexOf('[') > 0)
						{
							string[] array = FPArray.SplitString(text, "[");
							text = array[0];
							text2 = array[1];
						}
						string text3 = sqlParam.Value.ToString().Replace("'", "''");
						if (text3.IndexOf("[") >= 0 && text3.IndexOf("]") >= 0 && sqlParam.WhereType != WhereType.Like && sqlParam.WhereType != WhereType.NotLike)
						{
							stringBuilder.AppendFormat("[{0}]{1}{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), DbUtils.RegEsc(text3));
						}
						else
						{
							if (sqlParam.WhereType == WhereType.In)
							{
								string text4 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text4))
								{
									stringBuilder.AppendFormat("[{0}] IN({1})", text, text4);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotIn)
							{
								string text5 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text5))
								{
									stringBuilder.AppendFormat("([{0}] NOT IN({1}) OR [{0}] IS NULL)", text, text5);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Like)
							{
								if (text3 != "")
								{
									string[] array2 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text6 = "";
									foreach (string str in array2)
									{
										if (text6 != "")
										{
											text6 += " OR ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text6 += string.Format("[{0}] LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str));
											}
											else if (text2 == "#")
											{
												text6 += string.Format("[{0}] LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str));
											}
											else
											{
												text6 += string.Format("[{0}] LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str));
											}
										}
										else
										{
											text6 += string.Format("[{0}] LIKE '%{1}%'", text, DbUtils.RegEsc(str));
										}
									}
									if (array2.Length > 1)
									{
										text6 = "(" + text6 + ")";
									}
									stringBuilder.Append(text6);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotLike)
							{
								if (text3 != "")
								{
									string[] array4 = DbUtils.SplitString(text3, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text7 = "";
									foreach (string str2 in array4)
									{
										if (text7 != "")
										{
											text7 += " AND ";
										}
										if (text2 != "")
										{
											if (text2 == "*")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\":\"%{1}%\"%'", text, DbUtils.RegEsc(str2));
											}
											else if (text2 == "#")
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"%{1}%\":\"%'", text, DbUtils.RegEsc(str2));
											}
											else
											{
												text7 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str2));
											}
										}
										else
										{
											text7 += string.Format("[{0}] NOT LIKE '%{1}%'", text, DbUtils.RegEsc(str2));
										}
									}
									if (array4.Length > 1)
									{
										text7 = string.Format("(({0}) OR [{1}] IS NULL)", text7, text);
									}
									else
									{
										text7 = string.Format("({0} OR [{1}] IS NULL)", text7, text);
									}
									stringBuilder.Append(text7);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Contain)
							{
								string text8 = "";
								foreach (string str3 in DbUtils.SplitString(text3, ","))
								{
									if (text8 != "")
									{
										text8 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text8 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str3));
										}
										else if (text2 == "#")
										{
											text8 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str3));
										}
										else
										{
											text8 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str3));
										}
									}
									else
									{
										text8 += string.Format("(','+[{0}]+',') LIKE '%,{1},%'", text, DbUtils.RegEsc(str3));
									}
								}
								if (text8 != "")
								{
									stringBuilder.AppendFormat("({0})", text8);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.ContainOrEmpty)
							{
								string text9 = "";
								foreach (string str4 in DbUtils.SplitString(text3, ","))
								{
									if (text9 != "")
									{
										text9 += " OR ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text9 += string.Format("([{0}] LIKE '%\":\"%,{1},%\"%' OR [{0}] LIKE '%\":\"{1},%\"%' OR [{0}] LIKE '%\":\"%,{1}\"%' OR [{0}] LIKE '%\":\"\"%')", text, DbUtils.RegEsc(str4));
										}
										else if (text2 == "#")
										{
											text9 += string.Format("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str4));
										}
										else
										{
											text9 += string.Format("([{0}] LIKE '%\"{1}\":\"%,{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"{2},%\"%' OR [{0}] LIKE '%\"{1}\":\"%,{2}\"%' OR [{0}] LIKE '%\"{1}\":\"\"%')", text, text2, DbUtils.RegEsc(str4));
										}
									}
									else
									{
										text9 += string.Format("((','+[{0}]+',') LIKE '%,{1},%' OR [{0}]='' OR [{0}] IS NULL)", text, DbUtils.RegEsc(str4));
									}
								}
								if (text9 != "")
								{
									stringBuilder.AppendFormat("({0})", text9);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.NotContain)
							{
								string text10 = "";
								foreach (string str5 in DbUtils.SplitString(text3, ","))
								{
									if (text10 != "")
									{
										text10 += " AND ";
									}
									if (text2 != "")
									{
										if (text2 == "*")
										{
											text10 += string.Format("([{0}] NOT LIKE '%\":\"%,{1},%\"%' AND [{0}] NOT LIKE '%\":\"{1},%\"%' AND [{0}] NOT LIKE '%\":\"%,{1}\"%')", text, DbUtils.RegEsc(str5));
										}
										else if (text2 == "#")
										{
											text10 += string.Format("[{0}] NOT LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(str5));
										}
										else
										{
											text10 += string.Format("([{0}] NOT LIKE '%\"{1}\":\"%,{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"{2},%\"%' AND [{0}] NOT LIKE '%\"{1}\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str5));
										}
									}
									else
									{
										text10 += string.Format("(','+[{0}]+',') NOT LIKE '%,{1},%'", text, DbUtils.RegEsc(str5));
									}
								}
								if (text10 != "")
								{
									stringBuilder.AppendFormat("(({0}) OR [{1}] IS NULL)", text10, text);
								}
								else
								{
									stringBuilder.AppendFormat("1=1", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNull)
							{
								string[] array5 = DbUtils.SplitString(text, ",");
								if (array5.Length > 1)
								{
									string text11 = "";
									foreach (string arg in array5)
									{
										if (text11 != "")
										{
											text11 += " AND ";
										}
										text11 += string.Format("[{0}] IS NULL ", arg);
									}
									stringBuilder.Append("(" + text11 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNotNull)
							{
								string[] array6 = DbUtils.SplitString(text, ",");
								if (array6.Length > 1)
								{
									string text12 = "";
									foreach (string arg2 in array6)
									{
										if (text12 != "")
										{
											text12 += " AND ";
										}
										text12 += string.Format("[{0}] IS NOT NULL ", arg2);
									}
									stringBuilder.Append("(" + text12 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] IS NOT NULL", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNullOrEmpty)
							{
								string[] array7 = DbUtils.SplitString(text, ",");
								if (array7.Length > 1)
								{
									string text13 = "";
									foreach (string arg3 in array7)
									{
										if (text13 != "")
										{
											text13 += " AND ";
										}
										text13 += string.Format("([{0}] IS NULL OR [{0}]='')", arg3);
									}
									stringBuilder.Append("(" + text13 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("([{0}] IS NULL OR [{0}]='')", text);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsEmpty)
							{
								string[] array8 = DbUtils.SplitString(text, ",");
								if (array8.Length > 1)
								{
									string text14 = "";
									foreach (string arg4 in array8)
									{
										if (text14 != "")
										{
											text14 += " AND ";
										}
										text14 += string.Format("[{0}]=''", arg4);
									}
									stringBuilder.Append("(" + text14 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}]=''", text);
								}
							}
							else if (text2 != "")
							{
								if (text2 == "*")
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\":\"{1}\"%'", text, DbUtils.RegEsc(text3));
								}
								else if (text2 == "#")
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"%'", text, DbUtils.RegEsc(text3));
								}
								else
								{
									stringBuilder.AppendFormat("[{0}] LIKE '%\"{1}\":\"{2}\"%'", text, text2, DbUtils.RegEsc(text3));
								}
							}
							else
							{
								string text15 = text;
								int num = 0;
								using (List<DbParameter>.Enumerator enumerator = dbparamlist.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current.ParameterName == "@" + text15)
										{
											num++;
											text15 = text + num.ToString();
										}
									}
								}
								if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
								{
									stringBuilder.AppendFormat("([{0}]{1}@{2} OR [{0}] IS NULL)", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), text15);
								}
								else
								{
									stringBuilder.AppendFormat("[{0}]{1}@{2}", text, DbUtils.GetWhereTypeString(sqlParam.WhereType), text15);
								}
								DbParameter item = DbHelper.MakeInParam(text15, sqlParam.Value);
								dbparamlist.Add(item);
							}
							if (flag)
							{
								stringBuilder.Append(")");
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005CC8 File Offset: 0x00003EC8
		public static string CreateWhere<T>(string TableName, List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			Dictionary<string, AttrInfo> leftJoinList = DbHelper.GetLeftJoinList(typeof(T));
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType != SqlType.Update && sqlParam.SqlType != SqlType.Set && sqlParam.SqlType != SqlType.Insert && sqlParam.SqlType != SqlType.OrderBy)
				{
					if (sqlParam.SqlType == SqlType.And && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" AND ");
					}
					else if (sqlParam.SqlType == SqlType.Or && stringBuilder.Length > 0)
					{
						stringBuilder.Append(" OR ");
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						stringBuilder.Append(sqlParam.ParamName);
					}
					else
					{
						if (sqlParam.ParamName.StartsWith("("))
						{
							stringBuilder.Append("(");
						}
						bool flag = false;
						if (sqlParam.ParamName.EndsWith(")"))
						{
							flag = true;
						}
						string text = TableName;
						string text2 = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						string text3 = "";
						if (text2.IndexOf('[') > 0)
						{
							string[] array = FPArray.SplitString(text2, "[");
							text2 = array[0];
							text3 = array[1];
						}
						string text4 = sqlParam.Value.ToString().Replace("'", "''");
						if (leftJoinList.ContainsKey(text2))
						{
							AttrInfo attrInfo = leftJoinList[text2];
							if (attrInfo.Number > 0)
							{
								text = DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number;
							}
							else
							{
								text = DbHelper.DbConfig.prefix + attrInfo.TableName;
							}
							text2 = attrInfo.ColName;
						}
						if (text4.IndexOf("[") >= 0 && text4.IndexOf("]") >= 0 && sqlParam.WhereType != WhereType.Like && sqlParam.WhereType != WhereType.NotLike)
						{
							stringBuilder.AppendFormat("[{0}].[{1}]{2}{3}", new object[]
							{
								text,
								text2,
								DbUtils.GetWhereTypeString(sqlParam.WhereType),
								DbUtils.RegEsc(text4)
							});
						}
						else
						{
							if (sqlParam.WhereType == WhereType.In)
							{
								string text5 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text5))
								{
									stringBuilder.AppendFormat("[{0}].[{1}] IN({2})", text, text2, text5);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotIn)
							{
								string text6 = DbUtils.FormatInString(sqlParam.Value.ToString());
								if (!string.IsNullOrEmpty(text6))
								{
									stringBuilder.AppendFormat("([{0}].[{1}] NOT IN({2}) OR [{0}].[{1}] IS NULL)", text, text2, text6);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Like)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array2 = DbUtils.SplitString(text4, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text7 = "";
									foreach (string str in array2)
									{
										if (text7 != "")
										{
											text7 += " OR ";
										}
										if (text3 != "")
										{
											if (text3 == "*")
											{
												text7 += string.Format("[{0}].[{1}] LIKE '%\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str));
											}
											else if (text3 == "#")
											{
												text7 += string.Format("[{0}].[{1}] LIKE '%\"%{2}%\":\"%'", text, text2, DbUtils.RegEsc(str));
											}
											else
											{
												text7 += string.Format("[{0}].[{1}] LIKE '%\"{2}\":\"%{3}%\"%'", new object[]
												{
													text,
													text2,
													text3,
													DbUtils.RegEsc(str)
												});
											}
										}
										else
										{
											text7 += string.Format("[{0}].[{1}] LIKE '%{2}%'", text, text2, DbUtils.RegEsc(str));
										}
									}
									if (array2.Length > 1)
									{
										text7 = "(" + text7 + ")";
									}
									stringBuilder.Append(text7);
								}
								else
								{
									stringBuilder.Append("1=0");
								}
							}
							else if (sqlParam.WhereType == WhereType.NotLike)
							{
								if (sqlParam.Value.ToString() != "")
								{
									string[] array4 = DbUtils.SplitString(text4, new string[]
									{
										",",
										"|",
										";",
										" "
									});
									string text8 = "";
									foreach (string str2 in array4)
									{
										if (text8 != "")
										{
											text8 += " AND ";
										}
										if (text3 != "")
										{
											if (text3 == "*")
											{
												text8 += string.Format("[{0}].[{1}] NOT LIKE '%\":\"%{2}%\"%'", text, text2, DbUtils.RegEsc(str2));
											}
											else if (text3 == "#")
											{
												text8 += string.Format("[{0}].[{1}] NOT LIKE '%\"%{2}%\":\"%'", text, text2, DbUtils.RegEsc(str2));
											}
											else
											{
												text8 += string.Format("[{0}].[{1}] NOT LIKE '%\"{2}\":\"%{3}%\"%'", new object[]
												{
													text,
													text2,
													text3,
													DbUtils.RegEsc(str2)
												});
											}
										}
										else
										{
											text8 += string.Format("[{0}].[{1}] NOT LIKE '%{2}%'", text, text2, DbUtils.RegEsc(str2));
										}
									}
									if (array4.Length > 1)
									{
										text8 = string.Format("(({0}) OR [{1}].[{2}] IS NULL)", text8, text, text2);
									}
									else
									{
										text8 = string.Format("({0} OR [{1}].[{2}] IS NULL)", text8, text, text2);
									}
									stringBuilder.Append(text8);
								}
								else
								{
									stringBuilder.Append("1=1");
								}
							}
							else if (sqlParam.WhereType == WhereType.Contain)
							{
								string text9 = "";
								foreach (string str3 in DbUtils.SplitString(text4, ","))
								{
									if (text9 != "")
									{
										text9 += " OR ";
									}
									if (text3 != "")
									{
										if (text3 == "*")
										{
											text9 += string.Format("([{0}].[{1}] LIKE '%\":\"%,{2},%\"%' OR [{0}].[{1}] LIKE '%\":\"{2},%\"%' OR [{0}].[{1}] LIKE '%\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str3));
										}
										else if (text3 == "#")
										{
											text9 += string.Format("[{0}].[{1}] LIKE '%\"{2}\":\"%'", text, text2, DbUtils.RegEsc(str3));
										}
										else
										{
											text9 += string.Format("([{0}].[{1}] LIKE '%\"{2}\":\"%,{3},%\"%' OR [{0}].[{1}] LIKE '%\"{2}\":\"{3},%\"%' OR [{0}].[{1}] LIKE '%\"{2}\":\"%,{3}\"%')", new object[]
											{
												text,
												text2,
												text3,
												DbUtils.RegEsc(str3)
											});
										}
									}
									else
									{
										text9 += string.Format("(','+[{0}].[{1}]+',') LIKE '%,{2},%'", text, text2, DbUtils.RegEsc(str3));
									}
								}
								if (text9 != "")
								{
									stringBuilder.AppendFormat("({0})", text9);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.ContainOrEmpty)
							{
								string text10 = "";
								foreach (string str4 in DbUtils.SplitString(text4, ","))
								{
									if (text10 != "")
									{
										text10 += " OR ";
									}
									if (text3 != "")
									{
										if (text3 == "*")
										{
											text10 += string.Format("([{0}].[{1}] LIKE '%\":\"%,{2},%\"%' OR [{0}].[{1}] LIKE '%\":\"{2},%\"%' OR [{0}].[{1}] LIKE '%\":\"%,{2}\"%' OR [{0}].[{1}] LIKE '%\":\"\"%')", text, text2, DbUtils.RegEsc(str4));
										}
										else if (text3 == "#")
										{
											text10 += string.Format("[{0}].[{1}] LIKE '%\"{2}\":\"%'", text, text2, DbUtils.RegEsc(str4));
										}
										else
										{
											text10 += string.Format("([{0}].[{1}] LIKE '%\"{2}\":\"%,{3},%\"%' OR [{0}].[{1}] LIKE '%\"{2}\":\"{3},%\"%' OR [{0}].[{1}] LIKE '%\"{2}\":\"%,{3}\"%' OR [{0}].[{1}] LIKE '%\"{2}\":\"\"%')", new object[]
											{
												text,
												text2,
												text3,
												DbUtils.RegEsc(str4)
											});
										}
									}
									else
									{
										text10 += string.Format("((','+[{0}].[{1}]+',') LIKE '%,{2},%' OR [{0}].[{1}]='' OR [{0}].[{1}] IS NULL)", text, text2, DbUtils.RegEsc(str4));
									}
								}
								if (text10 != "")
								{
									stringBuilder.AppendFormat("({0})", text10);
								}
								else
								{
									stringBuilder.AppendFormat("1=0", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.NotContain)
							{
								string text11 = "";
								foreach (string str5 in DbUtils.SplitString(text4, ","))
								{
									if (text11 != "")
									{
										text11 += " AND ";
									}
									if (text3 != "")
									{
										if (text3 == "*")
										{
											text11 += string.Format("([{0}].[{1}] NOT LIKE '%\":\"%,{2},%\"%' AND [{0}].[{1}] NOT LIKE '%\":\"{2},%\"%' AND [{0}].[{1}] NOT LIKE '%\":\"%,{2}\"%')", text, text2, DbUtils.RegEsc(str5));
										}
										else if (text3 == "#")
										{
											text11 += string.Format("[{0}].[{1}] NOT LIKE '%\"{2}\":\"%'", text, text2, DbUtils.RegEsc(str5));
										}
										else
										{
											text11 += string.Format("([{0}].[{1}] NOT LIKE '%\"{2}\":\"%,{3},%\"%' AND [{0}].[{1}] NOT LIKE '%\"{2}\":\"{3},%\"%' AND [{0}].[{1}] NOT LIKE '%\"{2}\":\"%,{3}\"%')", new object[]
											{
												text,
												text2,
												text3,
												DbUtils.RegEsc(str5)
											});
										}
									}
									else
									{
										text11 += string.Format("(','+[{0}].[{1}]+',') NOT LIKE '%,{2},%'", text, text2, DbUtils.RegEsc(str5));
									}
								}
								if (text11 != "")
								{
									stringBuilder.AppendFormat("(({0}) OR [{1}].[{2}] IS NULL)", text11, text, text2);
								}
								else
								{
									stringBuilder.AppendFormat("1=1", new object[0]);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNull)
							{
								string[] array5 = DbUtils.SplitString(text2, ",");
								if (array5.Length > 1)
								{
									string text12 = "";
									foreach (string arg in array5)
									{
										if (text12 != "")
										{
											text12 += " AND ";
										}
										text12 += string.Format("[{0}].[{1}] IS NULL ", text, arg);
									}
									stringBuilder.Append("(" + text12 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}].[{1}] IS NULL", text, text2);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNotNull)
							{
								string[] array6 = DbUtils.SplitString(text2, ",");
								if (array6.Length > 1)
								{
									string text13 = "";
									foreach (string arg2 in array6)
									{
										if (text13 != "")
										{
											text13 += " AND ";
										}
										text13 += string.Format("[{0}].[{1}] IS NOT NULL ", text, arg2);
									}
									stringBuilder.Append("(" + text13 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}].[{1}] IS NOT NULL", text, text2);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsNullOrEmpty)
							{
								string[] array7 = DbUtils.SplitString(text2, ",");
								if (array7.Length > 1)
								{
									string text14 = "";
									foreach (string arg3 in array7)
									{
										if (text14 != "")
										{
											text14 += " AND ";
										}
										text14 += string.Format("([{0}].[{1}] IS NULL OR [{0}].[{1}]='')", text, arg3);
									}
									stringBuilder.Append("(" + text14 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("([{0}].[{1}] IS NULL OR [{0}].[{1}]='')", text, text2);
								}
							}
							else if (sqlParam.WhereType == WhereType.IsEmpty)
							{
								string[] array8 = DbUtils.SplitString(text2, ",");
								if (array8.Length > 1)
								{
									string text15 = "";
									foreach (string arg4 in array8)
									{
										if (text15 != "")
										{
											text15 += " AND ";
										}
										text15 += string.Format("[{0}].[{1}]=''", text, arg4);
									}
									stringBuilder.Append("(" + text15 + ")");
								}
								else
								{
									stringBuilder.AppendFormat("[{0}].[{1}]=''", text, text2);
								}
							}
							else if (text3 != "")
							{
								if (text3 == "*")
								{
									stringBuilder.AppendFormat("[{0}].[{1}] LIKE '%\":\"{2}\"%'", text, text2, DbUtils.RegEsc(text4));
								}
								else if (text3 == "#")
								{
									stringBuilder.AppendFormat("[{0}].[{1}] LIKE '%\"{2}\":\"%'", text, text2, DbUtils.RegEsc(text4));
								}
								else
								{
									stringBuilder.AppendFormat("[{0}].[{1}] LIKE '%\"{2}\":\"{3}\"%'", new object[]
									{
										text,
										text2,
										text3,
										DbUtils.RegEsc(text4)
									});
								}
							}
							else
							{
								string text16 = text2;
								int num = 0;
								using (List<DbParameter>.Enumerator enumerator = dbparamlist.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										if (enumerator.Current.ParameterName == "@" + text16)
										{
											num++;
											text16 = text2 + num.ToString();
										}
									}
								}
								if (sqlParam.WhereType == WhereType.NotEqual || sqlParam.WhereType == WhereType.LessThan || sqlParam.WhereType == WhereType.LessThanEqual || sqlParam.WhereType == WhereType.GreaterThan || sqlParam.WhereType == WhereType.GreaterThanEqual)
								{
									stringBuilder.AppendFormat("([{0}].[{1}]{2}@{3} OR [{0}].[{1}] IS NULL)", new object[]
									{
										text,
										text2,
										DbUtils.GetWhereTypeString(sqlParam.WhereType),
										text16
									});
								}
								else
								{
									stringBuilder.AppendFormat("[{0}].[{1}]{2}@{3}", new object[]
									{
										text,
										text2,
										DbUtils.GetWhereTypeString(sqlParam.WhereType),
										text16
									});
								}
								DbParameter item = DbHelper.MakeInParam(text16, sqlParam.Value);
								dbparamlist.Add(item);
							}
							if (flag)
							{
								stringBuilder.Append(")");
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00006B9C File Offset: 0x00004D9C
		public static string CreateSelect<T>(params SqlParam[] sqlparams) where T : new()
		{
			List<DbParameter> dbparamlist = new List<DbParameter>();
			return DbHelper.CreateSelect<T>(0, dbparamlist, sqlparams);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00006BB7 File Offset: 0x00004DB7
		public static string CreateSelect<T>(List<DbParameter> dbparamlist, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.CreateSelect<T>(0, dbparamlist, sqlparams);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00006BC1 File Offset: 0x00004DC1
		public static string CreateSelect<T>(int tops, List<DbParameter> dbparamlist, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.CreateSelect<T>(tops, OrderBy.DESC, dbparamlist, sqlparams);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00006BCC File Offset: 0x00004DCC
		public static string CreateSelect<T>(int tops, OrderBy orderby, List<DbParameter> dbparamlist, params SqlParam[] sqlparams) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			if (dbparamlist == null)
			{
				dbparamlist = new List<DbParameter>();
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text2 = "";
			string text3 = string.Format("[{0}]", text);
			string text4 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsBindField && !attrInfo.IsNtext)
				{
					if (attrInfo.IsPrimaryKey && text2 == "")
					{
						text2 = propertyInfo.Name;
					}
					string str = string.Format("[{0}].[{1}]", text, propertyInfo.Name);
					if (attrInfo.IsMap)
					{
						if (attrInfo.Number > 0)
						{
							str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number, attrInfo.ColName, propertyInfo.Name);
						}
						else
						{
							str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName, attrInfo.ColName, propertyInfo.Name);
						}
					}
					else if (attrInfo.IsLeftJoin)
					{
						if (attrInfo.Number > 0)
						{
							text3 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
							{
								text3,
								DbHelper.DbConfig.prefix + attrInfo.TableName,
								DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
								attrInfo.ColName,
								text,
								propertyInfo.Name
							});
						}
						else
						{
							text3 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
							{
								text3,
								DbHelper.DbConfig.prefix + attrInfo.TableName,
								attrInfo.ColName,
								text,
								propertyInfo.Name
							});
						}
					}
					else if (attrInfo.IsTempField)
					{
						if (DbHelper.DbConfig.dbtype == DbType.Access)
						{
							str = string.Concat(new string[]
							{
								"[@",
								propertyInfo.Name,
								"] AS [",
								propertyInfo.Name,
								"]"
							});
						}
						else
						{
							str = "[" + propertyInfo.Name + "]=@" + propertyInfo.Name;
						}
						DbParameter item = DbHelper.MakeInParam(propertyInfo.Name, propertyInfo.GetValue(t, null));
						dbparamlist.Add(item);
					}
					if (!string.IsNullOrEmpty(text4))
					{
						text4 += ",";
					}
					text4 += str;
				}
			}
			if (tops > 0)
			{
				stringBuilder.AppendFormat("SELECT TOP {0} {1} FROM {2}", tops, text4, text3);
			}
			else
			{
				stringBuilder.AppendFormat("SELECT {0} FROM {1}", text4, text3);
			}
			string text5 = "";
			bool flag = false;
			if (sqlparams != null && sqlparams.Length != 0)
			{
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.Set)
					{
						for (int j = 0; j < dbparamlist.Count; j++)
						{
							if (dbparamlist[j].ParameterName == "@" + sqlParam.ParamName.TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							}))
							{
								dbparamlist[j].Value = sqlParam.Value;
							}
						}
					}
					else if (sqlParam.SqlType == SqlType.OrderBy)
					{
						string text6 = text;
						string text7 = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						if (sqlParam.ParamName.IndexOf(".") > 0)
						{
							string[] array = DbUtils.SplitString(sqlParam.ParamName, ".", 2);
							text6 = array[0].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
							if (text6.StartsWith("FP_"))
							{
								text6 = text6.Replace("FP_", DbConfigs.Prefix);
							}
							else
							{
								text6 = DbConfigs.Prefix + text6;
							}
							text7 = array[1].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
						}
						if (text7 == text2 && text6 == text)
						{
							flag = true;
						}
						text5 = DbUtils.PushString(text5, string.Format("[{0}].[{1}] {2}", text6, text7, sqlParam.Value.ToString()));
					}
				}
				string text8 = DbHelper.CreateWhere<T>(text, dbparamlist, sqlparams);
				if (!string.IsNullOrEmpty(text8))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text8);
				}
			}
			if (!flag)
			{
				text5 = DbUtils.PushString(text5, string.Format("[{0}].[{1}] {2}", text, text2, orderby.ToString()));
			}
			if (text5 != "")
			{
				stringBuilder.Append(" ORDER BY " + text5);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000071BD File Offset: 0x000053BD
		public static string CreateSelect<T>(Pager pager, List<DbParameter> dbparamlist, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.CreateSelect<T>(pager, OrderBy.DESC, dbparamlist, sqlparams);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000071C8 File Offset: 0x000053C8
		public static string CreateSelect<T>(Pager pager, OrderBy orderby, List<DbParameter> dbparamlist, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			T t = Activator.CreateInstance<T>();
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			StringBuilder stringBuilder = new StringBuilder();
			string text2 = string.Format("[{0}]", text);
			string text3 = "";
			if (dbparamlist == null)
			{
				dbparamlist = new List<DbParameter>();
			}
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (propertyInfo.CanWrite)
				{
					AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
					if (attrInfo.IsBindField && !attrInfo.IsNtext)
					{
						if (attrInfo.IsPrimaryKey && text3 == "")
						{
							text3 = propertyInfo.Name;
						}
						string text4 = string.Format("[{0}].[{1}]", text, propertyInfo.Name);
						if (attrInfo.IsMap)
						{
							if (attrInfo.Number > 0)
							{
								text4 = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number, attrInfo.ColName, propertyInfo.Name);
							}
							else
							{
								text4 = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName, attrInfo.ColName, propertyInfo.Name);
							}
						}
						else if (attrInfo.IsLeftJoin)
						{
							if (attrInfo.Number > 0)
							{
								text2 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
								{
									text2,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
							else
							{
								text2 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
								{
									text2,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
						}
						else if (attrInfo.IsTempField)
						{
							if (DbHelper.DbConfig.dbtype == DbType.Access)
							{
								text4 = string.Concat(new string[]
								{
									"[@",
									propertyInfo.Name,
									"] AS [",
									propertyInfo.Name,
									"]"
								});
							}
							else
							{
								text4 = "[" + propertyInfo.Name + "]=@" + propertyInfo.Name;
							}
							DbParameter item = DbHelper.MakeInParam(propertyInfo.Name, propertyInfo.GetValue(t, null));
							dbparamlist.Add(item);
						}
						if (stringBuilder.ToString() == "")
						{
							stringBuilder.Append(text4);
						}
						else
						{
							stringBuilder.Append("," + text4);
						}
					}
				}
			}
			if (text3 == "")
			{
				text3 = "id";
			}
			string text5 = "1=1";
			string text6 = "";
			bool flag = false;
			if (sqlparams != null && sqlparams.Length != 0)
			{
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.Set)
					{
						for (int j = 0; j < dbparamlist.Count; j++)
						{
							if (dbparamlist[j].ParameterName == "@" + sqlParam.ParamName.TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							}))
							{
								dbparamlist[j].Value = sqlParam.Value;
							}
						}
					}
					else if (sqlParam.SqlType == SqlType.OrderBy)
					{
						string text7 = text;
						string text8 = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						if (sqlParam.ParamName.IndexOf(".") > 0)
						{
							string[] array = DbUtils.SplitString(sqlParam.ParamName, ".", 2);
							text7 = array[0].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
							if (text7.StartsWith("FP_"))
							{
								text7 = text7.Replace("FP_", DbConfigs.Prefix);
							}
							else
							{
								text7 = DbConfigs.Prefix + text7;
							}
							text8 = array[1].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
						}
						if (text8 == text3 && text7 == text)
						{
							flag = true;
						}
						text6 = DbUtils.PushString(text6, string.Format("[{0}].[{1}] {2}", text7, text8, sqlParam.Value.ToString()));
					}
				}
				string text9 = DbHelper.CreateWhere<T>(text, dbparamlist, sqlparams);
				if (!string.IsNullOrEmpty(text9))
				{
					text5 = text5 + " AND " + text9;
				}
			}
			pager.total = DbHelper.ExecuteCount<T>(text5, dbparamlist.ToArray());
			if (!flag)
			{
				text6 = DbUtils.PushString(text6, string.Format("[{0}].[{1}] {2}", text, text3, orderby.ToString()));
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (pager.pageindex == 1)
			{
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM {2} WHERE {3} ORDER BY {4}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					text2,
					text5,
					text6
				});
			}
			else
			{
				int num = (pager.pageindex - 1) * pager.pagesize;
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM {2} WHERE {3} AND [{4}].[{5}] NOT IN(SELECT TOP {6} [{4}].[{5}] FROM {2} WHERE {3} ORDER BY {7}) ORDER BY {7}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					text2,
					text5,
					text,
					text3,
					num,
					text6
				});
			}
			return stringBuilder2.ToString();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007848 File Offset: 0x00005A48
		public static string CreateSelect<T>(string keys, List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(keys))
			{
				keys = "*";
			}
			stringBuilder.AppendFormat("SELECT {0} FROM [{1}{2}]", keys, DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			string text = "";
			if (sqlparams != null && sqlparams.Length != 0)
			{
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.OrderBy)
					{
						text = DbUtils.PushString(text, string.Format("[{0}] {1}", sqlParam.ParamName.TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						}), sqlParam.Value.ToString()));
					}
				}
				string text2 = DbHelper.CreateWhere(dbparamlist, sqlparams);
				if (!string.IsNullOrEmpty(text2))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text2);
				}
			}
			if (text != "")
			{
				stringBuilder.Append(" ORDER BY " + text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00007958 File Offset: 0x00005B58
		public static string CreateInsert<T>(T targetObj, List<DbParameter> dbparamlist)
		{
			Type type = targetObj.GetType();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("INSERT INTO [{0}{1}] (", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(type));
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsBindField && !attrInfo.IsTempField && !attrInfo.IsIdentity && !attrInfo.IsMap && propertyInfo.GetValue(targetObj, null) != null)
				{
					list.Add(propertyInfo);
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo2 = list[j];
				stringBuilder2.AppendFormat("[{0}]", propertyInfo2.Name);
				stringBuilder3.AppendFormat("@{0}", propertyInfo2.Name);
				DbParameter item = DbHelper.MakeInParam<T>(propertyInfo2, targetObj);
				dbparamlist.Add(item);
				if (j < list.Count - 1)
				{
					stringBuilder2.Append(",");
					stringBuilder3.Append(",");
				}
			}
			stringBuilder.Append(stringBuilder2).Append(") VALUES(").Append(stringBuilder3).Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00007AB8 File Offset: 0x00005CB8
		public static string CreateUpdateSql<T>(T targetObj, List<DbParameter> dbparamlist)
		{
			Type type = targetObj.GetType();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE [{0}{1}] SET ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(type));
			PropertyInfo[] properties = type.GetProperties();
			List<PropertyInfo> list = new List<PropertyInfo>();
			bool flag = true;
			foreach (PropertyInfo propertyInfo in properties)
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsBindField && !attrInfo.IsTempField && !attrInfo.IsIdentity)
				{
					if (attrInfo.IsPrimaryKey && flag)
					{
						flag = false;
					}
					else if (!attrInfo.IsMap && propertyInfo.GetValue(targetObj, null) != null)
					{
						list.Add(propertyInfo);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo2 = list[j];
				stringBuilder.AppendFormat("[{0}]=@{0}", propertyInfo2.Name);
				DbParameter item = DbHelper.MakeInParam<T>(propertyInfo2, targetObj);
				dbparamlist.Add(item);
				if (j < list.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			List<PropertyInfo> primaryKeys = DbHelper.GetPrimaryKeys(type);
			if (primaryKeys.Count > 0)
			{
				stringBuilder.AppendFormat(" WHERE [{0}]=@{0}", primaryKeys[0].Name);
				DbParameter item2 = DbHelper.MakeInParam<T>(primaryKeys[0], targetObj);
				dbparamlist.Add(item2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00007C18 File Offset: 0x00005E18
		public static string CreateUpdate<T>(params SqlParam[] wheres)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type typeFromHandle = typeof(T);
			stringBuilder.AppendFormat("UPDATE [{0}{1}] SET ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			string text = "";
			foreach (SqlParam sqlParam in wheres)
			{
				string text2 = sqlParam.ParamName.TrimStart(new char[]
				{
					'['
				}).TrimEnd(new char[]
				{
					']'
				});
				if (sqlParam.SqlType == SqlType.Update)
				{
					if (text != "")
					{
						text += ",";
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						string text3 = sqlParam.Value.ToString();
						if (text2 == "")
						{
							text += text3;
						}
						else
						{
							text += string.Format("[{0}]={1}", text2, text3);
						}
					}
					else
					{
						Type type = sqlParam.Value.GetType();
						if (type == typeof(string) || type == typeof(DateTime) || type == typeof(DateTime?))
						{
							text += string.Format("[{0}]='{1}'", text2, sqlParam.Value.ToString());
						}
						else if (type == typeof(FPData))
						{
							text += string.Format("[{0}]='{1}'", text2, FPJson.ToJson(sqlParam.Value as FPData));
						}
						else if (type == typeof(List<FPData>))
						{
							text += string.Format("[{0}]='{1}'", text2, FPJson.ToJson(sqlParam.Value as List<FPData>));
						}
						else
						{
							text += string.Format("[{0}]={1}", text2, sqlParam.Value.ToString());
						}
					}
				}
			}
			stringBuilder.Append(text);
			string text4 = DbHelper.CreateWhere<T>(wheres);
			if (text4 != "")
			{
				stringBuilder.Append(" WHERE " + text4);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00007E30 File Offset: 0x00006030
		public static string CreateUpdate<T>(List<DbParameter> dbparamlist, SqlParam[] sqlparams)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type typeFromHandle = typeof(T);
			stringBuilder.AppendFormat("UPDATE [{0}{1}] SET ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			string text = "";
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType == SqlType.Update)
				{
					if (text != "")
					{
						text += ",";
					}
					string text2 = sqlParam.ParamName.TrimStart(new char[]
					{
						'['
					}).TrimEnd(new char[]
					{
						']'
					});
					if (sqlParam.WhereType == WhereType.Custom)
					{
						string text3 = sqlParam.Value.ToString();
						if (text2 == "")
						{
							text += text3;
						}
						else
						{
							text += string.Format("[{0}]={1}", text2, text3);
						}
					}
					else
					{
						text += string.Format("[{0}]=@{0}", text2);
						DbParameter item = DbHelper.MakeInParam("@" + text2, sqlParam.Value);
						dbparamlist.Add(item);
					}
				}
			}
			stringBuilder.Append(text);
			string text4 = DbHelper.CreateWhere(dbparamlist, sqlparams);
			if (text4 != "")
			{
				stringBuilder.Append(" WHERE " + text4);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00007F95 File Offset: 0x00006195
		public static int ExecuteNonQuery(string commandText)
		{
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, null);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00007F9F File Offset: 0x0000619F
		public static int ExecuteNonQuery(out int id, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, CommandType.Text, commandText, null);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00007FAA File Offset: 0x000061AA
		public static int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(commandType, commandText, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00007FB4 File Offset: 0x000061B4
		public static int ExecuteNonQuery(out int id, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, commandType, commandText, null);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00007FC0 File Offset: 0x000061C0
		public static int ExecuteNonQuery(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (string.IsNullOrEmpty(DbHelper.DbConfig.connectionstring))
			{
				throw new ArgumentNullException("ConnectionString");
			}
			int result;
			using (DbConnection dbConnection = DbHelper.Factory.CreateConnection())
			{
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				result = DbHelper.ExecuteNonQuery(dbConnection, commandType, commandText, commandParameters);
			}
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008034 File Offset: 0x00006234
		public static int ExecuteNonQuery(out int id, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (string.IsNullOrEmpty(DbHelper.DbConfig.connectionstring))
			{
				throw new ArgumentNullException("ConnectionString");
			}
			int result;
			using (DbConnection dbConnection = DbHelper.Factory.CreateConnection())
			{
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				result = DbHelper.ExecuteNonQuery(out id, dbConnection, commandType, commandText, commandParameters);
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000080A8 File Offset: 0x000062A8
		public static int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(connection, commandType, commandText, null);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000080B3 File Offset: 0x000062B3
		public static int ExecuteNonQuery(out int id, DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, connection, commandType, commandText, null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000080C0 File Offset: 0x000062C0
		public static int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
            DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out var flag);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			if (flag)
			{
				connection.Close();
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00008110 File Offset: 0x00006310
		public static int ExecuteNonQuery(out int id, DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			if (DbHelper.DbProvider.GetLastIdSql().Trim() == "")
			{
				throw new ArgumentNullException("GetLastIdSql is \"\"");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
            DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out var flag);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			dbCommand.CommandType = CommandType.Text;
			dbCommand.CommandText = DbHelper.DbProvider.GetLastIdSql();
			id = int.Parse(dbCommand.ExecuteScalar().ToString());
			if (flag)
			{
				connection.Close();
			}
			return result;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000081AF File Offset: 0x000063AF
		public static int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(transaction, commandType, commandText, null);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000081BC File Offset: 0x000063BC
		public static int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
            DbHelper.PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out _);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00008224 File Offset: 0x00006424
		private static DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, DbHelper.DbConnectionOwnership connectionOwnership)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			bool flag = false;
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
			dbCommand.CommandTimeout = 180;
			DbDataReader result;
			try
			{
				DbHelper.PrepareCommand(dbCommand, connection, transaction, commandType, commandText, commandParameters, out flag);
				DbDataReader dbDataReader;
				if (connectionOwnership == DbHelper.DbConnectionOwnership.External)
				{
					dbDataReader = dbCommand.ExecuteReader();
				}
				else
				{
					dbDataReader = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
				bool flag2 = true;
                IEnumerator enumerator = dbCommand.Parameters.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (((DbParameter)enumerator.Current).Direction != ParameterDirection.Input)
                    {
                        flag2 = false;
                    }
                }
                if (flag2)
				{
					dbCommand.Parameters.Clear();
				}
				result = dbDataReader;
			}
			catch (Exception ex)
			{
				if (flag)
				{
					connection.Close();
				}
				throw ex;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00008300 File Offset: 0x00006500
		public static DbDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(commandType, commandText, null);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000830C File Offset: 0x0000650C
		public static DbDataReader ExecuteReader(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (string.IsNullOrEmpty(DbHelper.DbConfig.connectionstring))
			{
				throw new ArgumentNullException("ConnectionString");
			}
			DbConnection dbConnection = null;
			DbDataReader result;
			try
			{
				dbConnection = DbHelper.Factory.CreateConnection();
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				result = DbHelper.ExecuteReader(dbConnection, null, commandType, commandText, commandParameters, DbHelper.DbConnectionOwnership.Internal);
			}
			catch (Exception ex)
			{
				if (dbConnection != null)
				{
					dbConnection.Close();
				}
				throw ex;
			}
			return result;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008384 File Offset: 0x00006584
		public static DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(connection, commandType, commandText, null);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000838F File Offset: 0x0000658F
		public static DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			return DbHelper.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbHelper.DbConnectionOwnership.External);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000839C File Offset: 0x0000659C
		public static DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(transaction, commandType, commandText, null);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000083A7 File Offset: 0x000065A7
		public static DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction != null && transaction.Connection == null)
			{
				throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
			}
			return DbHelper.ExecuteReader(transaction.Connection, transaction, commandType, commandText, commandParameters, DbHelper.DbConnectionOwnership.External);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000083E2 File Offset: 0x000065E2
		public static DataSet ExecuteDataset(string commandText)
		{
			return DbHelper.ExecuteDataset(CommandType.Text, commandText, null);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000083EC File Offset: 0x000065EC
		public static DataSet ExecuteDataset(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteDataset(commandType, commandText, null);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000083F8 File Offset: 0x000065F8
		public static DataSet ExecuteDataset(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (string.IsNullOrEmpty(DbHelper.DbConfig.connectionstring))
			{
				throw new ArgumentNullException("ConnectionString");
			}
			DataSet result;
			using (DbConnection dbConnection = DbHelper.Factory.CreateConnection())
			{
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				result = DbHelper.ExecuteDataset(dbConnection, commandType, commandText, commandParameters);
			}
			return result;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000846C File Offset: 0x0000666C
		public static DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteDataset(connection, commandType, commandText, null);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00008478 File Offset: 0x00006678
		public static DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
            DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out var flag);
			DataSet result;
			using (DbDataAdapter dbDataAdapter = DbHelper.Factory.CreateDataAdapter())
			{
				dbDataAdapter.SelectCommand = dbCommand;
				DataSet dataSet = new DataSet();
				dbDataAdapter.Fill(dataSet);
				dbCommand.Parameters.Clear();
				if (flag)
				{
					connection.Close();
				}
				result = dataSet;
			}
			return result;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00008504 File Offset: 0x00006704
		public static object ExecuteScalar(string commandText)
		{
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000850D File Offset: 0x0000670D
		public static object ExecuteScalar(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteScalar(commandType, commandText, null);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00008518 File Offset: 0x00006718
		public static object ExecuteScalar(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (string.IsNullOrEmpty(DbHelper.DbConfig.connectionstring))
			{
				throw new ArgumentNullException("ConnectionString");
			}
			object result;
			using (DbConnection dbConnection = DbHelper.Factory.CreateConnection())
			{
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				result = DbHelper.ExecuteScalar(dbConnection, commandType, commandText, commandParameters);
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000858C File Offset: 0x0000678C
		public static object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteScalar(connection, commandType, commandText, null);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00008598 File Offset: 0x00006798
		public static object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
            DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out var flag);
			object result = dbCommand.ExecuteScalar();
			dbCommand.Parameters.Clear();
			if (flag)
			{
				connection.Close();
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000085E7 File Offset: 0x000067E7
		public static string ExecuteField<T>(List<T> list) where T : new()
		{
			return DbHelper.ExecuteField<T>("", list);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000085F4 File Offset: 0x000067F4
		public static string ExecuteField<T>(string field, List<T> list) where T : new()
		{
			string text = "";
			Type typeFromHandle = typeof(T);
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in typeFromHandle.GetProperties())
			{
				if (DbHelper.GetAttrInfo(propertyInfo2).IsPrimaryKey && field == "")
				{
					propertyInfo = propertyInfo2;
					break;
				}
				if (propertyInfo2.Name.ToLower() == field.ToLower())
				{
					propertyInfo = propertyInfo2;
					break;
				}
			}
			if (propertyInfo != null)
			{
				foreach (T t in list)
				{
					string push = string.Empty;
					if (propertyInfo.GetValue(t, null) != null)
					{
						push = propertyInfo.GetValue(t, null).ToString();
					}
					text = DbUtils.PushString(text, push);
				}
			}
			return text;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000086E0 File Offset: 0x000068E0
		public static string ExecuteField<T>() where T : new()
		{
			return DbHelper.ExecuteField<T>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000086E8 File Offset: 0x000068E8
		public static string ExecuteField<T>(string field) where T : new()
		{
			return DbHelper.ExecuteField<T>(field);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000086F1 File Offset: 0x000068F1
		public static string ExecuteField<T>(params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteField<T>("", sqlparams);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00008700 File Offset: 0x00006900
		public static string ExecuteField<T>(string field, params SqlParam[] sqlparams) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			return DbHelper.ExecuteField(DbHelper.CreateSelect<T>(field, list, sqlparams).ToString(), list.ToArray());
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000872B File Offset: 0x0000692B
		public static string ExecuteField(string commandText, params DbParameter[] dbparams)
		{
			return DbHelper.ExecuteField(CommandType.Text, commandText, dbparams);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00008738 File Offset: 0x00006938
		public static string ExecuteField(CommandType commandType, string commandText, params DbParameter[] dbparams)
		{
			string text = "";
			IDataReader dataReader = DbHelper.ExecuteReader(commandType, commandText, dbparams);
			while (dataReader.Read())
			{
				string text2 = dataReader[0].ToString();
				if (!string.IsNullOrEmpty(text2))
				{
					text = DbUtils.PushString(text, text2);
				}
			}
			dataReader.Close();
			return text;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00008782 File Offset: 0x00006982
		public static List<T> ExecuteList<T>() where T : new()
		{
			return DbHelper.ExecuteList<T>();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000878A File Offset: 0x0000698A
		public static List<T> ExecuteList<T>(OrderBy orderby) where T : new()
		{
			return DbHelper.ExecuteList<T>(0, orderby);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00008793 File Offset: 0x00006993
		public static List<T> ExecuteList<T>(int tops) where T : new()
		{
			return DbHelper.ExecuteList<T>(tops, null);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000879C File Offset: 0x0000699C
		public static List<T> ExecuteList<T>(int tops, OrderBy orderby) where T : new()
		{
			return DbHelper.ExecuteList<T>(0, orderby, null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000087A6 File Offset: 0x000069A6
		public static List<T> ExecuteList<T>(params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(0, sqlparams);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000087AF File Offset: 0x000069AF
		public static List<T> ExecuteList<T>(OrderBy orderby, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(0, orderby, sqlparams);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000087BC File Offset: 0x000069BC
		public static List<T> ExecuteList<T>(int tops, params SqlParam[] sqlparams) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			return DbHelper.ExecuteList<T>(DbHelper.CreateSelect<T>(tops, list, sqlparams), list.ToArray());
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000087E4 File Offset: 0x000069E4
		public static List<T> ExecuteList<T>(int tops, OrderBy orderby, params SqlParam[] sqlparams) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			return DbHelper.ExecuteList<T>(DbHelper.CreateSelect<T>(tops, orderby, list, sqlparams), list.ToArray());
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000880B File Offset: 0x00006A0B
		public static List<T> ExecuteList<T>(Pager pager) where T : new()
		{
			return DbHelper.ExecuteList<T>(pager, null);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008814 File Offset: 0x00006A14
		public static List<T> ExecuteList<T>(Pager pager, OrderBy orderby) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			return DbHelper.ExecuteList<T>(DbHelper.CreateSelect<T>(pager, orderby, list, null), list.ToArray());
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000883C File Offset: 0x00006A3C
		public static List<T> ExecuteList<T>(Pager pager, params SqlParam[] sqlparams) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			return DbHelper.ExecuteList<T>(DbHelper.CreateSelect<T>(pager, list, sqlparams), list.ToArray());
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008862 File Offset: 0x00006A62
		public static List<T> ExecuteList<T>(string commandText) where T : new()
		{
			return DbHelper.ExecuteList<T>(CommandType.Text, commandText, null);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000886C File Offset: 0x00006A6C
		public static List<T> ExecuteList<T>(string commandText, params DbParameter[] dbparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(CommandType.Text, commandText, dbparams);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008876 File Offset: 0x00006A76
		public static List<T> ExecuteList<T>(CommandType commandType, string commandText) where T : new()
		{
			return DbHelper.ExecuteList<T>(commandType, commandText, null);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008880 File Offset: 0x00006A80
		public static List<T> ExecuteList<T>(CommandType commandType, string commandText, params DbParameter[] dbparams) where T : new()
		{
			IDataReader dataReader = DbHelper.ExecuteReader(commandType, commandText, dbparams);
			List<T> list = new List<T>();
			while (dataReader.Read())
			{
				T t = Activator.CreateInstance<T>();
				try
				{
					foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
					{
						if (propertyInfo.CanWrite)
						{
							AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
							if (attrInfo.IsBindField && !attrInfo.IsNtext)
							{
								string name = propertyInfo.Name;
								if (dataReader.GetOrdinal(name) != -1)
								{
									object obj = dataReader[name];
									if (!(obj is DBNull))
									{
										if (propertyInfo.PropertyType == typeof(FPData))
										{
											FPData fpdata = FPJson.ToModel<FPData>(obj.ToString());
											if (fpdata != null)
											{
												propertyInfo.SetValue(t, fpdata, null);
											}
										}
										else if (propertyInfo.PropertyType == typeof(List<FPData>))
										{
											List<FPData> list2 = FPJson.ToModel<List<FPData>>(obj.ToString());
											if (list2 != null)
											{
												propertyInfo.SetValue(t, list2, null);
											}
										}
										else
										{
											propertyInfo.SetValue(t, obj, null);
										}
									}
									else if (propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(long?) || propertyInfo.PropertyType == typeof(short?))
									{
										propertyInfo.SetValue(t, null, null);
									}
								}
							}
						}
					}
					list.Add(t);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			dataReader.Close();
			return list;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008A48 File Offset: 0x00006C48
		public static int ExecuteInsert<T>(T model)
		{
			List<DbParameter> list = new List<DbParameter>();
			string text = DbHelper.CreateInsert<T>(model, list);
			int num = 0;
			if (DbHelper.DbConfig.dbtype == DbType.Access)
			{
				DbHelper.ExecuteNonQuery(out num, CommandType.Text, text.ToString(), list.ToArray());
			}
			else
			{
				text = text + ";" + DbHelper.DbProvider.GetLastIdSql();
				num = DbUtils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, text, list.ToArray()).ToString(), 0);
			}
			Type typeFromHandle = typeof(T);
			PropertyInfo propertyInfo = null;
			foreach (PropertyInfo propertyInfo2 in typeFromHandle.GetProperties())
			{
				if (DbHelper.GetAttrInfo(propertyInfo2).IsIdentity)
				{
					propertyInfo = propertyInfo2;
					break;
				}
			}
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(model, num, null);
			}
			return num;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00008B10 File Offset: 0x00006D10
		public static int ExecuteUpdate<T>(T model)
		{
			List<DbParameter> list = new List<DbParameter>();
			string commandText = DbHelper.CreateUpdateSql<T>(model, list);
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008B38 File Offset: 0x00006D38
		public static int ExecuteUpdate<T>(params SqlParam[] sqlparams)
		{
			List<DbParameter> list = new List<DbParameter>();
			string commandText = DbHelper.CreateUpdate<T>(list, sqlparams);
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008B60 File Offset: 0x00006D60
		public static T ExecuteModel<T>(int primary) where T : new()
		{
			return DbHelper.ExecuteModel<T>(primary.ToString());
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008B70 File Offset: 0x00006D70
		public static T ExecuteModel<T>(string primary) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			string text2 = "";
			string text3 = string.Format("[{0}]", text);
			string text4 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (propertyInfo.CanWrite)
				{
					AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
					if (attrInfo.IsBindField && !attrInfo.IsTempField)
					{
						if (attrInfo.IsPrimaryKey && text4 == "")
						{
							text4 = propertyInfo.Name;
						}
						string str = string.Format("[{0}].[{1}]", text, propertyInfo.Name);
						if (attrInfo.IsMap)
						{
							if (attrInfo.Number > 0)
							{
								str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number, attrInfo.ColName, propertyInfo.Name);
							}
							else
							{
								str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName, attrInfo.ColName, propertyInfo.Name);
							}
						}
						else if (attrInfo.IsLeftJoin)
						{
							if (attrInfo.Number > 0)
							{
								text3 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
								{
									text3,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
							else
							{
								text3 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
								{
									text3,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
						}
						if (!string.IsNullOrEmpty(text2))
						{
							text2 += ",";
						}
						text2 += str;
					}
				}
			}
			List<DbParameter> list = new List<DbParameter>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT TOP 1 {0} FROM {1} WHERE ", text2, text3);
			stringBuilder.AppendFormat("[{0}].[{1}]=@{1}", text, text4);
			DbParameter item = DbHelper.MakeInParam(string.Format("@{0}", text4), primary);
			list.Add(item);
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), list.ToArray());
			if (dataReader.Read())
			{
				try
				{
					foreach (PropertyInfo propertyInfo2 in typeFromHandle.GetProperties())
					{
						if (propertyInfo2.CanWrite)
						{
							AttrInfo attrInfo2 = DbHelper.GetAttrInfo(propertyInfo2);
							if (attrInfo2.IsBindField && !attrInfo2.IsTempField)
							{
								string name = propertyInfo2.Name;
								if (dataReader.GetOrdinal(name) != -1)
								{
									object obj = dataReader[name];
									if (!(obj is DBNull))
									{
										if (propertyInfo2.PropertyType == typeof(FPData))
										{
											FPData fpdata = FPJson.ToModel<FPData>(obj.ToString());
											if (fpdata != null)
											{
												propertyInfo2.SetValue(t, fpdata, null);
											}
										}
										else if (propertyInfo2.PropertyType == typeof(List<FPData>))
										{
											List<FPData> list2 = FPJson.ToModel<List<FPData>>(obj.ToString());
											if (list2 != null)
											{
												propertyInfo2.SetValue(t, list2, null);
											}
										}
										else
										{
											propertyInfo2.SetValue(t, obj, null);
										}
									}
									else if (propertyInfo2.PropertyType == typeof(DateTime?) || propertyInfo2.PropertyType == typeof(int?) || propertyInfo2.PropertyType == typeof(short?) || propertyInfo2.PropertyType == typeof(long?))
									{
										propertyInfo2.SetValue(t, null, null);
									}
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			dataReader.Close();
			return t;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008FB8 File Offset: 0x000071B8
		public static T ExecuteModel<T>(params SqlParam[] sqlparams) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			string text2 = string.Format("[{0}]", text);
			StringBuilder stringBuilder = new StringBuilder();
			List<DbParameter> list = new List<DbParameter>();
			string text3 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (propertyInfo.CanWrite)
				{
					AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
					if (attrInfo.IsBindField)
					{
						string str = string.Format("[{0}].[{1}]", text, propertyInfo.Name);
						if (attrInfo.IsMap)
						{
							if (attrInfo.Number > 0)
							{
								str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number, attrInfo.ColName, propertyInfo.Name);
							}
							else
							{
								str = string.Format("[{0}].[{1}] AS [{2}]", DbHelper.DbConfig.prefix + attrInfo.TableName, attrInfo.ColName, propertyInfo.Name);
							}
						}
						else if (attrInfo.IsLeftJoin)
						{
							if (attrInfo.Number > 0)
							{
								text2 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
								{
									text2,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
							else
							{
								text2 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
								{
									text2,
									DbHelper.DbConfig.prefix + attrInfo.TableName,
									attrInfo.ColName,
									text,
									propertyInfo.Name
								});
							}
						}
						else if (attrInfo.IsTempField)
						{
							if (DbHelper.DbConfig.dbtype == DbType.Access)
							{
								str = string.Concat(new string[]
								{
									"[@",
									propertyInfo.Name,
									"] AS [",
									propertyInfo.Name,
									"]"
								});
							}
							else
							{
								str = "[" + propertyInfo.Name + "]=@" + propertyInfo.Name;
							}
							DbParameter item = DbHelper.MakeInParam(propertyInfo.Name, propertyInfo.GetValue(t, null));
							list.Add(item);
						}
						if (!string.IsNullOrEmpty(text3))
						{
							text3 += ",";
						}
						text3 += str;
					}
				}
			}
			stringBuilder.AppendFormat("SELECT TOP 1 {0} FROM {1} WHERE ", text3, text2);
			string text4 = "1=1";
			string text5 = "";
			if (sqlparams != null && sqlparams.Length != 0)
			{
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.Set)
					{
						for (int j = 0; j < list.Count; j++)
						{
							if (list[j].ParameterName == "@" + sqlParam.ParamName.TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							}))
							{
								list[j].Value = sqlParam.Value;
							}
						}
					}
					else if (sqlParam.SqlType == SqlType.OrderBy)
					{
						string text6 = text;
						string arg = sqlParam.ParamName.TrimStart(new char[]
						{
							'('
						}).TrimEnd(new char[]
						{
							')'
						}).TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						});
						if (sqlParam.ParamName.IndexOf(".") > 0)
						{
							string[] array = DbUtils.SplitString(sqlParam.ParamName, ".", 2);
							text6 = array[0].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
							if (text6.StartsWith("FP_"))
							{
								text6 = text6.Replace("FP_", DbConfigs.Prefix);
							}
							else
							{
								text6 = DbConfigs.Prefix + text6;
							}
							arg = array[1].TrimStart(new char[]
							{
								'('
							}).TrimEnd(new char[]
							{
								')'
							}).TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							});
						}
						text5 = DbUtils.PushString(text5, string.Format("[{0}].[{1}] {2}", text6, arg, sqlParam.Value.ToString()));
					}
				}
				string text7 = DbHelper.CreateWhere<T>(text, list, sqlparams);
				if (text7 != "")
				{
					text4 = text4 + " AND " + text7;
				}
			}
			stringBuilder.Append(text4);
			if (text5 != "")
			{
				stringBuilder.Append(" ORDER BY " + text5);
			}
			return DbHelper.ExecuteModel<T>(stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000951C File Offset: 0x0000771C
		public static T ExecuteModel<T>(string commandText, params DbParameter[] dbparams) where T : new()
		{
			T t = Activator.CreateInstance<T>();
			Type typeFromHandle = typeof(T);
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, commandText, dbparams);
			if (dataReader.Read())
			{
				try
				{
					foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
					{
						if (propertyInfo.CanWrite && DbHelper.GetAttrInfo(propertyInfo).IsBindField)
						{
							string name = propertyInfo.Name;
							if (dataReader.GetOrdinal(name) != -1)
							{
								object obj = dataReader[name];
								if (!(obj is DBNull))
								{
									if (propertyInfo.PropertyType == typeof(FPData))
									{
										FPData fpdata = FPJson.ToModel<FPData>(obj.ToString());
										if (fpdata != null)
										{
											propertyInfo.SetValue(t, fpdata, null);
										}
									}
									else if (propertyInfo.PropertyType == typeof(List<FPData>))
									{
										List<FPData> list = FPJson.ToModel<List<FPData>>(obj.ToString());
										if (list != null)
										{
											propertyInfo.SetValue(t, list, null);
										}
									}
									else
									{
										propertyInfo.SetValue(t, obj, null);
									}
								}
								else if (propertyInfo.PropertyType == typeof(DateTime?) || propertyInfo.PropertyType == typeof(int?) || propertyInfo.PropertyType == typeof(short?))
								{
									propertyInfo.SetValue(t, null, null);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			dataReader.Close();
			return t;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000096B4 File Offset: 0x000078B4
		public static int ExecuteDelete<T>(int id)
		{
			List<DbParameter> list = new List<DbParameter>();
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DELETE FROM [{0}{1}] WHERE ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			using (List<PropertyInfo>.Enumerator enumerator = DbHelper.GetPrimaryKeys(typeFromHandle).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					PropertyInfo propertyInfo = enumerator.Current;
					stringBuilder.AppendFormat("[{0}]=@{0}", propertyInfo.Name);
					DbParameter item = DbHelper.MakeInParam(string.Format("@{0}", propertyInfo.Name), id);
					list.Add(item);
				}
			}
			return DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009784 File Offset: 0x00007984
		public static int ExecuteDelete<T>(string idlist)
		{
			Type typeFromHandle = typeof(T);
			List<SqlParam> list = new List<SqlParam>();
			foreach (PropertyInfo propertyInfo in DbHelper.GetPrimaryKeys(typeFromHandle))
			{
				list.Add(DbHelper.MakeAndWhere(propertyInfo.Name, WhereType.In, idlist));
			}
			return DbHelper.ExecuteDelete<T>(list.ToArray());
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009800 File Offset: 0x00007A00
		public static int ExecuteDelete<T>(params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DELETE FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length != 0)
			{
				stringBuilder.Append(" WHERE ").Append(DbHelper.CreateWhere(list, sqlparams));
			}
			return DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009871 File Offset: 0x00007A71
		public static int ExecuteCount(string commandText)
		{
			return DbHelper.ExecuteCount(CommandType.Text, commandText);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000987A File Offset: 0x00007A7A
		public static int ExecuteCount(CommandType commandType, string commandText)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(commandType, commandText));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009888 File Offset: 0x00007A88
		public static int ExecuteCount(string commandText, params DbParameter[] commandParameters)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, commandParameters));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009897 File Offset: 0x00007A97
		public static int ExecuteCount(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(commandType, commandText, commandParameters));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000098A8 File Offset: 0x00007AA8
		public static int ExecuteCount<T>()
		{
			Type typeFromHandle = typeof(T);
			string arg = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			return DbHelper.ExecuteCount(string.Format("SELECT COUNT(*) FROM [{0}]", arg));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000098E8 File Offset: 0x00007AE8
		private static int ExecuteCount<T>(string where, params DbParameter[] commandParameters)
		{
			Type typeFromHandle = typeof(T);
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			string text2 = string.Format("[{0}]", text);
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsLeftJoin)
				{
					if (attrInfo.Number > 0)
					{
						text2 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
						{
							text2,
							DbHelper.DbConfig.prefix + attrInfo.TableName,
							DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
							attrInfo.ColName,
							text,
							propertyInfo.Name
						});
					}
					else
					{
						text2 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
						{
							text2,
							DbHelper.DbConfig.prefix + attrInfo.TableName,
							attrInfo.ColName,
							text,
							propertyInfo.Name
						});
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT COUNT(*) FROM {0}", text2), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteCount(CommandType.Text, stringBuilder.ToString(), commandParameters);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009A5C File Offset: 0x00007C5C
		public static int ExecuteCount<T>(string where)
		{
			Type typeFromHandle = typeof(T);
			string arg = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT COUNT(*) FROM [{0}]", arg), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteCount(stringBuilder.ToString());
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public static int ExecuteCount<T>(params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			string text = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			string text2 = string.Format("[{0}]", text);
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				AttrInfo attrInfo = DbHelper.GetAttrInfo(propertyInfo);
				if (attrInfo.IsLeftJoin)
				{
					if (attrInfo.Number > 0)
					{
						text2 = string.Format("({0} LEFT JOIN [{1}] AS [{2}] ON [{2}].[{3}]=[{4}].[{5}])", new object[]
						{
							text2,
							DbHelper.DbConfig.prefix + attrInfo.TableName,
							DbHelper.DbConfig.prefix + attrInfo.TableName + attrInfo.Number,
							attrInfo.ColName,
							text,
							propertyInfo.Name
						});
					}
					else
					{
						text2 = string.Format("({0} LEFT JOIN [{1}] ON [{1}].[{2}]=[{3}].[{4}])", new object[]
						{
							text2,
							DbHelper.DbConfig.prefix + attrInfo.TableName,
							attrInfo.ColName,
							text,
							propertyInfo.Name
						});
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT COUNT(*) FROM {0}", text2);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text3 = DbHelper.CreateWhere<T>(text, list, sqlparams);
				if (!string.IsNullOrEmpty(text3))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text3);
				}
			}
			return DbHelper.ExecuteCount(stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009C50 File Offset: 0x00007E50
		public static object ExecuteMax<T>(string colname)
		{
			Type typeFromHandle = typeof(T);
			string arg = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			string commandText = string.Format("SELECT MAX([{1}]) FROM [{0}]", arg, colname.TrimStart(new char[]
			{
				'['
			}).TrimEnd(new char[]
			{
				']'
			}));
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009CB4 File Offset: 0x00007EB4
		public static object ExecuteMax<T>(string colname, string where)
		{
			Type typeFromHandle = typeof(T);
			string arg = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT MAX([{1}]) FROM [{0}]", arg, colname.TrimStart(new char[]
			{
				'['
			}).TrimEnd(new char[]
			{
				']'
			})), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString());
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009D44 File Offset: 0x00007F44
		public static object ExecuteMax<T>(string colname, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string arg = DbHelper.DbConfig.prefix + DbHelper.GetModelTable(typeFromHandle);
			stringBuilder.AppendFormat("SELECT MAX([{1}]) FROM [{0}]", arg, colname.TrimStart(new char[]
			{
				'['
			}).TrimEnd(new char[]
			{
				']'
			}));
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhere(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public static object ExecuteSum<T>(string colname)
		{
			Type typeFromHandle = typeof(T);
			string commandText = string.Format("SELECT SUM([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009E20 File Offset: 0x00008020
		public static object ExecuteSum<T>(string colname, string where)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT SUM([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString());
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009E88 File Offset: 0x00008088
		public static object ExecuteSum<T>(string colname, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT SUM([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhere(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00009EFC File Offset: 0x000080FC
		public static object ExecuteAvg<T>(string colname)
		{
			Type typeFromHandle = typeof(T);
			string commandText = string.Format("SELECT AVG([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009F38 File Offset: 0x00008138
		public static object ExecuteAvg<T>(string colname, string where)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT AVG([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString());
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009FA0 File Offset: 0x000081A0
		public static object ExecuteAvg<T>(string colname, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT AVG([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhere(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000A014 File Offset: 0x00008214
		public static DataTable ExecuteGroupBy<T>(string colname)
		{
			DataTable dataTable = new DataTable();
			Type typeFromHandle = typeof(T);
			string text = "";
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("((?:,?)Fir\\((?:\\[?)([^\\s]+?)(?:\\]?),(?:\\[?)([^\\s]+?(?:\\]?))\\))", options);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in regex.Matches(colname))
			{
				Match match = (Match)obj;
				string value;
				if (match.Groups[3].ToString() != "")
				{
					string[] array = DbUtils.SplitString(match.Groups[3].ToString(), "]", 2);
					if (string.IsNullOrEmpty(array[1]))
					{
						array[1] = "DESC";
					}
					value = array[0] + " " + array[1];
				}
				else
				{
					value = match.Groups[2].ToString() + " DESC";
				}
				dictionary.Add(match.Groups[2].ToString(), value);
				colname = colname.Replace(match.Groups[0].ToString(), string.Empty);
			}
			foreach (string text2 in DbUtils.SplitString(colname, ","))
			{
				if (!text2.ToUpper().StartsWith("COUNT(") && !text2.ToUpper().StartsWith("SUM(") && !text2.ToUpper().StartsWith("AVG(") && !text2.ToUpper().StartsWith("FIR("))
				{
					if (text != "")
					{
						text += ",";
					}
					text = text + "[" + text2.TrimStart(new char[]
					{
						'['
					}).TrimEnd(new char[]
					{
						']'
					}) + "]";
				}
			}
			string modelTable = DbHelper.GetModelTable(typeFromHandle);
			if (text != "")
			{
				dataTable = DbHelper.ExecuteDataset(string.Format("SELECT {0} FROM [{1}{2}] GROUP BY {3}", new object[]
				{
					colname,
					DbHelper.DbConfig.prefix,
					modelTable,
					text
				})).Tables[0];
			}
			if (text != "" && dictionary.Count > 0)
			{
				foreach (string text3 in dictionary.Keys)
				{
					dataTable.Columns.Add(text3, typeof(string));
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						string text4 = "";
						foreach (string text5 in DbUtils.SplitString(text, ","))
						{
							if (text4 != "")
							{
								text4 += " AND ";
							}
							if (dataTable.Columns[text5.TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							})].DataType == typeof(string) || dataTable.Columns[text5.TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							})].DataType == typeof(DateTime))
							{
								text4 = string.Concat(new string[]
								{
									text4,
									text5,
									"='",
									dataTable.Rows[j][text5.TrimStart(new char[]
									{
										'['
									}).TrimEnd(new char[]
									{
										']'
									})].ToString(),
									"'"
								});
							}
							else
							{
								text4 = text4 + text5 + "=" + dataTable.Rows[j][text5.TrimStart(new char[]
								{
									'['
								}).TrimEnd(new char[]
								{
									']'
								})].ToString();
							}
						}
						DataTable dataTable2 = DbHelper.ExecuteTable(string.Format("SELECT TOP 1 [{0}] FROM [{1}{2}] WHERE {3} ORDER BY {4}", new object[]
						{
							text3,
							DbConfigs.Prefix,
							modelTable,
							text4,
							dictionary[text3]
						}));
						if (dataTable2.Rows.Count > 0)
						{
							dataTable.Rows[j][text3] = dataTable2.Rows[0][text3];
						}
					}
				}
			}
			return dataTable;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A51C File Offset: 0x0000871C
		public static DataTable ExecuteGroupBy<T>(string colname, params SqlParam[] sqlparams)
		{
			DataTable dataTable = new DataTable();
			Type typeFromHandle = typeof(T);
			string text = "";
			RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline;
			Regex regex = new Regex("((?:,?)Fir\\((?:\\[?)([^\\s]+?)(?:\\]?),(?:\\[?)([^\\s]+?(?:\\]?))\\))", options);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (object obj in regex.Matches(colname))
			{
				Match match = (Match)obj;
				string value;
				if (match.Groups[3].ToString() != "")
				{
					string[] array = DbUtils.SplitString(match.Groups[3].ToString(), "]", 2);
					if (string.IsNullOrEmpty(array[1]))
					{
						array[1] = "DESC";
					}
					value = array[0] + " " + array[1];
				}
				else
				{
					value = match.Groups[2].ToString() + " DESC";
				}
				dictionary.Add(match.Groups[2].ToString(), value);
				colname = colname.Replace(match.Groups[0].ToString(), string.Empty);
			}
			foreach (string text2 in DbUtils.SplitString(colname, ","))
			{
				if (!text2.ToUpper().StartsWith("COUNT(") && !text2.ToUpper().StartsWith("SUM(") && !text2.ToUpper().StartsWith("AVG(") && !text2.ToUpper().StartsWith("FIR("))
				{
					if (text != "")
					{
						text += ",";
					}
					text = text + "[" + text2.TrimStart(new char[]
					{
						'['
					}).TrimEnd(new char[]
					{
						']'
					}) + "]";
				}
			}
			string modelTable = DbHelper.GetModelTable(typeFromHandle);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT {0} FROM [{1}{2}]", colname, DbHelper.DbConfig.prefix, modelTable);
			List<DbParameter> list = new List<DbParameter>();
			string text3 = "";
			if (sqlparams != null)
			{
				string text4 = DbHelper.CreateWhere(list, sqlparams);
				if (!string.IsNullOrEmpty(text4))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text4);
				}
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.OrderBy)
					{
						text3 = DbUtils.PushString(text3, string.Format("[{0}] {1}", sqlParam.ParamName.TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						}), sqlParam.Value.ToString()));
					}
				}
			}
			if (text != "")
			{
				stringBuilder.AppendFormat(" GROUP BY {0}", text);
				if (!string.IsNullOrEmpty(text3))
				{
					stringBuilder.AppendFormat(" ORDER BY {0}", text3);
				}
				dataTable = DbHelper.ExecuteDataset(CommandType.Text, stringBuilder.ToString(), list.ToArray()).Tables[0];
			}
			if (text != "" && dictionary.Count > 0)
			{
				foreach (string text5 in dictionary.Keys)
				{
					dataTable.Columns.Add(text5, typeof(string));
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						string text6 = "";
						foreach (string text7 in DbUtils.SplitString(text, ","))
						{
							if (text6 != "")
							{
								text6 += " AND ";
							}
							if (dataTable.Columns[text7.TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							})].DataType == typeof(string) || dataTable.Columns[text7.TrimStart(new char[]
							{
								'['
							}).TrimEnd(new char[]
							{
								']'
							})].DataType == typeof(DateTime))
							{
								text6 = string.Concat(new string[]
								{
									text6,
									text7,
									"='",
									dataTable.Rows[j][text7.TrimStart(new char[]
									{
										'['
									}).TrimEnd(new char[]
									{
										']'
									})].ToString(),
									"'"
								});
							}
							else
							{
								text6 = text6 + text7 + "=" + dataTable.Rows[j][text7.TrimStart(new char[]
								{
									'['
								}).TrimEnd(new char[]
								{
									']'
								})].ToString();
							}
						}
						DataTable dataTable2 = DbHelper.ExecuteTable(string.Format("SELECT TOP 1 [{0}] FROM [{1}{2}] WHERE {3} ORDER BY {4}", new object[]
						{
							text5,
							DbConfigs.Prefix,
							modelTable,
							text6,
							dictionary[text5]
						}));
						if (dataTable2.Rows.Count > 0)
						{
							dataTable.Rows[j][text5] = dataTable2.Rows[0][text5];
						}
					}
				}
			}
			return dataTable;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000AAF4 File Offset: 0x00008CF4
		public static DataTable ExecuteTable(string sqlstring)
		{
			if (sqlstring != "" && sqlstring.IndexOf("[FP_") > 0)
			{
				sqlstring = sqlstring.Replace("[FP_", "[" + DbConfigs.Prefix);
			}
			return DbHelper.ExecuteDataset(sqlstring).Tables[0];
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000AB49 File Offset: 0x00008D49
		public static DataTable ExecuteTable(string table, params SqlParam[] sqlparams)
		{
			return DbHelper.ExecuteTable(table, "", sqlparams);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000AB58 File Offset: 0x00008D58
		public static DataTable ExecuteTable(string table, string colname, params SqlParam[] sqlparams)
		{
			if (!string.IsNullOrEmpty(table) && table.StartsWith("FP_"))
			{
				table = table.Replace("FP_", DbConfigs.Prefix);
			}
			if (string.IsNullOrEmpty(colname))
			{
				colname = "*";
			}
			string text = string.Format("SELECT {0} FROM {1}", colname, table);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text2 = DbHelper.CreateWhere(list, sqlparams);
				string text3 = "";
				if (!string.IsNullOrEmpty(text2))
				{
					text += string.Format(" WHERE {0}", text2);
				}
				foreach (SqlParam sqlParam in sqlparams)
				{
					if (sqlParam.SqlType == SqlType.OrderBy)
					{
						text3 = DbUtils.PushString(text3, string.Format("[{0}] {1}", sqlParam.ParamName.TrimStart(new char[]
						{
							'['
						}).TrimEnd(new char[]
						{
							']'
						}), sqlParam.Value.ToString()));
					}
				}
				if (!string.IsNullOrEmpty(text3))
				{
					text += string.Format(" ORDER BY {0}", text3);
				}
			}
			return DbHelper.ExecuteDataset(CommandType.Text, text, list.ToArray()).Tables[0];
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000AC79 File Offset: 0x00008E79
		public static string ExecuteSql(string sqlstring)
		{
			return DbHelper.DbProvider.ExecuteSql(sqlstring);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000AC86 File Offset: 0x00008E86
		public static string RunSql(string sqlstring)
		{
			return DbHelper.DbProvider.RunSql(sqlstring);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000AC94 File Offset: 0x00008E94
		public static void ClearDBLog()
		{
			if (DbHelper.m_dbconfig.dbtype == DbType.SqlServer)
			{
				string dbname = DbHelper.m_dbconfig.dbname;
				DbParameter dbParameter = DbHelper.MakeInParam("@DBName", DbType.SqlServer, 50, dbname);
				string commandText = DbHelper.m_dbconfig.prefix + "shrinklog";
				DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, commandText, new DbParameter[]
				{
					dbParameter
				});
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000ACF0 File Offset: 0x00008EF0
		public static DataTable GetDbTableList()
		{
			DataTable dataTable = new DataTable();
			using (DbConnection dbConnection = DbHelper.Factory.CreateConnection())
			{
				dbConnection.ConnectionString = DbHelper.DbConfig.connectionstring;
				dbConnection.Open();
				dataTable = dbConnection.GetSchema("Tables");
			}
			dataTable.DefaultView.Sort = "TABLE_NAME ASC";
			return dataTable.DefaultView.ToTable();
		}

		// Token: 0x0400001D RID: 29
		private static DbConfigInfo m_dbconfig;

		// Token: 0x0400001E RID: 30
		private static DbProviderFactory m_factory;

		// Token: 0x0400001F RID: 31
		private static IDbProvider m_dbprovider;

		// Token: 0x0200001B RID: 27
		private enum DbConnectionOwnership
		{
			// Token: 0x04000057 RID: 87
			Internal,
			// Token: 0x04000058 RID: 88
			External
		}
	}
}
