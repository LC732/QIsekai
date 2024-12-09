
using UnityEngine;

[System.Serializable]
public class Dialog
{
    public string name; // Nome do personagem
    [TextArea(3, 10)] // Área de texto maior no Inspector
    public string text; // Texto do diálogo
}