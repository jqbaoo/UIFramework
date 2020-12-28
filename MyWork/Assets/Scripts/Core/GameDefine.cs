﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

//消息类型
public enum E_MessageType
{
    SellGoods,
    GoodsBeClick,
    CameraMove
}
//物品的分类
public enum E_GoodsType
{
    Default,//全部
    Equipment,//装备
    Potions,//药水
    Rune,//符文
    Material//材料
}
//窗体的显示方式
public enum E_ShowUIMode
{
    //界面显示出来的时候,不需要去隐藏其他窗体(InforUI)
    DoNothing,
    //界面显示出来的时候,需要去隐藏其他窗体(但是不隐藏保持在最前方的窗体)
    HideOther,
    //界面显示出来的时候,需要去隐藏所有的窗体
    HideAll
}
//窗体的层级类型(父节点的类型)
public enum E_UIRootType
{
    KeepAbove,//保持在前方的窗体(DoNothing)
    Normal//普通窗体(1、HideOther 2、HideAll)
}
//窗体的ID
public enum E_UiId
{
    NullUI,
    MainUI,
    InforUI,
    LevelUI,
    PackUI,
    NoticeUI,
    PlayUI,
    PlayerUI,
    ExitUI,
    LoadingUI,
    AudioUI,
}
public class GameDefine
{
    //定义消息
    public const string message_UpdateCoin = "Message_UpdateCoin";
    public const string message_UpdatePack = "Message_UpdatePack";
    //定义命令
    public const string com_Sell_UpdateInfor = "Com_Sell_UpdateInfor";
    public const string com_Sell_UpdatePack = "Com_Sell_UpdatePack";

    public static Dictionary<E_UiId, string> dicPath = new Dictionary<E_UiId, string>()
    {
        { E_UiId.MainUI,"UIPrefab/"+"MainUI"},
        { E_UiId.InforUI,"UIPrefab/"+"InforUI"},
        { E_UiId.LevelUI,"UIPrefab/"+"LevelUI"},
        { E_UiId.PackUI,"UIPrefab/"+"PackUI"},
        { E_UiId.NoticeUI,"UIPrefab/"+"NoticeUI"},
        { E_UiId.PlayUI,"UIPrefab/"+"PlayUI"},
        { E_UiId.PlayerUI,"UIPrefab/"+"PlayerUI"},
        { E_UiId.ExitUI,"UIPrefab/"+"ExitUI"},
        { E_UiId.LoadingUI,"UIPrefab/"+"LoadingUI"},
        { E_UiId.AudioUI,"UIPrefab/"+"AudioUI"}
    };
    public static Type GetUIScriptType(E_UiId uiId)
    {
        Type scriptType = null;
        switch (uiId)
        {
            case E_UiId.NullUI:
                Debug.Log("自动添加脚本的时候,传入的窗体id为NullUI");
                break;
            case E_UiId.MainUI:
                scriptType = typeof(MainUI);
                break;
            case E_UiId.InforUI:
                scriptType = typeof(InforUI);
                break;
            case E_UiId.LevelUI:
                scriptType = typeof(LevelUI);
                break;
            case E_UiId.PackUI:
                scriptType = typeof(PackUI);
                break;
            case E_UiId.NoticeUI:
                scriptType = typeof(NoticeUI);
                break;
            case E_UiId.PlayUI:
                scriptType = typeof(PlayUI);
                break;
            case E_UiId.PlayerUI:
                scriptType = typeof(PlayerUI);
                break;
            case E_UiId.ExitUI:
                scriptType = typeof(ExitUI);
                break;
            case E_UiId.LoadingUI:
                scriptType = typeof(LoadingUI);
                break;
            case E_UiId.AudioUI:
                scriptType = typeof(AudioUI);
                break;
            default:
                break;
        }
        return scriptType;
    }
}