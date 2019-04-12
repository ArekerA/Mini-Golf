using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 5;
    public float smoothTime = 0.3F;
    public float collisionOffset = 1f;
    public float distanceToResetCameraPosition = 30f;
    public Vector3 offset;
    float horizontal;
    float vertical;
    Quaternion rotation;
    Touch touch;
    private Vector3 velocity = Vector3.zero;
    private bool isOnCollision;
    private Vector3 newPosition;

    void Start()

    {
        StaticUI.cameraSensitivity = 1;
        isOnCollision = false;
        offset = target.transform.position - transform.position;
        transform.position = target.transform.position - (rotation * offset);
#if UNITY_EDITOR
        rotateSpeed /= 2;
        #elif UNITY_ANDROID
            rotateSpeed /= 40;
        #endif
    }

    void Update()
    {
        #if UNITY_EDITOR
            MouseInput();
        #elif UNITY_ANDROID
            TouchInput();
        #endif
       if (!StaticUI.isButtonPressed)
        {
            target.transform.Rotate(vertical, horizontal, 0);
            rotation = Quaternion.Euler(target.transform.eulerAngles.x, target.transform.eulerAngles.y, 0);
            newPosition = target.transform.position - (rotation * offset);

            Debug.DrawRay(transform.position, (newPosition - transform.position)*15, Color.white);
            if (Physics.Raycast(transform.position, newPosition - transform.position, out RaycastHit hit, Vector3.Distance(transform.position, newPosition) + collisionOffset))
            {
                Debug.Log("Raycast hit " + hit.transform.gameObject.name);
                target.transform.Rotate(-vertical, -horizontal, 0);
            }
            else
            {
                transform.position = newPosition;
                Debug.DrawRay(transform.position, (target.transform.position - (rotation * offset)) - transform.position, Color.white);
            }
            if(Vector3.Distance(transform.position, newPosition) > distanceToResetCameraPosition)
            {
                transform.position = newPosition;
            }
            transform.LookAt(target.transform);
        }
    }
    void MouseInput()
    {
        if (StaticUI.isPauseMenu == false)
        {
            horizontal = Input.GetAxis("Mouse X") * rotateSpeed * StaticUI.cameraSensitivity;
            vertical = Input.GetAxis("Mouse Y") * rotateSpeed * StaticUI.cameraSensitivity;
        }
    }
    void TouchInput()
    {
        if (StaticUI.isPauseMenu == false)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    horizontal = touch.deltaPosition.x * rotateSpeed*StaticUI.cameraSensitivity;
                    vertical = touch.deltaPosition.y * rotateSpeed * StaticUI.cameraSensitivity;
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Stationary)
                {
                    horizontal = 0;
                    vertical = 0;
                }
            }
            else
            {
                horizontal = 0;
                vertical = 0;
            }
        }
    }

}
