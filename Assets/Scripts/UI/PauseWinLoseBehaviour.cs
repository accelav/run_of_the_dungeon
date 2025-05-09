using UnityEngine;

using UnityEngine.SceneManagement;

public class PauseWinLoseBehaviour : MonoBehaviour
{
    public static PauseWinLoseBehaviour instance;

    [SerializeField]
    GameObject canvasPause;
    [SerializeField]
    GameObject player;
    [SerializeField]
    bool paused;

    [SerializeField]
    GameObject panelPausa;
    [SerializeField]
    GameObject textoPausa;
    [SerializeField]
    GameObject botonesPausa;

    [SerializeField]
    GameObject canvasWin;
    [SerializeField]
    GameObject textWin;
    public GameObject brilloPrefab;
    public RectTransform canvasRect;
    int cantidadBrillos = 1;

    [SerializeField]
    GameObject canvasLose;
    [SerializeField]
    GameObject textLose;
    [SerializeField]
    GameObject hacha;
    [SerializeField]
    GameObject botonesPerder;
    public bool lose = false;

    void Awake()
    {
        LeanTween.init(800);
        instance = this;
    }

    void Start()
    {
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
        paused = false;
        canvasLose.SetActive(false);
        canvasWin.SetActive(false);
        
    }
    void Update()
    {
        if (!paused && CountDownBehaviour.instance.countDownFinish && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundsBehaviour.instance.PlayClip(2);
            canvasPause.SetActive(true);
            LeanTween.scale(panelPausa, Vector3.one, 1f).setDelay(.5f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true).setOnComplete(() => {
                LeanTween.moveLocal(textoPausa, new Vector3 (-70f, 160f,0f), 1f).setIgnoreTimeScale(true).setOnComplete(() =>
                {
                    LeanTween.moveLocal(botonesPausa, Vector3.zero, 2f).setEase(LeanTweenType.easeOutCirc).setIgnoreTimeScale(true);
                });
            });
            Time.timeScale = 0f;
            paused = true;
        }
        else if (paused && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundsBehaviour.instance.PlayClip(2);
            canvasPause.SetActive(false);
            LeanTween.scale(panelPausa, Vector3.zero, 0.0f).setIgnoreTimeScale(true);
            LeanTween.moveLocal(textoPausa, new Vector3(-1112f, 160f, 0f), 0.0f).setEase(LeanTweenType.easeOutExpo).setIgnoreTimeScale(true);
                    LeanTween.moveLocal(botonesPausa, new Vector3 (0f,-631f,0f), 0.0f) .setIgnoreTimeScale(true);

            Time.timeScale = 1f;
            paused = false;
        }
        if(canvasWin == true)
        {
            //SpawnBrillos();
        }
    }

    public void Continuar()
    {
        SoundsBehaviour.instance.PlayClip(1);
        Time.timeScale = 1f;
        canvasPause.SetActive(false);
    }

    public void Restart()
    {
        SoundsBehaviour.instance.PlayClip(1);
        SceneManager.LoadScene("Nivel1");
        Time.timeScale = 1f;
    }

    public void MenuInicio()
    {
        SoundsBehaviour.instance.PlayClip(1);
        SceneManager.LoadScene("RUNOFTHEDUNGEON");
    }

    public void NextLevel()
    {
        SoundsBehaviour.instance.PlayClip(1);
        SceneManager.LoadScene("Nivel2");
    }

    public void Winner()
    {
        SoundsBehaviour.instance.PlayClip(1);
        canvasWin.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Loser()
    {
        canvasLose.SetActive(true);
        SoundsBehaviour.instance.PlayClip(1);

        LeanTween.rotateAroundLocal(textLose, Vector3.forward, 720f, 1f).setIgnoreTimeScale(true).setOnComplete(() =>
        {
            LeanTween.rotateAroundLocal(hacha, Vector3.forward, 720f, 1f).setIgnoreTimeScale(true);
            LeanTween.moveLocal(hacha, new Vector3(-258f, 242f, 0f), 1f).setIgnoreTimeScale(true).setOnComplete(() =>
            {
                hacha.transform.SetParent(textLose.transform);
                LeanTween.rotateAroundLocal(textLose, Vector3.forward, -20f, 1f).setIgnoreTimeScale(true).setOnComplete(() =>
                {
                    LeanTween.rotateAroundLocal(textLose, Vector3.forward, 40f, 1f).setIgnoreTimeScale(true).setOnComplete(() =>
                    {
                        RectTransform hachaRect = hacha.GetComponent<RectTransform>();
                        Vector3 worldPos = hachaRect.position;
                        Quaternion worldRot = hachaRect.rotation;
                        Vector3 worldScale = hachaRect.lossyScale;
                        hachaRect.SetParent(canvasRect, worldPositionStays: true);
                        hachaRect.position = worldPos;
                        hachaRect.rotation = worldRot;
                        hachaRect.localScale = Vector3.one;

                        LeanTween.moveLocal(hacha, new Vector3(0f, -1100f, 0f), 0.5f).setIgnoreTimeScale(true);
                        LeanTween.rotateAroundLocal(hacha, Vector3.forward, 20f, 1f).setIgnoreTimeScale(true);
                        LeanTween.rotateAroundLocal(textLose, Vector3.forward, -20f, 1f).setIgnoreTimeScale(true).setOnComplete(() =>
                        {
                            LeanTween.scale(botonesPerder, Vector3.one, 0.5f).setIgnoreTimeScale(true);
                            LeanTween.moveLocal(botonesPerder, Vector3.zero, 0.5f).setIgnoreTimeScale(true);
                        });
                    });
                });
            });
        });
        

        Time.timeScale = 0f;

    }

    /*
    public void SpawnBrillos()
    {
        for (int i = 0; i < cantidadBrillos; i++)
        {
            GameObject brillo = Instantiate(brilloPrefab, canvasRect);
            brillo.transform.localScale = Vector3.zero;

            // Posición aleatoria dentro del canvas
            float x = Random.Range(0, canvasRect.rect.width);
            float y = Random.Range(0, canvasRect.rect.height);
            brillo.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            float escalaFinal = Random.Range(0.5f, 1f);

            LeanTween.scale(brillo, Vector3.one * escalaFinal, 1f).setEase(LeanTweenType.easeOutCubic).setIgnoreTimeScale(true).setOnComplete(() =>
                {
                    LeanTween.scale(brillo, Vector3.zero, 1f).setDelay(0.5f).setEase(LeanTweenType.easeInCubic).setIgnoreTimeScale(true).setOnComplete(() => {
                    Destroy(brillo);
                });
            });

        }
    }
    */
}
