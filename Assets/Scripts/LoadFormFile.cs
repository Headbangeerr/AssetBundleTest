using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadFormFile : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start ()
	{
	    string path = "AssetBundles/cubewall.unity3d";
        //AssetBundle ab=AssetBundle.LoadFromFile(path);

        //第一种加载AB的方式：LoadFromMemoryAsync（异步获取）
     //   AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
     //   //上述代码通过File类根据所给路径按比特流的形式读入文件，AssetBundle再通过异步加载的方式将内存中的字节流读入
	    //yield return request;
	    //AssetBundle ab = request.assetBundle;//通过AssetBundleCreateRequest对象的assetBundle属性就可以获得已经加载完成的AB对象


        //第二种加载方式：通过WWW对象
	    //while (!Caching.ready)//等待缓存可用再继续执行
	    //{
	    //    yield return null;
	    //}
     //   Debug.Log("caching is ready!");
     //   /************注意：该方法已经被弃用，被UnityWebRequest类替换**********************/
     //   //这里通过本地路径获取资源，第二个int类型参数为版本号，这里的第一个字符串参数需要在前面加上file前缀标识这个路径是一个本地路径
     //   //WWW www=WWW.LoadFromCacheOrDownload(@"file:///F:\GameDesigne\U3D_Project\AssetBundleProject\AssetBundles"+path,1);
	    //WWW www = WWW.LoadFromCacheOrDownload(@"http://localhost/AssetBundles/cubewall.unity3d", 1);
     //   yield return www;
     //   //当返回的错误信息返回空则代表成功获取，否则获取失败
	    //if (!string.IsNullOrEmpty(www.error))
	    //{
	    //    Debug.Log(www.error);
     //       yield break;	    
	    //}
     //   Debug.Log("success");
	    //AssetBundle ab = www.assetBundle;

        //第三种加载方式：UnityWebRequest
	    string uri = @"file:///F:\GameDesigne\U3D_Project\AssetBundleProject\AssetBundles\cubewall.unity3d";
        UnityWebRequest request=UnityWebRequest.GetAssetBundle(uri);
	    yield return request.Send();//需要使用send方法才能发送请求
	    //AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
	    AssetBundle ab = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;//直接通过request对象来获取assetbundle
	    GameObject cubeWall = ab.LoadAsset<GameObject>("CubeWall");
	    Instantiate(cubeWall);
        
        //通过AssetBundleManifest来获取指定物体的依赖元素
        AssetBundle manifestAB= AssetBundle.LoadFromFile("AssetBundles/AssetBundles");
	    AssetBundleManifest manifest = manifestAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

	    string[] dependencies = manifest.GetAllDependencies("cubewall.unity3d");
	    foreach (var name in dependencies)
	    {
            Debug.Log("name:" + name);
	        AssetBundle.LoadFromFile("AssetBundles/" + name);

	    }
	}
}
