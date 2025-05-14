using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpeedBasedAudio : MonoBehaviour
{
    public Rigidbody targetRigidbody; // El Rigidbody del objeto en movimiento
    public float maxVolume = 1.0f; // Volumen m�ximo del audio
    public float maxPitch = 3.0f; // Pitch m�ximo del audio
    public float maxSpeed = 20.0f; // Velocidad m�xima del objeto, usada para normalizar

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Asegurarse de que el audio est� en loop
        if (!audioSource.loop)
        {
            audioSource.loop = true;
        }

        // Asegurarse de que el audio est� reproduci�ndose
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void Update()
    {
        if (targetRigidbody != null)
        {
            float speed = targetRigidbody.velocity.magnitude;

            // Ajustar el volumen basado en la velocidad
            audioSource.volume = Mathf.Clamp(speed / maxSpeed, 0.0f, maxVolume);

            // Ajustar el pitch basado en la velocidad
            audioSource.pitch = Mathf.Clamp(speed / maxSpeed, 1.0f, maxPitch);
        }
        else
        {
            Debug.LogWarning("El Rigidbody del objeto en movimiento no est� asignado.");
        }
    }
}
