using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour 
{
    public float angle = 3.5f;

    public void FixedUpdate()
    {
        transform.Rotate(Vector3.right, angle * Time.deltaTime);
    }
}
