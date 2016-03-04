using UnityEngine;
using System.Collections;

public class PlayerEffect : MonoBehaviour {

    private Renderer[] renderArr;
    private NcCurveAnimation[] curveAnimArray;

    void Start()
    {
        renderArr = this.GetComponentsInChildren<Renderer>();
        curveAnimArray = this.GetComponentsInChildren<NcCurveAnimation>();
    }

    public void Show()
    {
        foreach (Renderer renderer in renderArr)
            renderer.enabled = true;
        foreach (NcCurveAnimation anim in curveAnimArray)
            anim.ResetAnimation();
    }
}
