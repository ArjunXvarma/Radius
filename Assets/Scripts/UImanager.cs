using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public Image target;
    Dictionary<string, Color> colors = new Dictionary<string, Color>();
    string[] colorNames = new string[] {"Blue", "White", "Orange", "Green"};

    // Start is called before the first frame update
    void Start()
    {
        colors.Add("Blue", new Color(27f/255, 156f/255, 252f/255));
        colors.Add("White", new Color(248f/255, 239f/255, 186f/255));
        colors.Add("Orange", new Color(249f/255, 127f/255, 81f/255));
        colors.Add("Green", new Color(85f/255, 230f/255, 193f/255));
        target.color = colors["Blue"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTargetColor()
    {
        target.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
    }

    public Color targetColor()
    {
        return target.color; 
    }
}
