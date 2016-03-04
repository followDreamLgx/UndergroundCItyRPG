using UnityEngine;
using System.Collections;

public class SkillButton : MonoBehaviour {

    public PosType posType = PosType.Basic;
    private PlayerAnimation playerAnimation;
    public float coldTime = 4;
    private float coldTimer = 0;//剩余的冷却时间
    private UISprite maskSprite;
    private UIButton btn;

    void Start()
    {
        if(transform.Find("Mask") != null)//处理basic类型
        {
            maskSprite = transform.Find("Mask").GetComponent<UISprite>();
        }
        btn = this.GetComponent<UIButton>();
        playerAnimation = TransriptManager._instance.player.GetComponent<PlayerAnimation>();
    }
    void Update()
    {
        if (maskSprite != null)//排除basic按钮
        {
            if (coldTimer > 0)
            {
                coldTimer -= Time.deltaTime;
                maskSprite.fillAmount = coldTimer / coldTime;
            }
            else if(coldTimer < 0)
            {
                Enable();
            }
            else
            {
                maskSprite.fillAmount = 0;
            }

        }
    }
	
    void OnPress(bool isPress)//也可以是OnPress
    {
        playerAnimation.OnAttackButtonClick(isPress,posType);
        if(isPress == true && maskSprite != null)
        {
            coldTimer = coldTime;
            Disable();
        }
    }
    void Disable()
    {
        Collider colliderLgx = this.GetComponent<Collider>();
        colliderLgx.enabled = false;
        btn.SetState(UIButtonColor.State.Normal, true);
    }
    void Enable()
    {
        Collider colliderLgx = this.GetComponent<Collider>();
        colliderLgx.enabled = true;
        btn.SetState(UIButtonColor.State.Normal, true);
    }
}
