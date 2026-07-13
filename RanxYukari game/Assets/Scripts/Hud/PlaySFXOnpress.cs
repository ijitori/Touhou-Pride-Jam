using UnityEngine;
using UnityEngine.Audio;
public class PlaySFXOnpress : MonoBehaviour
{
    SFXEmitter SFXEmitter;
    public AudioResource SoundToPlay;
    void Start()
    {
        SFXEmitter = this.gameObject.GetComponent<SFXEmitter>();
    }

    // Update is called once per frame
    public void Press()
    {
        SFXEmitter.CreateOneTimeSFX(SoundToPlay);
    }
}
