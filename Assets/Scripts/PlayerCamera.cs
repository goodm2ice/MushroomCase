using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCamera : MonoBehaviour
{
    public Transform player;

    public RangedValue distance = new RangedValue(3, 12, 8);
    public RangedValue horizontal = new RangedValue(0, 75, 60);
    public float yAngle = 180;
    public Vector3 rotationSpeed = new Vector3(2, 2, 2);
    public Vector2 moveSpeed = new Vector2(2, 1);

    private Transform yAxis;
    private Transform hAxis;
    private Transform cameraOrigin;
    private Vector3 initialValues;

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
        if (player == null)
        {
            Transform playerOrigin = GameObject.FindGameObjectWithTag("Player").transform;
            player = playerOrigin.Find("CamTarget") ?? playerOrigin;
        }

        yAxis = yAxis ?? transform.GetChild(0);
        hAxis = hAxis ?? yAxis.GetChild(0);
        cameraOrigin = cameraOrigin ?? hAxis.GetChild(0);

        hAxis.localPosition = Vector3.zero;
        initialValues = new Vector3(horizontal.cur, yAngle, distance.cur);
    }

    void FixedUpdate()
    {
        #region Привязка к позиции и повороту персонажа
        transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * moveSpeed.x);
        transform.rotation = Quaternion.Lerp(transform.rotation, player.rotation, Time.deltaTime * rotationSpeed.z);
        #endregion

        if (Input.GetButton("Reset Camera"))
        {
            horizontal.cur = initialValues.x;
            yAngle = initialValues.y;
            distance.cur = initialValues.z;
        }

        #region Установка расстояния от объекта до камеры
        if (Input.GetAxis(axisScaleCamera) != 0)
            distance.cur += Input.GetAxis(axisScaleCamera);
        Vector3 dD = Vector3.up * distance.cur;
        cameraOrigin.localPosition = Vector3.Lerp(cameraOrigin.localPosition, dD, Time.deltaTime * moveSpeed.y);
        cameraOrigin.LookAt(player);
        #endregion

        #region Поворот камеры
        if (Input.GetButton(axisRotateCamera))
        {
            horizontal.cur += Input.GetAxis(axisCameraY);
            yAngle += Input.GetAxis(axisCameraX);
        }

        // Поворот камеры под углом к горизонту
        Quaternion dH = Quaternion.AngleAxis(horizontal.cur, Vector3.right);
        hAxis.localRotation = Quaternion.Slerp(hAxis.localRotation, dH, Time.deltaTime * rotationSpeed.y);

        // Поворот камеры вокруг точки
        Quaternion dY = Quaternion.AngleAxis(yAngle, Vector3.up);
        yAxis.localRotation = Quaternion.Slerp(yAxis.localRotation, dY, Time.deltaTime * rotationSpeed.x);
        #endregion
    }
}
