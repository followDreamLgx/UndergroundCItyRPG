using UnityEngine;
using System.Collections;

//用于控制更新主角的装备
//Role物体保存了人物的各个装备以及用于显示人类的各个装备
public class KnapsackRole : MonoBehaviour {

    private KnapsackRoleEquip helmEquip;
    private KnapsackRoleEquip clothEquip;
    private KnapsackRoleEquip weaponEquip;
    private KnapsackRoleEquip shoesEquip;
    private KnapsackRoleEquip necklackEquip;
    private KnapsackRoleEquip braceletEquip;
    private KnapsackRoleEquip ringEquip;
    private KnapsackRoleEquip wingEquip;

    private UILabel hpLabel;
    private UILabel damageLabel;
    private UILabel expLabel;
    private UISlider expSlider;

    #region UnityMethod
    void Awake()
    {
        helmEquip = transform.Find("HelmSprite").GetComponent<KnapsackRoleEquip>();
        clothEquip = transform.Find("ClothSprite").GetComponent<KnapsackRoleEquip>();
        weaponEquip = transform.Find("WeaponSprite").GetComponent<KnapsackRoleEquip>();
        shoesEquip = transform.Find("ShoesSprite").GetComponent<KnapsackRoleEquip>();
        necklackEquip = transform.Find("NecklaceSprite").GetComponent<KnapsackRoleEquip>();
        braceletEquip = transform.Find("BraceletSprite").GetComponent<KnapsackRoleEquip>();
        ringEquip = transform.Find("RingSprite").GetComponent<KnapsackRoleEquip>();
        wingEquip = transform.Find("WingSprite").GetComponent<KnapsackRoleEquip>();

        hpLabel = transform.Find("HpBg/Label").GetComponent<UILabel>();
        damageLabel = transform.Find("DamageBg/Label").GetComponent<UILabel>();
        expLabel = transform.Find("ExpSlider/Label").GetComponent<UILabel>();
        expSlider = transform.Find("ExpSlider").GetComponent<UISlider>();

    }

    void Start()
    {
        PlayerImfor._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;//注册事件,_instance在Awake中产生，为了保证不为空，在Start中注册
    }
    void OnDestroy()
    {
        PlayerImfor._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;//注销事件
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    #endregion UnityMethod

    void OnPlayerInfoChanged(InfoType type)
    {
        if(type == InfoType.All
            || type == InfoType.Hp
            || type == InfoType.Damage
            || type == InfoType.Exp
            || type == InfoType.Equip)
        {
            UpdateShow();
        }
    }
    void UpdateShow()
    {
        PlayerImfor info = PlayerImfor._instance;

        helmEquip.SetInventoryItem(info.helmInventoryItem);
        clothEquip.SetInventoryItem(info.clothInventoryItem);
        weaponEquip.SetInventoryItem(info.weaponInventoryItem);
        shoesEquip.SetInventoryItem(info.shoesInventoryItem);
        necklackEquip.SetInventoryItem(info.necklaceInventoryItem);
        braceletEquip.SetInventoryItem(info.braceleteInventoryItem);
        ringEquip.SetInventoryItem(info.ringInventoryItem);
        wingEquip.SetInventoryItem(info.wingInventoryItem);


        hpLabel.text = info.HP.ToString();
        damageLabel.text = info.Damage.ToString();
        expSlider.value = (float)info.Exp / GameController.GetRequireExpByLevel(info.Level + 1);
        expLabel.text = info.Exp + "/" + GameController.GetRequireExpByLevel(info.Level + 1);
    }
}
