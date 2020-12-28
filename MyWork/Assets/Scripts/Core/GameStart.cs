using UnityEngine;
using System.Collections;
using GameCore;

public class GameStart : MonoBehaviour
{
    private static bool isFirstOpen = true;
    void Awake()
    {
        if (isFirstOpen)
        {
            //加载画布
            GameObject canvasPrefab = Resources.Load<GameObject>("UIPrefab/Canvas");
            GameObject canvas = Instantiate(canvasPrefab);
            //添加全局协程控制类
            GameTool.AddTheChildComponent<CoroutineController>(canvas, "UnitySingletonObj");
            //先把AudioManager挂上去,再挂UIManager,因为UIManager会调用到AudioManager里面的方法
            GameTool.AddTheChildComponent<AudioManager>(canvas, "UnitySingletonObj");
            GameTool.AddTheChildComponent<UIManager>(canvas, "UnitySingletonObj");
            //切换场景的时候,画布不被销毁
            DontDestroyOnLoad(canvas);
            isFirstOpen = false;
        }
        //加载所有配置表到内存里面
        DataController.Instance.LoadAllCfg();
        InitModel();
        InitController();

    }

    //视图初始化在每个页面启动的时候独自初始化

    //初始化模型
    private void InitModel()
    {
        MVC.RegisterModel(new InforData());
        MVC.RegisterModel(new PackData());
        MVC.RegisterModel(new PlayerData());

        InforData inforData = MVC.GetModel<InforData>();
        inforData.InitInforData();

        PackData packData = MVC.GetModel<PackData>();
        packData.InitPackData();

        PlayerData playerData = MVC.GetModel<PlayerData>();
        playerData.InitPlayerData();
    }
    //初始化控制器
    private void InitController()
    {
        MVC.RegisterController(GameDefine.com_Sell_UpdateInfor, typeof(Com_Sell_UpdateInfor));
        MVC.RegisterController(GameDefine.com_Sell_UpdatePack, typeof(Com_Sell_UpdataPack));
    }
}
