﻿using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class PlayerUI : View
{
    private Button btn_Return;
    //角色属性相关
    private Text txt_Name;
    private Text txt_GradeNum;
    private Text txt_MaxMpNum;
    private Text txt_MoveSpeedNum;
    private Text txt_AttackNum;
    private Text txt_MaxHpNum;
    private Text txt_AttackDistanceNum;
    //生成在场景里的角色
    private GameObject player;
    public override string Name
    {
        get { return "PlayerUI"; }
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
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Return.onClick.AddListener(ReturnUI);
        txt_Name = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Name");
        txt_GradeNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_GradeNum");
        txt_MaxMpNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_MaxMpNum");
        txt_MoveSpeedNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_MoveSpeedNum");
        txt_AttackNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_AttackNum");
        txt_MaxHpNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_MaxHpNum");
        txt_AttackDistanceNum = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_AttackDistanceNum");
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PlayerUI;
    }
    protected override void Start()
    {
        base.Start();
        InitPlayerInfor();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        if (player == null)
        {
            player = Instantiate(Resources.Load<GameObject>("Other/PlayerShow"));
            DontDestroyOnLoad(player);
        }
        player.SetActive(true);
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        player.SetActive(false);
    }
    private void InitPlayerInfor()
    {
        //获取角色等级
        int grade = GetModel<PlayerData>().Grade;
        txt_GradeNum.text = grade.ToString();
        txt_MaxMpNum.text = DataController.Instance.ReadCfg("MaxMp", grade, DataController.Instance.dicPlayer);
        txt_MoveSpeedNum.text = DataController.Instance.ReadCfg("MoveSpeed", grade, DataController.Instance.dicPlayer);
        txt_AttackNum.text = DataController.Instance.ReadCfg("Attack", grade, DataController.Instance.dicPlayer);
        txt_MaxHpNum.text = DataController.Instance.ReadCfg("MaxBlood", grade, DataController.Instance.dicPlayer);
        txt_AttackDistanceNum.text = DataController.Instance.ReadCfg("AttackDistance", grade, DataController.Instance.dicPlayer);


    }
    private void ReturnUI()
    {
        UIManager.Instance.ReturnBeforeUI(this.BeforeUiId);
    }

}
