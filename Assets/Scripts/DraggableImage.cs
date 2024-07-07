using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableImage : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private bool isDragging = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
        {
            // �⺻������ �̹����� �θ� �Ǵ� ĵ������ �ִ��� Ȯ���մϴ�.
            if (canvas != null)
            {
                // �̹����� �������� �̵��Ͽ� �ٸ� UI ��ҵ� ���� ǥ�õǵ��� �մϴ�.
                rectTransform.SetAsLastSibling();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // �̹����� ��ġ�� ���콺 �����Ϳ� �°� ������Ʈ�մϴ�.
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // �巡�װ� ������ ���� ó���� ���⿡ �߰��� �� �ֽ��ϴ�.
    }

    public void ToggleDragging()
    {
        isDragging = !isDragging;
    }
}
