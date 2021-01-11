using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {
    public float runSpeed = 4;
    public float forwardSpeed = 2;
    public float backwardSpeed = 0.75f;
    public float rotationSpeed = 100;
    public bool lookAtCamera = false;

    private Animator playerAnimator;
    private Transform head;
    private Transform camera;

    private void Start()
    {
        playerAnimator = transform.GetComponentInChildren<Animator>();
        head = transform.Find("Head").parent;
        camera = GameObject.Find("PlayerCamera").GetComponentInChildren<Camera>().transform;
    }

    void FixedUpdate()
    {
        float rawV = Input.GetAxis("Vertical");
        rawV *= rawV > 0 ? (Input.GetButton("Run") ? runSpeed : forwardSpeed) : backwardSpeed;
        
        playerAnimator?.SetBool("walk", rawV != 0);
        playerAnimator?.SetBool("mirror", rawV < 0);
        playerAnimator?.SetFloat("speed", rawV != 0 ? Math.Abs(rawV) / 2 : 1);
        
        Vector3 dP = transform.forward * rawV;
        transform.position += dP * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);

        if(head && camera && lookAtCamera)
            head.LookAt(camera);
    }
}
