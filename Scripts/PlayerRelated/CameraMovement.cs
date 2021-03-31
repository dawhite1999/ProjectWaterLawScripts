using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * StaticMan.mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * StaticMan.mouseSens * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60, 60);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    public void InitializeMouse()
    {
        StaticMan.mouseSens = PlayerPrefs.GetFloat("MouseSensitivity", 100);
        FindObjectOfType<PauseMan>().mouseSlider.value = StaticMan.mouseSens;
    }
}

