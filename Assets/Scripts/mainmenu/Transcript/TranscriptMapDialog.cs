using UnityEngine;
using System.Collections;

public class TranscriptMapDialog : MonoBehaviour {

    private UILabel desLabel;
    private UILabel energyLabel;
    private UIButton enterButton;
    private UIButton closeButton;
    private UILabel energyTagLabel;
    private TweenScale tween;
    void Awake()
    {
        desLabel = transform.Find("Sprite/DesLabel").GetComponent<UILabel>();
        energyTagLabel = transform.Find("Sprite/EnergyLabel").GetComponent<UILabel>();
        energyLabel = transform.Find("Sprite/EnergyLabel/Label").GetComponent<UILabel>();
        enterButton = transform.Find("BtnEnter").GetComponent<UIButton>();
        closeButton = transform.Find("BtnClose").GetComponent<UIButton>();
        tween = this.GetComponent<TweenScale>();

        EventDelegate ed1 = new EventDelegate(this, "OnEnter");
        enterButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed2);
    }

    public void ShowWarm()
    {
        energyLabel.enabled = false;
        energyTagLabel.enabled = false;
        enterButton.enabled = false;
        desLabel.text = "等级不够，无法进入";
        tween.PlayForward();
    }
    public void ShowDialog(BtnTranscript transcript)
    {
        energyLabel.enabled = true;
        energyTagLabel.enabled = true;
        enterButton.enabled = true;
        desLabel.text = transcript.des;
        energyLabel.text = "3";
        tween.PlayForward();
    }
    void OnEnter()
    {
        transform.parent.SendMessage("OnEnter");
    }
    void OnClose()
    {
        tween.PlayReverse();
    }
}
