using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputMovement : MonoBehaviour
{
    Dictionary<string, int> noteTypes = new Dictionary<string, int>();
    Dictionary<string, Tuple<int, int>> dirTypes = new Dictionary<string, Tuple<int, int>>();

    [System.Serializable]
    public struct NoteDirectionPair
    {
        public GameObject note;
        public GameObject direction;
    }
    public struct NoteDir
    {
        public int note;
        public Tuple<int, int> dir;
    }
    public GameObject[] noteSlots;       // Assign UI grid note slots in the Unity Editor
    public GameObject[] directionSlots;   // Assign UI grid direction slots in the Unity Editor
    //public Button yourUIButton;           // Assign your UI button in the Unity Editor
    
    private PlayerController player;
    void Start()
    {
        noteTypes.Add("WholeNote", 8);
        noteTypes.Add("HalfNote", 4);
        noteTypes.Add("QuarterNote", 2);
        noteTypes.Add("EighthNote", 1);
        noteTypes.Add("WholeRest", 8);
        noteTypes.Add("HalfRest", 4);
        noteTypes.Add("QuarterRest", 2);
        noteTypes.Add("EighthRest", 1);

        dirTypes.Add("Up", new Tuple<int, int>(0, 1));
        dirTypes.Add("Down", new Tuple<int, int>(0, -1));
        dirTypes.Add("Left", new Tuple<int, int>(-1, 0));
        dirTypes.Add("Right", new Tuple<int, int>(1, 0));

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public NoteDir[] GetNoteDirectionPairs()
    {
        NoteDir[] noteDirectionPairs = new NoteDir[Mathf.Min(noteSlots.Length, directionSlots.Length)];
        int sum = 0;
        for (int i = 0; i < noteDirectionPairs.Length; i++)
        {
            GameObject note = FindNoteInSlot(noteSlots[i]);
            GameObject direction = FindDirectionInSlot(directionSlots[i]);

            if (note != null && direction != null)
            {
                int noteValue = noteTypes[note.name.Substring(0, note.name.Length - 7)];
                Tuple<int, int> directionValue = dirTypes[direction.name.Substring(0, direction.name.Length - 7)];
                NoteDir pair = new NoteDir
                {
                    note = noteValue,
                    dir = directionValue
                };
                string noteName = note.name;
                //cut out the (Clone) part of the name
                noteName = noteName.Substring(0, noteName.Length - 7);
                sum += noteTypes[noteName];
                if(noteName == "EighthRest" || noteName == "QuarterRest" || noteName == "HalfRest" || noteName == "WholeRest"){
                    pair.dir = new Tuple<int, int>(0, 0);
                }
                noteDirectionPairs[i] = pair;

            }
        }
        Debug.Log($"Sum: {sum}");

        PrintNoteDirectionPairs(noteDirectionPairs);
        return noteDirectionPairs;
    }
    
    private bool isRotating = false;
    public IEnumerator RotatePlayerSmoothly(Vector3 targetDirection)
    {
        isRotating = true;

        Quaternion fromRotation = transform.rotation;
        Quaternion toRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        float elapsedTime = 0f;
        float rotationTime = 0.1f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(fromRotation, toRotation, elapsedTime / rotationTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = toRotation;
        isRotating = false;
    }
    IEnumerator move(NoteDir[] noteDirs)
    {
        foreach (NoteDir noteDir in noteDirs)
        {
            for (int k = 0; k < noteDir.note; k++)
            {
                if (!isRotating)
                {
                    StartCoroutine(RotatePlayerSmoothly(new Vector3(noteDir.dir.Item1, 0, noteDir.dir.Item2)));
                    
                }
                player.movePlayer(noteDir.dir);
                yield return new WaitForSeconds(0.5f);
            }
        }

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

    void PrintNoteDirectionPairs(NoteDir[] pairs)
    {
        foreach (var pair in pairs)
        {
            Debug.Log($"Note: {pair.note}, Direction: {pair.dir}");
        }
    }

    public void OnButtonClick()
    {
        NoteDir[] moves = GetNoteDirectionPairs();
        StartCoroutine(move(moves));
    }
}