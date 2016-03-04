using UnityEngine;
using System.Collections;

public class ServerProperty : MonoBehaviour {

    public string ip;
    public string name
    {
        set
        {
            UILabel label = transform.FindChild("Label").GetComponent<UILabel>();
            label.text = value;
        }
        get
        {
            return name;
        }
    }
    public int count;
    public void ServerSelect()
    {
        transform.root.SendMessage("OnServerSelect", transform.gameObject);
    }
}
