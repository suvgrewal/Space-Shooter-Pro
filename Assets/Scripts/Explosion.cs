using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _explosionSoundClip;

    void Start()
    {
        Destroy(this.gameObject, 3.0f);

        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = _explosionSoundClip;

        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
