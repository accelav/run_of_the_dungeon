using UnityEngine;
using System.Collections;

public class Soundtrack : MonoBehaviour
{
    public static Soundtrack instance;
    private AudioSource audioSource;

    public float delayBeforePlay = 2f;     // Espera antes de reproducir (en segundos)
    public float fadeDuration = 3f;        // Duración del fade-in

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0f;
        StartCoroutine(PlayWithFadeIn());
    }

    IEnumerator PlayWithFadeIn()
    {
        // Esperar antes de reproducir el audio
        yield return new WaitForSeconds(delayBeforePlay);

        audioSource.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Clamp01(timer / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f; // Asegurar que termina en 1
    }
}
