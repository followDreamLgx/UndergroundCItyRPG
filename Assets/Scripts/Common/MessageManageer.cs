using UnityEngine;
using System.Collections;

public class MessageManageer : MonoBehaviour {

    public static MessageManageer _instance;

    private UILabel messageLabel;

    private TweenAlpha tween;

    private bool isSetActive = true;

    void Awake()
    {
        _instance = this;
        messageLabel = transform.Find("Sprite/Label").GetComponent<UILabel>();
        tween = this.GetComponent<TweenAlpha>();

        EventDelegate ed = new EventDelegate(this, "OnTweenFinish");
        tween.onFinished.Add(ed);

        gameObject.SetActive(false);
    }

    //因为是动画结束了才播放，所以能够保证动画播放完毕
    public void OnTweenFinish()
    {
        //动画完成后会调用这个函数
        //当隐退完成时SetActive为false
        if(isSetActive == false)
        {
            gameObject.SetActive(false);
        }
    }
    public void ShowMessage(string message, float time = 1.0f)
    {
        gameObject.SetActive(true);
        StartCoroutine(Show(message, time));
    }

    IEnumerator Show(string message, float time)
    {
        isSetActive = true;
        
        tween.PlayForward();
        messageLabel.text = message;
        yield return new WaitForSeconds(time);
        isSetActive = false;
        tween.PlayReverse();
    }
    
}
