using UnityEngine;
using System.Collections;

public class TranscriptMapUI : MonoBehaviour {

    public static TranscriptMapUI _instance;
    private TweenPosition tween;
    private TranscriptMapDialog dialog;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        dialog = transform.Find("TranscriptMapDialog").GetComponent<TranscriptMapDialog>();
    }
	
    public void Show()
    {
        tween.PlayForward();
    
    }
    public void Hide()
    {
        tween.PlayReverse();
    }
    public void OnButtonTranscriptClick(BtnTranscript transcript)
    {
        PlayerImfor info = PlayerImfor._instance;
        if(info.Level >= transcript.needLevel)
        {
            dialog.ShowDialog(transcript);
        }
        else
        {
            dialog.ShowWarm();
        }
    }
    public void OnEnter()
    {

    }
}
