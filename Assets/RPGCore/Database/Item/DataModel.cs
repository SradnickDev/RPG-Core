using System;
using RPGCore.AssetBundle;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RPGCore.Database.Item
{
	public abstract class DataModel<T> where T : Object
	{
		public string Reference;

		public T Data
		{
			get
			{
				if (m_object == null)
				{
					m_object = Load();
				}

				return m_object;
			}
			set => m_object = value;
		}

		private T m_object;

		public DataModel() { }

		public DataModel(string reference)
		{
			Reference = reference;
		}

		public DataModel(T data)
		{
			m_object = data;
		}

		public virtual T Load()
		{
			if (string.IsNullOrEmpty(Reference))
			{
				return null;
			}

			var assetName = AssetBundleUtility.GetAssetName(Reference);
			var bundle = AssetBundleUtility.GetBundle(Reference);

			if (string.IsNullOrEmpty(assetName) || string.IsNullOrEmpty(bundle))
			{
				throw new Exception("No valid asset name or bundle name!");
			}

		#if UNITY_EDITOR

			//while not in play mode, means using the item editor
			//load the asset from AssetDatabase
			if (Application.isEditor && !Application.isPlaying)
			{
				return AssetBundleUtility.AssetFromAssetDatabase<T>(bundle, assetName);
			}
		#endif

			return AssetBundleHandle.LoadAsset<T>(bundle, assetName);
		}

		public virtual string Save()
		{
			//only execute in editor mode and while application is not playing
		#if UNITY_EDITOR
			if (Application.isEditor && !Application.isPlaying)
			{
				var name = AssetBundleUtility.CreateAssetName(m_object);
				return name;
			}

			throw new Exception("Cant Serialize Assets at runtime. Leave Play Mode!");
		#endif
		}
	}
}