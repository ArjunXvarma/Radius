using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRes : MonoBehaviour
{
    Resolution[] resolutions;
    Resolution currentRes;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        currentRes = Screen.currentResolution;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentRes.width && resolutions[i].height == currentRes.height)
            {
                Screen.SetResolution(1080, 1920, Screen.fullScreen);
            }
        }
    }
}
