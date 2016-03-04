using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

    public static InventoryUI _instance;
    public List<InventoryItemUI> itemUIList = new List<InventoryItemUI>();//所有的物品格子
    private UIButton clearUpButton;
    private UILabel inventoryLabel;
    private int count = 0;//有东西的格子有多少个

    #region UnityMethod
    void Awake()
    {
        _instance = this;
        clearUpButton = transform.Find("ButtonClearUp").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "OnClearUp");
        inventoryLabel = transform.Find("InventoryLabel").GetComponent<UILabel>();
        clearUpButton.onClick.Add(ed);
    }
    void Start()
    {
        InventoryManager._instance.OnInventoryChange += OnInventoryChange;
    }
    void Destory()
    {
        InventoryManager._instance.OnInventoryChange -= OnInventoryChange;
    }
    #endregion UnityMethod

    void OnInventoryChange()
    {
        UpdateShow();
    }
    void UpdateShow()
    {
        int temp = 0;
        for(int i = 0;i<InventoryManager._instance.inventoryItemList.Count;++i)
        {
            InventoryItem it = InventoryManager._instance.inventoryItemList[i];
            if(it.IsDressed == false)
                itemUIList[temp++].SetInventoryItem(it);//添加到背包
        }
        count = temp;
        for(int i = temp;i < itemUIList.Count;++i)
        {
            itemUIList[i].Clear();
        }
        
        inventoryLabel.text = count + "/28";
    }

    public void AddInventoryItem(InventoryItem it)
    {
        foreach(InventoryItemUI itUi in itemUIList)
        {
            if(itUi.it == null)
            {
                it.IsDressed = false;
                itUi.SetInventoryItem(it);
                break;
            }
        }
        ++count;
        inventoryLabel.text = count + "/28";
    }


    public void OnClearUp()
    {
        UpdateShow();
    }

    public void UpdateCount()
    {
        count = 0;
        foreach (InventoryItemUI itUi in itemUIList)
        {
            if (itUi.it != null)
            {
                count++;
            }
        }
        inventoryLabel.text = count + "/28";
    }
}
