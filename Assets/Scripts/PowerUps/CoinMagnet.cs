using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float magnetRadius = 5f;
    public float attractionSpeed = 5f;
    public float magnetDuration = 10f;
    private bool isMagnetActive = false;

    void Update()
    {
        if (isMagnetActive)
        {
            AttractCoins();
        }
    }

    public void ActivateMagnet()
    {
        isMagnetActive = true;
        StartCoroutine(DeactivateMagnetAfterTime());
    }

    IEnumerator DeactivateMagnetAfterTime()
    {
        yield return new WaitForSeconds(magnetDuration);
        isMagnetActive = false;
    }

    void AttractCoins()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magnetRadius);

        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Coin")) // o moneda
            {
                Transform coin = hit.transform;
                coin.position = Vector3.MoveTowards(coin.position, transform.position, attractionSpeed * Time.deltaTime);
            }
        }
    }
}

