using UnityEngine;
using System.Collections;

public class StartMenuController : MonoBehaviour {

    //动画控件
    public TweenScale startPanelTween;
    public TweenScale loginPanelTween;
    public TweenScale registerPanelTween;
    public TweenScale serverPanelTween;
    public TweenPosition startPanelPosiTween;
    public TweenPosition chatPanelPosiTween;
    public TweenPosition characterShowTween;

    public static string username;
    public static string password;

    //登录界面
    public UIInput userNameInputLogin;
    public UIInput passwordInputLogin;

    //start
    public UILabel userNameStart;
    public UILabel serverNameStart;

    //register
    public UIInput userNameRegister;
    public UIInput passwordRegister;
    public UIInput repasswordRegister;

    //服务器列表
    public bool haveInitServerlist = false;
    public UIGrid serverlistGrid;
    public GameObject serverItemRed;
    public GameObject serverItemGreen;
    public GameObject serverSelectedSprite;

    //角色选择
    public static StartMenuController _instance;
    private GameObject characterSelected;//当前选择的角色
    public UIInput characterNameInput;
    public Transform characterSelectedParent;   //用于确定已经选择角色的位置

    public UILabel nameLabelCharacterselect;
    public UILabel levelLabelCharacterselect;
    public GameObject[] characterArray;
    public GameObject[] characterSelectedArray;
    public UIInput characternameInput;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        InitServerlist();
    }
	public void OnUsernameClick()
    {
        startPanelTween.PlayForward();
        StartCoroutine(HidePanel(startPanelTween.gameObject));
        loginPanelTween.gameObject.SetActive(true);
        loginPanelTween.PlayForward();
    }

    //start中点击进入游戏
    public void OnEnterGameClick()
    {
        //连接服务器，验证用户名和服务器

        //进入游戏角色选择页面
    }
    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);
    }

    //login中点击了登录
    public void OnLoginClick()
    {
        //得到用户名和密码，存储起来
        //返回开始界面
        username = userNameInputLogin.value;
        password = passwordInputLogin.value;
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayReverse();

        userNameStart.text = username;
    }

    //用户登录面板跳到注册面板
    public void OnRegisterShowClick()
    {
        //隐藏当前面板，显示注册面板
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));
        registerPanelTween.gameObject.SetActive(true);
        registerPanelTween.PlayForward();
    }
   
    //关闭登录界面
    public void OnLoginCloseClick()
    {
        loginPanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayReverse();
    }

    //关闭注册的面板
    public void OnRegisterCanelClick()
    {
        registerPanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayReverse();        
    }

    //点击了注册的关闭按钮
    public void OnRegisterCloseClick()
    {
        OnRegisterCanelClick();
    }

    //注册页面点击注册并登录按钮
    public void OnRegisterAndLoginClick()
    {
        //1.本地校验
        //todo
        //2.连接成功
        //todo
        //3.连接成功
        //保存用户名和密码
        username = userNameRegister.value;
        password = passwordRegister.value;
        
        //返回到开始界面
        registerPanelTween.PlayReverse();
        StartCoroutine(HidePanel(registerPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayReverse();
        //更新当前登录的账号
        userNameStart.text = username;
    }
    public void InitServerlist()
    {
        if (haveInitServerlist == true)
            return;
        haveInitServerlist = true;
        //1.连接服务器，取得服务器列表的信息
        //2.根据上面的信息，添加服务器列表

        for(int i = 0;i < 20;++i)
        {
            string ip = "127.0.0.1:9880";
            string name = (i + 1) + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject go;
            if(count > 50)
            {
                //火爆
                go = NGUITools.AddChild(serverlistGrid.gameObject, serverItemRed);
            }
            else
            {   //流畅
                go = NGUITools.AddChild(serverlistGrid.gameObject, serverItemGreen);
            }
            ServerProperty sp = go.GetComponent<ServerProperty>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;
            serverlistGrid.AddChild(go.transform);
        }
    }
    //start中点击服务器
    public void OnServerClick()
    {
        startPanelTween.PlayForward();
        StartCoroutine(HidePanel(startPanelTween.gameObject));
        serverPanelTween.gameObject.SetActive(true);
        serverPanelTween.PlayForward();
    }

    public void OnServerSelect(GameObject serverGo)
    {
        serverSelectedSprite.GetComponent<UISprite>().spriteName = serverGo.GetComponent<UISprite>().spriteName;
        serverSelectedSprite.transform.FindChild("Label").GetComponent<UILabel>().text = 
            serverGo.transform.FindChild("Label").GetComponent<UILabel>().text;
    }

    public void OnServerPanelCanelClose()
    {
        serverPanelTween.PlayReverse();
        StartCoroutine(HidePanel(serverPanelTween.gameObject));
        startPanelTween.gameObject.SetActive(true);
        startPanelTween.PlayReverse();
        serverNameStart.text = serverSelectedSprite.transform.FindChild("Label").GetComponent<UILabel>().text;
    }

    public void OnEnterGameClik()
    {
        startPanelPosiTween.PlayForward();
        StartCoroutine(HidePanel(startPanelPosiTween.gameObject));
        chatPanelPosiTween.gameObject.SetActive(true);
        chatPanelPosiTween.PlayForward();
    }

    //选择了一个角色，传入角色的gameobject
    public void OnCharacterClick(GameObject go)
    {
        if (characterSelected == go)
            return;
        iTween.ScaleTo(go, new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        if (characterSelected != null)
        {
            iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
        }
        characterSelected = go;
    }

    //点击更换角色按钮
    public void OnButtonChangeCharacterClick()
    {
        //隐藏角色面板
        chatPanelPosiTween.PlayReverse();
        StartCoroutine(HidePanel(chatPanelPosiTween.gameObject));
        //显示展示角色选择面板
        characterShowTween.gameObject.SetActive(true);
        characterShowTween.PlayForward();
    }
    
    public void OnCharacterShowButtonSureClick()
    {
        //1，判断姓名输入的是否正确
        //TODO
        //2，判断是否选择角色
        //TODO
        int index = -1;
        //因为characterArray与characterSelectedArray中存放的角色只是形态不同，但是
        //下标对应的人物是一致的，index来确定characterSelectedArray的下标
        for (int i = 0; i < characterArray.Length; i++)
        {
            if (characterSelected == characterArray[i])
            {
                index = i;
                break;
            }
        }
        if (index == -1)
        {
            Debug.Log("asfasdfasfasf");
            return;
        }

        if (characterSelectedParent.childCount != 0)
            GameObject.Destroy(characterSelectedParent.GetChild(0).gameObject);// 销毁现有的角色
        //创建新选择的角色
        GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.parent = characterSelectedParent;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = new Vector3(1, 1, 1);
        //更新角色的名字和等级
        nameLabelCharacterselect.text = characternameInput.value;
        levelLabelCharacterselect.text = "Lv.1";

        OnCharacterShowButtonBackClick();
    }
    public void OnCharacterShowButtonBackClick()
    {
        characterShowTween.PlayReverse();
        StartCoroutine(HidePanel(characterShowTween.gameObject));
        chatPanelPosiTween.gameObject.SetActive(true);
        chatPanelPosiTween.PlayForward();
    }

}
