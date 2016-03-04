using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {

    private Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }


    public void OnAttackButtonClick(bool isPress,PosType posType)
    {
        if(posType == PosType.Basic)
        {
            if (isPress == true)
                anim.SetTrigger("Attack");
        }
        else
        {   //PosType设定了具体数值，可以当做整数使用
            //按键按下时，设置为true，抬起时设置为false
            anim.SetBool("Skill" + (int)posType,isPress);
        }
    }
}
