using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public Rigidbody2D rigidbody;
    bool touched = false;
    SpriteRenderer render;
    public int health = 3;
    public int score = 0;
    public Image[] healthBars;
    Color healthBarImage;
    GameManager gm;


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
        gm = GameObject.Find("LevelManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detecting touch input
        if (touched && health != 0)
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
        
        if (health == 0)
        {
            SceneManager.LoadScene(0);
        }

        if (checkIfObjectVisible(gm.prefabs) && transform.position.y < -12)
        {
            Debug.Log("U R DED");
        }
        else 
        {
            //Not visible code here
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Checking if the object that has collided with the player has the same color 
        // as the target color
        if (colors[other.name] == ui.targetColor())
        {
            render.color = colors[other.name];
            StartCoroutine(ui.changeTargetColor());
            score++;
        }
        else if (colors[other.name] != ui.targetColor())
            health--;
    }

    // Checks if any of the barriers are currently in the field of the camera
    bool checkIfObjectVisible(GameObject[] objects)
    {
        int counter = 0;

        for (int i = 0; i < objects.Length; i++)
            if (!objects[i].GetComponent<Renderer>().isVisible)
                counter++;

        if (counter == objects.Length)
            return true; // Returns true if all the objects 
                        // present in the barrier array are not present in the scene
        else    
            return false;
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

