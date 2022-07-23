using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    bool dragging = false;
    float distance;

    [SerializeField] EquipmentScrob equipmentData;

    [SerializeField] Image icon;
    [SerializeField] Image overlay;
    RectTransform rectTransform;

    RectTransform previousParent = null;

    InventorySlot currentSlot = null;

    // this will keep the ui element visible over everything else while it's mid-drag
    RectTransform dragZone;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        previousParent = rectTransform.parent.GetComponent<RectTransform>();

        overlay.enabled = false;

        dragZone = UILocator.LocateUI("dragzone");
    }

    public void Setup(EquipmentScrob data, InventorySlot slot)
    {
        currentSlot = slot;

        equipmentData = data;
        SetIcon(data.GetInfo()[0].sprite);
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
        StartCoroutine(DropRoutine());
    }

    IEnumerator DropRoutine()
    {
        dragging = false;

        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hit.collider != null)
        {
            var slot = hit.collider.GetComponent<InventorySlot>();
            if (slot != null && slot.currentItem == null)
            {
                rectTransform.anchoredPosition = slot.rectTransform.anchoredPosition;
                rectTransform.SetParent(slot.rectTransform);

                currentSlot.currentItem = null;
                slot.currentItem = this;

                yield break; 
            }
        }

        rectTransform.SetParent(previousParent);
        rectTransform.anchoredPosition = Vector2.zero;

        yield break;
    }

    void Update()
    {
        if (dragging)
        {
            rectTransform.position = Input.mousePosition;
        }
    }
}