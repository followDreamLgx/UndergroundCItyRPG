﻿using UnityEngine;
using System.Collections;

public class SkillManageer : MonoBehaviour {

    public static SkillManageer _instance;
    public TextAsset skillImfoText;
    private ArrayList skillList = new ArrayList();

    void Awake()
    {
        _instance = this;
        InitSkill();

    }

    void InitSkill()
    {
        string []skillArray = skillImfoText.ToString().Split('\n');
        foreach(string str in skillArray)
        {
            string[] proArray = str.Split(',');
            Skill skill = new Skill();
            skill.Id = int.Parse(proArray[0]);
            skill.Name = proArray[1];
            skill.Icon = proArray[2];
            switch(proArray[3])
            {
                case "Warrior":
                    skill.PlayerType = PlayerType.Warrior;
                    break;
                case "FemaleAssassin":
                    skill.PlayerType = PlayerType.FemaleAssassin;
                    break;
            }
            switch(proArray[4])
            {
                case "Basic":
                    skill.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    skill.SkillType = SkillType.Skill;
                    break;
            }
            switch(proArray[5])
            {
                case "Basic":
                    skill.PosType = PosType.Basic;
                    break;
                case "One":
                    skill.PosType = PosType.One;
                    break;
                case "Two":
                    skill.PosType = PosType.Two;
                    break;
                case "Three":
                    skill.PosType = PosType.Three;
                    break;
            }
            skill.ColdTime = int.Parse(proArray[6]);
            skill.Damage = int.Parse(proArray[7]);
            skill.Level = 1;
            skillList.Add(skill);
        }
    }

    public Skill GetSkillByPosition(PosType posType)
    {
        PlayerImfor info = PlayerImfor._instance;
        foreach(Skill skill in skillList)
        {
            if (skill.PlayerType == info.PlayerType
                && skill.PosType == posType)
                return skill;
        }
        return null;
    }
}
