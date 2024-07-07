using UnityEngine;

public class ToggleDragManager : MonoBehaviour
{
    public DraggableImage[] draggableImages;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (DraggableImage draggableImage in draggableImages)
            {
                draggableImage.ToggleDragging();
            }
        }
    }
}
