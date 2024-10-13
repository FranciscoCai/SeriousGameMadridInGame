using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nombreTexto;  // Texto donde aparecer� el nombre del personaje
    public Text dialogoTexto;  // Texto donde aparecer� la oraci�n
    public Animator animador;  // Para mostrar y ocultar el panel de di�logo (si es necesario)

    private Queue<string> oraciones;  // Cola para almacenar las oraciones del di�logo

    void Start()
    {
        oraciones = new Queue<string>();
    }

    // M�todo para iniciar el di�logo
    public void IniciarDialogo(Dialogues dialogo)
    {
        animador.SetBool("estaAbierto", true);  // Abre el panel de di�logo si tienes un sistema de animaci�n

        nombreTexto.text = dialogo.nombrePersonaje;

        oraciones.Clear();

        foreach (string oracion in dialogo.oraciones)
        {
            oraciones.Enqueue(oracion);  // A�ade cada oraci�n a la cola
        }

        MostrarSiguienteOracion();
    }

    // M�todo para mostrar la siguiente oraci�n
    public void MostrarSiguienteOracion()
    {
        if (oraciones.Count == 0)
        {
            TerminarDialogo();
            return;
        }

        string oracion = oraciones.Dequeue();
        StopAllCoroutines();  // Detiene cualquier ejecuci�n anterior de texto
        StartCoroutine(EscribirOracion(oracion));  // Muestra el texto letra por letra
    }

    // M�todo para escribir la oraci�n letra por letra
    IEnumerator EscribirOracion(string oracion)
    {
        dialogoTexto.text = "";
        foreach (char letra in oracion.ToCharArray())
        {
            dialogoTexto.text += letra;
            yield return null;  // Espera un frame entre cada letra (puedes ajustar la velocidad)
        }
    }

    // M�todo para terminar el di�logo
    void TerminarDialogo()
    {
        animador.SetBool("estaAbierto", false);  // Cierra el panel de di�logo
    }
}
