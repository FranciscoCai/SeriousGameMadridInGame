using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nombreTexto;  // Texto donde aparecerá el nombre del personaje
    public Text dialogoTexto;  // Texto donde aparecerá la oración
    public Animator animador;  // Para mostrar y ocultar el panel de diálogo (si es necesario)

    private Queue<string> oraciones;  // Cola para almacenar las oraciones del diálogo

    void Start()
    {
        oraciones = new Queue<string>();
    }

    // Método para iniciar el diálogo
    public void IniciarDialogo(Dialogues dialogo)
    {
        animador.SetBool("estaAbierto", true);  // Abre el panel de diálogo si tienes un sistema de animación

        nombreTexto.text = dialogo.nombrePersonaje;

        oraciones.Clear();

        foreach (string oracion in dialogo.oraciones)
        {
            oraciones.Enqueue(oracion);  // Añade cada oración a la cola
        }

        MostrarSiguienteOracion();
    }

    // Método para mostrar la siguiente oración
    public void MostrarSiguienteOracion()
    {
        if (oraciones.Count == 0)
        {
            TerminarDialogo();
            return;
        }

        string oracion = oraciones.Dequeue();
        StopAllCoroutines();  // Detiene cualquier ejecución anterior de texto
        StartCoroutine(EscribirOracion(oracion));  // Muestra el texto letra por letra
    }

    // Método para escribir la oración letra por letra
    IEnumerator EscribirOracion(string oracion)
    {
        dialogoTexto.text = "";
        foreach (char letra in oracion.ToCharArray())
        {
            dialogoTexto.text += letra;
            yield return null;  // Espera un frame entre cada letra (puedes ajustar la velocidad)
        }
    }

    // Método para terminar el diálogo
    void TerminarDialogo()
    {
        animador.SetBool("estaAbierto", false);  // Cierra el panel de diálogo
    }
}
