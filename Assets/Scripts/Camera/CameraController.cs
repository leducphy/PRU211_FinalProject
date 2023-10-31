using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset = new Vector3(0, 0, 0);  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        Vector3 camPosition = player.transform.position;
        camPosition.z = transform.position.z;
        transform.position = camPosition + offset;

    }
}
