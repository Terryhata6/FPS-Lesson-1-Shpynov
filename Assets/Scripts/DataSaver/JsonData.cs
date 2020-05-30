using System.IO;
using UnityEngine;

namespace Game
{
    public class JsonData<T> : IData<T>
    {
        #region IData
        public void Save(T data, string path)
        {
            var str = JsonUtility.ToJson(data);
            File.WriteAllText(path, str);
        }
        public T Load(string path)
        {
            var str = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(str);
        }
        #endregion
    }
}
