using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Aseg�rate de incluir esta directiva

public class SlideBetweenImages : MonoBehaviour
{
    public Image image1;
    public Image image2;
    public float slideSpeed = 1.0f;

    private bool isImage1 = true; // Estado inicial

    void Start()
    {
        // Configura la visibilidad inicial
        image1.gameObject.SetActive(true);
        image2.gameObject.SetActive(false);

        // Asegura que ambas im�genes est�n en la misma posici�n inicial
        image2.rectTransform.anchoredPosition = image1.rectTransform.anchoredPosition;
    }

    public void SlideToNextImage()
    {
        StartCoroutine(SlideImage());
    }

    IEnumerator SlideImage()
    {
        // Determina la posici�n final de las im�genes
        float targetPosX = isImage1 ? -image1.rectTransform.rect.width : 0;

        // Activa la imagen siguiente
        if (isImage1)
        {
            image2.gameObject.SetActive(true);
        }
        else
        {
            image1.gameObject.SetActive(true);
        }

        while (Mathf.Abs(image1.rectTransform.anchoredPosition.x - targetPosX) > 1.0f)
        {
            // Calcula el paso del deslizamiento
            float step = slideSpeed * Time.deltaTime;
            image1.rectTransform.anchoredPosition = Vector2.Lerp(image1.rectTransform.anchoredPosition, new Vector2(targetPosX, 0), step);
            image2.rectTransform.anchoredPosition = Vector2.Lerp(image2.rectTransform.anchoredPosition, new Vector2(targetPosX + image1.rectTransform.rect.width, 0), step);
            yield return null;
        }

        // Ajusta las posiciones finales exactamente
        image1.rectTransform.anchoredPosition = new Vector2(targetPosX, 0);
        image2.rectTransform.anchoredPosition = new Vector2(targetPosX + image1.rectTransform.rect.width, 0);

        // Oculta la imagen anterior y activa la nueva
        if (isImage1)
        {
            image1.gameObject.SetActive(false);
        }
        else
        {
            image2.gameObject.SetActive(false);
        }

        // Alterna entre las im�genes
        isImage1 = !isImage1;
    }
}