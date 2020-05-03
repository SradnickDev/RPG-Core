using System;
using System.IO;
using UnityEngine;

namespace RPGCore.Database
{
	public static class DatabaseUtility
	{
		public static ConnectionInfo Load(string path)
		{
			if (!File.Exists(path))
			{
				Save(new ConnectionInfo(), path);
				throw new Exception($"Connection info not exists, created empty at path :{path}");
			}

			var file = File.ReadAllText(path);
			return JsonUtility.FromJson<ConnectionInfo>(file);
		}

		public static void Save(this ConnectionInfo info, string path)
		{
			var fileContent = JsonUtility.ToJson(info);
			File.WriteAllText(path, fileContent);
		}
	}
}