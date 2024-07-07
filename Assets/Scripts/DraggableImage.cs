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
            // 기본적으로 이미지의 부모가 되는 캔버스에 있는지 확인합니다.
            if (canvas != null)
            {
                // 이미지를 앞쪽으로 이동하여 다른 UI 요소들 위에 표시되도록 합니다.
                rectTransform.SetAsLastSibling();
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // 이미지의 위치를 마우스 포인터에 맞게 업데이트합니다.
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 드래그가 끝났을 때의 처리를 여기에 추가할 수 있습니다.
    }

    public void ToggleDragging()
    {
        isDragging = !isDragging;
    }
}
