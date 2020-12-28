using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
namespace GameCore
{
    public class MVC
    {
        //存储MVS
        public static Dictionary<string, Model> Models = new Dictionary<string, Model>();//名字--模型
        public static Dictionary<string, View> Views = new Dictionary<string, View>();//名字--视图
        public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();//事件名--控制器

        //注册相关-------------------------------------------------------------------------
        //注册模型
        public static void RegisterModel(Model model)
        {
            if (Models.ContainsKey(model.Name))
            {
                return;
            }
            Models[model.Name] = model;
        }
        //注册模型
        public static void RegisterView(View view)
        {
            if (Views.ContainsKey(view.Name))
            {
                return;
            }
            view.RegisterEvents();
            Views[view.Name] = view;
        }
        //注册控制器
        public static void RegisterController(string eventName, Type controllerType)
        {
            if (CommandMap.ContainsKey(eventName))
            {
                return;
            }
            CommandMap[eventName] = controllerType;
        }

        //获取相关--------------------------------------------------------------------------
        //获取模型
        public static T GetModel<T>() where T : Model
        {
            foreach (Model item in Models.Values)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
        //获取视图
        public static T GetView<T>() where T : View
        {
            foreach (View item in Views.Values)
            {
                if (item is T)
                {
                    return (T)item;
                }
            }
            return null;
        }
        //发送事件
        public static void SendEvent(string eventName, object data = null)
        {
            //控制器响应事件
            if (CommandMap.ContainsKey(eventName))
            {
                Type t = CommandMap[eventName];
                //创建一个泛型参数所属类型的对象
                Controller c = Activator.CreateInstance(t) as Controller;
                //控制器执行
                c.Execute(data);
            }
            //视图响应事件
            foreach (View view in Views.Values)
            {
                if (view.AttentionEvents.Contains(eventName))
                {
                    //响应
                    view.HandleEvent(eventName, data);
                }
            }
        }
    }
}