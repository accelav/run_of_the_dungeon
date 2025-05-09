using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public string wallTag = "Wall";
    private Rigidbody rb;
    private bool isRotating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Para que no se incline
    }

    void FixedUpdate()
    {
        if (!isRotating)
        {
            rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }


    System.Collections.IEnumerator Rotate90Degrees()
    {
        isRotating = true;

        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0f, 360f, 0f);

        float duration = 0.3f;
        float time = 0f;

        while (time < duration)
        {
            transform.rotation = Quaternion.Slerp(startRot, endRot, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
        isRotating = false;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(wallTag) && !isRotating)
        {
            Debug.Log("¡Tocó una pared, girando!");
            StartCoroutine(Rotate90Degrees());
        }
    }
}