using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIUIMeter : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] Slider slider;
    [SerializeField] Image image;

    public Vector3 position
    {
        set
        {
            Debug.DrawLine(value, value + Vector3.up * 3);
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);
            GetComponent<RectTransform>().anchorMin = viewportPoint;
            GetComponent<RectTransform>().anchorMax = viewportPoint;
        }
    }

    // A property to control the value of a slider UI element
    public float value
    {
        set
        {
            // Set the value of the slider to the provided float value
            slider.value = value;
        }
    }

    // A property to control the text of a label UI element
    public string text
    {
        set
        {
            // Set the text of the label to the provided string value
            label.text = value;
        }
    }

    // A property to control the visibility of a GameObject
    public bool visible
    {
        set
        {
            // Activate or deactivate the GameObject based on the provided boolean value
            gameObject.SetActive(value);
        }
    }

    // A property to control the alpha (transparency) of an image UI element
    public float alpha
    {
        set
        {
            // Retrieve the current color of the image
            Color color = image.color;
            // Set the alpha component of the color to the provided float value
            color.a = value;
            // Update the color of the image with the modified color
            image.color = color;
        }
    }
}