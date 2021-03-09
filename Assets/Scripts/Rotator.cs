using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.position.x > 0)
            speed *= -1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, speed * Time.deltaTime); // Rotatates the object on the z-axis
    }
}
