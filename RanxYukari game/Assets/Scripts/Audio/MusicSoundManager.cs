using UnityEngine;
using UnityEngine.Audio;

public class MusicSoundManager : MonoBehaviour
{
    AudioSource AudioSource;
    
    void Awake()
    {
        AudioSource = this.gameObject.GetComponent<AudioSource>();
    }


    static public void ChangeSongStatic(AudioResource resource)
    {
        Debug.Log(GameObject.FindWithTag("Music"));
        GameObject.FindWithTag("Music").GetComponent<MusicSoundManager>().ChangeSong(resource);
    }

    static public void ChangeSFXStatic(AudioResource resource)
    {
        GameObject.FindWithTag("SFX").GetComponent<MusicSoundManager>().ChangeSong(resource);
    }

    void ChangeSong(AudioResource resource)
    {
        AudioSource.resource = resource;
        AudioSource.Play();    
    }  

    
}
