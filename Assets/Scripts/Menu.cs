using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public GameObject start;
    public GameObject information;
    public GameObject credits;
    [SerializeField] private string SceneToLoad;
    [SerializeField] private GameObject parkinsonInformation;
    [SerializeField] private GameObject creditsInformation;
    [SerializeField] private GameObject[] parkinsonInformationText;
    public int textNumber = 0;

    private void Start()
    {

    }
    public  void click_Start()
    {
        SceneManager.LoadScene(SceneToLoad);
        ActiveFalse();
    }
    public void click_Information()
    {
        parkinsonInformation.SetActive(true);
        ActiveFalse();
    }
    public void click_Credits()
    {
        creditsInformation.SetActive(true);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position, 1);
        ActiveFalse();
    }
    public void return_Menu()
    {
        start.SetActive(true);
        information.SetActive(true);
        credits.SetActive(true);
        parkinsonInformation.SetActive(false);
        creditsInformation.SetActive(false);
    }
    public void PasarPagia(int x)
    {
        textNumber = textNumber + x;
        if (textNumber <0)
        {
            textNumber = 0;
        }
        else if(textNumber >= parkinsonInformationText.Length-1) 
        {
            textNumber = parkinsonInformationText.Length-1;
        }
        if(textNumber != 0)
        {
            parkinsonInformationText[textNumber - 1].SetActive(false);
        }
        parkinsonInformationText[textNumber].SetActive(true);
        if (textNumber != 2)
        {
            parkinsonInformationText[textNumber + 1].SetActive(false);
        }
    }
    private void ActiveFalse()
    {
        start.SetActive(false);
        information.SetActive(false);
        credits.SetActive(false);
    }
}
