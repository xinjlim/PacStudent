using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip intro;
    public AudioClip normalState;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = intro;
        audioSource.Play();
        Invoke(nameof(PlayNormalState), intro.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayNormalState() {
        audioSource.clip = normalState;
        audioSource.loop = true;
        audioSource.Play();
    }
}
