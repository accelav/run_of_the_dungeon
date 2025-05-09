using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarController : MonoBehaviour
{
    public Transform player;           // Referencia al jugador
    public Transform startPoint;       // Cuando inicia el Juego
    public Transform endPoint;         // Cuando Finaliza el Juego
    public Slider progressSlider;      // Slider de UI

    public TextMeshProUGUI progressText; // Para Porcentaje


    void Update()
    {
        float totalDistance = Vector3.Distance(startPoint.position, endPoint.position);
        float playerDistance = Vector3.Distance(startPoint.position, player.position);
        float progress = Mathf.Clamp01(playerDistance / totalDistance);

        progressSlider.value = progress;

        // Mostrar porcentaje
        int percent = Mathf.RoundToInt(progress * 100f);
        progressText.text = percent + "%";
    }
}