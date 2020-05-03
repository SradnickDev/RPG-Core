using UnityEditor;
using UnityEngine;

namespace RPGCore.AssetBundle
{
	public static class AssetBundleUtility
	{
		public static string GetBundle(string path)
		{
			//removing brackets and return the bundle name
			var start = path.IndexOf('[') + 1;
			var end = path.IndexOf(']') - 1;
			return path.Substring(start, end);
		}

		public static string GetAssetName(string path)
		{
			var end = path.IndexOf(']') + 1;
			return path.Substring(end, path.Length - end);
		}
	#if UNITY_EDITOR
		public static string CreateAssetName(Object obj)
		{
			if (obj == null) return "";
			var path = AssetDatabase.GetAssetPath(obj);
			var bundle = AssetDatabase.GetImplicitAssetBundleName(path);
			return $"[{bundle}]" + obj.name;
		}

		public static string[] GetAssetPathsFromAssetBundle(string assetBundleName)
		{
			return AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);
		}

		public static T AssetFromAssetDatabase<T>(string bundle, string assetName) where T : Object
		{
			var assetPaths = GetAssetPathsFromAssetBundle(bundle);
			foreach (var path in assetPaths)
			{
				if (path.Contains(assetName))
				{
					return AssetDatabase.LoadAssetAtPath<T>(path);
				}
			}

			Debug.LogWarning($"Asset <b>{assetName}</b> from Bundle <b>{bundle}</b> could not be found");
			return null;
		}
	#endif
	}
}