using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelResizer : MonoBehaviour
{
    public RectTransform panel;

    void Start()
    {
        if (panel == null)
        {
            panel = GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        float width = Screen.width * 0.1f;
        panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
