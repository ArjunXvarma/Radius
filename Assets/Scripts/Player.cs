using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rigidbody;
    bool touched = false;
    SpriteRenderer render;
    Dictionary<string, Color> colors = new Dictionary<string, Color>();
    UImanager ui;
    string[] colorNames = new string[] {"Blue", "White", "Orange", "Green"};

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        colors.Add("Blue", new Color(27f/255, 156f/255, 252f/255));
        colors.Add("White", new Color(248f/255, 239f/255, 186f/255));
        colors.Add("Orange", new Color(249f/255, 127f/255, 81f/255));
        colors.Add("Green", new Color(85f/255, 230f/255, 193f/255));
        render.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
        ui = GameObject.Find("UIManager").GetComponent<UImanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touched)
        {
            touched = false;
            rigidbody.velocity = Vector2.up * jumpForce;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (colors[other.name] == ui.targetColor())
        {
            render.color = colors[other.name];
            ui.changeTargetColor();
            Debug.Log("Works");
        }
            
        else if (render.color != colors[other.name])
        {
            
        }
    }

    public void touchTrue()
    {
        touched = true;
    }

    public void touchFalse()
    {
        touched = false;
    }
}

