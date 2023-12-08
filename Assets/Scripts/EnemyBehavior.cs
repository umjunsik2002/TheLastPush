using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    
    Dictionary<string, int> noteTypes = new Dictionary<string, int>();
    Dictionary<string, Tuple<int, int>> dirTypes = new Dictionary<string, Tuple<int, int>>();

    public Tuple <int,int> EnemyDir = new Tuple<int,int>(0,0);

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

    // Manually assign note and direction game objects
    public GameObject[] manualNoteAssignments;
    public GameObject[] manualDirectionAssignments;

    private EnemyController enemy;

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

        enemy = GetComponent<EnemyController>();
    }

    public NoteDir[] GetNoteDirectionPairs()
    {
        NoteDir[] noteDirectionPairs = new NoteDir[Mathf.Min(manualNoteAssignments.Length, manualDirectionAssignments.Length)];
        int sum = 0;

        for (int i = 0; i < noteDirectionPairs.Length; i++)
        {
            GameObject note = manualNoteAssignments[i];
            GameObject direction = manualDirectionAssignments[i];

            if (note != null && direction != null)
            {
                int noteValue = noteTypes[note.name.Substring(0, note.name.Length)];
                Tuple<int, int> directionValue = dirTypes[direction.name.Substring(0, direction.name.Length)];

                NoteDir pair = new NoteDir
                {
                    note = noteValue,
                    dir = directionValue
                };

                string noteName = note.name;
                noteName = noteName.Substring(0, noteName.Length);
                sum += noteTypes[noteName];

                if (noteName == "EighthRest" || noteName == "QuarterRest" || noteName == "HalfRest" || noteName == "WholeRest")
                {
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

    public IEnumerator RotateEnemySmoothly(Vector3 targetDirection)
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
                    StartCoroutine(RotateEnemySmoothly(new Vector3(noteDir.dir.Item1, 0, noteDir.dir.Item2)));
                    EnemyDir = noteDir.dir;
                }
                enemy.moveEnemy(noteDir.dir);
                yield return new WaitForSeconds(0.5f);
            }
        }
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
