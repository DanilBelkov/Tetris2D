using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private AudioClip[] _clips;

    public void PlauSoundFreez()
    {
        _audio.PlayOneShot(_clips[0]);
    }
    public void PlauSoundDeleteLine()
    {
        _audio.PlayOneShot(_clips[1]);
    }
    public void PlauSoundPause()
    {
        _audio.PlayOneShot(_clips[2]);
    }
    public void PlauSoundGameOver()
    {
        _audio.PlayOneShot(_clips[3]);
    }
}
