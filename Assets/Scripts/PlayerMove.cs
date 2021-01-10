using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour {
    public float runSpeed = 4;
    public float forwardSpeed = 2;
    public float backwardSpeed = 0.75f;
    public float rotationSpeed = 100;

    void FixedUpdate()
    {
        float rawV = Input.GetAxis("Vertical");
        rawV *= rawV > 0 ? (Input.GetButton("Run") ? runSpeed : forwardSpeed) : backwardSpeed;
        Vector3 dP = transform.forward * rawV;
        transform.position += dP * Time.deltaTime;
        transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }
}
