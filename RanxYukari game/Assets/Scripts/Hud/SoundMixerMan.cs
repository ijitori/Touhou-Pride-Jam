using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] string ChannelName;
    void start()
    {
        AudioMixer.SetFloat("Master", 20f * Mathf.Log10(PlayerPrefs.GetFloat("Master" , 1)));
        AudioMixer.SetFloat("SFX", 20f * Mathf.Log10(PlayerPrefs.GetFloat("SFX" , 1)));
        AudioMixer.SetFloat("Music", 20f * Mathf.Log10(PlayerPrefs.GetFloat("Music" , 1)));
        float vol;
        AudioMixer.GetFloat(ChannelName, out vol);
        this.gameObject.GetComponent<Slider>().value = vol;
    }
    public void SetMasterVolume(float Value)
    {
        AudioMixer.SetFloat("Master", 20f * Mathf.Log10(Value));
        PlayerPrefs.SetFloat("Master", (Value));
        
        
    }

    public void SetSFXVolume(float Value)
    {
        AudioMixer.SetFloat("SFX", 20f * Mathf.Log10(Value));
        PlayerPrefs.SetFloat("SFX", (Value));
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float Value)
    {
        AudioMixer.SetFloat("Music", 20f * Mathf.Log10(Value));
        PlayerPrefs.SetFloat("Music", (Value));
        PlayerPrefs.Save();
    }
}
