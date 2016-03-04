using UnityEngine;
using System.Collections;

public class Knapsack : MonoBehaviour {

    public static Knapsack _instance;
    private EquipPopup equipPopup;
    private InventoryPopup inventoryPopup;


    private UIButton saleButton;
    private UILabel salePriceLabel;
    private InventoryItemUI itUI;
    private TweenPosition tween;
    private UIButton closeKnapsackButton;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        equipPopup = transform.Find("EquipPopup").GetComponent<EquipPopup>();
        inventoryPopup = transform.Find("InventoryPopup").GetComponent<InventoryPopup>();

        saleButton = transform.Find("Inventory/ButtonSell").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "OnSale");
        saleButton.onClick.Add(ed);
        salePriceLabel = transform.Find("Inventory/PriceSprite/Label").GetComponent<UILabel>();
        DisableButton();
        salePriceLabel.text = "";
        closeKnapsackButton = transform.Find("CloseButton").GetComponent<UIButton>();
        EventDelegate ed2 = new EventDelegate(this, "OnKnapsacklose");
        closeKnapsackButton.onClick.Add(ed2);
    }
	public void OnInventoryClick(object[] objectArray)
    {
        InventoryItem it = objectArray[0] as InventoryItem;
        bool isLeft = (bool)objectArray[1];

        if (it.INventory.InventoryTYPE == InventoryType.Equip)
        {
            KnapsackRoleEquip roleEquip = null;
            InventoryItemUI itUI = null;
            if(isLeft == true)
            {
                itUI = objectArray[2] as InventoryItemUI;
            }
            else
            {
                roleEquip = objectArray[2] as KnapsackRoleEquip;
            }
            inventoryPopup.Close();
            equipPopup.Show(it, itUI,roleEquip, isLeft);
        }
        else
        {
            InventoryItemUI itUI = objectArray[2] as InventoryItemUI;
            equipPopup.Close();
            inventoryPopup.Show(it,itUI);
        }
        if((it.INventory.InventoryTYPE == InventoryType.Equip && isLeft == true)
            || (it.INventory.InventoryTYPE != InventoryType.Equip))
        {
            this.itUI = objectArray[2] as InventoryItemUI;
            EnableButton();
            salePriceLabel.text = (this.itUI.it.INventory.Price * this.itUI.it.Count).ToString();
        }
    }

    public void DisableButton()
    {
        saleButton.SetState(UIButtonColor.State.Disabled, true);
        saleButton.GetComponent<BoxCollider>().enabled = false;//让button不可交互
        salePriceLabel.text = "";
    }
    void EnableButton()
    {
        saleButton.SetState(UIButtonColor.State.Normal, true);
        saleButton.GetComponent<BoxCollider>().enabled = true;
    }

    void OnSale()
    {
        int price = int.Parse(salePriceLabel.text);
        PlayerImfor._instance.AddCoin(price);
        InventoryManager._instance.RemoveInventoryItem(itUI.it);
        itUI.Clear();
        inventoryPopup.Close();
        equipPopup.Close();
        DisableButton();
    }
    public void Show()
    {
        tween.PlayForward();
    }
    public void Hide()
    {
        tween.PlayReverse();
    }
    public void OnKnapsacklose()
    {
        Hide();
    }
}
