using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputMovement : MonoBehaviour
{

    [System.Serializable]
    public struct NoteDirectionPair
    {
        public GameObject note;
        public GameObject direction;
    }

    public GameObject[] noteSlots;       // Assign UI grid note slots in the Unity Editor
    public GameObject[] directionSlots;   // Assign UI grid direction slots in the Unity Editor
    //public Button yourUIButton;           // Assign your UI button in the Unity Editor

    void Start()
    {
        
    }

    public NoteDirectionPair[] GetNoteDirectionPairs()
    {
        NoteDirectionPair[] noteDirectionPairs = new NoteDirectionPair[Mathf.Min(noteSlots.Length, directionSlots.Length)];

        for (int i = 0; i < noteDirectionPairs.Length; i++)
        {
            GameObject note = FindNoteInSlot(noteSlots[i]);
            GameObject direction = FindDirectionInSlot(directionSlots[i]);

            if (note != null && direction != null)
            {
                NoteDirectionPair pair = new NoteDirectionPair
                {
                    note = note,
                    direction = direction
                };

                noteDirectionPairs[i] = pair;
            }
        }

        PrintNoteDirectionPairs(noteDirectionPairs);
        return noteDirectionPairs;
    }

    GameObject FindNoteInSlot(GameObject slot)
    {
        Transform[] children = slot.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.CompareTag("Note"))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    GameObject FindDirectionInSlot(GameObject slot)
    {
        Transform[] children = slot.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.CompareTag("Direction"))
            {
                return child.gameObject;
            }
        }

        return null;
    }

    void PrintNoteDirectionPairs(NoteDirectionPair[] pairs)
    {
        foreach (var pair in pairs)
        {
            if (pair.note != null && pair.direction != null)
            {
                Debug.Log($"Note: {pair.note.name}, Direction: {pair.direction.name}");
            }
        }
    }

    public void OnButtonClick()
    {
        GetNoteDirectionPairs();
    }
}