using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(0.1f, 3f)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
}

public class Audio_Manager : MonoBehaviour
{
    public static Audio_Manager instance;

    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

            // Configuración para audio espacial
            sound.source.spatialBlend = 1f; // 1f indica audio completamente espacializado
            sound.source.minDistance = 1f; // Distancia mínima de audición
            sound.source.maxDistance = 10f; // Distancia máxima de audición
        }
    }

    public void Play(string name, Vector3 position)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Configurar la posición del sonido en el espacio
        sound.source.transform.position = position;

        sound.source.Play();
    }

    public void UpdateSoundPosition(string name, Vector3 newPosition)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == name);
        if (sound != null)
        {
            sound.source.transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
    }
}