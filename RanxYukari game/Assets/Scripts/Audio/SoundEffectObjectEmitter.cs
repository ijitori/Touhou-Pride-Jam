using UnityEngine;
using UnityEngine.Audio;

public class SFXEmitter : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void CreateLoopingSFX(AudioResource SFXAudioResource, string SoundID)
    {
        var SFXObject = new GameObject("Sound Effect");
        var SFXAudioSource = SFXObject.AddComponent<AudioSource>();
        var SFXManager = SFXObject.AddComponent<SFXObjectManager>();
        SFXManager.OneTimePlay = true;
        SFXAudioSource.resource = SFXAudioResource;
        SFXAudioSource.Play();
    }

    
    public void CreateOneTimeSFX(AudioResource SFXAudioResource)
    {
        var SFXObject = new GameObject("Sound Effect");
        var SFXAudioSource = SFXObject.AddComponent<AudioSource>();
        var SFXManager = SFXObject.AddComponent<SFXObjectManager>();
        SFXManager.OneTimePlay = true;
        SFXAudioSource.resource = SFXAudioResource;
        SFXAudioSource.Play();
    }
}
