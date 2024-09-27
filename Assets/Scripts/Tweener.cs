using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Tween activeTween;
    private MusicManager musicManager;
    private Vector3 prevPos;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = GetComponent<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTween is not null) {
            prevPos = activeTween.Target.position;

            activeTween.ElapsedTime += Time.deltaTime;
            float t = activeTween.ElapsedTime / activeTween.Duration;
            activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, t);

            bool isMoving = false;
            if(activeTween.Target.position != prevPos) {
                isMoving = true;
                prevPos = activeTween.Target.position;
            } else {
                isMoving = false;
            }
            musicManager.PlayMovingAudio(isMoving);

            if (activeTween.Target.position == activeTween.EndPos) { activeTween = null; }
        }
    }

    public void AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration) {
        if (activeTween is null) {
            activeTween = new Tween(targetObject, startPos, endPos, duration);
        }
    }
}
