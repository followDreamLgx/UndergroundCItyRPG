using UnityEngine;
using System.Collections;

public class TaskItemUI : MonoBehaviour {

    private UISprite tasktypeSprite;
    private UISprite iconSprite;
    private UILabel desLabel;
    private UILabel nameLabel;
    private UISprite reward1Sprite;
    private UILabel reward1Label;
    private UISprite reward2Sprite;
    
    private UILabel reward2Label;
    private UIButton rewardButton;
    private UIButton combatButton;
    private UILabel combatLabel;

    private Task task;
    void Awake()
    {
        tasktypeSprite = transform.Find("TaskType").GetComponent<UISprite>();
        iconSprite = transform.Find("IconBg").GetComponent<UISprite>();
        desLabel = transform.Find("DesLabel").GetComponent<UILabel>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        reward1Sprite = transform.Find("Reward1Sprite").GetComponent<UISprite>();
        reward1Label = transform.Find("Reward1Sprite/Reward1Label").GetComponent<UILabel>();
        reward2Sprite = transform.Find("Reward2Sprite").GetComponent<UISprite>();
        reward2Label = transform.Find("Reward2Sprite/Reward2Label").GetComponent<UILabel>();
        rewardButton = transform.Find("RewardButton").GetComponent<UIButton>();
        combatButton = transform.Find("CombatButton").GetComponent<UIButton>();
        combatLabel = transform.Find("CombatButton/Label").GetComponent<UILabel>();

        EventDelegate ed1 = new EventDelegate(this, "OnCombat");
        combatButton.onClick.Add(ed1);
        EventDelegate ed2 = new EventDelegate(this, "OnReward");
        rewardButton.onClick.Add(ed2);
    }

    public void SetTask(Task task)
    {
        this.task = task;
        task.OnTaskChange += this.OnTaskChange;
        UpdateShow();
    }
    void UpdateShow()
    {
        switch (task.TaskTYpe)
        {
            case TaskType.Main:
                tasktypeSprite.spriteName = "pic_Mainline";
                break;
            case TaskType.Reward:
                tasktypeSprite.spriteName = "pic_Reward";
                break;
            case TaskType.Daily:
                tasktypeSprite.spriteName = "pic_Daily";
                break;
        }
        iconSprite.spriteName = task.Icon;
        nameLabel.text = task.Name;
        desLabel.text = task.Des;
        if (task.Coin > 0 && task.Diamond > 0)
        {
            reward1Sprite.gameObject.SetActive(true);
            reward2Sprite.gameObject.SetActive(true);
            reward1Sprite.spriteName = "Gold";
            reward1Label.text = "X" + task.Coin;
            reward2Sprite.spriteName = "Diamond";
            reward2Label.text = "X" + task.Diamond;
        }
        else if (task.Coin > 0)
        {
            reward1Sprite.gameObject.SetActive(true);
            reward1Sprite.spriteName = "Gold";
            reward1Label.text = "X" + task.Coin;
            reward2Sprite.gameObject.SetActive(false);
        }
        else if (task.Diamond > 0)
        {
            reward2Sprite.gameObject.SetActive(true);
            reward2Sprite.spriteName = "Diamond";
            reward2Label.text = "X" + task.Diamond;
            reward1Sprite.gameObject.SetActive(false);
        }
        switch (task.TaskProgress)
        {
            case TaskProgress.NoStart:
                rewardButton.gameObject.SetActive(false);
                combatLabel.text = "下一步";
                break;
            case TaskProgress.Accept:
                rewardButton.gameObject.SetActive(false);
                combatLabel.text = "战斗";
                break;
            case TaskProgress.Complete:
                combatButton.gameObject.SetActive(false);
                rewardButton.gameObject.SetActive(true);
                break;
            case TaskProgress.Reward:
                break;
            default:
                break;
        }
    }
    public void OnCombat()
    {
        TaskUI._instance.Hide();
        TaskManager._instance.OnExcuteTask(task);
        
    }
    public void OnReward()
    {

    }
   
    void OnTaskChange()
    {
        UpdateShow();
    }
}
