using UnityEngine;
using System.Collections;

public class MouseOr : MonoBehaviour 
{
    public Transform target;
    public float distance = 10.0f;
    public float minDist = 2.0f;
    public float maxDist = 10.0f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20;
    public float yMaxLimit = 80;

    private float x = 0.0f;
    private float y = 0.0f;

    public void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    public void LateUpdate()
    {
        if (target != null)
        {
            if (Input.GetMouseButton(1))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }


            if (y < -360)
                y += 360;
            if (y > 360)
                y -= 360;


            y = Mathf.Clamp(y, yMaxLimit, yMinLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.09f);
            transform.position = Vector3.Slerp(transform.position, position, 0.09f);

            zoomCamera();
        }
    }

    public void zoomCamera()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (distance != maxDist)
                distance++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (distance != minDist)
                distance--;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
