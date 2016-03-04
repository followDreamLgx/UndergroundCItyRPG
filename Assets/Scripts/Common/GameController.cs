using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //计算角色升级所需要的经验
	public static int GetRequireExpByLevel(int level)
    {
        return (int)((level - 1f) * (100 + (100 + 10f * (level - 2f))) / 2f);
    }
}
