using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRelocate : MonoBehaviour
{
    private List<Transform> EnvironmentItems;
    // Start is called before the first frame update
    void Start()
    {
        Transform parentTransform = transform;
        EnvironmentItems = new List<Transform>();
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            EnvironmentItems.Add(parentTransform.GetChild(i));
        }

        // In danh sách các con
        //foreach (Transform child in childTransforms)
        //{
        //    Debug.Log("Tên con: " + child.name);
        //}
           
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        Relocate();
    }

    public void Relocate()
    {
        Vector3 pos;
        float rand;
        foreach (Transform item in EnvironmentItems)
        {
            pos = item.position;
            rand = Random.Range(-20, 20);
            pos.x += rand;
            item.position = pos;
        }
    }
}
