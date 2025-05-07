using UnityEngine;
using UnityEngine.UI;

public class HelpImage : MonoBehaviour
{
    public Sprite imageToShow; // Drag your PNG here in Inspector

    private Image img;

    private GameObject player;

    void Start()
    {
        img = GetComponent<Image>();
        img.sprite = imageToShow;
        // if imageshow empty disable the image
        if (img.sprite == null)
        {
            img.enabled = false;
            return;
        }
        img.enabled = true;

        player = GameObject.Find("Player");
        if (player != null)
            player.SetActive(false);
    }

    void Update()
    {
        if (img.enabled && Input.GetMouseButtonDown(0))
        {
            // Check if mouse is over the image
            Vector2 localMousePosition = img.rectTransform.InverseTransformPoint(Input.mousePosition);
            if (img.rectTransform.rect.Contains(localMousePosition))
            {
                img.enabled = false; // Hide image on click
                if (player != null)
                    player.SetActive(true);
            }
        }

        if (img.enabled && Input.anyKeyDown)
        {
            img.enabled = false;
            if (player != null)
                player.SetActive(true);
        }
    }
}
