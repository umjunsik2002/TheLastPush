using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollerMaterial : MonoBehaviour
{
    [SerializeField] private float _x, _y;
 
    void Update()
    {
        GetComponent<Renderer> ().material.mainTextureOffset = new Vector2(_x * Time.time, _y * Time.time);
    }
}