using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    Transform cameraTrans;
    public int panSpeed;
    public int rotationAmount;
    public Vector3 zoomAmount;
    public int moveTime;
    Vector3 newZoom;
    // Start is called before the first frame update
    void Start()
    {
        cameraTrans = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var newPos = new Vector3(0, 0, 0);
        var newRotation = new Vector3(0, 0, 0);
        newZoom = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            Debug.Log("W or up");
            newPos += transform.forward * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            Debug.Log("S or down");
            newPos -= transform.forward * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("D or right");
            newPos += transform.right * panSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("A or left");
            newPos -= transform.right * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q");
            newRotation += Vector3.up * rotationAmount;
        }

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E");
            newRotation += Vector3.down * rotationAmount;
        }
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("R");
            newPos += Vector3.up * panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("F");
            newPos += Vector3.down * panSpeed * Time.deltaTime;
        }
        transform.Translate(newPos, Space.Self);
        transform.Rotate(newRotation * moveTime * Time.deltaTime, Space.Self);
    }
}
