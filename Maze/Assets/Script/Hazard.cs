using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Hazard Script
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.Die();
        }

        // Check for other interactions, such as walls or goals
        if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))  // Wall layer
        {
            Wall wall = other.GetComponent<Wall>();
            if (wall != null)
            {
                Debug.Log("Hazard hit a wall!");
                // Add any additional logic here, if needed
            }
        }

        // If it hits any other hazards (for example, other projectiles or dangers)
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))  // Hazard layer
        {
            Debug.Log("Hazard hit another hazard!");
            // Handle hazard-hazard interaction if needed
        }


    }




}
