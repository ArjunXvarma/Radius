using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public Image target;
    Player player;
    public Text playerScore, heightDisplay;
    Dictionary<string, Color> colors;
    string[] colorNames;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        colors = player.colorsDictionary();
        colorNames = player.colorNamesArray();


        // Assigns a random color to the target Image
        target.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
    }

    // Update is called once per frame
    void Update()
    {
        playerScore.text = player.score.ToString(); 
        heightDisplay.text = player.transform.position.y.ToString(); 
    }

    // returns the color of the target image
    public Color targetColor()
    {
        return target.color; 
    }

    // changes the target image's color
    public IEnumerator changeTargetColor()
    {
        yield return new WaitForSeconds(1.5f);
        target.color = colors[colorNames[Random.RandomRange(0, colorNames.Length)]];
    }
}
