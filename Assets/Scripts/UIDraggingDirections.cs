using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggingDirections : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;
    
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private GameObject clone;
    private Vector2 cloneOriginalPosition;
    [SerializeField] private AudioSource juice;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        float offset = 0.01f;
        if (Mathf.Abs(rectTransform.anchoredPosition.x - originalPosition.x) < offset &&
            Mathf.Abs(rectTransform.anchoredPosition.y - originalPosition.y) < offset)
        {
            clone = Instantiate(gameObject, transform.parent);
            RectTransform cloneRectTransform = clone.GetComponent<RectTransform>();
            cloneRectTransform.anchoredPosition = rectTransform.anchoredPosition;
            cloneOriginalPosition = cloneRectTransform.anchoredPosition;
            canvasGroup.blocksRaycasts = false;
            cloneRectTransform.SetAsLastSibling();
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (clone != null)
        {
            clone.GetComponent<RectTransform>().anchoredPosition += eventData.delta / GetCanvasScaleFactor();
        }
        else
        {
            rectTransform.anchoredPosition += eventData.delta / GetCanvasScaleFactor();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        juice.Play();
        if (clone != null)
        {
            canvasGroup.blocksRaycasts = true;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(clone.transform.position, rectTransform.sizeDelta, 0f);
            float minDistance = float.MaxValue;
            RectTransform targetDirectionBoxRect = null;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("DirectionBox"))
                {
                    RectTransform directionBoxRect = collider.GetComponent<RectTransform>();
                    float distance = Vector3.Distance(clone.transform.position, directionBoxRect.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetDirectionBoxRect = directionBoxRect;
                    }
                }
            }

            if (targetDirectionBoxRect != null)
            {
                // Remove existing note child if any old direction
                foreach (Transform child in targetDirectionBoxRect.transform)
                {
                    Destroy(child.gameObject);
                }

                Vector3 localPos = rectTransform.parent.InverseTransformPoint(targetDirectionBoxRect.position);
                clone.GetComponent<RectTransform>().anchoredPosition = localPos;

                // Set the cloned note as a child of the target direction box
                clone.transform.SetParent(targetDirectionBoxRect);
            }
            else
            {
                Destroy(clone);
            }
        }
        else
        {
            canvasGroup.blocksRaycasts = true;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(rectTransform.position, rectTransform.sizeDelta, 0f);
            float minDistance = float.MaxValue;
            RectTransform targetDirectionBoxRect = null;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("DirectionBox"))
                {
                    RectTransform directionBoxRect = collider.GetComponent<RectTransform>();
                    float distance = Vector3.Distance(rectTransform.position, directionBoxRect.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetDirectionBoxRect = directionBoxRect;
                    }
                }
            }

            if (targetDirectionBoxRect != null)
            {
                // Remove existing note child if any
                foreach (Transform child in targetDirectionBoxRect.transform)
                {
                    Destroy(child.gameObject);
                }

                // Set the note as a child of the target note box
                rectTransform.SetParent(targetDirectionBoxRect);

                
                Vector3 localPos = rectTransform.parent.InverseTransformPoint(targetDirectionBoxRect.position);
                rectTransform.anchoredPosition = localPos;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private float GetCanvasScaleFactor()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("Canvas component not found in parent.");
            return 1f; // Default scale factor if canvas is not found
        }

        return canvas.scaleFactor;
    }
}