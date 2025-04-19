using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltoPowerUp : MonoBehaviour
{
    public float jumpMultiplier = 2f;
    public float powerupDuration = 10f;

   // script de puntos dobles falta colocar cosas

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          //  PlayerMovement pm = other.GetComponent<PlayerMovement>();
         //   if (pm != null)
          //  {
          //      pm.ActivateHighJump(jumpMultiplier, powerupDuration);
          //  }

            Destroy(gameObject); // Destruye el power-up tras activarlo
        }
    }
}


