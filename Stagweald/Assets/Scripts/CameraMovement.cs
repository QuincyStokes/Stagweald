using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Sensitivity")]
    public float xSensitivity;
    public float ySensitivity;

    [Header("References")]
    public Transform orientation;
    public Transform crossbow;

    private float xRotation;
    private float yRotation;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void LateUpdate()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        //y rotation is movement along x axis, add it to rotate in that direction
        yRotation += mouseX;
        //x rotation is movement slong y axis, subtract it to rotate in that direction
        xRotation -= mouseY;
        //constrict rotation looking left and right, player is not an owl
        xRotation = Mathf.Clamp(xRotation,-90f, 90);

        //apply the rotation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        transform.position = orientation.position;
        
    }
}
