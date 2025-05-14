using UnityEngine;
using UnityEngine.UI;

public class ButtonActive : MonoBehaviour
{
    public Image displayImage;
    public Sprite[] scenarioImages;

    public void SelectScenario(int index)
    {
        if (index >= 0 && index < scenarioImages.Length)
        {
            displayImage.sprite = scenarioImages[index];
        }
    }
}
