using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        animator.SetBool("Run", isRunning);
        animator.SetBool("Idle", !isRunning);
    }
}