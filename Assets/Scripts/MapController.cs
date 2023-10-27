using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    
    [SerializeField] public Transform player;
    public float currentDistance = 0f;
    public float limitDistance = 100f;
    public float respawnDistance = 130f;
    // Start is called before the first frame update
    void Start()
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
        if (this.currentDistance < this.limitDistance && this.currentDistance > -this.limitDistance) 
        {
            return;
        }
        Vector3 position = transform.position;
        if (this.currentDistance > this.limitDistance)
        {
            position.x += this.respawnDistance;
        }else if (this.currentDistance < -this.limitDistance)
        {
            position.x -= this.respawnDistance;
        }
        transform.position= position;
    }

    private void GetDistance()
    {
        this.currentDistance = this.player.position.x - transform.position.x;
    }
}
