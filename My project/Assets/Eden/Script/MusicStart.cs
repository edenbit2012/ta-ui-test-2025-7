using UnityEngine;

public class MusicManager : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
