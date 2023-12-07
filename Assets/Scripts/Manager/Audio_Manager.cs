using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance; // Singleton para acceder fácilmente desde otros scripts
    public AudioSource soundSource; // Fuente de audio para efectos de sonido
    public AudioSource musicSource; // Fuente de audio para la música de ambiente

    void Awake()
    {
        // Configura el Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayClickSound(AudioClip clickSound = null)
    {
        // Reproduce el sonido del clic del botón
        if (clickSound != null)
        {
            soundSource.clip = clickSound;
            soundSource.Play();
        }
    }

    // Puedes agregar más funciones para reproducir diferentes sonidos según sea necesario
}