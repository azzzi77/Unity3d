using System.IO;
using UnityEngine;

namespace Geekbrains
{
    public sealed class SaveDataRepository : BaseController, IExecute
    {
        private readonly IData<SerializableGameObject> _data;
        private CharacterController _gamer;
        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public void Execute()
        {
            if (!IsActive) return;

        }

        public void Initialization()
        {
            _gamer = ServiceLocatorMonoBehaviour.GetService<CharacterController>();
        }



        public SaveDataRepository()
		{
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				_data = new PlayerPrefsData();
			}
			else
			{
				_data = new JsonData<SerializableGameObject>();
			}
			_path = Path.Combine(Application.dataPath, _folderName);
			
		}

		public void Save()
		{
			if (!Directory.Exists(Path.Combine(_path)))
			{
				Directory.CreateDirectory(_path);
			}
			var player = new SerializableGameObject
			{
				Pos = _gamer.transform.position,
				Name = "Gamer",
				IsEnable = true
			};

			_data.Save(player, Path.Combine(_path, _fileName));
		}

		public void Load()
		{
			var file = Path.Combine(_path, _fileName);
			if (!File.Exists(file)) return;
			var newPlayer = _data.Load(file);
            _gamer.transform.position = newPlayer.Pos;
            _gamer.name = newPlayer.Name;
            _gamer.gameObject.SetActive(newPlayer.IsEnable);
			Debug.Log(newPlayer);
		}
	}
}