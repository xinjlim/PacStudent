using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private float t = 0.0f;
    private float speed = 2.0f;
    private Vector2 lastInput = Vector2.zero;
    private Vector2 currentInput = Vector2.zero;
    private int[,] levelMap = {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,5,0,3,3,5,1,2,2,2,2,2},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0,0,0,0,4,0,0,0,5,0,0,0,0,0,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0,0,0,0,5,0,3,3,5,1,2,2,2,2,2},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0,0,4,4,3,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0,0,0,0,0,0,4,4,5,2,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3,3,0,3,4,4,3,4,5,2,0,0,0,0,0},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4,4,0,3,4,4,3,4,5,1,2,2,2,2,1},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,4,4,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3,3,4,4,3,5,4,4,5,3,4,4,3,5,2},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4,4,4,4,3,5,3,3,5,3,4,4,3,5,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3,3,5,3,4,4,4,3,5,3,4,4,3,5,2},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4,4,5,4,0,0,0,4,5,4,0,0,4,6,2},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4,4,5,3,4,4,4,3,5,3,4,4,3,5,2},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4,4,5,5,5,5,5,5,5,5,5,5,5,5,2},
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7,7,2,2,2,2,2,2,2,2,2,2,2,2,1}
        };
    private int mapRow, mapCol;
    public Animator animatorController;
    public MusicManager musicManager;
    private bool isMoving = false;

    void Start()
    {   
        transform.position = new Vector2(-9.45f, 10.7f);
        mapRow = 1;
        mapCol = 1;
    }

    void Update()
    {
        Teleport();
        if (Input.GetKeyDown(KeyCode.W)) {
            lastInput = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = Vector2.right;
        } 

        if (t == 0 && lastInput != Vector2.zero) {
            if (TryMovePacStudent(lastInput)) {
                currentInput = lastInput;
            } else {
                TryMovePacStudent(currentInput);
            }
        }
        
        if (t > 0) {
            t += speed * Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, t);
            isMoving = true;

            if (t >= 1.0f) {
                transform.position = endPos;
                t = 0.0f;
                isMoving = false;
            }
            PlayAnimation();
            musicManager.PlayMovingAudio(isMoving, isEating(mapRow, mapCol));
        }
    }

    private bool TryMovePacStudent(Vector2 direction) {
        int endRow = mapRow - (int) direction.y;
        int endCol = mapCol + (int) direction.x;

        if (isWalkable(endRow, endCol)) {
            mapRow = endRow;
            mapCol = endCol;

            startPos = transform.position;
            endPos = startPos + direction;
            t = 0.001f;
            return true;
        } 

        return false;
    }

    private bool isWalkable(int row, int col) {
        if (row < 0 || row >= levelMap.GetLength(0) || col < 0 || col >= levelMap.GetLength(1)) {
            return false;
        }

        int sprite = levelMap[row, col];
        return sprite == 0 || sprite == 5 || sprite == 6;
    }

    private bool isEating(int row, int col) {
        int sprite = levelMap[row, col];
        return sprite == 5 || sprite == 6;
    }

    private void PlayAnimation() {
        float y = endPos.y - startPos.y;
        float x = endPos.x - startPos.x;

        animatorController.ResetTrigger("Stop");
        animatorController.ResetTrigger("GoRight");
        animatorController.ResetTrigger("GoLeft");
        animatorController.ResetTrigger("GoUp");
        animatorController.ResetTrigger("GoDown");

        if (!isMoving) {
            animatorController.SetTrigger("Stop");
        } else if (y == 0) {
            animatorController.SetTrigger(x > 0 ? "GoRight" : "GoLeft");
        } else {
            animatorController.SetTrigger(y > 0 ? "GoUp" : "GoDown");
        }
    }

    private void Teleport() {
        Vector3 leftmost = new Vector3(-10.45f, -2.3f, 0.0f);
        Vector3 rightmost = new Vector3(16.55f, -2.3f, 0.0f);

        if (Mathf.Approximately(transform.position.y, -2.3f)) {
            if(Mathf.Approximately(transform.position.x, leftmost.x)) {
                transform.position = rightmost;
                mapCol = levelMap.GetLength(1) - 1;
            } else if (Mathf.Approximately(transform.position.x, rightmost.x)) {
                transform.position = leftmost;
                mapCol = 0;
            }
        }
    }
}
