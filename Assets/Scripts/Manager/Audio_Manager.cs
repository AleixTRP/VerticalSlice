using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance; // Singleton para acceder f�cilmente desde otros scripts
    public AudioSource soundSource; // Fuente de audio para efectos de sonido
    public AudioSource musicSource; // Fuente de audio para la m�sica de ambiente

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
        // Reproduce el sonido del clic del bot�n
        if (clickSound != null)
        {
            soundSource.clip = clickSound;
            soundSource.Play();
        }
    }

    // Puedes agregar m�s funciones para reproducir diferentes sonidos seg�n sea necesario
}