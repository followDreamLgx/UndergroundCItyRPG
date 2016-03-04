using UnityEngine;
using System.Collections;

public class TaskManager : MonoBehaviour {

    public static TaskManager _instance;
    public TextAsset taskInfoText;
    private ArrayList taskList = new ArrayList();
    private Task currentTask;
    private PlayerAutoMove playerAutoMove;

    public PlayerAutoMove PlayerAutoMove
    {
        get 
        {
            if (playerAutoMove == null)
                playerAutoMove = GameObject.FindWithTag("Player").GetComponent<PlayerAutoMove>();
            return playerAutoMove; 
        }
    }
    

    //
    /// <summary>
    /// 初始化任务信息
    /// </summary>
    
    void Awake()
    {
        _instance = this;
        InitTask();
        
        
    }

    public void InitTask()
    {
        string[] taskInfoArrary = taskInfoText.ToString().Split('\n');
        foreach(string str in taskInfoArrary)
        {
            string[] proArray = str.Split('|');
            Task task = new Task();
            task.Id = int.Parse(proArray[0]);
            switch(proArray[1])
            {
                case "Main":
                    task.TaskTYpe = TaskType.Main;
                    break;
                case "Reward":
                    task.TaskTYpe = TaskType.Reward;
                    break;
                case "Daily":
                    task.TaskTYpe = TaskType.Daily;
                    break;
            }
            task.Name = proArray[2];
            task.Icon = proArray[3];
            task.Des = proArray[4];
            task.Coin = int.Parse(proArray[5]);
            task.Diamond = int.Parse(proArray[6]);
            task.TalkNpc = proArray[7];
            task.IdNpc = int.Parse(proArray[8]);
            task.IdTranscript = int.Parse(proArray[9]);
            
            taskList.Add(task);
        }
    }
    public ArrayList GetTaskList()
    {
        return taskList;
    }

    //执行某个任务
    public void OnExcuteTask(Task task)
    {
        currentTask = task;
        if(task.TaskProgress == TaskProgress.NoStart)
        {   //导航到NPC处接受任务
            PlayerAutoMove.SetDestination(NPCManager._instance.GetNpcById(task.IdNpc).transform.position);
        }
        else if(task.TaskProgress ==  TaskProgress.Accept)
        {
            playerAutoMove.SetDestination(NPCManager._instance.transript.transform.position);
        }
    }

    public void OnAcceptTask()
    {
        currentTask.TaskProgress = TaskProgress.Accept;
        //寻路到副本入口
        OnExcuteTask(currentTask);
    }
	public void OnArriveDestination()
    {
        if(currentTask.TaskProgress == TaskProgress.NoStart)//到达NPC的位置
        {   
            NPCDialogUI._instance.Show(currentTask.TalkNpc);//显示NPC的对话
        }
        else//到达副本入口
        {
            
        }
    }
}
