using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FloorWheelBehaviour : MonoBehaviour
{
    [SerializeField] float speedPos;
    float position;
    float currentPos;
    bool changingDir;
    void Start()
    {
        changingDir = false;
    }

    
    void FixedUpdate()
    {
        currentPos = transform.position.x;
        


        if (!changingDir)
        {
            transform.position = new Vector3(position-- * Time.deltaTime * speedPos, transform.position.y, transform.position.z);
            if (currentPos <= -3)
            {
                changingDir = true;

            }
        }
       if (changingDir)
        {
            transform.position = new Vector3(position++ * Time.deltaTime * speedPos, transform.position.y, transform.position.z);
            if (currentPos >= 3)
            {
                changingDir = false;

            }
        }
        
    }

    void Position(float position)
    {
        
    }
}
