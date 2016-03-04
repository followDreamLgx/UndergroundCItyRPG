﻿using UnityEngine;
using System.Collections;

public class NPCDialogUI : MonoBehaviour {

    public static NPCDialogUI _instance;
    private TweenPosition tween;
    private UILabel npcTalkLabel;
    private UIButton acceptButton;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        npcTalkLabel = transform.Find("Label").GetComponent<UILabel>();
        acceptButton = transform.Find("AcceptButton").GetComponent<UIButton>();
        EventDelegate ed = new EventDelegate(this, "OnAccept");
        acceptButton.onClick.Add(ed);
    }
	// Use this for initialization
	void Start () {
	
	}
	

    public void Show(string npcTalk)
    {
        npcTalkLabel.text = npcTalk;
        tween.PlayForward();
    }

    void OnAccept()
    {
        //通知任务管理器已经接收
        TaskManager._instance.OnAcceptTask();
        //隐藏对话框
        tween.PlayReverse();
    }
}
