using UnityEngine;
using System.Collections;

public class PlayerBar : MonoBehaviour {

    private UISprite headSprite;
    private UILabel nameLabel;
    private UILabel levelLabel;
    private UILabel energyLabel;
    private UISlider energySlider;
    private UILabel toughenLabel;
    private UISlider toughenSlider;
    private UIButton energyPlusButton;
    private UIButton toughenPlusButton;
    private UIButton headButton;

    void Awake()
    {
        headSprite = this.transform.Find("HeadSprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        
        energySlider = transform.Find("EnergyProgressBar").GetComponent<UISlider>();
        energyPlusButton = transform.Find("EnergyProgressBar/EnergyPlusButton").GetComponent<UIButton>();
        energyLabel = transform.Find("EnergyProgressBar/Label").GetComponent<UILabel>();

        toughenSlider = transform.Find("ToughenProgressBar").GetComponent<UISlider>();
        toughenPlusButton = transform.Find("ToughenProgressBar/ToughenPlusButton").GetComponent<UIButton>();
        toughenLabel = transform.Find("ToughenProgressBar/Label").GetComponent<UILabel>();

        headButton = transform.Find("HeadSprite/HeadButton").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "OnHeadButtonClick");//类似于外部添加事件
        headButton.onClick.Add(ed);//为按钮注册点击事件
    }
    void Start()
    {
        PlayerImfor._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;//注册事件,_instance在Awake中产生，为了保证不为空，在Start中注册

    }
    void OnDestroy()
    {
        PlayerImfor._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;//注销事件
    }
    //当我们的主角信息改变时触发这个方法
    void OnPlayerInfoChanged(InfoType type)
    {
        if (type == InfoType.All
            ||type == InfoType.Name
            || type == InfoType.Level
            || type == InfoType.HeadPortrait || type == InfoType.Energy
            || type == InfoType.Toughen)
        {
            UpdateShow();
        }
    }
    //更新显示
    void UpdateShow()
    {
        PlayerImfor info = PlayerImfor._instance;
        headSprite.spriteName = info.HeadPortrait;
        levelLabel.text = info.Level.ToString();
        nameLabel.text = info.Name.ToString();
        energySlider.value = info.Energy / 100f;
        energyLabel.text = info.Energy + "/100";
        toughenSlider.value = info.Toughen / 50f;
        toughenLabel.text = info.Toughen + "/50";
    }
    public void OnHeadButtonClick()
    {
        PlayerStatus._instance.Show();
    }
}
