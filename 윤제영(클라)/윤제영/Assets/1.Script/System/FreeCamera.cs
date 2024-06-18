using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FreeCamera : MonoBehaviour
{
    private Camera thisCamera;

    private float nomalMove = 5f;
    private float fastMove = 10f;
    private float rotationSpeed = 2f;
    private float zoomSpeed = 10f;
    private float yRotation = 0f;

    private float minX = -20f;
    private float maxX = 20f;
    private float minY = 5f;
    private float maxY = 20f;
    private float minZ = -120f;
    private float maxZ = -40f;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }
    private void Update()
    {
        CameraMove();
    }
    private void CameraMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) ? fastMove : nomalMove;
        ////카메라 이동
        Vector3 moveDirection = new Vector3(x, 0f, y).normalized;
        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;

        transform.Translate(move);

        // 카메라 회전 및 카메라 상승 하강
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yRotation -= mouseY * rotationSpeed;
            yRotation = Mathf.Clamp(yRotation, -90f, 90f);

            transform.Rotate(Vector3.up * mouseX * rotationSpeed);
            transform.localEulerAngles = new Vector3(yRotation, transform.localEulerAngles.y, 0f);

            if (Input.GetKey(KeyCode.E))
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }
        }

        RaycastHit hit;
        Vector3 desiredPosition = transform.position;

        if (Physics.Linecast(Camera.main.transform.position, desiredPosition, out hit))
        {
            Debug.Log("충돌");
            // 충돌이 감지되면 카메라 위치를 조정
            transform.position = hit.point;
        }

        // 카메라 이동 제한
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;

        //카메라 줌
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        float nowZoom = thisCamera.fieldOfView - zoom;

        thisCamera.fieldOfView = Mathf.Clamp(nowZoom, 40f, 60f);
    }
}
