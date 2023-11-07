using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;

    // 他のBGMを再生するためのメソッド
    public void PlayVictory()
    {
        audioSource.Stop(); // 現在のBGMを停止
        audioSource.PlayOneShot(victory); // 新しいBGMを再生
    }

    public void PlayDefeat()
    {
        audioSource.Stop(); // 現在のBGMを停止
        audioSource.PlayOneShot(defeat); // 新しいBGMを再生
    }
}
