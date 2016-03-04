using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAttack : MonoBehaviour {


    private Dictionary<string, PlayerEffect> effectDict = new Dictionary<string, PlayerEffect>();

    void Start()
    {
        PlayerEffect[] peArray = this.GetComponentsInChildren<PlayerEffect>();//这里修改为了GetComponents
        foreach(PlayerEffect pe in peArray)
        {
            effectDict.Add(pe.gameObject.name, pe);//将特效加入字典
        }
    }
    //0 normal skill1 skill2 skill3
    //1 effect name
    //2 sound name
    //3 move forward
    //4 jump height
	void Attack(string args)
    {
        string []proArray = args.Split(',');
        //1 show effect
        string effectName = proArray[1];
        ShowPlayerEffect(effectName);
        //2 play sound
        string soundName = proArray[2];
        SoundManager._instance.Play(soundName);
        //3 move forward,主角在使用技能时前冲的效果
        float moveForward = float.Parse(proArray[3]);
        if(moveForward > 0.1)
        {
            iTween.MoveBy(this.gameObject, Vector3.forward * moveForward, 0.3f);
        }
    }
    void ShowPlayerEffect(string effectName)
    {
        PlayerEffect pe;
        if(effectDict.TryGetValue(effectName,out pe) == true)
        {
            pe.Show();
        }

    }
}
