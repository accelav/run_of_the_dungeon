
using UnityEngine;

public class SoundsBehaviour : MonoBehaviour
{
    public static SoundsBehaviour instance;
    AudioSource audioSource;
    public AudioClip[] clip;
    /*
    Clip 0 = Boton1
    Clip 1 = Boton2
    Clip 2 = ChoqueEnemigo
    Clip 3 = Tropiezo
    Clip 4 = Saw
    Clip 5 = Hammer
    Clip 6 = hacha
    Clip 7 = stars
    Clip 8 = Iman
    Clip 9 = Vida
    Clip 10 = Escudo
    Clip 11 = Ralent�
    Clip 12 = Intro

    */
    // Start is called before the first frame update
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
        PlayIntroSound();

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayClip(int number)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[number]);
        }

    }
    public void PlayIntroSound()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip[12]);
        }
    }
}