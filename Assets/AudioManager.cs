using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public void ChangeAudio(float audio)
    {
        mixerGroup.audioMixer.SetFloat("volume", audio);
    }
}
