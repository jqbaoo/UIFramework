﻿using UnityEngine;
using System.Collections;
//单例模式(多个类共用一个实例)
//不继承于MonoBehaviour
//继承于MonoBehaviour

namespace GameCore
{
    //不继承MonoBehaviour的单例模式
    public class Singleton<T> where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }
        protected Singleton()
        {

        }

    }
    //继承MonoBehaviour的单例模式
    public class UnitySingleton<T> : MonoBehaviour where T : Component
    {
        //放在场景里面的一个空物体,用来挂载项目中所有的继承于Mono的单例脚本
        private static GameObject go;
        protected static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {

                    if (go == null)
                    {
                        go = GameObject.Find("UnitySingletonObj");
                        if (go == null)
                        {
                            Debug.LogError("场景里面找不到UnitySingletonObj这个物体");
                            return null;
                        }
                    }
                    instance = go.GetComponent<T>();
                }
                return instance;
            }
        }
        protected UnitySingleton()
        {

        }
    }

}
