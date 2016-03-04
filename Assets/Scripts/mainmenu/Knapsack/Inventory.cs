using UnityEngine;
using System.Collections;

//道具的类型，装备，药品，宝箱
public enum InventoryType
{
    Equip,
    Drug,
    Box
}
//枚举装备类型
public enum EquipType
{
    Helm,//头盔
    Cloth,//衣服
    Weapon,//武器
    Shoes,//鞋子
    Necklace,//项链
    Bracelet,//手镯
    Ring,//戒指
    Wing,//翅膀
}

//定义了装备的信息
public class Inventory {
    private int id;
    private string name;
    private string icon;//在图集中的名称
    private InventoryType inventoryType;//物品类型，装备或者其它物品
    private EquipType equipType;//装备的类型
    private int price = 0;//价格
    private int startLevel = 1;//星级
    private int quality = 1;//品质
    private int damage = 0;//伤害值
    private int hp = 0;//血量
    private int power = 0;//战斗力，生命力越大，战斗力越高。。。
    private InfoType infoType;//作用在主角的哪些属性上
    private int appValue;//作用值，qpply value
    private string des;//描述
	

    public int AppValue
    {
        get
        {
            return appValue;
        }
        set
        {
            appValue = value;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }
    public string Des
    {
        get
        {
            return des;
        }
        set
        {
            des = value;
        }
    }
    public EquipType EquipType
    {
        get
        {
            return equipType;
        }
        set
        {
            equipType = value;
        }
    }
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;
        }
    }
    public string Icon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }
    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }
    public InfoType InfoType 
    {
        get
        {
            return infoType;
        }
        set
        {
            infoType = value;
        }
    }
    public InventoryType InventoryTYPE 
    {
        get
        {
            return inventoryType;
        }
        set
        {
            inventoryType = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;
        }
    }
    public int Price
    {
        get
        {
            return price;
        }
        set
        {
            price = value;
        }
    }
    public int Quality
    {
        get
        {
            return quality;
        }
        set
        {
            quality = value;
        }
    }
    public int StartLevel
    {
        get
        {
            return startLevel;
        }
        set
        {
            startLevel = value;
        }
    }
}
