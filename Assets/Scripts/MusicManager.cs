using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource background;
    public AudioSource pacState;
    public AudioSource wallCollision;
    [SerializeField]
    private AudioClip normalState;
    [SerializeField]
    private AudioClip moving;
    [SerializeField]
    private AudioClip eating;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayNormalState();
    }

    // Update is called once per frame
    void Update()
    {
        wallCollision.Stop();
    }

    public void PlayNormalState() {
        background.clip = normalState;
        background.loop = true;
        background.Play();
    }

    public void PlayMovingAudio(bool isMoving, bool isEating) {
        if (isMoving && !pacState.isPlaying && !isEating) {
            pacState.clip = moving;
            pacState.Play();
        } else if (isMoving && !pacState.isPlaying && isEating) {
            pacState.clip = eating;
            pacState.Play();
        } else if (!isMoving && pacState.isPlaying && !isEating) {
            pacState.Stop();
        }
    }

    public void PlayCollisionAudio() {
        if (!wallCollision.isPlaying) {
             wallCollision.Play();
        }
    }
}
