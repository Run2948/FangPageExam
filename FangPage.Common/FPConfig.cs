using System;
using System.Configuration;

namespace FangPage.Common
{
	// Token: 0x0200000A RID: 10
	public class FPConfig
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003484 File Offset: 0x00001684
		public static string GetAppSettingsKeyValue(string keyName)
		{
			string text = ConfigurationManager.AppSettings.Get(keyName);
			bool flag = !string.IsNullOrEmpty(text);
			string result;
			if (flag)
			{
				result = text;
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000034BC File Offset: 0x000016BC
		private static bool AppSettingsKeyExists(string strKey, Configuration config)
		{
			foreach (string a in config.AppSettings.Settings.AllKeys)
			{
				bool flag = a == strKey;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003508 File Offset: 0x00001708
		public static void AppSettingsSave(string strKey, string newValue)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			bool flag = FPConfig.AppSettingsKeyExists(strKey, configuration);
			if (flag)
			{
				configuration.AppSettings.Settings[strKey].Value = newValue;
				configuration.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
			}
			else
			{
				configuration.AppSettings.Settings.Add(strKey, newValue);
				configuration.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003580 File Offset: 0x00001780
		public static string GetConnectionStringsElementValue(string ConnectionStringsName)
		{
			ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[ConnectionStringsName];
			return connectionStringSettings.ConnectionString;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000035A4 File Offset: 0x000017A4
		public static void ConnectionStringsSave(string ConnectionStringsName, string elementValue)
		{
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			configuration.ConnectionStrings.ConnectionStrings[ConnectionStringsName].ConnectionString = elementValue;
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(ConnectionStringsName);
		}
	}
}
