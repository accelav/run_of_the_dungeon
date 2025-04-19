using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{
    [SerializeField] float speedRotation;
    float angle;
    bool isHitting;

    void Start()
    {
        
        isHitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        angle = transform.localEulerAngles.x;
        if (angle <= 5)
        {
            isHitting = false;
        }
        if (angle >= 85)
        {
            isHitting = true;
        }
        if (isHitting)
        {
            Rotation(-speedRotation);
        }
        if (!isHitting)
        {
            Rotation(speedRotation);
        }

        
    }

    void Rotation(float rotation)
    {
        
        transform.Rotate(new Vector3(0,rotation, 0));
        
    }
}
