using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class CambiarVineta : MonoBehaviour
{
   

    public Sprite[] panels;

    public Image panelImage;

    public string PruebaJesus;

    int index;

    // Update is called once per frame
    void Update()
    {
        panelImage = GetComponent<Image>();

        

        if (Input.GetMouseButtonDown(0))
        {
            NextPanel();
        }
          
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
            SceneManager.LoadScene(PruebaJesus);
        }
    }
}
