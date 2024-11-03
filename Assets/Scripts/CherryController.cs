using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject bonus;
    private float timeInterval = 10.0f;
    private float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCherry());
    }

    private IEnumerator SpawnCherry() {
        while (true) {
            yield return new WaitForSeconds(timeInterval);
            
            Vector2 spawnPos;
            Vector2 endPos;
            GetRandomPos(out spawnPos, out endPos);
            GameObject cherry = Instantiate(bonus, spawnPos, Quaternion.identity);
            StartCoroutine(MoveCherry(cherry, spawnPos, endPos));
        }
    }

    private IEnumerator MoveCherry(GameObject cherry, Vector2 spawnPos, Vector2 endPos) {
        float t = 0.0f;
        while (t < 1.0f) {
            t += speed * Time.deltaTime;
            cherry.transform.position = Vector2.Lerp(spawnPos, endPos, t);
            if (t >= 1.0f) {
                endPos = spawnPos;
                Destroy(cherry);
            }
            yield return null;
        }
    }

    private void GetRandomPos(out Vector2 spawnPos, out Vector2 endPos) {
        Camera mainCam = Camera.main;
        float x = 0, y = 0;

        int side = Random.Range(0, 4);
        switch (side) {
            case 0: // left
                y = Random.Range(0f, 1f);
                spawnPos = mainCam.ViewportToWorldPoint(new Vector2(-0.1f, y));
                endPos = mainCam.ViewportToWorldPoint(new Vector2(1.1f, y));
                break;
            case 1: // right
                y = Random.Range(0f, 1f);
                spawnPos = mainCam.ViewportToWorldPoint(new Vector2(1.1f, y));
                endPos = mainCam.ViewportToWorldPoint(new Vector2(-0.1f, y));
                break;
            case 2: // top
                x = Random.Range(0f, 1f);
                spawnPos = mainCam.ViewportToWorldPoint(new Vector2(x, 1.1f));
                endPos = mainCam.ViewportToWorldPoint(new Vector2(x, -0.1f));
                break;
            case 3: // bottom
                x = Random.Range(0f, 1f);
                spawnPos = mainCam.ViewportToWorldPoint(new Vector2(x, -0.1f));
                endPos = mainCam.ViewportToWorldPoint(new Vector2(x, 1.1f));
                break;
            default:
                spawnPos = Vector2.zero;
                endPos = Vector2.zero;
                break;
        }
    }
}
