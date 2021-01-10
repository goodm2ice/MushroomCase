using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;

    public RangedValue distance = new RangedValue(3, 12, 8);
    public RangedValue horizontalAngle = new RangedValue(0, 75, 60);
    public float yAngle = 180;
    public Vector3 rotationSpeed = new Vector3(2, 2, 2);
    public Vector2 moveSpeed = new Vector2(2, 1);

    private Transform yAxis;
    private Transform hAxis;
    private Transform cameraOrigin;
    private Vector3 initialValues;

    void Start()
    {
        if (player == null)
        {
            Transform playerOrigin = GameObject.FindGameObjectWithTag(GCM.Get("PlayerTag")).transform;
            player = playerOrigin.Find(GCM.Get("PlayerCamTargetName")) ?? playerOrigin;
        }

        yAxis = yAxis ?? transform.GetChild(0);
        hAxis = hAxis ?? yAxis.GetChild(0);
        cameraOrigin = cameraOrigin ?? hAxis.GetChild(0);

        hAxis.localPosition = Vector3.zero;
        initialValues = new Vector3(horizontalAngle.cur, yAngle, distance.cur);
    }

    void FixedUpdate()
    {
        #region Привязка к позиции и повороту персонажа
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * moveSpeed.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * rotationSpeed.z);
        #endregion

        if (Input.GetButton(GCM.Get("ResetCameraBtn")))
        {
            horizontalAngle.cur = initialValues.x;
            yAngle = initialValues.y;
            distance.cur = initialValues.z;
        }

        #region Установка расстояния от объекта до камеры
        distance.cur += Input.GetAxis(GCM.Get("ScaleCameraAxis"));
        Vector3 dD = Vector3.up * distance.cur;
        cameraOrigin.localPosition = Vector3.Lerp(cameraOrigin.localPosition, dD, Time.deltaTime * moveSpeed.y);
        cameraOrigin.LookAt(player);
        #endregion

        #region Поворот камеры
        if (Input.GetButton(GCM.Get("RotateCameraBtn")))
        {
            horizontalAngle.cur += Input.GetAxis(GCM.Get("CameraYAxis"));
            yAngle += Input.GetAxis(GCM.Get("CameraXAxis"));
        }

        // Поворот камеры под углом к горизонту
        Quaternion dH = Quaternion.AngleAxis(horizontalAngle.cur, Vector3.right);
        hAxis.localRotation = Quaternion.Slerp(hAxis.localRotation, dH, Time.deltaTime * rotationSpeed.y);

        // Поворот камеры вокруг точки
        Quaternion dY = Quaternion.AngleAxis(yAngle, Vector3.up);
        yAxis.localRotation = Quaternion.Slerp(yAxis.localRotation, dY, Time.deltaTime * rotationSpeed.x);
        #endregion
    }
}
