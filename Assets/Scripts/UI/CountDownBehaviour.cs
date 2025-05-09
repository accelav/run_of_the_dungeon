using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownBehaviour : MonoBehaviour
{
    public TextMeshProUGUI countDown;
    public float time = 3f;
    public GameObject player;

    public static CountDownBehaviour instance;
    public bool countDownFinish = false;

    private void Awake()
    {
        instance = this;
    }
    public IEnumerator CountDown()
    {
        float tiempo = time;

        while (tiempo > 0)
        {

            countDown.text = Mathf.Ceil(tiempo).ToString();
            yield return new WaitForSeconds(1f);

            tiempo -= 1f;
        }

        countDown.text = "¡YA!";
        yield return new WaitForSeconds(1f);
        countDown.gameObject.SetActive(false);
        countDownFinish = true;

        player.GetComponent<PlayerController>().enabled = true;
    }
}
