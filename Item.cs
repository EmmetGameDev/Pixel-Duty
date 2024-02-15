using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite icon;
    [Header("Variable 1")]
    public int var1;
    public int req1;
    [Header("Variable 2")]
    public int var2;
    public int req2;
    //Handling items creating passive particles
    [Header("Particles")]
    public bool doesMakePassPart;
    public Color particleColor;
}