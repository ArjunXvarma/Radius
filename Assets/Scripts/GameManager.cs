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

    // Creating a dictionary of all the barriers so that it is easy to delete any barrier,
    // this helps in memory management
    private List<GameObject> activeCirlces = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Generating the first set of barriers
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
        // Checking if the players position is greater than a certain value so that the 
        // spawning of barriers can happen at the right momement 
        if (playerPos.position.y - 35 > ySpawn - (numberOfCircles * circleLength))
        {
            spawnCircle(Random.RandomRange(0, prefabs.Length));
            deleteCircle();
        }
    }

    public void spawnCircle(int circleIndex)
    {
        // Creating an array of Vector3 values, of size 2, to randomly pick the 
        // x position(either left or right) of the barriers(the ones that are not circular in shape)
        Vector3[] leftRightPos = new Vector3[] {new Vector3(-1.3f, 0, 0), new Vector3(1.3f,0,0)};
        Vector3 pos;

        // Adding if-checks to assign the correct position to the barrier 
        if (circleIndex > 1)
            pos = (transform.up * ySpawn) + leftRightPos[Random.RandomRange(0, leftRightPos.Length)];
        else
            pos = transform.up * ySpawn;

        GameObject circle = Instantiate(prefabs[circleIndex], pos, Quaternion.identity);
        ySpawn += circleLength;
        
        // Adding the barrier to the dicitionary
        activeCirlces.Add(circle);
    }

    // Function to delete the first element of the dictionary, i.e. the first 'Barrier'
    // in the dicitonary
    void deleteCircle()
    {
        Destroy(activeCirlces[0]);
        activeCirlces.RemoveAt(0);
    }
}
