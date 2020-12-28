using UnityEngine;
using System.Collections;
using GameCore;

//模型基类
public abstract class Model 
{
    //模型名称
    public abstract string Name { get; }
    //发送消息给视图层，通知视图层更新
    protected void SendEvent(string eventName, object data = null)
    {
        //Debug.Log("发送消息给视图层，通知视图层更新");/////
        MVC.SendEvent(eventName, data);
    }
}
