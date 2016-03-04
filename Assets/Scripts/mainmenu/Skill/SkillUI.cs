using UnityEngine;
using System.Collections;

public class SkillUI : MonoBehaviour {

    public static SkillUI _instance;
    private UILabel skillNameLabel;
    private UILabel skillDesLabel;
    private UIButton closeButton;
    private UIButton upgradeButton;
    private UILabel upgradeButtonLabel;
    private Skill skill;
    private TweenPosition tween;

    void Awake()
    {
        _instance = this;
        skillNameLabel = transform.Find("Bg/SkillNameLabel").GetComponent<UILabel>();
        skillDesLabel = transform.Find("Bg/DesLabel").GetComponent<UILabel>();
        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        upgradeButton = transform.Find("UpgradeButton").GetComponent<UIButton>();
        upgradeButtonLabel = transform.Find("UpgradeButton/Label").GetComponent<UILabel>();
        tween = transform.GetComponent<TweenPosition>();
        skillNameLabel.text = "";
        skillDesLabel.text = "";
        DisableUpgradeButton("选择技能");
        EventDelegate ed = new EventDelegate(this, "OnUpgrade");
        upgradeButton.onClick.Add(ed);

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);

    }


    void DisableUpgradeButton(string label = "")
    {
        upgradeButton.SetState(UIButtonColor.State.Disabled, true);
        Collider collider = upgradeButton.GetComponent<Collider>();
        collider.enabled = false;
        if(label != "")
        {
            upgradeButtonLabel.text = label;
        }
       
    }
    void EnableUpgradeButton(string label = "")
    {
        upgradeButton.SetState(UIButtonColor.State.Normal, true);
        Collider collider = upgradeButton.GetComponent<Collider>();
        collider.enabled = true;
        if (label != "")
        {
            upgradeButtonLabel.text = label;
        }
    }

    //用于选择技能与界面更新
    public void OnSkillClick(Skill skill)
    {
        this.skill = skill;
        skillNameLabel.text = skill.Name + " " + "LV." + skill.Level;
        skillDesLabel.text = "当前技能的攻击力为:" + (skill.Damage * skill.Level) +
            "下一级技能攻击力为:" + (skill.Damage * (skill.Level + 1)) + "升级所需要的金币数量:" + (500 * (skill.Level + 1));
        PlayerImfor info = PlayerImfor._instance;
        if (500 * (skill.Level + 1) < info.Coin)
        {
            if (skill.Level < info.Level)
                EnableUpgradeButton("升级");
            else
                DisableUpgradeButton("已达最大等级");
        }
        else
            DisableUpgradeButton("金币不足");
    }
    void OnUpgrade()
    {
        PlayerImfor info = PlayerImfor._instance;
        if(skill.Level < info.Level)
        {
            int coinNeed = 500 * (skill.Level + 1);
            bool isSuccess = info.GetCoin(coinNeed);
            if(isSuccess)
            {
                skill.Upgrade();
                OnSkillClick(skill);
            }
            else
            {
                DisableUpgradeButton("金币不足");
            }
        }
        else
            DisableUpgradeButton("已达最大等级");
    }

    public void Show()
    {
        tween.PlayForward();
    }
    public void Hide()
    {
        tween.PlayReverse();
    }
    void OnClose()
    {
        Hide();
    }
}
