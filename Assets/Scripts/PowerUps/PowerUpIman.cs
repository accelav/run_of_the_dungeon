using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIman : MonoBehaviour
{
    public float duracionIman = 5f; // Duraci�n configurable desde el Inspector

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(8);
            // Obtenemos el objeto ra�z en caso de colisionar con un hijo como "pelvis"
            PlayerController playerController = other.transform.root.GetComponent<PlayerController>();

            if (playerController != null)
            {
                playerController.ActivarIman(duracionIman);  // Usamos la duraci�n p�blica
                Destroy(gameObject);
            }
         
        }
     
    }
}