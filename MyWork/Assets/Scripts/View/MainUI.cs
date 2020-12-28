﻿using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class MainUI : View
{
    //滚动公告相关
    //滚动速度
    private float speed = 60;
    //用于显示滚动字幕的Text
    private Text txt_Notice;
    //滚动字幕的初始位置
    private Vector3 startPs;
    //滚动字幕移动结束的位置
    private Vector3 finishPs;
    //滚动字幕的父物体Content(显示框)
    private RectTransform content;
    //字幕移动的距离
    private float moveDistance = 0;
    

    private Button btn_ToLevelUI;
    private Button btn_ToPackUI;
    private Button btn_Notice;
    private Button btn_ToPlayer;
    private Button btn_Audio;
    //private GameObject btn_ToLevelUI;
    public override string Name
    {
        get { return "MainUI"; }
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
        btn_ToLevelUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToLevelUI");
        btn_ToLevelUI.onClick.AddListener(ToLevelUI);
        btn_ToPackUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToPackUI");
        btn_ToPackUI.onClick.AddListener(ToPackUI);
        btn_Notice = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Notice");
        btn_Notice.onClick.AddListener(ShowNoticeUI);
        btn_Audio = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Audio");
        btn_Audio.onClick.AddListener(ShowAudioUI);
        btn_ToPlayer = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToPlayer");
        btn_ToPlayer.onClick.AddListener(ToPlayerUI);

        txt_Notice = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Notice");
        content = GameTool.GetTheChildComponent<RectTransform>(this.gameObject, "Content");
        startPs = txt_Notice.transform.localPosition;
        //移动的距离=字体的宽度+显示框的宽度
        moveDistance = txt_Notice.rectTransform.sizeDelta.x+ content.sizeDelta.x;
        finishPs = new Vector3(startPs.x- moveDistance, startPs.y, startPs.z);

    }
    void Update()
    {
        if (txt_Notice.transform.localPosition.x < finishPs.x)
        {
            txt_Notice.transform.localPosition = startPs;
        }
        else
        {
            txt_Notice.transform.localPosition = new Vector3(txt_Notice.transform.localPosition.x -Time.deltaTime*speed, startPs.y, startPs.z);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        EventDispatcher.TriggerEvent(E_MessageType.CameraMove, true);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventDispatcher.TriggerEvent(E_MessageType.CameraMove, false);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.MainUI;
       
    }
    private void ToLevelUI()
    {
        // GameDebuger.Log("显示关卡窗体");
        UIManager.Instance.ShowUI(E_UiId.LevelUI);
    }
    private void ToPackUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PackUI);
    }
    private void ShowNoticeUI()
    {
        UIManager.Instance.ShowUI(E_UiId.NoticeUI);
    }
    private void ToPlayerUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PlayerUI);
    }
    private void ShowAudioUI()
    {
        UIManager.Instance.ShowUI(E_UiId.AudioUI);
    }
}