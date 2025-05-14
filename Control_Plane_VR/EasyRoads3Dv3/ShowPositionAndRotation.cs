using UnityEngine;
using UnityEngine.UI;

public class ShowZRotation : MonoBehaviour
{
    public GameObject targetObject; // Arrastra el objeto al que quieres seguir
    public Text infoText; // Arrastra el Text del Canvas a este campo en el inspector

    private void Update()
    {
        if (targetObject != null)
        {
            // Obtiene la rotación del objeto en grados alrededor del eje Z
            float zRotation = targetObject.transform.eulerAngles.z;

            // Actualiza el texto del Canvas
            infoText.text = $"Rotación: {zRotation:F2}°";
        }
        else
        {
            infoText.text = "No se ha asignado un objeto.";
        }
    }
}
