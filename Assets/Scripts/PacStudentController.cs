using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private float t = 0.0f;
    private float speed = 2.0f;
    private Vector2 lastInput;
    private Vector2 currentInput;
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

    void Start()
    {   
        transform.position = new Vector2(-9.45f, 10.7f);
        mapRow = 1;
        mapCol = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) {
            lastInput = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            lastInput = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.S)) {
            lastInput = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.D)) {
            lastInput = Vector2.right;
        } 

        if (t == 0) {
            if (TryMovePacStudent(lastInput)) {
                currentInput = lastInput;
            } else {
                TryMovePacStudent(currentInput);
            }
        }
        
        if (t > 0) {
            t += speed * Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, endPos, t);
            PlayAnimation();

            if (t >= 1.0f) {
                transform.position = endPos;
                t = 0.0f;
            }
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

    private void PlayAnimation() {
        float y = endPos.y - startPos.y;
        float x = endPos.x - startPos.x;

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
    }
}
