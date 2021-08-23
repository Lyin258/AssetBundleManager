using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例自动添加类
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance==null)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();//生成的对象名为他的类型名
            instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
        }
        return instance;
    }
}
