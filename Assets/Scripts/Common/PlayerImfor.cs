using UnityEngine;
using System.Collections;

public enum InfoType
{
    Name,
    HeadPortrait,
    Level,
    Power,
    Exp,
    Diamond,
    Coin,
    Energy,
    Toughen,
    Hp,
    Damage,
    Equip,
    All

}
public enum PlayerType
{
    Warrior,
    FemaleAssassin
}
public class PlayerImfor : MonoBehaviour
{
    public static PlayerImfor _instance;

    #region property
    private string _name;//
    private string _headPortrait;//
    private int _level = 1;//
    private int _power = 1;//攻击力//
    private int _exp = 0;//经验值//
    private int _diamond;//
    private int _coin;  //
    private int _energy;//体力//
    private int _toughen;//历练//
    private int _hp;
    private int _damage;
    private PlayerType _playerType;


    //private int _helmID = 0;//
    //private int _clothID = 0;//
    //private int _weaponID = 0;//
    //private int _shoesID = 0;//
    //private int _necklaceID = 0;//
    //private int _braceletID = 0;
    //private int _ringID = 0;//
    //private int wingID = 0;//

    public InventoryItem helmInventoryItem;
    public InventoryItem clothInventoryItem;
    public InventoryItem weaponInventoryItem;
    public InventoryItem shoesInventoryItem;
    public InventoryItem necklaceInventoryItem;
    public InventoryItem braceleteInventoryItem;
    public InventoryItem ringInventoryItem;
    public InventoryItem wingInventoryItem;

    #endregion property

    public float energyTimer = 0;
    public float toughenTimer = 0;


    #region GetSetMethod
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
    public string HeadPortrait
    {
        get
        {
            return _headPortrait;
        }
        set
        {
            _headPortrait = value;
        }
    }
    public int Level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
        }
    }
    public int Power
    {
        get
        {
            return _power;
        }
        set
        {
            _power = value;
        }
    }
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;
        }
    }
    public int Diamond
    {
        get
        {
            return _diamond;
        }
        set
        {
            _diamond = value;
        }
    }
    public int Coin
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
        }
    }
    public int Energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
        }
    }
    public int Toughen
    {
        get
        {
            return _toughen;
        }
        set
        {
            _toughen = value;
        }
    }

    public int HP
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    public int Damage
    {
        get
        {
            return _damage;
        }
        set
        {
            _damage = value;
        }
    }
    public PlayerType PlayerType
    {
        get { return _playerType; }
        set { _playerType = value; }
    }
    #endregion get set method

    #region unityEvent
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        #region EnergyToughenTimeCount
        if (this.Energy < 100)
        {
            energyTimer += Time.deltaTime;
            if(energyTimer > 60)
            {
                this.Energy += 1;
                energyTimer = 0;
                OnPlayerInfoChanged(InfoType.Energy);
            }
        }
        else
        {
            energyTimer = 0;
        }
        if(this.Toughen < 50)
        {
            toughenTimer += Time.deltaTime;
            if(toughenTimer > 60)
            {
                this.Toughen += 1;
                toughenTimer = 0;
                OnPlayerInfoChanged(InfoType.Toughen);
            }
        }
        else
        {
            toughenTimer = 0;
        }
        #endregion
    }
    #endregion

    //定义委托
    public delegate void OnPlayerInfoChangedEvent(InfoType type);

    public event OnPlayerInfoChangedEvent OnPlayerInfoChanged;

    InventoryItem EquipDressRetOriginal(ref InventoryItem original,ref InventoryItem toDress,ref bool isDress,bool isDressOff = false)
    {
        if(isDressOff == false)
        {
            InventoryItem temp = original;
            original = toDress;
            isDress = temp == null ? false : true;
            return temp;
        }
        else
        {
            original = null;
            return null;
        }

    }
    InventoryItem EquipIsDress(EquipType type,ref bool isDress,ref InventoryItem it,bool isDressOff = false)
    {
        switch (type)
        {
            case EquipType.Helm:
                return EquipDressRetOriginal(ref helmInventoryItem, ref it, ref isDress,isDressOff);
            case EquipType.Cloth:
                return EquipDressRetOriginal(ref clothInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Weapon:
                return EquipDressRetOriginal(ref weaponInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Shoes:
                return EquipDressRetOriginal(ref shoesInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Necklace:
                return EquipDressRetOriginal(ref necklaceInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Bracelet:
                return EquipDressRetOriginal(ref braceleteInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Ring:
                return EquipDressRetOriginal(ref ringInventoryItem, ref it, ref isDress, isDressOff);
            case EquipType.Wing:
                return EquipDressRetOriginal(ref wingInventoryItem, ref it, ref isDress, isDressOff);
        }
        return null;//这个只是为了编译能够通过
    }
    public void DressOn(InventoryItem it)
    {
        it.IsDressed = true;
        //首先检测有没有穿上相同类型的装备
        bool isDressed = false;
        InventoryItem inventoryItemDressed = null;
        inventoryItemDressed = EquipIsDress(it.INventory.EquipType, ref isDressed, ref it);

        //有
        if(isDressed)//将重复的衣服脱下来
        {
            inventoryItemDressed.IsDressed = false;
            InventoryUI._instance.AddInventoryItem(inventoryItemDressed);

        }
        OnPlayerInfoChanged(InfoType.Equip);
        //把已经存在的脱掉，放到背包中
        //没有
        //直接穿上
    }

    //调用的时候，playerimfor持有it对应的装备
    //
    public void DressOff(InventoryItem it)
    {
        bool isDressTemp = false;
        EquipIsDress(it.INventory.EquipType,ref isDressTemp, ref it, true);//从playerimfor脚本上脱掉装备
        InventoryUI._instance.AddInventoryItem(it);//装备放入背包
        OnPlayerInfoChanged(InfoType.Equip);//更新装备的显示
    }



    public void InventoryUse(InventoryItem it,int count)
    {
        //使用效果
        //TODO
        //处理物品使用后是否还存在
        it.Count -= count;
        if(it.Count <= 0)
        {
            InventoryManager._instance.inventoryItemList.Remove(it);//从内存中移除物品
        }
        else
        {

        }
    }

    public int GetOverRallPower()
    {
        float power = this.Power;
        if(helmInventoryItem != null)
        {
            power += helmInventoryItem.INventory.Power * (1 + (helmInventoryItem.Level - 1) / 10f);
        }
        if(clothInventoryItem != null)
        {
            power += clothInventoryItem.INventory.Power * (1 + (clothInventoryItem.Level - 1) / 10f);
        }
        if(weaponInventoryItem != null)
        {
            power += weaponInventoryItem.INventory.Power * (1 + (weaponInventoryItem.Level - 1) / 10f);
        }
        if (shoesInventoryItem != null)
        {
            power += shoesInventoryItem.INventory.Power * (1 + (shoesInventoryItem.Level - 1) / 10f);
        }
        if (necklaceInventoryItem != null)
        {
            power += necklaceInventoryItem.INventory.Power * (1 + (necklaceInventoryItem.Level - 1) / 10f);
        }
        if (braceleteInventoryItem != null)
        {
            power += braceleteInventoryItem.INventory.Power * (1 + (braceleteInventoryItem.Level - 1) / 10f);
        }
        if (ringInventoryItem != null)
        {
            power += ringInventoryItem.INventory.Power * (1 + (ringInventoryItem.Level - 1) / 10f);
        }
        if (wingInventoryItem != null)
        {
            power += wingInventoryItem.INventory.Power * (1 + (wingInventoryItem.Level - 1) / 10f);
        }
        return (int)power;
    }

    //取得所需要的金币数
    public bool GetCoin(int count)
    {
        if(Coin >= count)
        {
            Coin -= count;
            OnPlayerInfoChanged(InfoType.Coin);
            return true;

        }
        return false;
    }
    public void AddCoin(int count)
    {
        Coin += count;
        OnPlayerInfoChanged(InfoType.Coin);
    }

    void Init()
    {
        this.Coin = 9870;
        this.Diamond = 1234;
        this.Energy = 48;
        this.Exp = 123;
        this.HeadPortrait = "headportrailfloorMale";
        this.Level = 12;
        this.Name = "followDreamLgx";
        this.Power = 1745;
        this.Toughen = 34;

        //穿上装备
        //this.BraceletID = 1001;
        //this.WingID = 1002;
        //this.RingID = 1003;
        //this.ClothID = 1004;
        //this.HelmID = 1005;
        //this.WeaponID = 1006;
        //this.NecklaceID = 1007;
        //this.ShoesID = 1008;
        InitHpDamagePower();

        OnPlayerInfoChanged(InfoType.All);
    }
    void InitHpDamagePower()
    {
        this.HP = this.Level * 100;
        this.Damage = this.Level * 50;
        this.Power = this.HP + this.Damage;
        

    }
    public void ChangeName(string newName)
    {
        this.Name = newName;
        OnPlayerInfoChanged(InfoType.Name);
    }
    void PutonEquip(int id)
    {
        if (id == 0)
            return;
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id,out inventory);
        this.HP += inventory.Hp;
        this.Damage += inventory.Damage;
        this.Power += inventory.Power;
    }

    void PutoffEquip(int id)
    {
        Inventory inventory = null;
        InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        this.HP -= inventory.Hp;
        this.Damage -= inventory.Damage;
        this.Power -= inventory.Power;
    }

}
