using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private Camera camera;
    [SerializeField]
    private GameObject outsideCorner;
    [SerializeField]
    private GameObject outsideWall;
    [SerializeField]
    private GameObject insideCorner;
    [SerializeField]
    private GameObject insideWall;
    [SerializeField]
    private GameObject normalPellet;
    [SerializeField]
    private GameObject powerPellet;
    [SerializeField]
    private GameObject tJunction;
    int[,] levelMap =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] layouts = GameObject.FindGameObjectsWithTag("Layout");
        foreach (GameObject layout in layouts) {
            Destroy(layout);
        }

        for (int y = 0; y < levelMap.GetLength(0); ++y) {
            for (int x = 0; x < levelMap.GetLength(1); ++x) {
                int value = levelMap[y,x];
                switch (value) {
                    case 1: 
                        Instantiate(outsideCorner, new Vector3(x, -y), Quaternion.identity); 
                        break;
                    case 2: 
                        Instantiate(outsideWall, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(insideCorner, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(insideWall, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 5:
                        Instantiate(normalPellet, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 6:
                        Instantiate(powerPellet, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 7: 
                        Instantiate(tJunction, new Vector3(x, -y), Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }

        camera = GetComponent<Camera>();
        camera.orthographicSize = levelMap.GetLength(0) + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
