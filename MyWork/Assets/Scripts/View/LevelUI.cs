using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelUI : View
{

    private Button btn_Return;
    private Button btn_ToPackUI;
    private Button btn_MainScene;
    //关卡预制体
    private GameObject levelUp;
    private GameObject levelDown;
    private Transform content;

    //所有的关卡
    public override string Name
    {
        get { return "LevelUI"; }
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
        btn_ToPackUI = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToPackUI");
        btn_MainScene = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_MainScene");

        content = GameTool.FindTheChild(this.gameObject, "Content");

        btn_Return.onClick.AddListener(ReturnUI);
        btn_ToPackUI.onClick.AddListener(ToPackUI);
        btn_MainScene.onClick.AddListener(ToMainScene);

    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.LevelUI;
        this.uiType.showMode = E_ShowUIMode.HideOther;

    }
    protected override void Start()
    {
        InitLevels();
    }
    private void InitLevels()
    {
        levelUp = Resources.Load<GameObject>("Level/LevelUp");
        levelDown = Resources.Load<GameObject>("Level/LevelDown");
        AutoSetContentWidth(15);
        GameObject level;
        for (int i = 0; i < 15; i++)
        {
            if (i % 2 == 0)//LevelDown
            {
                level = Instantiate(levelDown);
            }
            else//LevelUp
            {
                level = Instantiate(levelUp);
            }
            LevelEntity levelEntity = GameTool.AddTheChildComponent<LevelEntity>(level, "Btn_Level");
            levelEntity.levelId = i + 1;
            GameTool.GetTheChildComponent<Text>(level, "Txt_Level").text = (i + 1).ToString();
            string sceneName = DataController.Instance.ReadCfg("SceneName", i + 1, DataController.Instance.dicLevel);
            GameTool.GetTheChildComponent<Text>(level, "Txt_LevelName").text = sceneName;
            GameTool.AddChildToParent(content, level.transform);
        }
    }
    private void AutoSetContentWidth(int levelCount)
    {
        //获取关卡预制体的宽度
        float width = content.GetComponent<GridLayoutGroup>().cellSize.x;
        //每一个关卡的间距
        float offset = content.GetComponent<GridLayoutGroup>().spacing.x;
        float allWidth = width * levelCount + offset * (levelCount - 1);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(allWidth, 0);
    }
    private void ReturnUI()
    {
        //UIManager.Instance.ShowUI(this.BeforeUiId);
        UIManager.Instance.ReturnBeforeUI(this.BeforeUiId);
    }
    private void ToPackUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PackUI);
    }
    private void ToMainScene()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Debug.Log("已在主城");
            return;
        }
        SceneController.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.InforUI);
            UIManager.Instance.ShowUI(E_UiId.MainUI, false);
        });
    }
}
