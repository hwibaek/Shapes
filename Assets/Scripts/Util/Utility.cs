using System;
using UnityEngine;

namespace Util
{
    public static class ComponentUtil
    {
        public static T GetOrAddComponent<T>(this GameObject obj) where T : Component => obj.GetComponent<T>() ?? obj.AddComponent<T>();
    }

    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get => _instance;
            protected set
            {
                if (_instance == null) _instance = value;
                else if (_instance != value) Destroy(value.gameObject);
            }
        }

        public abstract void Awake();
    }
}