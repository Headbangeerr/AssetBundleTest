using System.IO;
using UnityEditor;

public class AssetBundles {
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundle()
    {
        string dir = "AssetBundles";
        //使用IO库中的Directory对象判断目录是否存在，如果不存在，则在项目跟目录中创建指定目录
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }
        //创建AssetBundle包，参数1表示包的输出目录，参数2表示创建选项，这里选择None，参数3表示目标平台环境
        BuildPipeline.BuildAssetBundles("AssetBundles", BuildAssetBundleOptions.None, 
            BuildTarget.StandaloneWindows64);

    }
}
