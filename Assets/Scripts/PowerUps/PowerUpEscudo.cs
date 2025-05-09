using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEscudo : MonoBehaviour
{
    public float duracionEscudo = 5f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(10);
            PlayerController player = other.transform.root.GetComponent<PlayerController>();
            if (player != null)
            {
                player.ActivarEscudo(duracionEscudo);
                Destroy(gameObject);
            }
        }
    }
}

