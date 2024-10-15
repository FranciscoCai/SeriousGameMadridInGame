using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Titulo : MonoBehaviour
{
    public string Escena;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
        SceneManager.LoadScene(Escena);
        }
    }
}
