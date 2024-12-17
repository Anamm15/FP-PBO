using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    public AudioClip attackSound;
    public AudioClip hurtSound;
    public AudioClip walkSound;
    public AudioClip specialAttackSound;

    public void playSfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
