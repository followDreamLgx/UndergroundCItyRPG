using UnityEngine;
using System.Collections;

public class SkillItemUi : MonoBehaviour {

    public PosType posType;
    private Skill skill;
    private UISprite sprite;
    private UIButton button;
    public bool isSelect = false;

    public UIButton Button
    {
        get 
        {
            if (button == null)
                button = this.GetComponent<UIButton>();
            return button; 
        }

        set { button = value; }
    }

    public UISprite Sprite
    {
        get 
        {
            if(sprite == null)
            {
                sprite = this.GetComponent<UISprite>();
            }
            return sprite; 
        }
        set { sprite = value; }
    }
    void Awake()
    {
        

    }
    void Start()
    {
        UpdateShow();
        if (isSelect)
            OnClick();
    }

    void UpdateShow()
    {
        skill = SkillManageer._instance.GetSkillByPosition(posType);
        Sprite.spriteName = skill.Icon;
        Button.normalSprite = skill.Icon;
    }
    void OnClick()
    {
        transform.parent.parent.SendMessage("OnSkillClick", skill);
    }

}
