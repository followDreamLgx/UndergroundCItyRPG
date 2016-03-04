using UnityEngine;
using System.Collections;

public class EquipPopup : MonoBehaviour {

    public PowerShow powerShow;
    private InventoryItem it;
    private InventoryItemUI itUI;
    private KnapsackRoleEquip roleEquip;

    private UISprite equipSprite;
    private UILabel nameLabel;
    private UILabel quelityLabel;
    private UILabel damageLabel;
    private UILabel hpLabel;
    private UILabel powerLabel;
    private UILabel desLabel;
    private UILabel levelLabel;
    private UILabel btnLabel;
    private UIButton closeButton;
    private UIButton equipButton;


    bool isLeft;
    private UIButton upgradeButton;

    void Awake()
    {
        equipSprite = transform.Find("EquipBg/Sprite").GetComponent<UISprite>();
        nameLabel = transform.Find("NameLabel").GetComponent<UILabel>();
        quelityLabel = transform.Find("QualityLabel/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageLabel/Label").GetComponent<UILabel>();
        hpLabel = transform.Find("HpLabel/Label").GetComponent<UILabel>();
        powerLabel = transform.Find("PowerLabel").GetComponent<UILabel>();
        desLabel = transform.Find("DestLabel").GetComponent<UILabel>();
        levelLabel = transform.Find("LevelLabel/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("EquipButton/Label").GetComponent<UILabel>();

        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        equipButton = transform.Find("EquipButton").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        closeButton.onClick.Add(ed1);

        EventDelegate ed2 = new EventDelegate(this, "OnEquip");
        equipButton.onClick.Add(ed2);
        
        upgradeButton = transform.Find("UpGradeButton").GetComponent<UIButton>();
        EventDelegate ed3 = new EventDelegate(this, "OnUpgrade");//注册更新按钮的点击事件
        upgradeButton.onClick.Add(ed3);
    }
    public void Show(InventoryItem it,InventoryItemUI itUI,KnapsackRoleEquip roleEquip, bool isLeft)
    {
        this.isLeft = isLeft;
        this.gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        this.roleEquip = roleEquip;
        if(isLeft)
        {
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(-Mathf.Abs(pos.x), pos.y, pos.z);
            btnLabel.text = "装备";
        }
        else
        {
            btnLabel.text = "卸下";
            Vector3 pos = transform.localPosition;
            transform.localPosition = new Vector3(Mathf.Abs(pos.x), pos.y, pos.z);
        }
        equipSprite.spriteName = it.INventory.Icon;
        nameLabel.text = it.INventory.Name;
        quelityLabel.text = it.INventory.Quality.ToString();
        damageLabel.text = it.INventory.Damage.ToString();
        hpLabel.text = it.INventory.Hp.ToString();
        powerLabel.text = it.INventory.Power.ToString();
        desLabel.text = it.INventory.Des;
        levelLabel.text = it.Level.ToString();
    }

    void ClearObject()
    {
        it = null;
        itUI = null;
    }
    public void OnClose()
    {
        transform.parent.SendMessage("DisableButton");
        Close();
    }

    //点击卸下和装备的时候触发
    public void OnEquip()
    {
        int startValue = PlayerImfor._instance.GetOverRallPower();
        if (isLeft)//从背包装备到身上
        {
            if (itUI != null)
                itUI.Clear();//清背包的显示
            PlayerImfor._instance.DressOn(it);
        }
        else//从身上脱下
        {
            roleEquip.Clear();//把身上的一件装备清空
            PlayerImfor._instance.DressOff(it);
        }
        int endValue = PlayerImfor._instance.GetOverRallPower();
        powerShow.ShowPowerChange(startValue, endValue);

        InventoryUI._instance.SendMessage("UpdateCount");
        OnClose();
    }
    //点击了升级按钮
    public void OnUpgrade()
    {
        int coinNeed = (it.Level + 1) * it.INventory.Price;//升级所需要的金币数
        bool isSuccess = PlayerImfor._instance.GetCoin(coinNeed);
        if (isSuccess)
        {
            it.Level += 1;
            
            //播放音乐啊等等
        }
        else
        {
            MessageManageer._instance.ShowMessage("金 币 不 足");
            ///给出提示信息表示金币不足
        }
    }
    public void Close()
    {
        ClearObject();
        this.gameObject.SetActive(false);
    }
}
