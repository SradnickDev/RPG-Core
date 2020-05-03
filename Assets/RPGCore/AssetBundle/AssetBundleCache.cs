using System.Collections.Generic;
using UnityEditor;

#if UNITY_EDITOR
namespace RPGCore.AssetBundle
{
	internal class AssetBundleCache
	{
		private Dictionary<string, IEnumerable<string>> m_bundles;

		public AssetBundleCache()
		{
			m_bundles = new Dictionary<string, IEnumerable<string>>();
			CacheAllBundles();
		}

		private void CacheAllBundles()
		{
			var bundles = GetAllAssetBundleNames();
			foreach (var bundle in bundles)
			{
				var paths = GetAssetPathsFromAssetBundle(bundle);
				m_bundles.Add(bundle, paths);
			}
		}

		public string[] GetAssetPathsFromAssetBundle(string assetBundleName)
		{
			return AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);
		}

		public string GetAssetBundleName(string assetPath)
		{
			var importer = AssetImporter.GetAtPath(assetPath);
			if (importer == null)
			{
				return string.Empty;
			}

			var bundleName = importer.assetBundleName;
			if (importer.assetBundleVariant.Length > 0)
			{
				bundleName = bundleName + "." + importer.assetBundleVariant;
			}

			return bundleName;
		}

		public string GetImplicitAssetBundleName(string assetPath)
		{
			return AssetDatabase.GetImplicitAssetBundleName(assetPath);
		}

		public string[] GetAllAssetBundleNames()
		{
			return AssetDatabase.GetAllAssetBundleNames();
		}
	}
}
#endif