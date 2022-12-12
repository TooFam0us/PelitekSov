using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Transform plr;

    private float camDistance = -6f;
    private Vector3 previousPosition;
    private float prevX;
    private const float maxX = 85;
    private const float minX = 10;
    private Vector3 velocity = Vector3.zero;
    private float prevZ = -3f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;
            //Debug.Log(direction.y);
            //Debug.Log(cam.transform.localEulerAngles.x);

            float rotationAroundYAxis = -direction.x * 250; // cam moves horizontally
            //float rotationAroundXAxis = direction.y * 180; // cam moves vertically
            //float xAngle = prevX + rotationAroundXAxis;
            //Debug.Log(xAngle);

            //if (xAngle > maxX)
            //{
            //    if (xAngle < minX)
            //    {
            //        cam.transform.rotation = Quaternion.Euler(maxX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            //    }
            //    else
            //    {
            //        if (xAngle < minX)
            //        {
            //            cam.transform.rotation = Quaternion.Euler(minX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            //        }
            //        else
            //        {
            //            cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            //        }
            //    }
            //}

            //if (xAngle > maxX)
            //{
            //    if (xAngle < minX)
            //    {
            //        cam.transform.rotation = Quaternion.Euler(maxX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            //    }
            //    else
            //    {
            //        cam.transform.rotation = Quaternion.Euler(minX, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            //    }
            //}
            //else
            //{
            //    cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            //}

            //cam.transform.rotation = Quaternion.Euler(40, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
            
            previousPosition = newPosition;
            prevX = cam.transform.localEulerAngles.x;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (camDistance < -3.5f)
            {
                camDistance += 0.5f;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (camDistance > -7.5f)
            {
                camDistance -= 0.5f;
            }
        }

        target.position = Vector3.SmoothDamp(target.position, plr.position, ref velocity, 0.15f);
        cam.transform.position = target.position;
        //cam.transform.Translate(new Vector3(0, 1.4f, camDistance));
        float zLerp = Mathf.Lerp(prevZ, camDistance, 0.1f);
        cam.transform.Translate(new Vector3(0, 1.4f, zLerp));
        prevZ = zLerp;

    }
}
