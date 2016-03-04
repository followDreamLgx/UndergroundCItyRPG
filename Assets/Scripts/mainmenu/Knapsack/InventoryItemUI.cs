using UnityEngine;
using System.Collections;

//用于控制背包中的装备信息
//挂在背包中每个装备的格子上
public class InventoryItemUI : MonoBehaviour {

    private UISprite sprite;//显示物品的图片
    private UILabel label;//显示物品的数量
    public InventoryItem it = null;//记录当前格子中的物品信息

    private UISprite Sprite
    {
        get
        {
            if(sprite == null)
            {
                sprite = transform.Find("Sprite").GetComponent<UISprite>();
            }
            return sprite;
        }
        set
        {
            sprite = value;
        }
    }
    private UILabel Label
    {
        get
        {
            if(label == null)
            {
                label = transform.Find("Label").GetComponent<UILabel>();
            }
            return label;
        }
        set
        {
            label = value;
        }
    }
	public void SetInventoryItem(InventoryItem it)
    {
        this.it = it;
        Sprite.spriteName = it.INventory.Icon;
        if (it.Count <= 1)
            Label.text = "";
        else//只有道具的数量大于1的时候才会显示数字 
            Label.text = it.Count.ToString();
    }
    public void Clear()
    {
        it = null;
        Label.text = "";
        Sprite.spriteName = "bg_Prop";
    }

    //使用NGUI的时候，假如点击了按钮，当点击的时候就会调用这个函数
    //当点击了背包中的装备时调用此函数，将自身对应的装备与接下来介绍装备弹框的位置告诉负责弹框的函数
    public void OnClick()
    {
        if (it != null)
        {
            object[] objectArr = new object[3];
            objectArr[0] = it;
            objectArr[1] = true;//true代表在右边弹出装备的详细信息
            objectArr[2] = this;//点击了装备，有可能要穿上，当穿上的时候原本的格子需要做改变，所以传递了自己
            transform.parent.parent.parent.SendMessage("OnInventoryClick", objectArr);
        }
    }
    //改变背包的数字
    public void ChangeCount(int count)
    {
        if (it.Count - count <= 0)
            Clear();
        else if (it.Count - count == 1)
            label.text = "";
        else
            label.text = (it.Count - count).ToString();
    }
}
