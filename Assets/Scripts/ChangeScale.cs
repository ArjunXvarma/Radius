using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    Vector3 minScale;
    public Vector3 maxScale;
    public float speed = 2f, duration = 5f;
    public bool repeatable;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        minScale = transform.localScale;
        while (repeatable)
        {
            yield return repeatLerp(minScale, maxScale, duration);
            yield return repeatLerp(maxScale, minScale, duration);
        }
    }
    
    public IEnumerator repeatLerp(Vector3 a, Vector3 b, float t)
    {
        float i = 0;
        float rate = (1 / t) * speed;
        while(i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
