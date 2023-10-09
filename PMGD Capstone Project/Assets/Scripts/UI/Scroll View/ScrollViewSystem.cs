using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSystem : MonoBehaviour
{
    private ScrollRect scrollRect;
    private RectTransform contentRect;

    [SerializeField] ItemSlot[] itemLength;

    [SerializeField] ScrollButton leftButton;
    [SerializeField] ScrollButton rightButton;
    [SerializeField] ScrollButton downButton;
    [SerializeField] ScrollButton upButton;

    // Kecepatan scroll yang akan dihitung
    [SerializeField] private float baseScrollSpeed;
    private float scrollSpeed;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentRect = scrollRect.content;
    }

    private void Update()
    {
        if (leftButton != null && leftButton.isDown)
        {
            ScrollLeft();
        }

        if (rightButton != null && rightButton.isDown)
        {
            ScrollRight();
        }

        if (downButton != null && downButton.isDown)
        {
            ScrollDown();
        }

        if (upButton != null && upButton.isDown)
        {
            ScrollUp();
        }

        itemLength = GetComponentsInChildren<ItemSlot>();
        SetScrollSpeed(itemLength.Length);

        KeyboardControl();
    }

    void KeyboardControl()
    {
        float scrollUpInput = InputManager.inputSystem.UI.ScrollUp.ReadValue<float>();
        float scrollDownInput = InputManager.inputSystem.UI.ScrollDown.ReadValue<float>();

        if(scrollUpInput > 0)
        {
            ScrollUp();
        }
        
        if(scrollDownInput > 0)
        {
            ScrollDown();
        }
    }

    [ContextMenu("Scroll Left")]
    public void ScrollLeft()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition >= 0)
            {
                scrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    [ContextMenu("Scroll Right")]
    public void ScrollRight()
    {
        if (scrollRect != null)
        {
            if (scrollRect.horizontalNormalizedPosition <= 1)
            {
                scrollRect.horizontalNormalizedPosition += scrollSpeed;
            }
        }
    }

    [ContextMenu("Scroll Down")]
    public void ScrollDown()
    {
        if (scrollRect != null)
        {
            if (scrollRect.verticalNormalizedPosition >= 0)
            {
                scrollRect.verticalNormalizedPosition -= scrollSpeed;
            }
        }
    }

    [ContextMenu("Scroll Up")]
    public void ScrollUp()
    {
        if (scrollRect != null)
        {
            if (scrollRect.verticalNormalizedPosition <= 1)
            {
                scrollRect.verticalNormalizedPosition += scrollSpeed;
            }
        }
    }

    // Method ini akan mengatur ulang kecepatan scroll berdasarkan jumlah item
    public void SetScrollSpeed(int itemCount)
    {
        // Hitung scrollSpeed berdasarkan jumlah item
        if (itemCount > 0)
        {
            scrollSpeed = baseScrollSpeed / itemCount;
        }
    }
}
