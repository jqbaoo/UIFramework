using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class AudioUI : View
{
    private Button btn_Close;
    private Slider slider_Value;
    private Toggle toggle_Mute;
    protected override void Awake()
    {
        base.Awake();
    }
    public override string Name
    {
        get { return "AudioUI"; }
    }
    public override void HandleEvent(string eventName, object data)
    {
        throw new System.NotImplementedException();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.AudioUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
        MVC.RegisterView(this);
    }
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        slider_Value = GameTool.GetTheChildComponent<Slider>(this.gameObject, "Slider_Value");
        slider_Value.value = AudioManager.Instance.audioValue;
        slider_Value.onValueChanged.AddListener(SetVolumeValue);
        toggle_Mute = GameTool.GetTheChildComponent<Toggle>(this.gameObject, "Toggle_Mute");
        toggle_Mute.isOn = !AudioManager.Instance.isOpenAudio;
        toggle_Mute.onValueChanged.AddListener(OpenOrCloseAudio);
        btn_Close.onClick.AddListener(Close);
    }
    private void SetVolumeValue(float value)
    {
        AudioManager.Instance.SetVolumeValue(value);
    }
    private void OpenOrCloseAudio(bool isOn)
    {
        AudioManager.Instance.OpenOrCloseAudio(isOn);
    }
    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);
    }
}
