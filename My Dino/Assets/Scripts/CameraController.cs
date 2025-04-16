using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public enum CameraDirection
{
    DINO,
    FB,
}

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 dinoCameraPosition;
    [SerializeField] Vector3 fbCameraPosition;
    //[SerializeField] Transform centerReference;
    [SerializeField] float movingProportion;
    [SerializeField] Transform dino;

    private CameraDirection lastCameraDirection;
    public static CameraDirection cameraDirection = CameraDirection.DINO;
    private float slowSpeed = 0.04f;
    private float fastSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        transform.position = GetMoveToPosition();
        //transform.LookAt(GetTargetPosition());
        // speed = movingProportion;
        lastCameraDirection = cameraDirection;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraDirection != lastCameraDirection)
        {
            UpdateSpeed();
            lastCameraDirection = cameraDirection;
        }

        Vector3 moveToPosition = GetMoveToPosition();
        //Vector3 targetPosition = GetTargetPosition();

        // Move to the target position smoothly
        MoveAndFaceAtTargetSmooth(moveToPosition);
    }

    private void UpdateSpeed()
    {
        if (lastCameraDirection == CameraDirection.DINO && cameraDirection == CameraDirection.FB)
        {
            movingProportion = slowSpeed;
        }
        else if (lastCameraDirection == CameraDirection.FB && cameraDirection == CameraDirection.DINO)
        {
            movingProportion = fastSpeed;
        }
    }


    private void MoveAndFaceAtTargetSmooth(Vector3 moveTo)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTo, movingProportion);
        //transform.LookAt(target);
    }

    private Vector3 GetMoveToPosition()
    {
        // switch (cameraDirection)
        // {
        //     case CameraDirection.DINO:
        //         movingProportion = 0.01f;
        //         return dinoCameraPosition;
        //     case CameraDirection.FB:
        //         movingProportion = speed;
        //         return fbCameraPosition;
        // }

        // return Vector3.zero;

        return cameraDirection == CameraDirection.DINO ? dinoCameraPosition : fbCameraPosition;
    }

    //private Vector3 GetTargetPosition()
    //{
    //    switch (cameraDirection)
    //    {
    //        case CameraDirection.DINO: return initialCameraPosition;
    //        case CameraDirection.FB: return centerReference.position;
    //    }

    //    return Vector3.zero;
    //}
}