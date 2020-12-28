using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class InforUI : View
{
    private Text txt_coinCount;
    public override string Name
    {
        get { return "InforUI"; }
    }
    public override void RegisterEvents()
    {
        //base.RegisterEvents();
        AttentionEvents.Add(GameDefine.message_UpdateCoin);
    }
    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == GameDefine.message_UpdateCoin)
        {
            UpdateCoin((int)data);
        }
    }
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        txt_coinCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_CoinCount");
      
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.InforUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
        MVC.RegisterView(this);
    }
    protected override void Start()
    {
        base.Start();
        //txt_coinCount.text = InforData.Instance.CoinCount.ToString();
        txt_coinCount.text = GetModel<InforData>().CoinCount.ToString();
    }
    public override void AddMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.SellGoods, UpdateCoin);
    }
    public override void RemoveMessageListener()
    {
        EventDispatcher.RemoveListener<int>(E_MessageType.SellGoods, UpdateCoin);
    }
    private void UpdateCoin(int coinCount)
    {
        txt_coinCount.text = coinCount.ToString();
    }
}
