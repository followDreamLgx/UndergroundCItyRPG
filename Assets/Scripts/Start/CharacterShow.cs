using UnityEngine;
using System.Collections;

public class CharacterShow : MonoBehaviour {

	public void OnPress(bool isPress)
    {
        if (isPress == false)
        {
            StartMenuController._instance.OnCharacterClick(this.transform.parent.gameObject);
        }
    }
}
