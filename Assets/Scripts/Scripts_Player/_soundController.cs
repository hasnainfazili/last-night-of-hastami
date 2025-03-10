using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class _soundController : MonoBehaviour
{
    public AudioSource _player;
    public AudioClip rock;
    public AudioClip woosh;
    public AudioClip _groundHit;
    public AudioClip kick;

   
    private void PlayAudio()
    {
        _player.clip = rock;
        _player.Play();
    }
    void PlaySlash()
    {
        _player.clip = woosh;
        _player.Play();
    }
    void PlayWoosh()
    {
        _player.clip = kick;
        _player.Play();
    }
    void PlayHit()
    {
        _player.clip = _groundHit;
        _player.Play();
    }
  
}
