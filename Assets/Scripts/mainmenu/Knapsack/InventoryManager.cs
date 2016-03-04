using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryManager : MonoBehaviour {

    
    public TextAsset listInfo;
    public  Dictionary<int, Inventory> inventoryDict = new Dictionary<int, Inventory>();
    //public  Dictionary<int, InventoryItem> inventoryItemDict = new Dictionary<int, InventoryItem>();
    public List<InventoryItem> inventoryItemList = new List<InventoryItem>();
    public static InventoryManager _instance;

    public delegate void OnInventoryChangeEvent();
    public event OnInventoryChangeEvent OnInventoryChange;

    void Awake()
    {
        _instance = this;
        ReadInventoryInfo();

    }
    void Start()
    {
        ReadInventoryItemInfo();
    }
    //物品信息的初始化
    //将文本的内容按回车，再按|分割，读取之后放入inventoryDict
    //文本中记录了所有物品的类型，以及作用
    void ReadInventoryInfo()
    {
        string str = listInfo.ToString();//将读取的内容转换为string
        string[] itemStrArray = str.Split('\n');
        foreach (string itemStr in itemStrArray)
        {
            string[] proArray = itemStr.Split('|');
            Inventory inventory = new Inventory();
            inventory.Id = int.Parse(proArray[0]);
        
            inventory.Name = proArray[1];
            inventory.Icon = proArray[2];
            switch(proArray[3])
            {
                case "Equip":
                    inventory.InventoryTYPE = InventoryType.Equip;
                    break;
                case "Drug":
                    inventory.InventoryTYPE = InventoryType.Drug;
                    break;
                case "Box":
                    inventory.InventoryTYPE = InventoryType.Box;
                    break;
            }
            if(inventory.InventoryTYPE == InventoryType.Equip)
            {
                switch(proArray[4])
                {
                    case "Helm":
                        inventory.EquipType = EquipType.Helm;
                        break;
                    case "Cloth":
                        inventory.EquipType = EquipType.Cloth;
                        break;
                    case "Weapon":
                        inventory.EquipType = EquipType.Weapon;
                        break;
                    case "Shoes":
                        inventory.EquipType = EquipType.Shoes;
                        break;
                    case "Necklace":
                        inventory.EquipType = EquipType.Necklace;
                        break;
                    case "Bracelet":
                        inventory.EquipType = EquipType.Bracelet;
                        break;
                    case "Ring":
                        inventory.EquipType = EquipType.Ring;
                        break;
                    case "Wing":
                        inventory.EquipType = EquipType.Wing;
                        break;
                }
            }
            
            inventory.Price = int.Parse(proArray[5]);
            if(inventory.InventoryTYPE == InventoryType.Equip)
            {
                inventory.StartLevel = int.Parse(proArray[6]);
                inventory.Quality = int.Parse(proArray[7]);
                inventory.Damage = int.Parse(proArray[8]);
                inventory.Hp = int.Parse(proArray[9]);
                inventory.Power = int .Parse(proArray[10]);
            }
            if(inventory.InventoryTYPE == InventoryType.Drug)
            {
                inventory.AppValue = int.Parse(proArray[12]);
            }
            inventory.Des = proArray[13];
            inventoryDict.Add(inventory.Id, inventory);
        }
    }

    //完成角色背包信息的初始化，获得拥有的物品
    //生成的物品必须是inventoryDict中有所记录的类型
    void ReadInventoryItemInfo()
    {
        //TODO,需要连接服务器，取得当前角色的物品信息
        //随机生成主角拥有的物品
        for(int i = 0;i < 20;i++)
        {
            int id = Random.Range(1001, 1020);
            Inventory j = null;
            inventoryDict.TryGetValue(id, out j);
            
            if(j.InventoryTYPE == InventoryType.Equip)
            {
                InventoryItem it = new InventoryItem();
                it.INventory = j;
                it.Level = Random.Range(1, 10);
                it.Count = 1;
                inventoryItemList.Add(it);
                //inventoryItemDict.Add(id, it);
            }
            else
            {
                //先判断背包里面是否已经存在物品
                InventoryItem it = null;

                bool isExit = false;// = inventoryItemDict.TryGetValue(id, out it);
                foreach(InventoryItem temp in inventoryItemList)
                {
                    if(temp.INventory.Id == id)
                    {
                        isExit = true;
                        it = temp;
                        break;
                    }
                }
                if(isExit)
                    it.Count++;
                else
                {
                    it = new InventoryItem();
                    it.INventory = j;
                    it.Count = 1;
                    inventoryItemList.Add(it);
                }
            }
        }
        OnInventoryChange();
    }

    public void RemoveInventoryItem(InventoryItem it)
    {
        this.inventoryItemList.Remove(it);
    }
}
