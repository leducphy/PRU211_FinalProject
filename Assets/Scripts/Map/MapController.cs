using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    
    [SerializeField] public Transform player;
    [SerializeField] GameObject Environment;
    public float currentDistance = 0f;
    public float limitDistance = 100f;
    public float respawnDistance = 130f;
     
    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        Spawning();
        GetDistance();
    }

    public void Spawning()
    {
        if (this.currentDistance < this.limitDistance) 
        {
            return;
        }

        //Environment.Set
        Environment.GetComponent<RandomRelocate>().Relocate();
        Vector3 position = transform.position;
        position.x += this.respawnDistance;
        transform.position= position;
    }

    private void GetDistance()
    {
        this.currentDistance = this.player.position.x - transform.position.x;
    }
}
