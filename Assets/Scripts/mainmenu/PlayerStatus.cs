using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

    public static PlayerStatus _instance;
    private UISprite headSprite;
    private UILabel levelLabel;
    private UILabel nameLabel;
    private UILabel powerLabel;

    private UISlider expSlider;
    private UILabel expLabel;

    private UILabel coinLabel;
    private UILabel diamondLabel;

    private UILabel energyLable;
    private UILabel energyRestorPartLabel;
    private UILabel energyRestoreAllLabel;

    private UILabel toughenLable;
    private UILabel toughenRestorePartLabel;
    private UILabel toughenRestoreAllLabel;

    private float energyToughenTimer = 0;

    private TweenPosition tween;
    private UIButton closeButton;

    private UIButton changeNameButton;
    private GameObject changeNameGo;
    private UIInput nameInput;
    private UIButton sureButton;
    private UIButton cancelButton;


    void Awake()
    {
        _instance = this;

        headSprite = transform.Find("HeadSprite").GetComponent<UISprite>();
        levelLabel = transform.Find("LevelLabel").GetComponent<UILabel>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel").GetComponent<UILabel>();
        
        expSlider = transform.Find("ExpProgressBar").GetComponent<UISlider>();
        expLabel = transform.Find("ExpProgressBar/Label").GetComponent<UILabel>();

        coinLabel = transform.Find("CoinLabel/Label").GetComponent<UILabel>();
        diamondLabel = transform.Find("DiamondLabel/Label").GetComponent<UILabel>();

        energyLable = transform.Find("EnergyLabel/NumLabel").GetComponent<UILabel>();
        energyRestorPartLabel = transform.Find("EnergyLabel/RestorePartTime").GetComponent<UILabel>();
        energyRestoreAllLabel = transform.Find("EnergyLabel/RestoreAllTime").GetComponent<UILabel>();
        
        toughenLable = transform.Find("ToughenLabel/NumLabel").GetComponent<UILabel>();
        toughenRestorePartLabel = transform.Find("ToughenLabel/RestorePartTime").GetComponent<UILabel>();
        toughenRestoreAllLabel = transform.Find("ToughenLabel/RestoreAllTime").GetComponent<UILabel>();

        tween = transform.GetComponent<TweenPosition>();
        closeButton = transform.Find("ButtonClose").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "OnButtonCloseClick");
        closeButton.onClick.Add(ed);

        changeNameButton = transform.Find("ButtonChangeName").GetComponent<UIButton>();
        changeNameGo = transform.Find("ChangeNameBg").gameObject;
        nameInput = changeNameGo.transform.Find("NameInput").GetComponent<UIInput>();
        sureButton = changeNameGo.transform.Find("SureButton").GetComponent<UIButton>();
        cancelButton = changeNameGo.transform.Find("CancelButton").GetComponent<UIButton>();
        changeNameGo.SetActive(false);

        EventDelegate ed2 = new EventDelegate(this, "OnButtonChangeNameClick");
        changeNameButton.onClick.Add(ed2);
        EventDelegate ed3 = new EventDelegate(this,"OnButtonSureClick");
        sureButton.onClick.Add(ed3);
        EventDelegate ed4 = new EventDelegate(this, "OnButtonCancelClcik");
        cancelButton.onClick.Add(ed4);
    }
    void Start()
    {
        PlayerImfor._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;//注册事件,_instance在Awake中产生，为了保证不为空，在Start中注册

    }
    void Update()
    {
        energyToughenTimer += Time.deltaTime;
        if (energyToughenTimer > 1)
        {
            UpdateEnergyAndToughenShow();
            energyToughenTimer = 0;
        }
    }
    void OnDestroy()
    {
        PlayerImfor._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;//注销事件
    }
    void OnPlayerInfoChanged(InfoType type)
    {
        UpdateShow();
    }
    void UpdateShow()
    {
        PlayerImfor info = PlayerImfor._instance;
        headSprite.spriteName = info.HeadPortrait;
        levelLabel.text = info.Level.ToString();
        nameLabel.text = info.Name.ToString();
        powerLabel.text = info.Power.ToString();
        int requireExp = GameController.GetRequireExpByLevel(info.Level + 1);
        expSlider.value = (float)(info.Exp) / requireExp;
        expLabel.text = info.Exp + "/" + requireExp;
        diamondLabel.text = info.Diamond.ToString();
        coinLabel.text = info.Coin.ToString();

        UpdateEnergyAndToughenShow();//更新体力与历练
    }
    void UpdateEnergyAndToughenShow()
    {
        PlayerImfor info = PlayerImfor._instance;
        energyLable.text = info.Energy.ToString() + "/100";
        if (info.Energy >= 100)
        {
            energyRestorPartLabel.text = "00:00:00";
            energyRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            //部分恢复时间，只存在后两位
            int remainTime = 60 - (int)info.energyTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            energyRestorPartLabel.text = "00:00:" + str;

            //全部恢复时间
            int minus = 100 - info.Energy;
            int hours = minus / 60;
            minus = minus % 60;
            string hoursStr;
            if (hours != 0)
                hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            else
                hoursStr = "00";
            string minusStr = minus <= 9 ? "0" + minus : minus.ToString();
            energyRestoreAllLabel.text = hoursStr + ":" + minusStr + ":" + str;
        }

        toughenLable.text = info.Toughen + "/50";
        if(info.Toughen >= 50)
        {
            toughenRestorePartLabel.text = "00:00:00";
            toughenRestoreAllLabel.text = "00:00:00";
        }
        else
        {
            //部分恢复时间，只存在后两位
            int remainTime = 60 - (int)info.toughenTimer;
            string str = remainTime <= 9 ? "0" + remainTime : remainTime.ToString();
            toughenRestorePartLabel.text = "00:00:" + str;

            //全部恢复时间
            int minus = 50 - info.Toughen;
            int hours = minus / 60;
            minus = minus % 60;

            string hoursStr;
            if (hours != 0)
                hoursStr = hours <= 9 ? "0" + hours : hours.ToString();
            else
                hoursStr = "00";
            string minusStr = minus <= 9 ? "0" + minus : minus.ToString();
            toughenRestoreAllLabel.text = hoursStr + ":" + minusStr + ":" + str;
        }
    }

    public void Show()
    {
        tween.PlayForward();
    }
    public void OnButtonCloseClick()
    {
        tween.PlayReverse();
    }
    public void OnButtonChangeNameClick()
    {
        changeNameGo.SetActive(true);
    }
    public void OnButtonSureClick()
    {
        //联网校验名字是否重复
        //TODO
        PlayerImfor._instance.ChangeName(nameInput.value);
    }
    public void OnButtonCancelClcik()
    {
        changeNameGo.SetActive(false);
    }
}
