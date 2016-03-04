using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float velocity = 5;
    private Animator anim;
    
	// Use this for initialization
	void Start () {
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Rigidbody rigid = this.GetComponent<Rigidbody>();
        Vector3 nowVel = rigid.velocity;
        if(Mathf.Abs(h) > 0.1f || Mathf.Abs(v) > 0.1f)
        {
            //Empty State说明主角没有在进行攻击，只有不处于攻击状态时才能进行移动
            if(anim.GetCurrentAnimatorStateInfo(1).IsName("EmptyState") == true)
            {
                rigid.velocity = new Vector3(velocity * h, nowVel.y, velocity * v);
                anim.SetBool("Move", true);
                transform.LookAt(new Vector3(h, 0, v) + transform.position);
            }
            else
                rigid.velocity = new Vector3(0, nowVel.y, 0);
        }
        else
        {
            rigid.velocity = new Vector3(0, nowVel.y, 0);
            anim.SetBool("Move", false);
        }
	}
}
