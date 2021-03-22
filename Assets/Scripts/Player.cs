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
    Color red;
    [SerializeField]
    bool neutralPowerup = false;

    [Range(0, 50f)]
    public float lerpTime;
    [SerializeField]
    Color[] transColors;
    int colorIndex;
    float t = 0;

    TrailRenderer trail;

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
        ColorUtility.TryParseHtmlString ("#FF4339", out red);

        // Adding color values and their respective keys to the dictionary
        colors.Add("Blue", new Color(27f/255, 156f/255, 252f/255));
        colors.Add("White", new Color(248f/255, 239f/255, 186f/255));
        colors.Add("Orange", new Color(249f/255, 127f/255, 81f/255));
        colors.Add("Green", new Color(85f/255, 230f/255, 193f/255));
        render.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
        ui = GameObject.Find("UIManager").GetComponent<UImanager>();
        gm = GameObject.Find("LevelManager").GetComponent<GameManager>();
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (neutralPowerup)
            transistionColor();

        // Detecting touch input
        if (touched && health != 0)
        {
            touched = false;
            rigidbody.velocity = Vector2.up * jumpForce;
        }

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        trail.colorGradient = gradient;

        // Tracks the health bar UI of the player
        if (health == 2)
        {
            healthBars[0].color = red;
            healthBars[1].color = red;
            healthBars[2].color = healthBarImage;
        }

        else if (health == 1)
        {
            healthBars[0].color = red;
            healthBars[1].color = healthBarImage;
            healthBars[2].color = healthBarImage;
            
        }
            
        else if (health == 0)
        {
            healthBars[0].color = healthBarImage;
            healthBars[1].color = healthBarImage;
            healthBars[2].color = healthBarImage;
        }  
        
        if (health == 0)
            SceneManager.LoadScene(0);

        if (checkIfObjectVisible(gm.prefabs) && transform.position.y < -12)
            Debug.Log("U R DED");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Checking if the object that has collided with the player has the same color 
        // as the target color
        if (colors[other.name] == ui.targetColor() && other.name != "PowerupIcon")
        {
            render.color = colors[other.name];
            StartCoroutine(ui.changeTargetColor());
            score++;
        }
        else if (colors[other.name] != ui.targetColor() && !neutralPowerup)
            health--;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "PowerupIcon")
        {
            //transistionColor();
            neutralPowerup = true;
            StartCoroutine(neutralCountDown());
            Destroy(other.gameObject);
        }
    }

    // Checks if any of the barriers are currently in the field of the camera
    bool checkIfObjectVisible(GameObject[] objects)
    {
        int counter = 0;

        for (int i = 0; i < objects.Length; i++)
            if (!objects[i].GetComponent<Renderer>().isVisible)
                counter++;

        if (counter == objects.Length)
            return true; // Returns true if ALL the objects 
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

    void transistionColor()
    {
        render.color = Color.Lerp(render.color, transColors[colorIndex], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        render.color = new Color(render.color.r, render.color.g, render.color.b, 1);

        if (t > 0.9f)
        {
            t = 0;
            colorIndex++;
            colorIndex = (colorIndex >= transColors.Length) ? 0 : colorIndex;
        }
    }

    void setTrailColor()
    {   
        // GradientColorKey[] colorKey;
        // GradientAlphaKey[] alphaKey;
        // Gradient gradient = new Gradient();

        // colorKey = new GradientColorKey[1];
        // alphaKey = new GradientAlphaKey[2];

        // colorKey[0].color = render.color;
        // colorKey[0].time = 0.5f;
        

        // alphaKey[0].alpha = 1.0f;
        // alphaKey[0].time = 0.0f;
        // alphaKey[1].alpha = 0.0f;
        // alphaKey[1].time = 1.0f;
        

        // gradient.SetKeys(colorKey, alphaKey);

        // trail.colorGradient = gradient;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        trail.colorGradient = gradient;
    }

    IEnumerator neutralCountDown()
    {
        yield return new WaitForSeconds(10.0f);
        neutralPowerup = false;
    }
}

