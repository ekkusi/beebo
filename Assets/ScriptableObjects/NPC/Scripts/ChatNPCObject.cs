using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chat NPC", menuName = "NPC/Chat NPC")]
public class ChatNPCObject : ScriptableObject
{
    public Sprite sprite;
    public string message;
}
