using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Makes this stuff visible in the inspector.
public class Dialogue
{

    public string name; // NPC/Main character name

    [TextArea(3, 10)]  // Makes the text boxes bigger in Unity inspector for visability when writing lengthier dialogue.
    public string[] sentences; // Dialogue

}
