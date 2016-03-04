using UnityEngine;
using System.Collections;

//用于控制主句穿在身上的八个装备栏
public class KnapsackRoleEquip : MonoBehaviour {

    private UISprite _sprite;
    private InventoryItem it;
    private InventoryItemUI itUI;

    public UISprite Sprite
    {
        get
        {
            if(_sprite == null)
            {
                _sprite = transform.Find("Sprite").GetComponent<UISprite>();
            }
            return _sprite;
        }
    }


	public void SetID(int id)
    {
        Inventory inventory = null;
        bool isExit = InventoryManager._instance.inventoryDict.TryGetValue(id, out inventory);
        if(isExit)
        {
            Sprite.spriteName = inventory.Icon;
        }
    }
    public void SetInventoryItem(InventoryItem it)
    {
        if (it == null)
            return;
        this.it = it;
        Sprite.spriteName = it.INventory.Icon;

    }
    //使用NGUI的时候，假如使用了按钮，当点击的时候就会调用这个函数
    //当点击了穿戴在人物身上的装备时调用此函数，将自身对应的装备与接下来介绍装备弹框的位置告诉
    //
    public void OnPress(bool isPress)
    {
        if(isPress && it != null)
        {
            Debug.Log("点击了穿在人身上的装备");
            object []objectArr = new object[3];
            objectArr[0] = it;
            objectArr[1] = false;//false代表在左边弹出装备详细信息
            objectArr[2] = this;
            transform.parent.parent.SendMessage("OnInventoryClick", objectArr);
        }
    }

    public void Clear()
    {
        it = null;
        Sprite.spriteName = "bg_prop";
    }
}
