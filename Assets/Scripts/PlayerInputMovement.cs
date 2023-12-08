using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInputMovement : MonoBehaviour
{
    Dictionary<string, int> noteTypes = new Dictionary<string, int>();
    Dictionary<string, Tuple<int,int> > dirTypes = new Dictionary<string, Tuple<int,int>>();

    [System.Serializable]
    public struct NoteDirectionPair
    {
        public GameObject note;
        public GameObject direction;
    }
    public struct NoteDir{
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
    public NoteDir GetNoteDir(NoteDirectionPair noteDirectionPair){
        NoteDir noteDir = new NoteDir();
        noteDir.note = noteTypes[noteDirectionPair.note.name.Substring(0, noteDirectionPair.note.name.Length - 7)];
        noteDir.dir = new Tuple<int, int>(dirTypes[noteDirectionPair.direction.name.Substring(0, noteDirectionPair.direction.name.Length - 7)].Item1, dirTypes[noteDirectionPair.direction.name.Substring(0, noteDirectionPair.direction.name.Length - 7)].Item2);
        return noteDir;
    }
    public NoteDirectionPair[] GetNoteDirectionPairs()
    {
        NoteDirectionPair[] noteDirectionPairs = new NoteDirectionPair[Mathf.Min(noteSlots.Length, directionSlots.Length)];
        int sum = 0;
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
                string noteName = note.name;
                //cut out the (Clone) part of the name
                noteName = noteName.Substring(0, noteName.Length - 7);
                sum += noteTypes[noteName];
                noteDirectionPairs[i] = pair;
                NoteDir noteDir = GetNoteDir(pair);
                for(int k = 0; k <= noteDir.note; k++){
                    move(noteDir);
                }
            }
        }
        Debug.Log($"Sum: {sum}");

        PrintNoteDirectionPairs(noteDirectionPairs);
        return noteDirectionPairs;
    }
    IEnumerator move(NoteDir noteDir){
        player.movePlayer(noteDir.dir);
        yield return new WaitForSeconds(1);

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