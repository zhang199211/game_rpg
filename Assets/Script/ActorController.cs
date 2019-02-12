using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;
    public PlayerInput pi;

    public float moveSpeed;
    public float runMultiplier=2;

    [SerializeField]
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 movingVec;

    private void Awake()
    {
        pi = gameObject.GetComponent<PlayerInput>();
        anim = model.GetComponent<Animator>();

        rigid = gameObject.GetComponent<Rigidbody>();


    }

    private void Update()
    {
        float targetRunMulti = (pi.run ? 2.0f : 1.0f);
        anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), targetRunMulti, 0.1f));
        if (pi.jump)
        {
            anim.SetTrigger("jump");
        }
        if (pi.Dmag > 0.1f)
        {
            Vector3 targetForward = Vector3.Slerp(model.transform.forward, pi.DVec,0.3f);
            model.transform.forward = targetForward;
        }
        movingVec = pi.Dmag * model.transform.forward* moveSpeed*(pi.run? runMultiplier : 1.0f);
    }

    private void FixedUpdate()
    {
        rigid.position += movingVec * Time.fixedDeltaTime;
    }
}
