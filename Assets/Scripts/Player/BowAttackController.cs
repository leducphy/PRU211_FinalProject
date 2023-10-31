using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttackController : MonoBehaviour
{
    [SerializeField] Transform ArrowPoint;
    [SerializeField] GameObject ArrowPrefab;
    PlayerMovementController playerMovementController;
    // Start is called before the first frame update

   
    void Start()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (playerMovementController.weapon.Equals("Bow"))
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }


    }

    void Shoot()
    {
        Instantiate(ArrowPrefab, ArrowPoint.position, ArrowPoint.rotation);
    }
}
