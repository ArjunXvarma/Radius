using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rigidbody;
    bool touched = false;
    SpriteRenderer render;
    public int health = 3;

    public Image[] healthBars;
    Color healthBarImage;

    // Stores the value of the Color values used, key for each value is the name of the color 
    Dictionary<string, Color> colors = new Dictionary<string, Color>();
    UImanager ui;

    // Stores all of the keys of the colors dictionary
    string[] colorNames = new string[] {"Blue", "White", "Orange", "Green"};

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        healthBarImage.a = 0;

        // Adding color values and their respective keys to the dictionary
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
        // Detecting touch input
        if (touched)
        {
            touched = false;
            rigidbody.velocity = Vector2.up * jumpForce;
        }
        
        // Tracks the health bar UI of the player
        if (health == 2)
            healthBars[2].color = healthBarImage;
        else if (health == 1)
            healthBars[1].color = healthBarImage;
        else if (health == 0)
            healthBars[0].color = healthBarImage;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Checking if the object that has collided with the player has the same color 
        // as the target color
        if (colors[other.name] == ui.targetColor())
        {
            render.color = colors[other.name];
            ui.changeTargetColor();
        }
        else if (colors[other.name] != ui.targetColor())
            health--;
    }

    public void touchTrue()
    {
        touched = true;
    }

    public void touchFalse()
    {
        touched = false;
    }

    // Rerturns the color dictionary, this is utilised by the UImanager.cs script
    public Dictionary<string, Color> colorsDictionary()
    {
        return colors;
    }

    // Rerturns the color Array, this is utilised by the UImanager.cs script
    public string[] colorNamesArray()
    {
        return colorNames;
    }
}

