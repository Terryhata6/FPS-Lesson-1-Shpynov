using System.IO;
using UnityEngine;

namespace Game
{
    public sealed class SaveDataRepository
    {
        #region SaveDataRepository
        private readonly IData<SerializableGameObject> _data;
        private const string _folderName = "DataSave";
        private const string _fileName = "dataSave.Bat";
        private readonly string _path = null;
        
        public SaveDataRepository() 
        {
            _path = Path.Combine(Application.dataPath, _folderName);
            _data = new JsonData<SerializableGameObject>();
        }
        #endregion
        #region Methods
        public void Save()
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }
            Transform temp = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
            var player = new SerializableGameObject
            {
                Position = temp.position,
                Rotation = temp.rotation,
                Name = "Save1",
                IsEnable = true
            };

            _data.Save(player, Path.Combine(_path, _fileName));
        }
        public void Load() 
        {
            
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;
            var temp = _data.Load(file);
            ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform.position = temp.Position;
            ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform.rotation = temp.Rotation;            
        }
        #endregion
    }
}

