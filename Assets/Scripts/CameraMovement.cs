using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed;
    [SerializeField] private float scrollSpeedSpeed;
    [SerializeField] private float panBorderThickness;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;


    private bool isBlocked;
    
    void Update()
    {
        if (GameManager.instance.gameIsOver)
        {
            enabled = false;
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
            isBlocked = !isBlocked;

        if (isBlocked)
            return;

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        float pos = transform.position.y;
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos -= scroll * scrollSpeedSpeed * Time.deltaTime;
        pos = Mathf.Clamp(pos, minY, maxY);
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
