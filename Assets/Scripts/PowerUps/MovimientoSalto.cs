using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSalto : MonoBehaviour
{
 
        public float jumpForce = 10f;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {/*
                if (IsGrounded()) // tu funci�n para comprobar si est� en el suelo
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                }
            }*/
        }
    }

        // ... tu c�digo de grounded check aqu�
    


}
