using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoObstacle : MonoBehaviour
{
    [SerializeField] float slowMotionSpeed;
    [SerializeField] PlayerController player;
    float velocity;
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            SoundsBehaviour.instance.PlayClip(11);
            velocity = player.forwardSpeed;
            player.forwardSpeed *= slowMotionSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player.forwardSpeed = velocity;
        }
    }

}
