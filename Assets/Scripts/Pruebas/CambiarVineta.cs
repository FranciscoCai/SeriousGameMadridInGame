using UnityEngine;
using UnityEngine.UI;

public class CambiarVineta : MonoBehaviour
{
   

    public Sprite[] panels;

    public Image panelImage;

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
            gameObject.SetActive(false);
        }
    }
}
