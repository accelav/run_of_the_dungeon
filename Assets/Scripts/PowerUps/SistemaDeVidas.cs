
using UnityEngine;

public class SistemaDeVidas : MonoBehaviour
{
    public static SistemaDeVidas instance { get; private set; }

    private const string PuntosGuardado = "Vidas";

    public int vidas = 3;
    public int RecuperarVida = 1;

    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }


    void Update()
    {
        if (vidas <= 0)
        {
            PauseWinLoseBehaviour.instance.Loser();
            vidas = 3;
            // Time.timeScale = 0;
        }
    }
    public void RecuperarCorazon()
    {
        vidas = Mathf.Min (vidas + RecuperarVida,3); //Para limitar las vidas a 3 máximo
        ActualizarVidasUI();
    }

    public void PerdidaVida()
    {
        vidas--;
        ActualizarVidasUI();
    }

    public void ActualizarVidasUI()
    {
        heart1.SetActive(vidas >= 1);
        heart2.SetActive(vidas >= 2);
        heart3.SetActive(vidas >= 3);
    }
}
