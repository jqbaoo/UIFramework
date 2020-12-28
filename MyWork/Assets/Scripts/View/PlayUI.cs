using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class PlayUI : View {

    private Button btn_Exit;
    public override string Name
    {
        get { return "PlayUI"; }
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
        btn_Exit = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Exit");
      
        btn_Exit.onClick.AddListener(ShowExitUI);
      
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PlayUI;
        this.uiType.showMode = E_ShowUIMode.HideAll;
    }
    private void ShowExitUI()
    {
        // Debug.Log("显示退出界面");
        UIManager.Instance.ShowUI(E_UiId.ExitUI);
    }
  
}
