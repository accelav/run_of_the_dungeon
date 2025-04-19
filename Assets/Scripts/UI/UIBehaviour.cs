using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject canvasInicio;

    void Start()
    {
        canvasInicio.SetActive(true);
    }
    void Update()
    {
        if (canvasInicio==true)
        {
            if (Input.anyKeyDown)
            {
                EmpezarJuego();
            }
        }
        else
        {

        }
        
    }

    void EmpezarJuego()
    {
        canvasInicio.SetActive(false);
    }
}
