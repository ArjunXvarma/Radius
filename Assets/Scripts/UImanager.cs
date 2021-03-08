using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public Image target;
    Player player;
    Dictionary<string, Color> colors;
    string[] colorNames;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        colors = player.colorsDictionary();
        colorNames = player.colorNamesArray();
        target.color = colors["Blue"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // changes the target image's color
    public void changeTargetColor()
    {
        target.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
    }

    // returns the color of the target image
    public Color targetColor()
    {
        return target.color; 
    }
}
