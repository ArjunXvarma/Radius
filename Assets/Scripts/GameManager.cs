using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public float ySpawn = 0;
    public float circleLength = 10;
    public int numberOfCircles = 5;
    public Transform playerPos;
    private List<GameObject> activeCirlces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfCircles; i++)
        {
            if (i == 0)
                spawnCircle(0);
            else
                spawnCircle(Random.RandomRange(0, prefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPos.position.y - 35 > ySpawn - (numberOfCircles * circleLength))
        {
            spawnCircle(Random.RandomRange(0, prefabs.Length));
            deleteCircle();
        }
    }

    public void spawnCircle(int circleIndex)
    {
        Vector3[] leftRightPos = new Vector3[] {new Vector3(-1.3f, 0, 0), new Vector3(1.3f,0,0)};
        Vector3 pos;

        if (circleIndex > 1)
            pos = (transform.up * ySpawn) + leftRightPos[Random.RandomRange(0, leftRightPos.Length)];
        else
            pos = transform.up * ySpawn;

        GameObject circle = Instantiate(prefabs[circleIndex], pos, Quaternion.identity);
        ySpawn += circleLength;
        activeCirlces.Add(circle);
    }

    void deleteCircle()
    {
        Destroy(activeCirlces[0]);
        activeCirlces.RemoveAt(0);
    }
}
