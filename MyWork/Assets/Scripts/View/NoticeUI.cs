using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class NoticeUI : View
{
    private Button btn_Close;
    //用于显示公告内容的Text
    private Text txt_Details;
    public override string Name
    {
        get { return "NoticeUI"; }
    }
    public override void RegisterEvents()
    {
        base.RegisterEvents();
    }
    public override void HandleEvent(string eventName, object data)
    {
        throw new System.NotImplementedException();
    }
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        txt_Details = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Details");
        btn_Close.onClick.AddListener(Close);
        //从配置表读取公告内容
        txt_Details.text = DataController.Instance.ReadCfg("Details",1,DataController.Instance.dicNotice);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.NoticeUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
    }
}
