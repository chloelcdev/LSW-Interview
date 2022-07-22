using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool dragging = false;
    float distance;

    [SerializeField] Image icon;
    [SerializeField] Image overlay;
    RectTransform rectTransform;

    RectTransform previousParent = null;

    // this will keep the ui element visible over everything else while it's mid-drag
    RectTransform dragZone;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        previousParent = rectTransform.parent.GetComponent<RectTransform>();

        overlay.enabled = false;

        dragZone = UILocator.LocateUI("dragzone");
    }

    public void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
        overlay.sprite = sprite;
    }

    public void OnPointerEnter(PointerEventData data)
    {
        overlay.enabled = true;
    }

    public void OnPointerExit(PointerEventData data)
    {

        if (!dragging)
            overlay.enabled = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        StartDrag();
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (dragging)
            overlay.enabled = false;

        Drop();
    }

    void StartDrag()
    {
        dragging = true;
        rectTransform.SetParent(dragZone);
    }

    void Drop()
    {
        dragging = false;

        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hit.collider != null)
        {
            var slot = hit.collider.GetComponent<InventorySlot>();
            if (slot != null)
            {
                rectTransform.anchoredPosition = slot.rectTransform.anchoredPosition;
                rectTransform.SetParent(slot.rectTransform);
                return;
            }
        }

        rectTransform.SetParent(previousParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }

    void Update()
    {
        if (dragging)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}