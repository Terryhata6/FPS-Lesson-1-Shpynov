using System;
using UnityEngine;

namespace Game 
{
    [Serializable]
    public struct SerializableGameObject
    {
        #region SerializableGameObject
        public string Name; //поля для сохранения
        public bool IsEnable;
        public SerializableVector3 Position;
        public SerializableQuaternion Rotation;
        public SerializableVector3 Scale;
        #endregion
        #region Methods
        public override string ToString() { return null; }
        #endregion
    }    

    [Serializable]
    public struct SerializableVector3
    {
        #region SerializableVector3
        public float X;
        public float Y;
        public float Z;             

        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static implicit operator SerializableVector3(Vector3 value)
        {
            return new SerializableVector3(value.x, value.y, value.z);
        }

        public static implicit operator Vector3(SerializableVector3 value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
        #endregion
        #region Methods
        public override string ToString() 
        {
            return $"X: {X}, Y: {Y}, Z: {Z}"; 
        }
        #endregion
    }

    [Serializable]
    public struct SerializableQuaternion
    {
        #region SerializableQuaternion
        public float X;
        public float Y;
        public float Z;
        public float W;
        
        public SerializableQuaternion(float x, float y, float z, float w) 
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static implicit operator SerializableQuaternion(Quaternion value) 
        {
            return new SerializableQuaternion(value.x, value.y, value.z, value.w);
        }

        public static implicit operator Quaternion(SerializableQuaternion value)
        {
            return new Quaternion(value.X, value.Y, value.Z, value.W);
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            return $"X: {X}, Y: {Y}, Z:{Z}; W: {W}";
        }
        #endregion
    }
}