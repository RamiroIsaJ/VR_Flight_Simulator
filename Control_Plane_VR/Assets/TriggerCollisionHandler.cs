using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class TriggerCollisionHandler : MonoBehaviour
{
    private AudioSource airplaneAudioSource;
    public Text gameOverText; // Asigna el UI Text en el Inspector

    void Start()
    {
        // Busca el componente AudioSource en un GameObject específico en la jerarquía
        GameObject audioObject = GameObject.Find("AirplaneAudio");
        if (audioObject != null)
        {
            airplaneAudioSource = audioObject.GetComponent<AudioSource>();

            if (airplaneAudioSource == null)
            {
                Debug.LogError("No se encontró un componente AudioSource en el GameObject 'AirplaneAudio'.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el GameObject 'AirplaneAudio' en la jerarquía.");
        }

        // Asegúrate de que el UI Text esté desactivado al inicio
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisión detectada con: " + other.gameObject.name);

        // Detener el juego
        StopGame();

        // Detener el audio
        StopAudio();

        // Mostrar mensaje de Game Over y reiniciar la escena después de 5 segundos
        StartCoroutine(ShowGameOverAndRestart());
    }

    private void StopGame()
    {
        // Detiene el tiempo del juego
        Time.timeScale = 0;

        // Opcionalmente, puedes mostrar un mensaje o una pantalla de "Game Over"
        Debug.Log("¡Juego detenido! Colisión detectada.");
    }

    private void StopAudio()
    {
        if (airplaneAudioSource != null)
        {
            airplaneAudioSource.Stop();
            Debug.Log("Audio detenido.");
        }
    }

    private IEnumerator ShowGameOverAndRestart()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        yield return new WaitForSecondsRealtime(5); // Esperar 5 segundos en tiempo real

        // Reiniciar la escena actual
        Time.timeScale = 1; // Asegúrate de restaurar el tiempo de juego antes de reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
