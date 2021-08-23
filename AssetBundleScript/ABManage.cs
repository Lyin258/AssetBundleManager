using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//AB包API
//单例模式
//委托
//协程
//字典

public class ABManage : SingletonAutoMono<ABManage>
{
    //AB包不能重复加载
    /// <summary>
    /// 主包
    /// </summary>
    private AssetBundle mainAB = null;
    /// <summary>
    /// 主包依赖
    /// </summary>
    private AssetBundleManifest manifest = null;

    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// AB包的路径
    /// </summary>
    private string PathURL
    {
        get { return Application.streamingAssetsPath + "/"; }
    }
    
    /// <summary>
    /// 主包名
    /// </summary>
    private string MainABName
    {
        get
        {
#if UNITY_ANDROID
            return "Android";
#elif UNITY_IOS
            return "IOS";
#else
            return "Windows";
#endif
        }
    }

    /// <summary>
    /// 加载AB包
    /// </summary>
    /// <param name="abName"></param>
    public void LoadAB(string abName)
    {
        AssetBundle ab;
        //加载AB包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(PathURL + MainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        }
        //获取依赖包信息
        string[] Strs = manifest.GetAllDependencies(abName);
        for (int i = 0; i < Strs.Length; i++)
        {
            //判断依赖包是否已加载
            if (!abDic.ContainsKey(Strs[i]))
            {
                ab = AssetBundle.LoadFromFile(PathURL + Strs[i]);
                abDic.Add(Strs[i], ab);
            }
        }

        //加载资源包
        //判断依赖包是否已加载
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(PathURL + abName);
            abDic.Add(abName, ab);
        }
    }

    //同步加载不指定类型
    public Object LoadRes(string abName,string resName)
    {
        LoadAB(abName);
        //加载资源
        Object obj = abDic[abName].LoadAsset(resName);
        //判断加载的资源是否为GameObject对象，如果是则实例化在返回
        if (obj is GameObject)
        {
            return Instantiate(obj);
        }
        else return obj;
    }
    //同步加载指定类型
    public Object LoadRes(string abName,string resName,System.Type type)
    {
        LoadAB(abName);
        //加载资源
        Object obj = abDic[abName].LoadAsset(resName,type);
        //判断加载的资源是否为GameObject对象，如果是则实例化在返回
        if (obj is GameObject)
            return Instantiate(obj);
        else 
            return obj;
    }
    //根据泛型指定类型
    public T LoadRes<T>(string abName, string resName) where T:Object
    {
        LoadAB(abName);
        //加载资源
        T obj = abDic[abName].LoadAsset<T>(resName);
        //判断加载的资源是否为GameObject对象，如果是则实例化在返回
        if (obj is GameObject)
            return Instantiate(obj);
        else
            return obj;
    }

    //单独卸载一个资源包
    public void UnLoad(string abName)
    {
        if (abDic.ContainsKey(abName))
        {
            abDic[abName].Unload(false);
            abDic.Remove(abName);
        }
    }

    //卸载所有的资源包
    public void ClearAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB = null;
        manifest = null;
    }

    //异步加载
    //加载AB包不为异步加载，从AB包中加载资源使用异步加载
    //通过资源名加载
    public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
    {
        LoadAB(abName);
        //加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        yield return abr;
        //通过委托返回给外部
        //判断加载的资源是否为GameObject对象
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //通过资源类型Type加载
    public void LoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
    {
        LoadAB(abName);
        //加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName,type);
        yield return abr;
        //通过委托返回给外部
        //判断加载的资源是否为GameObject对象
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset));
        else
            callBack(abr.asset);
    }

    //通过泛型加载
    public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callBack));
    }
    private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack)where T:Object
    {
        LoadAB(abName);
        //加载资源
        AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        yield return abr;
        //通过委托返回给外部
        //判断加载的资源是否为GameObject对象
        if (abr.asset is GameObject)
            callBack(Instantiate(abr.asset)as T);
        else
            callBack(abr.asset as T);
    }
}
