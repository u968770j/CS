using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] AudioClip victory;
    [SerializeField] AudioClip defeat;

    // ����BGM���Đ����邽�߂̃��\�b�h
    public void PlayVictory()
    {
        audioSource.Stop(); // ���݂�BGM���~
        audioSource.PlayOneShot(victory); // �V����BGM���Đ�
    }

    public void PlayDefeat()
    {
        audioSource.Stop(); // ���݂�BGM���~
        audioSource.PlayOneShot(defeat); // �V����BGM���Đ�
    }
}
