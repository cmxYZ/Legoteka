using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float sensivity = 3.0f;

    private float rotationX;
    private float rotationY;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distanceFromTarget = 10.0f;

    private Vector3 currentRotation;
    private Vector3 smoothVelocity = Vector3.zero;

    [SerializeField]
    private float smoothTime = 3.0f;

    public float zoomOutMin = 1;
    public float zoomOutMax = 7;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            float x = Input.GetAxis("Mouse X") * sensivity;
            float y = Input.GetAxis("Mouse Y") * -sensivity;

            rotationY += x;
            rotationX += y;

            rotationX = Mathf.Clamp(rotationX, -40, 40);

            Vector3 nextRotation = new Vector3(rotationX, rotationY);
            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity, smoothTime);

            transform.localEulerAngles = currentRotation;

            transform.position = target.position - transform.forward * distanceFromTarget;
        }
        else if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
    }

    void zoom(float inc)
    {
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - inc, zoomOutMin, zoomOutMax);
    }
}
