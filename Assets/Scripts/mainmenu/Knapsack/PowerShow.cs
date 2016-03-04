using UnityEngine;
using System.Collections;

public class PowerShow : MonoBehaviour {

    private float startValue = 0;
    private int endValue = 0;
    private bool isStart = false;

    private int speed = 10;
    private bool isUp = true;
    private bool isSetActive = false;

    private UILabel numLabel;
    private TweenAlpha tween;

    void Awake()
    {
        numLabel = transform.Find("Label").GetComponent<UILabel>();
        tween = this.GetComponent<TweenAlpha>();
        EventDelegate ed = new EventDelegate(this, "OnTweenFinish");
        tween.onFinished.Add(ed);

        gameObject.SetActive(false);
    }
    void Update()
    {
        if(isStart)
        {
            if (isUp)
            {
                startValue += speed * Time.deltaTime;
                if(startValue >= endValue)
                {
                    startValue = endValue;
                    isStart = false;
                }
            }
            else
            {
                startValue -= speed * Time.deltaTime;
                if (startValue <= endValue)
                {
                    startValue = endValue;
                    isStart = false;
                }
            }
            numLabel.text = (int)startValue + "";
        }
    }

    public void OnTweenFinish()
    {
        if (isSetActive == true)
        {
            tween.PlayReverse();
            isSetActive = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
            
    }

    public void ShowPowerChange(int startValue,int endValue)
    {
        isSetActive = true;
        gameObject.SetActive(true);
        tween.PlayForward();
        this.startValue = startValue;
        this.endValue = endValue;
        if (startValue > endValue)
            isUp = false;
        else
            isUp = true;
        isStart = true;
    }
}
