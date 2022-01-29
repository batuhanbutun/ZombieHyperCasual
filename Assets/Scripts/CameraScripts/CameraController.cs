using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    public float mouseSensitivity = 200f;
    public float xRotation = 0f;
    public Transform playerBody;

    void Update()
    {

        if (_gameStats.isGameOver == false)
        {
            float mouseX = (Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime);
            float mouseY = (Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX, Space.Self);
        }
           
        
      

    }
}
