using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManagment : MonoBehaviour
{
    public CinemachineVirtualCamera introCamera;
    public float offset = 368f;
    public GameObject player;
    

    private void Start()
    {
        player.GetComponent<PlayerController>().enabled = false;
    }

    private void Update()
    {
        if (offset >= -1)
        {
            introCamera.GetComponent<CinemachineCameraOffset>().m_Offset = new Vector3(0, 0, offset);
            offset -= 2;
        }
        if (offset == 0)
        {
            CountDownBehaviour.instance.countDown.gameObject.SetActive(true);
            StartCoroutine(CountDownBehaviour.instance.CountDown());
        }
    }
}
