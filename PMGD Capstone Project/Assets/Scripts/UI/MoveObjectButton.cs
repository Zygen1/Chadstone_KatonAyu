using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveObjectButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Option")]
    [SerializeField] bool moveUp;
    [SerializeField] bool moveDown;

    [SerializeField] GameObject objToMove;
    [SerializeField] float speed;
    [SerializeField] Vector2 dir;
    [SerializeField] float maxVertical;

    private bool isPointerDown = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        StartCoroutine(ContinuousMovement());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
    }

    private IEnumerator ContinuousMovement()
    {
        while (isPointerDown)
        {
            if (moveUp)
            {
                if (objToMove.transform.localPosition.y > maxVertical)
                {
                    objToMove.transform.Translate(dir * speed * Time.deltaTime);
                }
            }

            if (moveDown)
            {
                if (objToMove.transform.localPosition.y < maxVertical)
                {
                    objToMove.transform.Translate(dir * speed * Time.deltaTime);
                }
            }

            yield return null;
        }
    }
}
