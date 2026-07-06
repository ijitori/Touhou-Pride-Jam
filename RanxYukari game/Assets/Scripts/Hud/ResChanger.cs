using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResChanger : MonoBehaviour
{
    public TMPro.TMP_Dropdown val;
    public Toggle Fullscreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ScreenChange()
    {
        switch (val.value)
        {
            case 2:
                Debug.Log("wa");
                if (Fullscreen)
                { 
                    Screen.fullScreen = true;
                }
                Screen.SetResolution(640, 480, Fullscreen.isOn);
                break;
            case 1:
                if (Fullscreen)
                { 
                    Screen.fullScreen = true;
                }
                Screen.SetResolution(1280, 720, Fullscreen.isOn);
                Debug.Log("wa");
                break;
            case 0:
                if (Fullscreen)
                { 
                    Screen.fullScreen = true;
                }
                Screen.SetResolution(1920, 1080, Fullscreen.isOn);
                Debug.Log("wa");
                break;
            default:
                if (Fullscreen)
                { 
                    Screen.fullScreen = true;
                }
                Screen.SetResolution(640, 480, Fullscreen);
                break;
        }
    }
}
