using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string nombrePersonaje;  // Nombre del personaje que habla
    [TextArea(3, 10)]  // Hace que el �rea de texto en el inspector sea m�s grande
    public string[] oraciones;  // Array de oraciones para este di�logo
}
