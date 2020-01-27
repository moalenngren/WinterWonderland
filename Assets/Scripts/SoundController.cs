using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    [SerializeField] AudioClip dieSound;
    [SerializeField] AudioClip jumpSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        if (instance != null)
        {
            Debug.LogError("There cannot be more than one instance of SoundController in the scene");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayDieSound()
    {
        instance.audioSource.clip = instance.dieSound;
        instance.audioSource.Play();
    }

    public static void PlayJumpSound()
    {
        instance.audioSource.clip = instance.jumpSound;
        instance.audioSource.Play();
    }
}
