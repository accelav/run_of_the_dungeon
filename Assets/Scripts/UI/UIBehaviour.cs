using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject canvasInicio;
    [SerializeField]
    GameObject textPressAnyButton;

    void Start()
    {
        canvasInicio.SetActive(true);
        LeanTween.scale(textPressAnyButton, Vector3.one * 0.2f, 1f).setEaseInOutSine().setLoopPingPong();
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
    }

    void EmpezarJuego()
    {
        SoundsBehaviour.instance.PlayClip(1);
        canvasInicio.SetActive(false);
    }
}
