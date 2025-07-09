using UnityEngine;
using UnityEngine.UI;

public class CrosshairUIManager : MonoBehaviour
{
    [SerializeField] private Image crosshairImage;

    public void UpdateCrosshair(Sprite sprite, Vector2 scale)
    {
        if (crosshairImage == null) return;

        crosshairImage.sprite = sprite;
        crosshairImage.rectTransform.localScale = scale;
        crosshairImage.enabled = sprite != null;
    }
}