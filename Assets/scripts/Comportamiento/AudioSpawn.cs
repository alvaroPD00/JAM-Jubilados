using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactivar : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ReproducirAudio();
    }

    private void OnEnable()
    {
        ReproducirAudio();
    }

    private void ReproducirAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se encontró AudioSource en " + gameObject.name);
        }
    }
}
