using UnityEngine;
using UnityEngine.Audio;

public class PlaySongOnStart : MonoBehaviour
{
    public AudioResource Song;
    void Start()
    {
        MusicSoundManager.ChangeSongStatic(Song);
    }
}
