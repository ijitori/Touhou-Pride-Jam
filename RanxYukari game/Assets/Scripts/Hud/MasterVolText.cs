using UnityEngine;
using UnityEngine.Audio;
using TMPro;
public class MasterVolText : MonoBehaviour
{
    [SerializeField] AudioMixer AudioMixer;
    [SerializeField] string ChannelName;
    float Vol;
    TMP_Text Display;
    

    void Start()
    {
        Display = this.gameObject.GetComponent<TMP_Text>();
    }

    
    void Update()
    {
        AudioMixer.GetFloat(ChannelName, out Vol);
        Display.text = ("" + Vol);
    }
}
