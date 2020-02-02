using UnityEngine;
using System;

namespace Geekbrains
{
	public class PlayerPrefsData : IData<SerializableGameObject>
	{
		public void Save(SerializableGameObject data, string path = null)
		{
			PlayerPrefs.SetString("Name", data.Name);
			PlayerPrefs.SetFloat("PosX", data.Pos.X);
			PlayerPrefs.SetString("IsEnable", data.IsEnable.ToString());

			//-----------------------------
			PlayerPrefs.Save();
		}

		public SerializableGameObject Load(string path = null)
		{
			var result = new SerializableGameObject();

			var key = "Name";
			if (PlayerPrefs.HasKey(key))
			{
				result.Name = PlayerPrefs.GetString(key);
			}

			key = "PosX";
			if (PlayerPrefs.HasKey(key))
			{
				result.Pos.X = PlayerPrefs.GetFloat(key);
			}

			key = "IsEnable";
			if (PlayerPrefs.HasKey(key))
			{
				//result.IsEnable = PlayerPrefs.GetString(key).TryBool();
			}
			return result;
		}

		public void Clear()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}