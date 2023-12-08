using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDraggingNotes : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Vector2 originalPosition;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private GameObject clone;
    private Vector2 cloneOriginalPosition;
    [SerializeField] private AudioSource juice;
    [SerializeField] private ParticleSystem juice2; 

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
            cloneRectTransform.SetParent(transform.parent); // Set the parent to the same as the original note
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
        juice2.transform.position = transform.position;
        juice2.Play();
        if (clone != null)
        {
            canvasGroup.blocksRaycasts = true;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(clone.transform.position, rectTransform.sizeDelta, 0f);
            float minDistance = float.MaxValue;
            RectTransform targetNoteBoxRect = null;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NoteBox"))
                {
                    RectTransform noteBoxRect = collider.GetComponent<RectTransform>();
                    float distance = Vector3.Distance(clone.transform.position, noteBoxRect.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetNoteBoxRect = noteBoxRect;
                    }
                }
            }

            if (targetNoteBoxRect != null)
            {
                // Remove existing note child if any old notes
                foreach (Transform child in targetNoteBoxRect.transform)
                {
                    Destroy(child.gameObject);
                }

                
                Vector3 localPos = rectTransform.parent.InverseTransformPoint(targetNoteBoxRect.position);
                clone.GetComponent<RectTransform>().anchoredPosition = localPos;
                // Set the cloned note as a child of the target note box
                clone.transform.SetParent(targetNoteBoxRect);
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
            RectTransform targetNoteBoxRect = null;

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("NoteBox"))
                {
                    RectTransform noteBoxRect = collider.GetComponent<RectTransform>();
                    float distance = Vector3.Distance(rectTransform.position, noteBoxRect.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        targetNoteBoxRect = noteBoxRect;
                    }
                }
            }

            if (targetNoteBoxRect != null)
            {
                // Remove existing note child if any
                foreach (Transform child in targetNoteBoxRect.transform)
                {
                    Destroy(child.gameObject);
                }

                // Set the note as a child of the target note box
                rectTransform.SetParent(targetNoteBoxRect);
                Vector3 localPos = rectTransform.parent.InverseTransformPoint(targetNoteBoxRect.position);
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
