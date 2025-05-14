using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Transition : MonoBehaviour
{
    public Image displayImage; // El objeto de imagen en el que se mostrar�n las im�genes
    public Sprite[] images; // Array de im�genes
    public float transitionSpeed = 0.5f; // Velocidad de la transici�n (cuanto menor sea, m�s lenta ser�)
    private int currentIndex = 0;
    private int nextIndex = 0;
    private bool isTransitioning = false;
    private float transitionStartTime;

    void Start()
    {
        if (images.Length > 0)
        {
            displayImage.sprite = images[currentIndex];
        }
    }

    void Update()
    {
        if (!isTransitioning)
        {
            // Controla la transici�n autom�tica entre im�genes
            nextIndex = (currentIndex + 1) % images.Length;
            StartCoroutine(TransitionToNextImage());
        }
    }

    IEnumerator TransitionToNextImage()
    {
        isTransitioning = true;
        transitionStartTime = Time.time;

        // Clona el objeto de imagen actual
        Image nextImage = Instantiate(displayImage, displayImage.transform.parent);

        // Configura la nueva imagen para que inicie fuera de la pantalla a la derecha
        nextImage.rectTransform.localPosition = new Vector3(displayImage.rectTransform.rect.width, 0, 0);
        nextImage.sprite = images[nextIndex];

        // Mueve ambas im�genes para simular el desplazamiento
        while (Time.time - transitionStartTime <= 1.0f / transitionSpeed)
        {
            float t = (Time.time - transitionStartTime) * transitionSpeed;
            // Calcula las posiciones para que la transici�n cubra toda la imagen
            displayImage.rectTransform.localPosition = Vector3.Lerp(Vector3.zero, new Vector3(-displayImage.rectTransform.rect.width, 0, 0), t);
            nextImage.rectTransform.localPosition = Vector3.Lerp(new Vector3(displayImage.rectTransform.rect.width, 0, 0), Vector3.zero, t);
            yield return null;
        }

        // Asegura que la posici�n final sea correcta
        displayImage.rectTransform.localPosition = Vector3.zero;
        nextImage.rectTransform.localPosition = Vector3.zero;

        // Actualiza el �ndice actual al siguiente �ndice
        currentIndex = nextIndex;

        // Elimina la imagen clonada
        Destroy(nextImage.gameObject);

        isTransitioning = false;
    }
}
