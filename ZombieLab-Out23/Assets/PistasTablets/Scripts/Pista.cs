using UnityEngine;

[CreateAssetMenu(fileName = "Pista", menuName = "ScriptableObjects/PistaScriptableObject", order = 1)]
public class Pista : ScriptableObject
{
    public string PistaName;
    public string TextRich;
    public float sizeText = 150;
    public Sprite image;
}
