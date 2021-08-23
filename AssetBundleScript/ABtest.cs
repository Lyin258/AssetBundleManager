using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABtest : MonoBehaviour
{
    void Start()
    {
        //同步
        GameObject obj = ABManage.GetInstance().LoadRes("model", "Cube", typeof(GameObject)) as GameObject;
        obj.transform.position = Vector3.down;
        GameObject obj1 = ABManage.GetInstance().LoadRes<GameObject>("model", "Cube");
        obj1.transform.position = Vector3.up;
        //异步
        ABManage.GetInstance().LoadResAsync<GameObject>("model","Cube",(obj2)=> { obj2.transform.position = Vector3.left; });
        ABManage.GetInstance().LoadResAsync<GameObject>("model", "Cube", (obj3) => { obj3.transform.position = Vector3.right; });
    }
    //    //加载材质
    //    //AssetBundle mater = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "material");
    //    //加载AB包
    //    AssetBundle assetBundle=AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
    //    //加载AB包的资源


    //    //加载主包
    //    AssetBundle abMain = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "OnWindows");
    //    //加载主包中的固定文件
    //    AssetBundleManifest abManifest = abMain.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
    //    //从固定文件中得到依赖信息
    //    string[] strs = abManifest.GetAllDependencies("model");
    //    //得到依赖包的名字
    //    for (int i = 0; i < strs.Length; i++)
    //    {
    //        AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + strs[i]);
    //    }


    //    assetBundle.LoadAsset("Cube");//使用文件名加载同名不同类型不准确
    //    GameObject obj=assetBundle.LoadAsset<GameObject>("Cube");//使用泛型加载或者typeof指定类型
    //    GameObject obj2 = assetBundle.LoadAsset("Plane", typeof(GameObject)) as GameObject;
    //    Instantiate(obj);//实例化model下的Cube
    //    Instantiate(obj2);//实例化model下的Plane

    //    assetBundle.Unload(false);//卸载当前AB包,参数为true时也卸载场景中已加载的资源


    //    //AB包不能重复加载
    //    //异步加载
    //    //StartCoroutine(LoadAB("model", "Cylinder"));

    //}

    //IEnumerator LoadAB(string ABName,string resourcesName)
    //{
    //    //加载AB包
    //    AssetBundleCreateRequest abc=AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
    //    yield return abc;
    //    //加载资源
    //    AssetBundleRequest abq=abc.assetBundle.LoadAssetAsync(resourcesName,typeof(GameObject));
    //    yield return abq;
    //    GameObject c = abq.asset as GameObject;
    //    Instantiate(c);
    //}


    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        //卸载所有加载的AB包资源，参数为true会把AB包已经加载在场景中的资源也卸载
    //        AssetBundle.UnloadAllAssetBundles(true);
    //    }
    //}
}
