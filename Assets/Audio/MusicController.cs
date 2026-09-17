using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip Intro, Loop;
    [SerializeField] private AudioMixerSnapshot defaultSnapshot, pausedSnapshot;
    [SerializeField] private float transitionDuration = 0.3f;
    [SerializeField] private AudioSource loopSource, introSource;
    // [SerializeField] private InputAction _pauseAction;
    
    
    private void Awake()
    {
        // _loopSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        StartMusic();
        PauseStateController.Instance.PauseAction.performed += context => FadeAudio(PauseStateController.Instance.Paused);
    }

    private void Update()
    {
        
    }

    private void FadeAudio(bool fadeOut)
    {
        AudioMixerSnapshot target = fadeOut ? pausedSnapshot : defaultSnapshot;
        if (target != null)
        {
            target.TransitionTo(transitionDuration);
        }
    }

    // private IEnumerator StartMusic()
    // {
    //     _loopSource.clip = Intro;
    //     _loopSource.Play();
    //     Debug.Log(_loopSource.clip.length);
    //     yield return new WaitForSeconds(_loopSource.clip.length);
    //     _loopSource.clip = Loop;
    //     _loopSource.PlayScheduled();
    //     _loopSource.loop = true;
    // }

    [SerializeField] private double introPadding = 0.1;
    private void StartMusic()
    {
        double startTime = AudioSettings.dspTime + 0.1;
        double introDuration = (double)introSource.clip.samples / introSource.clip.frequency;
        introSource.PlayScheduled(startTime);
        loopSource.PlayScheduled(startTime + introDuration + introPadding);
    }
    
}
