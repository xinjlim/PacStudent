using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private PacMovement movement;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<PacMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform transform = player.transform;
        if (transform.position == new Vector3(-9.45f, 10.7f, 0.0f)) {
            movement.AddTween(transform, transform.position, new Vector3(-4.44f, 10.7f, 0.0f), 2.5f);
        }

        else if (transform.position == new Vector3(-4.44f, 10.7f, 0.0f)) {
            movement.AddTween(transform, transform.position, new Vector3(-4.44f, 6.7f, 0.0f), 2.0f);
        }   

        else if (transform.position == new Vector3(-4.44f, 6.7f, 0.0f)) {
            movement.AddTween(transform, transform.position, new Vector3(-9.45f, 6.7f, 0.0f), 2.5f);
        } 

        else if (transform.position == new Vector3(-9.45f, 6.7f, 0.0f)) {
            movement.AddTween(transform, transform.position, new Vector3(-9.45f, 10.7f, 0.0f), 2.0f);
        } 
    }
}
