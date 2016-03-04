using UnityEngine;
using System.Collections;

//挂在InventoryPopup上，
//InventoryPopup物体用于在背包中点击药品（可能还有其它）时显示
//这个脚本用于更新这些内容
public class InventoryPopup : MonoBehaviour {


    private UILabel nameLabel;
    private UISprite inventorySprite;
    private UILabel desLabel;
    private UILabel btnLabel;
    private InventoryItem it;//存储了


    private UIButton closeButton;
    private UIButton useButton;
    private UIButton useBatchingButton;
    private InventoryItemUI itUI;

    void Awake()
    {
        nameLabel = transform.Find("Bg/NameLabel").GetComponent<UILabel>();
        inventorySprite = transform.Find("Bg/Sprite/Sprite").GetComponent<UISprite>();
        desLabel = transform.Find("Bg/Label").GetComponent<UILabel>();
        btnLabel = transform.Find("Bg/ButtonUseBatching/Label").GetComponent<UILabel>();

        closeButton = transform.Find("CloseButton").GetComponent<UIButton>();
        useButton = transform.Find("Bg/ButtonUse").GetComponent<UIButton>();
        useBatchingButton = transform.Find("Bg/ButtonUseBatching").GetComponent<UIButton>();

        EventDelegate ed1 = new EventDelegate(this, "OnClose");
        EventDelegate ed2 = new EventDelegate(this, "OnUse");
        EventDelegate ed3 = new EventDelegate(this, "OnUserBatching");
        closeButton.onClick.Add(ed1);
        useButton.onClick.Add(ed2);
        useBatchingButton.onClick.Add(ed3);
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Show(InventoryItem it,InventoryItemUI itUI)
    {
        this.gameObject.SetActive(true);
        this.it = it;
        this.itUI = itUI;
        nameLabel.text = it.INventory.Name;
        inventorySprite.spriteName = it.INventory.Icon;
        desLabel.text = it.INventory.Des;
        btnLabel.text = "批量使用(" + it.Count + ")";
    }

    public void OnClose()
    {
        Close();
        transform.parent.SendMessage("OnDisable");
        transform.parent.SendMessage("DisableButton");
    }
    public void Close()
    {
        Clear();
        gameObject.SetActive(false);
    }
    public void OnUse()
    {
        itUI.ChangeCount(1);
        PlayerImfor._instance.InventoryUse(it, 1);
        OnClose();

    }
    public void OnUserBatching()
    {
        itUI.ChangeCount(it.Count);
        PlayerImfor._instance.InventoryUse(it, it.Count);
        OnClose();
    }
    void Clear()
    {
        it = null;
        itUI = null;
    }
}
