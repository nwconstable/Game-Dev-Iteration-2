using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            cameraTransform.Translate(Vector3.left * Time.deltaTime * cameraSpeed);
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            cameraTransform.Translate(Vector3.right * Time.deltaTime * cameraSpeed);
        }
        if((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            cameraTransform.Translate(Vector3.up * Time.deltaTime * cameraSpeed);
        }
        if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)))
        {
            cameraTransform.Translate(Vector3.down * Time.deltaTime * cameraSpeed);
        }
    }
}
