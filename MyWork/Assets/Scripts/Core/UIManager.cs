using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GameCore
{
    //UI管理类,用于管理所有的UI界面
    public class UIManager : UnitySingleton<UIManager>
    {
        //缓存所有打开过的窗体(已经加载过的窗体)
        private Dictionary<E_UiId, View> dicAllUI;
        //缓存正在显示的窗体
        private Dictionary<E_UiId, View> dicShowUI;

        //缓存刚显示出来的当前窗体
        private View currentUI = null;
        //缓存上一个窗体
        //private View beforeUI = null;
        private E_UiId beforeUiId = E_UiId.NullUI;

        //缓存画布
        private Transform canvas;
        //缓存画布下面的两个窗体父节点
        private Transform keepAboveUIRoot;
        private Transform normalUIRoot;

        private void Awake()
        {

            if (dicAllUI == null)
            {
                dicAllUI = new Dictionary<E_UiId, View>();
            }
            if (dicShowUI == null)
            {
                dicShowUI = new Dictionary<E_UiId, View>();
            }
            InitUIManager();
        }
        //初始化
        private void InitUIManager()
        {
            canvas = this.transform.parent;
            //切换场景德 时候,画布不被销毁
            DontDestroyOnLoad(canvas);
            if (keepAboveUIRoot == null)
            {
                keepAboveUIRoot = GameTool.FindTheChild(canvas.gameObject, "KeepAboveUIRoot");
            }
            if (normalUIRoot == null)
            {
                normalUIRoot = GameTool.FindTheChild(canvas.gameObject, "NormalUIRoot");
            }
            ShowUI(E_UiId.InforUI);
            ShowUI(E_UiId.MainUI);
        }
        //预加载
        private void PrefabLoadUI()
        {
            //禁用音效
            AudioManager.Instance.SetEffectSourceEnable(false);
            ShowUI(E_UiId.PackUI, false);
            //开启音效
            AudioManager.Instance.SetEffectSourceEnable(true);
        }
        //供外界调用,显示窗体的方法(参数:所要显示的窗体,是否需要存储上一个窗体的Id)
        public void ShowUI(E_UiId uiId, bool isNeedSaveBeforeUiId = true)
        {
            OnShowUI(uiId, isNeedSaveBeforeUiId);
        }
        //供外界调用,反向切换窗体的方法(界面返回按钮)
        //参数为所要切换回去的窗体ID
        public void ReturnBeforeUI(E_UiId uiId)
        {
            OnShowUI(uiId, false);
        }
        //供外界调用,隐藏指定单个窗体的方法
        public void HideSingleUI(E_UiId uiId)
        {
            if (dicShowUI.ContainsKey(uiId))
            {
                dicShowUI[uiId].HideUI();
                dicShowUI.Remove(uiId);
            }
        }
        private void OnShowUI(E_UiId uiId, bool isNeedSaveBeforeUiId)
        {
            if (uiId == E_UiId.NullUI)
            {
                //uiId = E_UiId.MainUI;
                Debug.LogError("显示窗体的时候传入了NullUI");
                return;
            }
            //判断
            View View = JudgeShowUI(uiId);
            View.ShowUI();
            if (isNeedSaveBeforeUiId)
            {
                View.BeforeUiId = beforeUiId;
            }
        }
        private View JudgeShowUI(E_UiId uiId)
        {
            //1、将要显示的窗体是否正在显示,如果正在显示就不用处理其他逻辑了
            if (dicShowUI.ContainsKey(uiId))
            {
                return null;
            }
            //2、判断将要显示的窗体是否加载过
            View View = GetView(uiId);
            if (View == null)//说明窗体没有被加载过
            {
                string path = GameDefine.dicPath[uiId];
                GameObject theUI = Resources.Load<GameObject>(path);
                if (theUI != null)//有加载到了
                {
                    //把窗体生成出来
                    GameObject willShowUI = Instantiate(theUI);
                    //防止加载出来的预制体处于隐藏状态
                    if (!willShowUI.activeInHierarchy)
                    {
                        willShowUI.SetActive(true);
                    }
                    View = willShowUI.GetComponent<View>();
                    //View==null说明窗体上没有挂载UI脚本
                    if (View == null)
                    {
                        //自动添加对应的UI脚本
                        Type type = GameDefine.GetUIScriptType(uiId);
                        //为窗体自动添加脚本
                        View = willShowUI.AddComponent(type) as View;
                    }
                    //获取该窗体对应的父节点
                    Transform uiRoot = GetTheUIRoot(View);
                    GameTool.AddChildToParent(uiRoot, willShowUI.transform);
                    willShowUI.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                    willShowUI.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
                    //窗体第一次被加载,要缓存到dicAllUI这个字典里面
                    dicAllUI.Add(uiId, View);
                }
                else
                {
                    Debug.LogError("在路径" + path + "下面加载不到窗体,请查看该路径下面是否有窗体" + uiId + "的预制体");
                }

            }
            UpdateDicShowUIAndHideUI(View);
            return View;
        }
        //更新字典并且隐藏对应的窗体
        private void UpdateDicShowUIAndHideUI(View View)
        {
            if (View.isNeedDealWithUI)//说明这是一个普通窗体
            {
                if (dicShowUI.Count > 0)
                {

                    if (View.uiType.showMode == E_ShowUIMode.HideAll)
                    {
                        //隐藏所有的窗体,包括最前方窗体
                        HideAllUI(true, View);
                    }
                    else// E_ShowUIMode.HideOther+ E_ShowUIMode.DoNothing
                    {
                        if (View.uiType.showMode == E_ShowUIMode.HideOther)
                        {
                            //隐藏所有的窗体,不包括保持在最前方的窗体
                            HideAllUI(false, View);
                        }

                    }
                }
            }
            dicShowUI.Add(View.GetUiId, View);
        }
        public void HideAllUI(bool isHideAboveUI, View View)
        {
            if (isHideAboveUI)//隐藏所有的窗体
            {
                //uiItem就是正在显示的窗体,需要隐藏掉
                foreach (View uiItem in dicShowUI.Values)
                {
                    uiItem.HideUI();
                }
                dicShowUI.Clear();
            }
            else  //隐藏所有的窗体,但是不隐藏最前方的窗体
            {
                //存储将要隐藏的窗体
                List<E_UiId> listRemove = new List<E_UiId>();
                foreach (View uiItem in dicShowUI.Values)
                {
                    if (uiItem.uiType.uiRootType == E_UIRootType.KeepAbove)//这是保持在最前方的窗体
                    {
                        continue;
                    }
                    else//普通窗体,需要隐藏掉
                    {
                        uiItem.HideUI();
                        listRemove.Add(uiItem.GetUiId);
                    }
                }
                for (int i = 0; i < listRemove.Count; i++)
                {
                    dicShowUI.Remove(listRemove[i]);
                    //存储上一个跳转过来的窗体Id
                    if (i == listRemove.Count - 1)
                    {
                        beforeUiId = listRemove[i];
                        // GameDebuger.Log("上一个窗体的Id"+ beforeUiId);
                    }
                }
            }
        }
        //在dicAllUI里面获取窗体
        private View GetView(E_UiId uiId)
        {
            if (dicAllUI.ContainsKey(uiId))
            {
                return dicAllUI[uiId];
            }
            else
            {
                return null;
            }
        }
        //判断窗体父节点类型(UIRoot的类型)
        private Transform GetTheUIRoot(View View)
        {
            if (View.uiType.uiRootType == E_UIRootType.KeepAbove)
            {
                return keepAboveUIRoot;
            }
            else
            {
                return normalUIRoot;
            }
        }
    }

}
