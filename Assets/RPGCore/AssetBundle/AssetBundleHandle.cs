using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RPGCore.AssetBundle
{
	public static class AssetBundleHandle
	{
		private static Dictionary<string, UnityEngine.AssetBundle> m_bundles;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		public static void Initialize()
		{
			m_bundles = new Dictionary<string, UnityEngine.AssetBundle>();

			foreach (var bundle in UnityEngine.AssetBundle.GetAllLoadedAssetBundles())
			{
				m_bundles.Add(bundle.name, bundle);
			}

			LoadBundleFromFile("icons");
		}

		public static void LoadBundleFromFile(string bundleName)
		{
			if (IsBundleLoaded(bundleName))
			{
				Debug.LogWarning($"AssetBundle : {bundleName} already loaded.");
				return;
			}

			var path = Path.Combine(Application.streamingAssetsPath, bundleName);
			var request = UnityEngine.AssetBundle.LoadFromFileAsync(path);

			request.completed += asyncOperation =>
			{
				Debug.Log($"AssetBundle : {request.assetBundle.name} loaded.");
				m_bundles.Add(request.assetBundle.name, request.assetBundle);
			};
		}

		private static bool IsBundleLoaded(string bundleName) => m_bundles.ContainsKey(bundleName);

		public static T LoadAsset<T>(string bundle, string assetName) where T : Object
		{
			if (!IsBundleLoaded(bundle))
			{
				throw new Exception($"Bundle : {bundle} is not loaded, asset cant be loaded!");
			}

			return m_bundles[bundle].LoadAsset<T>(assetName);
		}
	}
}