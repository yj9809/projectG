using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    private Camera thisCamera;

    private float moveSpeed = 5f;
    private float rotationSpeed = 2f;
    private float zoomSpeed = 10f;
    private float yRotation = 0f;
    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        ////카메라 이동
        Vector3 moveDirection = new Vector3(x, 0f, y).normalized;
        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(move);

        // 카메라 회전
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yRotation -= mouseY * rotationSpeed;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX * rotationSpeed);
            transform.localEulerAngles = new Vector3(yRotation, transform.localEulerAngles.y, 0f);
        }

        //카메라 줌
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float nowZoom = thisCamera.fieldOfView - zoom;

        thisCamera.fieldOfView = Mathf.Clamp(nowZoom, 40f, 60f);
    }
}
