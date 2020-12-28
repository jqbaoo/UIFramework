using UnityEngine;
using System.Collections;
using System;
namespace GameCore
{
    public abstract class Controller
    {
        //获取模型
        protected T GetModel<T>() where T : Model
        {
            return MVC.GetModel<T>();
        }
        //获取视图
        protected T GetView<T>() where T : View
        {
            return MVC.GetView<T>();
        }
        //注册模型
        protected void RegisterModel(Model model)
        {
            MVC.RegisterModel(model);
        }
        //注册视图
        protected void RegisterView(View view)
        {
            MVC.RegisterView(view);
        }
        //注册控制器（自身）
        protected void RegisterController(string eventName, Type controller)
        {
            MVC.RegisterController(eventName, controller);
        }
        //处理系统消息
        public abstract void Execute(object data);
    }
}