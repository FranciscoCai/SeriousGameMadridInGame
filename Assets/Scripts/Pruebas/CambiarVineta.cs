using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class CambiarVineta : MonoBehaviour
{

    public Sprite[] panels;

    public Image panelImage;

    public string Nivel;

    public string Menu;

    int index;
 
    // Update is called once per frame
    void Update()
    {
        panelImage = GetComponent<Image>(); 
    }
    public void click_Start()
    {
        Debug.Log("Tus muertos pisaos y repisaos");
    }
    public void NextPanel()
    {
       

        if (index < panels.Length - 1)
        {
            index++;
            panelImage.sprite = panels[index];
        }
        else
        {
            SceneManager.LoadScene(Nivel);
        }
    }
    public void PreviousPanel()
    {
        if (index > 0)
        {
            index--;
            panelImage.sprite = panels[index];
        }
        else
        {
            SceneManager.LoadScene(Menu);
        }
    }
}
