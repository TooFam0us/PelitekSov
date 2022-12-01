using System.Collections;
using System.Collections.Generic;
// using System.Diagnostics;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    
    private Vector3 previousPosition;
    private float prevX;
    private const float maxX = 85;
    private const float minX = 10;

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

            float rotationAroundYAxis = -direction.x * 180; // cam moves horizontally
            float rotationAroundXAxis = direction.y * 180; // cam moves vertically
            float xAngle = prevX + rotationAroundXAxis;
            Debug.Log(xAngle);

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

            cam.transform.rotation = Quaternion.Euler(40, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
            cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);

            
            previousPosition = newPosition;
            prevX = cam.transform.localEulerAngles.x;
        }

        cam.transform.position = target.position;
        cam.transform.Translate(new Vector3(0, 0, -8)); // -8 = distance from player

        //Debug.Log(cam.transform.localEulerAngles.x);

    }
}
