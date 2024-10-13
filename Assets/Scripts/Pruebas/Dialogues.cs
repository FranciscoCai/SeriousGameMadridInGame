using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string nombrePersonaje;  // Nombre del personaje que habla
    [TextArea(3, 10)]  // Hace que el área de texto en el inspector sea más grande
    public string[] oraciones;  // Array de oraciones para este diálogo
}
