using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour {

    private UILabel coinLabel;
    private UILabel diamondLabel;
    private UIButton coinPlusButton;
    private UIButton diamondPlusButton;
    void Awake()
    {
        coinLabel = transform.Find("CoinBg/Label").GetComponent<UILabel>();
        diamondLabel = transform.Find("DiamondBg/Label").GetComponent<UILabel>();
        coinPlusButton = transform.Find("CoinBg/PlusButton").GetComponent<UIButton>();
        diamondPlusButton = transform.Find("DiamondBg/PlusButton").GetComponent<UIButton>();
    }
    void Start()
    {
        PlayerImfor._instance.OnPlayerInfoChanged += this.OnPlayerInfoChanged;//注册事件,_instance在Awake中产生，为了保证不为空，在Start中注册

    }
    void OnDestroy()
    {
        PlayerImfor._instance.OnPlayerInfoChanged -= this.OnPlayerInfoChanged;//注销事件
    }

    void OnPlayerInfoChanged(InfoType type)
    {
        if(type == InfoType.All
            || type == InfoType.Coin
            || type == InfoType.Diamond)
        {
            if (type == InfoType.Coin)
                Debug.Log("钱变啦");
            UpdateShow();
        }
    }
    void UpdateShow()
    {
        PlayerImfor info = PlayerImfor._instance;
        coinLabel.text = info.Coin.ToString();
        diamondLabel.text = info.Diamond.ToString();
    }
}
