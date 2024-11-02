using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource background;
    public AudioSource pacState;
    [SerializeField]
    private AudioClip normalState;
    [SerializeField]
    private AudioClip moving;
    // Start is called before the first frame update
    void Start()
    {
        PlayNormalState();
        pacState.clip = moving;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayNormalState() {
        background.clip = normalState;
        background.loop = true;
        background.Play();
    }

    public void PlayMovingAudio(bool isMoving) {
        if (isMoving && !pacState.isPlaying) {
            pacState.Play();
        } 
        
        else if (!isMoving && pacState.isPlaying) {
            pacState.Stop();
        }
        
    }
}
