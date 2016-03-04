using UnityEngine;
using System.Collections;


public enum TaskType{
    Main,//主线任务
    Reward,//赏金任务
    Daily//日常任务
}
//任务的过程
public enum TaskProgress{
    NoStart,
    Accept,
    Complete,
    Reward
}
public class Task {

    private int id;

    public int Id
    {
      get { return id; }
      set { id = value; }
    }
    private TaskType taskTYpe;

    public TaskType TaskTYpe
    {
      get { return taskTYpe; }
      set { taskTYpe = value; }
    }


    private string name;

    public string Name
    {
      get { return name; }
      set { name = value; }
    }
    private string icon;

    public string Icon
    {
      get { return icon; }
      set { icon = value; }
    }
    private string des;

    public string Des
    {
      get { return des; }
      set { des = value; }
    }
    private int coin;

    public int Coin
    {
      get { return coin; }
      set { coin = value; }
    }
    private int diamond;

    public int Diamond
    {
      get { return diamond; }
      set { diamond = value; }
    }
    private string talkNpc;

    public string TalkNpc
    {
      get { return talkNpc; }
      set { talkNpc = value; }
    }
    private int idNpc;

    public int IdNpc
    {
      get { return idNpc; }
      set { idNpc = value; }
    }
    private int idTranscript;

    public int IdTranscript
    {
      get { return idTranscript; }
      set { idTranscript = value; }
    }
    private TaskProgress taskProgress = TaskProgress.NoStart;

    public TaskProgress TaskProgress
    {
      get { return taskProgress; }
      set 
      {
          if(TaskProgress != value)
          {
              taskProgress = value;
              OnTaskChange();
          }
          
      }
    }

    public delegate void OnTaskChangeEvent();
    public event OnTaskChangeEvent OnTaskChange;
}
