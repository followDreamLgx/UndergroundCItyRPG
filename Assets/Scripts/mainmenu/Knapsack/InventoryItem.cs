using UnityEngine;
using System.Collections;

//包含了一件装备的所有的信息
public class InventoryItem{

    private Inventory inventory;
    private int level;
    private int count;
    private bool isDressed = false;


    #region GetSetMethod
    public Inventory INventory
    {
        get
        {
            return inventory;
        }
        set
        {
            inventory = value;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;
        }
    }
    public bool IsDressed
    {
        get
        {
            return isDressed;
        }
        set
        {
            isDressed = value;
        }
    }

    #endregion GetSetMethod
}
