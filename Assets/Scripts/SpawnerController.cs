using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] GameObject prefabGameObject;
    [SerializeField] GameObject SpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefabGameObject, SpawnPoint.transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
