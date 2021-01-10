using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;

    public RangedValue distance = new RangedValue(3, 10, 5);
    public RangedValue horizontal = new RangedValue(-240, 240, -240);
    public float yAngle = 0;
    public Vector4 speed = new Vector4(2, 2, 2, 1);

    private Transform hAxis;
    private Transform camera;

    [SerializeField]
    private string axisCameraX = "Camera X";
    [SerializeField]
    private string axisCameraY = "Camera Y";
    [SerializeField]
    private string axisRotateCamera = "Rotate Camera";
    [SerializeField]
    private string axisScaleCamera = "Scale Camera";

    void Start()
    {
        if (hAxis == null)
            hAxis = transform.GetChild(0);
        if (camera == null)
            camera = hAxis.GetChild(0);

        hAxis.localPosition = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (Input.GetButton(axisRotateCamera))
        {
            horizontal.cur -= Input.GetAxis(axisCameraY);
            yAngle += Input.GetAxis(axisCameraX);
        }

        if(Input.GetAxis(axisScaleCamera) != 0) {
            distance.cur += Input.GetAxis(axisScaleCamera);
        }

        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * speed.w);

        Vector3 dD = Vector3.up * distance.cur;
        camera.localPosition = Vector3.Lerp(camera.localPosition, dD, Time.deltaTime * speed.z);
        camera.LookAt(player);

        Quaternion dH = Quaternion.AngleAxis(horizontal.cur / (float)Math.PI, Vector3.right);
        hAxis.localRotation = Quaternion.Slerp(hAxis.localRotation, dH, Time.deltaTime * speed.y);
        
        Quaternion dY = Quaternion.AngleAxis(yAngle / (float)Math.PI, Vector3.up);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, dY, Time.deltaTime * speed.x);
    }
}
