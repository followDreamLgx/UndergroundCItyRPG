﻿using UnityEngine;
using System.Collections;

public class BtnTranscript : MonoBehaviour {

    public int id;//副本的ID
    public int needLevel;
    public string scenceName;
    public string des = "这里是一个阴森恐怖的地方，你敢出来溜溜吗";

	public void OnClick()
    {
        transform.parent.SendMessage("OnButtonTranscriptClick", this);
    }
}
