using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;

namespace FangPage.Data
{
	// Token: 0x02000006 RID: 6
	public class DbHelper
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002510 File Offset: 0x00000710
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002528 File Offset: 0x00000728
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

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002530 File Offset: 0x00000730
		public static IDbProvider DbProvider
		{
			get
			{
				if (DbHelper.m_dbprovider == null)
				{
					switch (DbHelper.DbConfig.dbtype)
					{
					case DbType.SqlServer:
						DbHelper.m_dbprovider = new SqlServerProvider();
						break;
					case DbType.Access:
						DbHelper.m_dbprovider = new AccessProvider();
						break;
					}
				}
				return DbHelper.m_dbprovider;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000257A File Offset: 0x0000077A
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

		// Token: 0x06000025 RID: 37 RVA: 0x00002597 File Offset: 0x00000797
		public static void ResetDbProvider()
		{
			DbConfigs.ReSet();
			DbHelper.m_dbconfig = null;
			DbHelper.m_factory = null;
			DbHelper.m_dbprovider = null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B0 File Offset: 0x000007B0
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

		// Token: 0x06000027 RID: 39 RVA: 0x00002618 File Offset: 0x00000818
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

		// Token: 0x06000028 RID: 40 RVA: 0x000026A8 File Offset: 0x000008A8
		public static bool IsBindField(PropertyInfo info)
		{
			if (info == null)
			{
				return false;
			}
			object[] customAttributes = info.GetCustomAttributes(true);
			BindField bindField = null;
			bool flag = false;
			foreach (object obj in customAttributes)
			{
				if (obj is BindField)
				{
					bindField = (obj as BindField);
					flag = true;
					break;
				}
			}
			return !flag || bindField == null || bindField.IsBind;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002708 File Offset: 0x00000908
		public static bool IsNText(PropertyInfo info)
		{
			if (info == null)
			{
				return false;
			}
			object[] customAttributes = info.GetCustomAttributes(true);
			NText ntext = null;
			bool flag = false;
			foreach (object obj in customAttributes)
			{
				if (obj is NText)
				{
					ntext = (obj as NText);
					flag = true;
					break;
				}
			}
			return flag && ntext != null && ntext.IsNText;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002768 File Offset: 0x00000968
		public static bool IsIdentity(PropertyInfo info)
		{
			if (info == null)
			{
				return false;
			}
			object[] customAttributes = info.GetCustomAttributes(true);
			Identity identity = null;
			bool flag = false;
			foreach (object obj in customAttributes)
			{
				if (obj is Identity)
				{
					identity = (obj as Identity);
					flag = true;
					break;
				}
			}
			return flag && identity != null && identity.IsIdentity;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027C8 File Offset: 0x000009C8
		public static bool IsPrimaryKey(PropertyInfo info)
		{
			if (info == null)
			{
				return false;
			}
			object[] customAttributes = info.GetCustomAttributes(true);
			PrimaryKey primaryKey = null;
			bool flag = false;
			foreach (object obj in customAttributes)
			{
				if (obj is PrimaryKey)
				{
					primaryKey = (obj as PrimaryKey);
					flag = true;
					break;
				}
			}
			return flag && primaryKey != null && primaryKey.IsPrimaryKey;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002828 File Offset: 0x00000A28
		public static List<PropertyInfo> GetIdentitys(Type type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (DbHelper.IsIdentity(propertyInfo))
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000286C File Offset: 0x00000A6C
		public static List<PropertyInfo> GetPrimaryKeys(Type type)
		{
			List<PropertyInfo> list = new List<PropertyInfo>();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (DbHelper.IsPrimaryKey(propertyInfo))
				{
					list.Add(propertyInfo);
				}
			}
			return list;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028B0 File Offset: 0x00000AB0
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
			if (bindTable != null)
			{
				text = bindTable.TableName;
			}
			if (modelPrefix != null)
			{
				text = modelPrefix.Prefix + "_" + text;
			}
			return text;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002937 File Offset: 0x00000B37
		public static DbParameter MakeInParam(string ParamName, object Value)
		{
			if (!ParamName.StartsWith("@"))
			{
				ParamName = "@" + ParamName;
			}
			return DbHelper.DbProvider.MakeParam(ParamName, Value);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002960 File Offset: 0x00000B60
		public static DbParameter MakeInParam<T>(PropertyInfo propInfo, T targetObj)
		{
			string paramName = string.Format("@{0}", propInfo.Name);
			object value = propInfo.GetValue(targetObj, null);
			return DbHelper.DbProvider.MakeParam(paramName, value);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002998 File Offset: 0x00000B98
		public static DbParameter MakeInParam(string ParamName, DbType DbType, int Size, object Value)
		{
			return DbHelper.MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029A4 File Offset: 0x00000BA4
		public static DbParameter MakeOutParam(string ParamName, DbType DbType, int Size)
		{
			return DbHelper.MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029B0 File Offset: 0x00000BB0
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

		// Token: 0x06000034 RID: 52 RVA: 0x000029FD File Offset: 0x00000BFD
		public static SqlParam MakeAndWhere(string ParamName)
		{
			return new SqlParam(SqlType.And, ParamName, WhereType.Custom, null);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002A09 File Offset: 0x00000C09
		public static SqlParam MakeAndWhere(string ParamName, object Value)
		{
			return new SqlParam(SqlType.And, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002A14 File Offset: 0x00000C14
		public static SqlParam MakeAndWhere(string ParamName, WhereType ParamType, object Value)
		{
			return new SqlParam(SqlType.And, ParamName, ParamType, Value);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002A1F File Offset: 0x00000C1F
		public static SqlParam MakeOrWhere(string ParamName)
		{
			return new SqlParam(SqlType.Or, ParamName, WhereType.Custom, null);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A2B File Offset: 0x00000C2B
		public static SqlParam MakeOrWhere(string ParamName, object Value)
		{
			return new SqlParam(SqlType.Or, ParamName, WhereType.Equal, Value);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A36 File Offset: 0x00000C36
		public static SqlParam MakeOrWhere(string ParamName, WhereType ParamType, object Value)
		{
			return new SqlParam(SqlType.Or, ParamName, ParamType, Value);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A41 File Offset: 0x00000C41
		public static SqlParam MakeWhere(SqlType WhereType, string ParamName, WhereType ParamType, object Value)
		{
			return new SqlParam(WhereType, ParamName, ParamType, Value);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A4C File Offset: 0x00000C4C
		public static SqlParam MakeSet(string ParamName, object Value)
		{
			return new SqlParam(SqlType.Set, ParamName, Value);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A56 File Offset: 0x00000C56
		public static OrderByParam MakeOrderBy(string FieldName, OrderBy OrderByType)
		{
			return new OrderByParam(FieldName, OrderByType);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A60 File Offset: 0x00000C60
		public static string CreateWhereSql(List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (SqlParam sqlParam in sqlparams)
			{
				if (sqlParam.SqlType != SqlType.Set)
				{
					if (sqlParam.SqlType == SqlType.And && stringBuilder.ToString() != "")
					{
						stringBuilder.Append(" AND ");
					}
					else if (sqlParam.SqlType == SqlType.Or && stringBuilder.ToString() != "")
					{
						stringBuilder.Append(" OR ");
					}
					if (sqlParam.WhereType == WhereType.Custom)
					{
						stringBuilder.AppendFormat(sqlParam.ParamName, new object[0]);
					}
					else
					{
						int num = 0;
						string text = sqlParam.ParamName;
						foreach (DbParameter dbParameter in dbparamlist)
						{
							if (dbParameter.ParameterName == "@" + sqlParam.ParamName)
							{
								num++;
								text = sqlParam.ParamName + num.ToString();
							}
						}
						if (sqlParam.WhereType == WhereType.Equal)
						{
							stringBuilder.AppendFormat("[{0}]=@{1}", sqlParam.ParamName, text);
						}
						else if (sqlParam.WhereType == WhereType.NotEqual)
						{
							stringBuilder.AppendFormat("[{0}]<>@{1}", sqlParam.ParamName, text);
						}
						else if (sqlParam.WhereType == WhereType.GreaterThan)
						{
							stringBuilder.AppendFormat("[{0}]>@{1}", sqlParam.ParamName, text);
						}
						else if (sqlParam.WhereType == WhereType.GreaterThanEqual)
						{
							stringBuilder.AppendFormat("[{0}]>=@{1}", sqlParam.ParamName, text);
						}
						else if (sqlParam.WhereType == WhereType.LessThan)
						{
							stringBuilder.AppendFormat("[{0}]<@{1}", sqlParam.ParamName, text);
						}
						else if (sqlParam.WhereType == WhereType.LessThanEqual)
						{
							stringBuilder.AppendFormat("[{0}]<=@{1}", sqlParam.ParamName, text);
						}
						if (sqlParam.WhereType == WhereType.In)
						{
							if (!string.IsNullOrEmpty(sqlParam.Value.ToString()))
							{
								stringBuilder.AppendFormat("[{0}] IN({1})", sqlParam.ParamName, sqlParam.Value);
							}
							else
							{
								stringBuilder.AppendFormat("[{0}] IN(0)", sqlParam.ParamName);
							}
						}
						else if (sqlParam.WhereType == WhereType.NotIn)
						{
							if (!string.IsNullOrEmpty(sqlParam.Value.ToString()))
							{
								stringBuilder.AppendFormat("[{0}] NOT IN({1})", sqlParam.ParamName, sqlParam.Value);
							}
							else
							{
								stringBuilder.AppendFormat("[{0}] NOT IN(0)", sqlParam.ParamName);
							}
						}
						else if (sqlParam.WhereType == WhereType.Like)
						{
							string[] array = sqlParam.ParamName.Split(new char[]
							{
								','
							});
							string text2 = "";
							foreach (string arg in array)
							{
								if (text2 != "")
								{
									text2 += " OR ";
								}
								text2 += string.Format("[{0}] LIKE '%{1}%'", arg, DbUtils.RegEsc(sqlParam.Value.ToString()));
							}
							if (array.Length > 1)
							{
								text2 = "(" + text2 + ")";
							}
							stringBuilder.Append(text2);
						}
						else if (sqlParam.WhereType == WhereType.NotLike)
						{
							stringBuilder.AppendFormat(" NOT LIKE '%{0}%'", DbUtils.RegEsc(sqlParam.Value.ToString()));
						}
						else
						{
							string text3 = sqlParam.Value.ToString();
							if (text3.IndexOf("[") >= 0 && text3.IndexOf("]") >= 0)
							{
								stringBuilder.AppendFormat("{0}", DbUtils.RegEsc(text3));
							}
							else
							{
								DbParameter item = DbHelper.MakeInParam(text, sqlParam.Value);
								dbparamlist.Add(item);
							}
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E18 File Offset: 0x00001018
		public static string CreateSelectSql<T>(List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += string.Format("[{0}]", propertyInfo.Name);
				}
			}
			stringBuilder.AppendFormat("SELECT {0} FROM [{1}{2}]", text, DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			if (sqlparams != null && sqlparams.Length > 0)
			{
				string text2 = DbHelper.CreateWhereSql(dbparamlist, sqlparams);
				if (!string.IsNullOrEmpty(text2))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text2);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EEC File Offset: 0x000010EC
		public static string CreateSelectSql<T>(string keys, List<DbParameter> dbparamlist, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo) && DbHelper.IsPrimaryKey(propertyInfo) && string.IsNullOrEmpty(keys))
				{
					keys = string.Format("[{0}]", propertyInfo.Name);
				}
			}
			stringBuilder.AppendFormat("SELECT {0} FROM [{1}{2}]", keys, DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			if (sqlparams != null && sqlparams.Length > 0)
			{
				string text = DbHelper.CreateWhereSql(dbparamlist, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002FAC File Offset: 0x000011AC
		public static string CreateInsertSql<T>(T targetObj, List<DbParameter> dbparamlist)
		{
			Type type = targetObj.GetType();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("INSERT INTO [{0}{1}] (", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(type));
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (!DbHelper.IsIdentity(propertyInfo) && DbHelper.IsBindField(propertyInfo))
				{
					Type propertyType = propertyInfo.PropertyType;
					object value = propertyInfo.GetValue(targetObj, null);
					if (value != null)
					{
						list.Add(propertyInfo);
					}
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

		// Token: 0x06000041 RID: 65 RVA: 0x000030F8 File Offset: 0x000012F8
		public static string CreateUpdateSql<T>(T targetObj, List<DbParameter> dbparamlist)
		{
			Type type = targetObj.GetType();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("UPDATE [{0}{1}] SET ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(type));
			PropertyInfo[] properties = type.GetProperties();
			List<PropertyInfo> list = new List<PropertyInfo>();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!DbHelper.IsIdentity(propertyInfo) && !DbHelper.IsPrimaryKey(propertyInfo) && DbHelper.IsBindField(propertyInfo))
				{
					object value = propertyInfo.GetValue(targetObj, null);
					if (value != null)
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
			string text = "";
			List<PropertyInfo> primaryKeys = DbHelper.GetPrimaryKeys(type);
			foreach (PropertyInfo propertyInfo3 in primaryKeys)
			{
				if (text != "")
				{
					text += " AND ";
				}
				text += string.Format("[{0}]=@{0}", propertyInfo3.Name);
				DbParameter item2 = DbHelper.MakeInParam<T>(propertyInfo3, targetObj);
				dbparamlist.Add(item2);
			}
			if (text != "")
			{
				stringBuilder.AppendFormat(" WHERE {0}", text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000032A4 File Offset: 0x000014A4
		public static string CreateUpdateSql<T>(List<DbParameter> dbparamlist, SqlParam[] wheres)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Type typeFromHandle = typeof(T);
			stringBuilder.AppendFormat("UPDATE [{0}{1}] SET ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			string text = "";
			foreach (SqlParam sqlParam in wheres)
			{
				if (sqlParam.SqlType == SqlType.Set)
				{
					if (text != "")
					{
						text += ",";
					}
					text += string.Format("[{0}]=@{0}", sqlParam.ParamName);
					DbParameter item = DbHelper.MakeInParam("@" + sqlParam.ParamName, sqlParam.Value);
					dbparamlist.Add(item);
				}
			}
			stringBuilder.Append(text);
			string text2 = DbHelper.CreateWhereSql(dbparamlist, wheres);
			if (text2 != "")
			{
				stringBuilder.Append(" WHERE " + text2);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003396 File Offset: 0x00001596
		public static int ExecuteNonQuery(string commandText)
		{
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, null);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000033A0 File Offset: 0x000015A0
		public static int ExecuteNonQuery(out int id, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, CommandType.Text, commandText, null);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000033AB File Offset: 0x000015AB
		public static int ExecuteNonQuery(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(commandType, commandText, null);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000033B5 File Offset: 0x000015B5
		public static int ExecuteNonQuery(out int id, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, commandType, commandText, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000033C0 File Offset: 0x000015C0
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

		// Token: 0x06000048 RID: 72 RVA: 0x00003434 File Offset: 0x00001634
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

		// Token: 0x06000049 RID: 73 RVA: 0x000034A8 File Offset: 0x000016A8
		public static int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(connection, commandType, commandText, null);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000034B3 File Offset: 0x000016B3
		public static int ExecuteNonQuery(out int id, DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(out id, connection, commandType, commandText, null);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000034C0 File Offset: 0x000016C0
		public static int ExecuteNonQuery(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
			bool flag = false;
			DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out flag);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			if (flag)
			{
				connection.Close();
			}
			return result;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003514 File Offset: 0x00001714
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
			bool flag = false;
			DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out flag);
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

		// Token: 0x0600004D RID: 77 RVA: 0x000035B5 File Offset: 0x000017B5
		public static int ExecuteNonQuery(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteNonQuery(transaction, commandType, commandText, null);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000035C0 File Offset: 0x000017C0
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
			bool flag = false;
			DbHelper.PrepareCommand(dbCommand, transaction.Connection, transaction, commandType, commandText, commandParameters, out flag);
			int result = dbCommand.ExecuteNonQuery();
			dbCommand.Parameters.Clear();
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003628 File Offset: 0x00001828
		private static DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DbParameter[] commandParameters, DbHelper.DbConnectionOwnership connectionOwnership)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			bool flag = false;
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
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
				foreach (object obj in dbCommand.Parameters)
				{
					DbParameter dbParameter = (DbParameter)obj;
					if (dbParameter.Direction != ParameterDirection.Input)
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

		// Token: 0x06000050 RID: 80 RVA: 0x00003700 File Offset: 0x00001900
		public static DbDataReader ExecuteReader(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(commandType, commandText, null);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000370C File Offset: 0x0000190C
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

		// Token: 0x06000052 RID: 82 RVA: 0x00003784 File Offset: 0x00001984
		public static DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(connection, commandType, commandText, null);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000378F File Offset: 0x0000198F
		public static DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			return DbHelper.ExecuteReader(connection, null, commandType, commandText, commandParameters, DbHelper.DbConnectionOwnership.External);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000379C File Offset: 0x0000199C
		public static DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteReader(transaction, commandType, commandText, null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000037A7 File Offset: 0x000019A7
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

		// Token: 0x06000056 RID: 86 RVA: 0x000037E2 File Offset: 0x000019E2
		public static DataSet ExecuteDataset(string commandText)
		{
			return DbHelper.ExecuteDataset(CommandType.Text, commandText, null);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000037EC File Offset: 0x000019EC
		public static DataSet ExecuteDataset(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteDataset(commandType, commandText, null);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000037F8 File Offset: 0x000019F8
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

		// Token: 0x06000059 RID: 89 RVA: 0x0000386C File Offset: 0x00001A6C
		public static DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteDataset(connection, commandType, commandText, null);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003878 File Offset: 0x00001A78
		public static DataSet ExecuteDataset(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
			bool flag = false;
			DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out flag);
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

		// Token: 0x0600005B RID: 91 RVA: 0x00003904 File Offset: 0x00001B04
		public static object ExecuteScalar(string commandText)
		{
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000390D File Offset: 0x00001B0D
		public static object ExecuteScalar(CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteScalar(commandType, commandText, null);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003918 File Offset: 0x00001B18
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

		// Token: 0x0600005E RID: 94 RVA: 0x0000398C File Offset: 0x00001B8C
		public static object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText)
		{
			return DbHelper.ExecuteScalar(connection, commandType, commandText, null);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003998 File Offset: 0x00001B98
		public static object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			DbCommand dbCommand = DbHelper.Factory.CreateCommand();
			bool flag = false;
			DbHelper.PrepareCommand(dbCommand, connection, null, commandType, commandText, commandParameters, out flag);
			object result = dbCommand.ExecuteScalar();
			dbCommand.Parameters.Clear();
			if (flag)
			{
				connection.Close();
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000039E9 File Offset: 0x00001BE9
		public static List<T> ExecuteList<T>(string commandText) where T : new()
		{
			return DbHelper.ExecuteList<T>(CommandType.Text, commandText, null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000039F3 File Offset: 0x00001BF3
		public static List<T> ExecuteList<T>(string commandText, params DbParameter[] dbparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(CommandType.Text, commandText, dbparams);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000039FD File Offset: 0x00001BFD
		public static List<T> ExecuteList<T>(CommandType commandType, string commandText) where T : new()
		{
			return DbHelper.ExecuteList<T>(commandType, commandText, null);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003A08 File Offset: 0x00001C08
		public static List<T> ExecuteList<T>(CommandType commandType, string commandText, params DbParameter[] dbparams) where T : new()
		{
			IDataReader dataReader = DbHelper.ExecuteReader(commandType, commandText, dbparams);
			List<T> list = new List<T>();
			while (dataReader.Read())
			{
				T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
				try
				{
					Type typeFromHandle = typeof(T);
					foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
					{
						string name = propertyInfo.Name;
						if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
						{
							object obj = dataReader[name];
							if (!(obj is DBNull) && propertyInfo.CanWrite)
							{
								propertyInfo.SetValue(t, obj, null);
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

		// Token: 0x06000064 RID: 100 RVA: 0x00003AF0 File Offset: 0x00001CF0
		public static List<T> ExecuteList<T>() where T : new()
		{
			return DbHelper.ExecuteList<T>();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static List<T> ExecuteList<T>(OrderBy orderby) where T : new()
		{
			return DbHelper.ExecuteList<T>(orderby, null);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003B01 File Offset: 0x00001D01
		public static List<T> ExecuteList<T>(params OrderByParam[] orderbys) where T : new()
		{
			return DbHelper.ExecuteList<T>(orderbys, null);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003B0A File Offset: 0x00001D0A
		public static List<T> ExecuteList<T>(params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(OrderBy.DESC, sqlparams);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003B14 File Offset: 0x00001D14
		public static List<T> ExecuteList<T>(OrderBy orderby, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text2 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text2 = text2 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			string text3 = string.Format("[{0}] {1}", text, orderby.ToString());
			string text4 = string.Format("SELECT {0} FROM [{1}{2}] WHERE {3} ORDER BY {4}", new object[]
			{
				stringBuilder,
				DbHelper.DbConfig.prefix,
				DbHelper.GetModelTable(typeFromHandle),
				text2,
				text3
			});
			return DbHelper.ExecuteList<T>(text4.ToString(), list.ToArray());
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003C80 File Offset: 0x00001E80
		public static List<T> ExecuteList<T>(OrderByParam orderby, params SqlParam[] sqlparams) where T : new()
		{
			OrderByParam[] orderbys = new OrderByParam[]
			{
				orderby
			};
			return DbHelper.ExecuteList<T>(orderbys, sqlparams);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public static List<T> ExecuteList<T>(OrderByParam[] orderbys, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			string text2 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text3 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text3 = text3 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			if (orderbys != null && orderbys.Length > 0)
			{
				for (int j = 0; j < orderbys.Length; j++)
				{
					if (text2 != "")
					{
						text2 += ",";
					}
					text2 += string.Format("[{0}] {1}", orderbys[j].ParamName, orderbys[j].OrderBy.ToString());
				}
			}
			else
			{
				text2 = string.Format("[{0}] DESC", text);
			}
			string text4 = string.Format("SELECT {0} FROM [{1}{2}] WHERE {3} ORDER BY {4}", new object[]
			{
				stringBuilder,
				DbHelper.DbConfig.prefix,
				DbHelper.GetModelTable(typeFromHandle),
				text3,
				text2
			});
			return DbHelper.ExecuteList<T>(text4.ToString(), list.ToArray());
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003E6A File Offset: 0x0000206A
		public static List<T> ExecuteList<T>(Pager pager) where T : new()
		{
			return DbHelper.ExecuteList<T>(pager, OrderBy.DESC, null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003E74 File Offset: 0x00002074
		public static List<T> ExecuteList<T>(Pager pager, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteList<T>(pager, OrderBy.DESC, sqlparams);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003E7E File Offset: 0x0000207E
		public static List<T> ExecuteList<T>(Pager pager, OrderBy orderby) where T : new()
		{
			return DbHelper.ExecuteList<T>(pager, orderby, null);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E88 File Offset: 0x00002088
		public static List<T> ExecuteList<T>(Pager pager, OrderBy orderby, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text2 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text2 = text2 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			string text3 = text;
			string text4 = string.Format("[{0}] {1}", text, orderby.ToString());
			StringBuilder stringBuilder2 = new StringBuilder();
			if (pager.pageindex == 1)
			{
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} ORDER BY {5}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					DbHelper.DbConfig.prefix,
					DbHelper.GetModelTable(typeFromHandle),
					text2,
					text4
				});
			}
			else
			{
				int num = (pager.pageindex - 1) * pager.pagesize;
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} AND [{5}] NOT IN(SELECT TOP {6} [{5}] FROM [{2}{3}] WHERE {4} ORDER BY {7}) ORDER BY {7}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					DbHelper.DbConfig.prefix,
					DbHelper.GetModelTable(typeFromHandle),
					text2,
					text3,
					num,
					text4
				});
			}
			pager.total = DbHelper.ExecuteCount<T>(text2, list.ToArray());
			return DbHelper.ExecuteList<T>(stringBuilder2.ToString(), list.ToArray());
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004099 File Offset: 0x00002299
		public static List<T> ExecuteList<T>(Pager pager, params OrderByParam[] orderbys) where T : new()
		{
			return DbHelper.ExecuteList<T>(pager, orderbys, null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000040A4 File Offset: 0x000022A4
		public static List<T> ExecuteList<T>(Pager pager, OrderByParam[] orderbys, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			string text2 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text3 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text3 = text3 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			string text4 = "";
			if (orderbys != null && orderbys.Length > 0)
			{
				for (int j = 0; j < orderbys.Length; j++)
				{
					if (text4 == "")
					{
						text4 = orderbys[j].ParamName;
					}
					if (text2 != "")
					{
						text2 += ",";
					}
					text2 += string.Format("[{0}] {1}", orderbys[j].ParamName, orderbys[j].OrderBy.ToString());
				}
			}
			else
			{
				text4 = text;
				text2 = string.Format("[{0}] DESC", text);
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			if (pager.pageindex == 1)
			{
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} ORDER BY {5}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					DbHelper.DbConfig.prefix,
					DbHelper.GetModelTable(typeFromHandle),
					text3,
					text2
				});
			}
			else
			{
				int num = (pager.pageindex - 1) * pager.pagesize;
				stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} AND [{5}] NOT IN(SELECT TOP {6} [{5}] FROM [{2}{3}] WHERE {4} ORDER BY {7}) ORDER BY {7}", new object[]
				{
					pager.pagesize,
					stringBuilder,
					DbHelper.DbConfig.prefix,
					DbHelper.GetModelTable(typeFromHandle),
					text3,
					text4,
					num,
					text2
				});
			}
			pager.total = DbHelper.ExecuteCount<T>(text3, list.ToArray());
			return DbHelper.ExecuteList<T>(stringBuilder2.ToString(), list.ToArray());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004332 File Offset: 0x00002532
		public static List<T> ExecuteTopList<T>(int tops) where T : new()
		{
			return DbHelper.ExecuteTopList<T>(tops, OrderBy.DESC, null);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000433C File Offset: 0x0000253C
		public static List<T> ExecuteTopList<T>(int tops, OrderBy orderby) where T : new()
		{
			return DbHelper.ExecuteTopList<T>(tops, orderby, null);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004346 File Offset: 0x00002546
		public static List<T> ExecuteTopList<T>(int tops, params SqlParam[] sqlparams) where T : new()
		{
			return DbHelper.ExecuteTopList<T>(tops, OrderBy.DESC, sqlparams);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004350 File Offset: 0x00002550
		public static List<T> ExecuteTopList<T>(int tops, OrderBy orderby, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text2 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text2 = text2 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			string text3 = string.Format("[{0}] {1}", text, orderby.ToString());
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} ORDER BY {5}", new object[]
			{
				tops,
				stringBuilder,
				DbHelper.DbConfig.prefix,
				DbHelper.GetModelTable(typeFromHandle),
				text2,
				text3
			});
			return DbHelper.ExecuteList<T>(stringBuilder2.ToString(), list.ToArray());
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000044CB File Offset: 0x000026CB
		public static List<T> ExecuteTopList<T>(int tops, params OrderByParam[] orderbys) where T : new()
		{
			return DbHelper.ExecuteTopList<T>(tops, orderbys, null);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000044D8 File Offset: 0x000026D8
		public static List<T> ExecuteTopList<T>(int tops, OrderByParam[] orderbys, params SqlParam[] sqlparams) where T : new()
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			string text2 = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (DbHelper.IsPrimaryKey(propertyInfo) && text == "")
					{
						text = propertyInfo.Name;
					}
					if (stringBuilder.ToString() == "")
					{
						stringBuilder.AppendFormat("[{0}]", propertyInfo.Name);
					}
					else
					{
						stringBuilder.AppendFormat(",[{0}]", propertyInfo.Name);
					}
				}
			}
			if (text == "")
			{
				text = "id";
			}
			string text3 = string.Format("[{0}]>0", text);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				text3 = text3 + " AND " + DbHelper.CreateWhereSql(list, sqlparams);
			}
			string a = "";
			if (orderbys != null && orderbys.Length > 0)
			{
				for (int j = 0; j < orderbys.Length; j++)
				{
					if (a == "")
					{
						a = orderbys[j].ParamName;
					}
					if (text2 != "")
					{
						text2 += ",";
					}
					text2 += string.Format("[{0}] {1}", orderbys[j].ParamName, orderbys[j].OrderBy.ToString());
				}
			}
			else
			{
				text2 = string.Format("[{0}] DESC", text);
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.AppendFormat("SELECT TOP {0} {1} FROM [{2}{3}] WHERE {4} ORDER BY {5}", new object[]
			{
				tops,
				stringBuilder,
				DbHelper.DbConfig.prefix,
				DbHelper.GetModelTable(typeFromHandle),
				text3,
				text2
			});
			return DbHelper.ExecuteList<T>(stringBuilder2.ToString(), list.ToArray());
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000046D4 File Offset: 0x000028D4
		public static int ExecuteInsert<T>(T model)
		{
			List<DbParameter> list = new List<DbParameter>();
			string text = DbHelper.CreateInsertSql<T>(model, list);
			int result = 0;
			if (DbHelper.DbConfig.dbtype == DbType.Access)
			{
				DbHelper.ExecuteNonQuery(out result, CommandType.Text, text.ToString(), list.ToArray());
			}
			else
			{
				text = text + ";" + DbHelper.DbProvider.GetLastIdSql();
				result = DbUtils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, text, list.ToArray()).ToString(), 0);
			}
			return result;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004748 File Offset: 0x00002948
		public static int ExecuteUpdate<T>(T model)
		{
			List<DbParameter> list = new List<DbParameter>();
			string commandText = DbHelper.CreateUpdateSql<T>(model, list);
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00004770 File Offset: 0x00002970
		public static int ExecuteUpdate<T>(params SqlParam[] sqlparams)
		{
			List<DbParameter> list = new List<DbParameter>();
			string commandText = DbHelper.CreateUpdateSql<T>(list, sqlparams);
			return DbHelper.ExecuteNonQuery(CommandType.Text, commandText, list.ToArray());
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00004798 File Offset: 0x00002998
		public static T ExecuteModel<T>(string commandText, params DbParameter[] dbparams) where T : new()
		{
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			Type typeFromHandle = typeof(T);
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, commandText, dbparams);
			if (dataReader.Read())
			{
				try
				{
					foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
					{
						string name = propertyInfo.Name;
						if (DbHelper.IsBindField(propertyInfo))
						{
							object obj = dataReader[name];
							if (!(obj is DBNull))
							{
								propertyInfo.SetValue(t, obj, null);
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

		// Token: 0x0600007B RID: 123 RVA: 0x00004858 File Offset: 0x00002A58
		public static T ExecuteModel<T>(int id) where T : new()
		{
			List<DbParameter> list = new List<DbParameter>();
			T t = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT * FROM [{0}{1}] WHERE ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<PropertyInfo> primaryKeys = DbHelper.GetPrimaryKeys(typeFromHandle);
			using (List<PropertyInfo>.Enumerator enumerator = primaryKeys.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					PropertyInfo propertyInfo = enumerator.Current;
					stringBuilder.AppendFormat("[{0}]=@{0}", propertyInfo.Name);
					DbParameter item = DbHelper.MakeInParam(string.Format("@{0}", propertyInfo.Name), id);
					list.Add(item);
				}
			}
			IDataReader dataReader = DbHelper.ExecuteReader(CommandType.Text, stringBuilder.ToString(), list.ToArray());
			if (dataReader.Read())
			{
				try
				{
					foreach (PropertyInfo propertyInfo2 in typeFromHandle.GetProperties())
					{
						string name = propertyInfo2.Name;
						if (DbHelper.IsBindField(propertyInfo2))
						{
							object obj = dataReader[name];
							if (!(obj is DBNull))
							{
								propertyInfo2.SetValue(t, obj, null);
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

		// Token: 0x0600007C RID: 124 RVA: 0x000049CC File Offset: 0x00002BCC
		public static T ExecuteModel<T>(params SqlParam[] sqlparams) where T : new()
		{
			if (default(T) == null)
			{
				Activator.CreateInstance<T>();
			}
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties())
			{
				if (DbHelper.IsBindField(propertyInfo) && !DbHelper.IsNText(propertyInfo))
				{
					if (!string.IsNullOrEmpty(text))
					{
						text += ",";
					}
					text += string.Format("[{0}]", propertyInfo.Name);
				}
			}
			stringBuilder.AppendFormat("SELECT {0} FROM [{1}{2}]", text, DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				stringBuilder.Append(" WHERE ").Append(DbHelper.CreateWhereSql(list, sqlparams));
			}
			return DbHelper.ExecuteModel<T>(stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00004ABC File Offset: 0x00002CBC
		public static int ExecuteDelete<T>(int id)
		{
			List<DbParameter> list = new List<DbParameter>();
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DELETE FROM [{0}{1}] WHERE ", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<PropertyInfo> primaryKeys = DbHelper.GetPrimaryKeys(typeFromHandle);
			using (List<PropertyInfo>.Enumerator enumerator = primaryKeys.GetEnumerator())
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

		// Token: 0x0600007E RID: 126 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static int ExecuteDelete<T>(string idlist)
		{
			Type typeFromHandle = typeof(T);
			List<SqlParam> list = new List<SqlParam>();
			List<PropertyInfo> primaryKeys = DbHelper.GetPrimaryKeys(typeFromHandle);
			foreach (PropertyInfo propertyInfo in primaryKeys)
			{
				list.Add(DbHelper.MakeAndWhere(propertyInfo.Name, WhereType.In, idlist));
			}
			return DbHelper.ExecuteDelete<T>(list.ToArray());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00004C0C File Offset: 0x00002E0C
		public static int ExecuteDelete<T>(params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DELETE FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null && sqlparams.Length > 0)
			{
				stringBuilder.Append(" WHERE ").Append(DbHelper.CreateWhereSql(list, sqlparams));
			}
			return DbHelper.ExecuteNonQuery(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004C7F File Offset: 0x00002E7F
		public static int ExecuteCount(string commandText)
		{
			return DbHelper.ExecuteCount(CommandType.Text, commandText);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004C88 File Offset: 0x00002E88
		public static int ExecuteCount(CommandType commandType, string commandText)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(commandType, commandText));
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004C96 File Offset: 0x00002E96
		public static int ExecuteCount(string commandText, params DbParameter[] commandParameters)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, commandText, commandParameters));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004CA5 File Offset: 0x00002EA5
		public static int ExecuteCount(CommandType commandType, string commandText, params DbParameter[] commandParameters)
		{
			return DbUtils.StrToInt(DbHelper.ExecuteScalar(commandType, commandText, commandParameters));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004CB4 File Offset: 0x00002EB4
		public static int ExecuteCount<T>()
		{
			Type typeFromHandle = typeof(T);
			string commandText = string.Format("SELECT COUNT(*) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			return DbHelper.ExecuteCount(commandText);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004CF0 File Offset: 0x00002EF0
		public static int ExecuteCount<T>(string where, params DbParameter[] commandParameters)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT COUNT(*) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle)), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteCount(CommandType.Text, stringBuilder.ToString(), commandParameters);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004D58 File Offset: 0x00002F58
		public static int ExecuteCount<T>(string where)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT COUNT(*) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle)), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteCount(stringBuilder.ToString());
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004DC0 File Offset: 0x00002FC0
		public static int ExecuteCount<T>(params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT COUNT(*) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle));
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhereSql(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteCount(stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004E34 File Offset: 0x00003034
		public static object ExecuteMax<T>(string colname)
		{
			Type typeFromHandle = typeof(T);
			string commandText = string.Format("SELECT MAX([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004E70 File Offset: 0x00003070
		public static object ExecuteMax<T>(string colname, string where)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat(string.Format("SELECT MAX([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname), new object[0]);
			if (!string.IsNullOrEmpty(where))
			{
				stringBuilder.AppendFormat(" WHERE {0}", where);
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString());
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004ED8 File Offset: 0x000030D8
		public static object ExecuteMax<T>(string colname, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT MAX([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhereSql(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004F4C File Offset: 0x0000314C
		public static object ExecuteSum<T>(string colname)
		{
			Type typeFromHandle = typeof(T);
			string commandText = string.Format("SELECT SUM([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			return DbHelper.ExecuteScalar(CommandType.Text, commandText);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004F88 File Offset: 0x00003188
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

		// Token: 0x0600008D RID: 141 RVA: 0x00004FF0 File Offset: 0x000031F0
		public static object ExecuteSum<T>(string colname, params SqlParam[] sqlparams)
		{
			Type typeFromHandle = typeof(T);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("SELECT SUM([{2}]) FROM [{0}{1}]", DbHelper.DbConfig.prefix, DbHelper.GetModelTable(typeFromHandle), colname);
			List<DbParameter> list = new List<DbParameter>();
			if (sqlparams != null)
			{
				string text = DbHelper.CreateWhereSql(list, sqlparams);
				if (!string.IsNullOrEmpty(text))
				{
					stringBuilder.AppendFormat(" WHERE {0}", text);
				}
			}
			return DbHelper.ExecuteScalar(CommandType.Text, stringBuilder.ToString(), list.ToArray());
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00005063 File Offset: 0x00003263
		public static string ExecuteSql(string sqlstring)
		{
			return DbHelper.DbProvider.RunSql(sqlstring);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005070 File Offset: 0x00003270
		public static void ClearDBLog()
		{
			if (DbHelper.m_dbconfig.dbtype == DbType.SqlServer)
			{
				string dbname = DbHelper.m_dbconfig.dbname;
				DbParameter dbParameter = DbHelper.MakeInParam("@DBName", (DbType)22, 50, dbname);
				string commandText = DbHelper.m_dbconfig.prefix + "shrinklog";
				DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, commandText, new DbParameter[]
				{
					dbParameter
				});
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000050D0 File Offset: 0x000032D0
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

		// Token: 0x0400000C RID: 12
		private static DbConfigInfo m_dbconfig;

		// Token: 0x0400000D RID: 13
		private static DbProviderFactory m_factory;

		// Token: 0x0400000E RID: 14
		private static IDbProvider m_dbprovider;

		// Token: 0x02000007 RID: 7
		private enum DbConnectionOwnership
		{
			// Token: 0x04000010 RID: 16
			Internal,
			// Token: 0x04000011 RID: 17
			External
		}
	}
}
