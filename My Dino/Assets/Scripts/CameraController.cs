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


    public static CameraDirection cameraDirection = CameraDirection.DINO;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = GetMoveToPosition();
        //transform.LookAt(GetTargetPosition());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveToPosition = GetMoveToPosition();
        //Vector3 targetPosition = GetTargetPosition();


        // Move to the target position smoothly
        MoveAndFaceAtTargetSmooth(moveToPosition);
    }

    private void MoveAndFaceAtTargetSmooth(Vector3 moveTo)
    {
        transform.position = Vector3.MoveTowards(transform.position, moveTo, movingProportion);
        //transform.LookAt(target);
    }

    private Vector3 GetMoveToPosition()
    {
        switch (cameraDirection)
        {
            case CameraDirection.DINO: return dinoCameraPosition;
            case CameraDirection.FB: return fbCameraPosition;
        }

        return Vector3.zero;
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
