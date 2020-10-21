﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Cinemachine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public float aimDuration = 0.1f; // 조준하는데 걸리는 시간.
    public Rig aimLayer;

    Camera mainCamera; // 카메라의 y축 회전에 대한 캐릭터 대응을 하기위한 카메라값 가져올 용도로 선언

    public Transform cameraLookAt; // vCam 쳐다보는곳 

    public AxisState xAxis; // 시네머신 FreeLook 카메라에 있는 요소 가져오기 --> 에디터 상에서 수치 조절해줄 것.
    public AxisState yAxis;

    void Start()
    {
        mainCamera = Camera.main;
    }


    private void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y; // 카메라의 y축 각도
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime); // 캐릭터가 카메라의 회전에 대응하여 회전 하는 것

        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);

        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
    }

    private void Update()
    {
        if(Input.GetButton("Fire2"))
        {
            aimLayer.weight += Time.deltaTime / aimDuration;
        }
        else
        {
            aimLayer.weight -= Time.deltaTime / aimDuration;
        }
    }
}