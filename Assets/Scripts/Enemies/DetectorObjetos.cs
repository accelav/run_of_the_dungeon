using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorObjetos : MonoBehaviour
{
  
    EnemyController enemyController;
    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
    }

    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Jumpable"))
        {
           // Debug.Log("Saltar");
            enemyController.TryJump();
        }
        if (other.CompareTag("Obstacle"))
        {
            //  Debug.Log("Cambiar carril");
            enemyController.TryChangeLane();
        }
        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(2);
            SistemaDeVidas.instance.PerdidaVida();
            Destroy(gameObject);
        }
    }
}
