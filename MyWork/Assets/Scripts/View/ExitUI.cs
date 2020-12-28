using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class ExitUI : View
{

    private Button btn_Close;
    private Button btn_ToMainUI;
    private Button btn_ToLevelUI;
    private Button btn_ToPackUI;
    public override string Name
    {
        get { return "ExitUI"; }
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
        Init();
    }
    private void Init()
    {
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        btn_ToMainUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToMainUI");
        btn_ToLevelUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToLevelUI");
        btn_ToPackUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToPackUI");

        btn_Close.onClick.AddListener(Close);
        btn_ToMainUI.onClick.AddListener(ToMainUI);
        btn_ToLevelUI.onClick.AddListener(ToLevelUI);
        btn_ToPackUI.onClick.AddListener(ToPackUI);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ExitUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }
    private void ToMainUI()
    {
        SceneController.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
            UIManager.Instance.ShowUI(E_UiId.MainUI, false);
        });
    }
    private void ToLevelUI()
    {
        UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
    }
    private void ToPackUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PackUI, false);
    }

    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
    }
}
