using UnityEngine;

namespace Game
{
    /// <summary>
    /// Хранилище патронов оружия
    /// </summary>
    public struct Clip
    {
        public int VolumeAmmunition; // объем обоймы до перезарядки
        public int TotalAmmunition; //Всего патронов
        public int CountAmmunition;
    }
}
