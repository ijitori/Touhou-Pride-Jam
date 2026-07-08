using UnityEngine;
using UnityEngine.Audio;

public class SFXObjectManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;

    private bool HasAlreadyPlayedThisTurn;
    public AudioMixer MasterAudioMixer ;//= Resources.Load<AudioMixer>("Assets/MasterAudioMixer");
    AudioMixerGroup sfxGroup;

    public bool OneTimePlay;
    public float TotalTimer;
    public float timer;
    
    void Start()
    {
        
        AudioSource = this.gameObject.GetComponent<AudioSource>();
        AudioClip = AudioSource.clip;
        MasterAudioMixer = Resources.Load<AudioMixer>("MasterAudioMixer");
        
        
        //AudioMixer.FindSnapshot("SFX");
        sfxGroup = MasterAudioMixer.FindMatchingGroups("SFX")[0];
        AudioSource.outputAudioMixerGroup = sfxGroup;

        TotalTimer = AudioClip.length;
        

    }

    
    void Update()
    {
        //if (TurnMaster.AnimationPhase)
        //{
          
                if(OneTimePlay)
                {
                    timer += Time.deltaTime;
                    if(timer >= TotalTimer)
                    {
                        Destroy(gameObject);
                    }
                }
               
        //}
    }
}
