using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
public class PressPlay : MonoBehaviour
{
    public AudioResource MenuSong;
    public AudioResource StageSong;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        MusicSoundManager.ChangeSongStatic(MenuSong);
    }

    [SerializeField]
    GameObject Fader;
    public void Press()
    {
        StartCoroutine("Fade");
        MusicSoundManager.ChangeSongStatic(StageSong);
        //SceneManager.LoadScene("Game");
    }

    IEnumerator Fade()
    {
        
        for (float i = 0; i !<= 1; i += Time.deltaTime)
        {
            if(i >= 0.9){ i = 1;}
            Debug.Log(i);
            Fader.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        yield return new WaitForSeconds(0.4f);
        
        SceneManager.LoadScene("Prolog");
        StartCoroutine("ReFade");

    }

    IEnumerator ReFade()
    {
        //yield return new WaitForSeconds(0.4f);
        Debug.Log("Unfading");
        
        for (float i = 1; i! >= 0; i -= Time.deltaTime)
        {
            Debug.Log("doing");
            Debug.Log(i);
            Fader.GetComponent<CanvasGroup>().alpha = i;
            yield return null;
        }
        yield return null;

    }
    
}
