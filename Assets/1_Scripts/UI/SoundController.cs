using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("- Player Sound")]
    public AudioSource playerAudio;        // 플레이어 스피커
    public AudioSource playerAudio_Double;     // 겹치는 오디오용 스피커 -> 숨소리

    public AudioClip[] audioWalking;             // 음원
    public AudioClip[] audioRunningBreathe;

    public AudioClip audioDead;
    public AudioClip[] audioBeAttacked;


    [Header("- Scene Sound")]
    public AudioSource SceneAudio;         // 씬 스피커
    public AudioClip audioLoadingComplete;

    private void Start() {
        playerAudio = gameObject.AddComponent<AudioSource>();
        playerAudio_Double = gameObject.AddComponent<AudioSource>();

        SceneAudio = gameObject.AddComponent<AudioSource>();
    }


}
