using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour
{
    public float distance = 3;
    public float yAngle = 0;
    public RangedValue hValue = new RangedValue(-240, 240, -240);

    private Transform hAxis;
    private Transform yAxis;

    void Start()
    {
        if (hAxis == null)
            hAxis = transform.parent;
        if (yAxis == null)
            yAxis = hAxis.parent;

        hAxis.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Rotate Camera"))
        {
            hValue.cur -= Input.GetAxis("Camera Y");
            yAngle += Input.GetAxis("Camera X");
        }

        transform.localPosition = new Vector3(0, distance, 0);
        transform.LookAt(hAxis);
        hAxis.localRotation = Quaternion.Euler(hValue.cur / (float)Math.PI, 0, 0);
        yAxis.localRotation = Quaternion.Euler(0, yAngle / (float)Math.PI, 0);
    }
}
