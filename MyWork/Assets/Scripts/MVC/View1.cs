using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameCore
{
    //窗体类型(显示类型与父节点类型)
    public class UIType
    {
        //如何确定默认值的类型?用到的比较多的就设置为默认值
        public E_ShowUIMode showMode = E_ShowUIMode.HideOther;
        public E_UIRootType uiRootType = E_UIRootType.Normal;
    }
    //视图基类
    public abstract class View : MonoBehaviour, IMessageListener
    {
        //窗体类型
        public UIType uiType;
        //窗体的位置信息
        protected RectTransform thisTrans;
        //当前窗体的ID
        protected E_UiId uiId = E_UiId.NullUI;
        //上一个跳转过来的窗体ID
        private E_UiId beforeUiId = E_UiId.NullUI;

        #region MVC部分
        //视图名称
        public abstract string Name { get; }
        //需要关心事件的列表
        [HideInInspector]
        public List<string> AttentionEvents = new List<string>();

        //注册需要关心的事件
        public virtual void RegisterEvents()
        {

        }
        //事件处理函数
        public abstract void HandleEvent(string eventName, object data);
        //获取模型
        protected T GetModel<T>() where T : Model
        {
            //return null;/////
            return MVC.GetModel<T>();
        }
        //发送消息给控制器层
        protected void SendEvent(string eventName, object data = null)
        {
            MVC.SendEvent(eventName, data);
        }
        #endregion

        #region 公有属性、方法，对外开放使用
        //获取当前窗体的ID
        public E_UiId GetUiId
        {
            get
            {
                return uiId;
            }
        }
        //上一个窗体Id的属性(可读可写)
        public E_UiId BeforeUiId
        {
            get
            {
                return beforeUiId;
            }

            set
            {
                beforeUiId = value;
            }
        }
        //对外提供一个属性,判断显示出来后是否需要处理其他窗体(是否需要隐藏其他窗体)
        public bool isNeedDealWithUI
        {
            get
            {
                if (this.uiType.uiRootType == E_UIRootType.KeepAbove)
                {
                    return false;
                }
                return true;

            }
        }

        //显示窗体
        public virtual void ShowUI()
        {
            this.gameObject.SetActive(true);

        }
        //隐藏窗体
        public virtual void HideUI(Action del = null)
        {
            this.gameObject.SetActive(false);
            if (del != null)
            {
                del();
            }
            //保存数据
            Save();
        }
        #endregion

        #region 接口
        public virtual void AddMessageListener()
        {

        }

        public virtual void RemoveMessageListener()
        {

        }
        #endregion

        #region 生命周期函数
        protected virtual void Awake()
        {
            if (uiType == null)
            {
                uiType = new UIType();
            }
            thisTrans = this.GetComponent<RectTransform>();
            InitUiOnAwake();
            InitDataOnAwake();
        }
        protected virtual void Start()
        {

        }
        protected virtual void OnEnable()
        {
            PlayAudio();
        }
        protected virtual void OnDisable()
        {

        }
        protected virtual void OnDestroy()
        {
            RemoveMessageListener();
        }
        #endregion

        //初始化界面元素(查找界面元素,获取监听等逻辑就放在这个方法里面)
        protected virtual void InitUiOnAwake()
        {

        }
        //初始化界面数据(窗体的ID、窗体类型等)
        protected virtual void InitDataOnAwake()
        {
            AddMessageListener();
        }
        //保存数据
        protected virtual void Save()
        {

        }
        //窗体显示出来的时候播放音效
        protected virtual void PlayAudio()
        {
            //调用播放音效的方法
            AudioManager.Instance.PlayEffectMusic("UIShow");
        }
    }
}