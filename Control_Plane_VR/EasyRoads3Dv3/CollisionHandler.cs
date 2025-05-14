using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        Debug.Log("¡Juego detenido! Colisión detectada.");

        // Mostrar un mensaje o una pantalla de "Game Over" (opcional)
        // ...

        // Para reiniciar el juego, llama a RestartGame después de unos segundos
        Invoke("RestartGame", 3f); // Espera 3 segundos antes de reiniciar el juego
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
