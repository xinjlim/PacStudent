using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
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
    private Camera camera;
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
        camera = GetComponent<Camera>();
        camera.orthographicSize = levelMap.GetLength(0) + 1;
        GameObject[] layouts = GameObject.FindGameObjectsWithTag("Layout");
        foreach (GameObject layout in layouts) {
            Destroy(layout);
        }

        for (int y = 0; y < levelMap.GetLength(0); ++y) {
            for (int x = 0; x < levelMap.GetLength(1); ++x) {
                int value = levelMap[y,x];
                GameObject newItem;
                switch (value) {
                    case 1: 
                        newItem = Instantiate(outsideCorner, new Vector3(x, -y), Quaternion.identity); 

                        if (y > 0 && x+1 < levelMap.GetLength(1) && levelMap[y-1, x] == 2 && levelMap[y, x+1] == 2) {
                            newItem.transform.Rotate(0, 0, 90);
                        } 
                        else if (x > 0 && y > 0 && levelMap[y,x-1] == 2 && levelMap[y-1,x] == 2) {
                            newItem.transform.Rotate(0, 0, 180);
                        }
                        else if (x > 0 && y+1 < levelMap.GetLength(0) && levelMap[y,x-1] == 2 && levelMap[y+1,x] == 2) {
                            newItem.transform.Rotate(0, 0, 270);
                        }

                        break;
                    case 2: 
                        newItem = Instantiate(outsideWall, new Vector3(x, -y), Quaternion.identity);
                        if (y == 0) {
                            newItem.transform.Rotate(0, 0, 90);
                        } 
                        else if (levelMap[y+1, x] != 2 && levelMap[y-1, x] != 2) {
                            newItem.transform.Rotate(0, 0, 90);
                        }
                        break;
                    case 3:
                        newItem = Instantiate(insideCorner, new Vector3(x, -y), Quaternion.identity);
                        if (y > 0 && x+1 < levelMap.GetLength(0)-1 
                            && (levelMap[y,x+1] == 3 || levelMap[y,x+1] == 4)
                            && (levelMap[y-1,x] == 3 || levelMap[y-1,x] == 4)) {
                            newItem.transform.Rotate(0, 0, 90);
                        } 
                        else if (x > 0 && y > 0
                            && (levelMap[y,x-1] == 3 || levelMap[y,x-1] == 4) 
                            && (levelMap[y-1,x] == 3 || levelMap[y-1,x] == 4)) {
                            newItem.transform.Rotate(0, 0, 180);
                        }
                        else if (x > 0 && y+1 < levelMap.GetLength(1) 
                            && (levelMap[y+1,x] == 3 ||levelMap[y+1,x] == 4) 
                            && (levelMap[y,x-1] == 3 ||levelMap[y,x-1] == 4)) {
                            newItem.transform.Rotate(0, 0, 270);
                        }

                        break;
                    case 4:
                        newItem = Instantiate(insideWall, new Vector3(x, -y), Quaternion.identity);

                        if (x > 0 && x+1 < levelMap.GetLength(1) && levelMap[y,x-1] == 3 && levelMap[y,x+1] == 3) {
                            newItem.transform.Rotate(0, 0, 90);
                        }
                        else if (x > 0 && x+1 < levelMap.GetLength(1) && levelMap[y,x-1] == 3 && levelMap[y,x+1] == 4) {
                            newItem.transform.Rotate(0, 0, 90);
                        }
                        else if (x > 0 && x+1 < levelMap.GetLength(1) && levelMap[y,x-1] == 4 && levelMap[y,x+1] == 3) {
                            newItem.transform.Rotate(0, 0, 90);
                        }
                        else if (x > 0 && x+1 < levelMap.GetLength(1) && levelMap[y,x-1] == 4 && levelMap[y,x+1] == 4) {
                            newItem.transform.Rotate(0, 0, 90);
                        } 

                        break;
                    case 5:
                        newItem = Instantiate(normalPellet, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 6:
                        newItem = Instantiate(powerPellet, new Vector3(x, -y), Quaternion.identity);
                        break;
                    case 7: 
                        newItem = Instantiate(tJunction, new Vector3(x, -y), Quaternion.identity);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
