
using UnityEngine;

public class LevelStatus : MonoBehaviour
{

    [SerializeField] bool playing;

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.instance.LevelStatus(!playing);
        }
        
    }
}
