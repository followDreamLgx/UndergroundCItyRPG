using UnityEngine;
using System.Collections;

public class LoadScenceProgressBar : MonoBehaviour {

    public static LoadScenceProgressBar _instance;
    private GameObject bg;
    private UISlider progressBar;
    private bool isAsyn = false;
    private AsyncOperation ao = null;
    void Awake()
    {
        _instance = this;
        bg = this.transform.Find("Bg").gameObject;
        
        progressBar = transform.Find("Bg/ProgressBar").GetComponent<UISlider>();

        //Application.LoadLevelAsync(2);//异步加载场景
        gameObject.SetActive(false);
    }
    public void Show(AsyncOperation ao)
    {
        gameObject.SetActive(true);
        isAsyn = true;
        this.ao = ao;
    }
    void Update()
    {
        if(isAsyn)//开始导入场景
        {
            progressBar.value = ao.progress;
        }
    }
}
