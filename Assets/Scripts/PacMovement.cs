using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMovement : MonoBehaviour
{
    
    private Tween activeTween;
    private MusicManager musicManager;
    private Vector3 prevPos;
    [SerializeField]
    private Animator animatorController;
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

                Vector3 currentPos = activeTween.Target.position;
                float x = currentPos.x - prevPos.x;
                float y = currentPos.y - prevPos.y;
                
                animatorController.ResetTrigger("GoRight");
                animatorController.ResetTrigger("GoLeft");
                animatorController.ResetTrigger("GoUp");
                animatorController.ResetTrigger("GoDown");

                if (y == 0) {
                    if (x > 0) {
                        animatorController.SetTrigger("GoRight");
                    } else {
                        animatorController.SetTrigger("GoLeft");
                    }
                } else {
                    if (y > 0) {
                        animatorController.SetTrigger("GoUp");
                    } else {
                        animatorController.SetTrigger("GoDown");
                    }
                }

                prevPos = currentPos;
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
