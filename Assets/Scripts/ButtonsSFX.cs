using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSFX : MonoBehaviour
{
    private AudioSource sfx;
    public AudioClip itemSelectSound;
    public AudioClip itemDeselectSound;
    public AudioClip buttonMouseOverSound;
    public AudioClip scoreIncreaseSound;
    public AudioClip scoreDecreaseSound;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
    }

    public void PlaySelectSound()
    {
        sfx.PlayOneShot(itemSelectSound);
    }

    public void PlayDeselectSound()
    {
        sfx.PlayOneShot(itemDeselectSound);
    }

    public void PlayMouseOverSound()
    {
        sfx.PlayOneShot(buttonMouseOverSound);
    }

    public void PlayIncreaseSound()
    {
        sfx.PlayOneShot(scoreIncreaseSound);
    }

    public void PlayDecreaseSound()
    {
        sfx.PlayOneShot(scoreDecreaseSound);
    }
}
