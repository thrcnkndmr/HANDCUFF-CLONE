using System;
using System.Collections;
using System.Collections.Generic;
using Blended;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public DynamicJoystick dynamicJoystick;
    public float speed;
    public float turnSpeed;

    private Animator playerAnimator;
    
    private TouchManager touchManager;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Run = Animator.StringToHash("Run");

    private void Start()
    {
        touchManager = TouchManager.Instance;
        playerAnimator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        touchManager = TouchManager.Instance;
        touchManager.onTouchMoved += OnTouchMoved;
        touchManager.onTouchEnded += OnTouchEnded;

    }

    public void OnDisable()
    {
        touchManager = TouchManager.Instance;
        touchManager.onTouchMoved -= OnTouchMoved;
        touchManager.onTouchEnded -= OnTouchEnded;

        
    }

    private void OnTouchMoved(TouchInput touchInput)
    {
        playerAnimator.SetBool("RunB",true);


        float horizontal = dynamicJoystick.Horizontal;
        float vertical = dynamicJoystick.Vertical;
        Vector3 addedPos = new Vector3(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        var transform1 = transform;
        transform1.position += addedPos;
        
        Vector3 direction = Vector3.forward * vertical + Vector3.right * horizontal;
        transform.rotation = Quaternion.Slerp(transform1.rotation, Quaternion.LookRotation(direction),turnSpeed*Time.deltaTime);
    }

    private void OnTouchEnded(TouchInput touchInput)
    {
        playerAnimator.SetBool("RunB",false);
    }
    
}
