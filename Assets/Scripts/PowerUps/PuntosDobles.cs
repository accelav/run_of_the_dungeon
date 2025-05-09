using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosDobles : MonoBehaviour
{
    public float duracion = 5f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(8);
            // Se obtiene el componente PlayerController del objeto raíz del objeto 'other'
            PlayerController playerController = other.transform.root.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.ActivarPuntosDobles(duracion);
                Destroy(gameObject);
            }
        }
    }
}