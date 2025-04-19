using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedasMagneticas : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<CoinMagnet>().ActivateMagnet();
            Destroy(gameObject); // destruye el power-up
        }
    }
}

