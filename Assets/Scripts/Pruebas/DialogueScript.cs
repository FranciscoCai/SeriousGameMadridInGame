using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;

    public Image panel;

    public string[] linesOne;
    public string[] linesTwo;

    public float textSpeed = 0.05f;

    public bool condicionOne = false; //Si quieres poner más condiciones solo tienes que añadir mas y repetir todo el puto proceso q hay aqui

    private CanvasGroup canvasGroup;

    private bool alreadyCleared = false;

    int index;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        StartDialogueOne();
         canvasGroup = dialoguePanel.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        if(condicionOne == true && !alreadyCleared)
        {
            dialogueText.text = string.Empty;
            StartDialogueTwo();
            canvasGroup.alpha = 1;

            alreadyCleared = true;

            panel.gameObject.SetActive(true);
        }
        if(Input.GetMouseButtonDown(0) && condicionOne == false)
        {
            if (dialogueText.text == linesOne[index])
            {
                NextLineOne();

            }
            else
            {
                StopAllCoroutines();
               // StopCoroutine(WriteLineOne());
                dialogueText.text = linesOne[index];
            }

        }
        if (Input.GetMouseButtonDown(0) && condicionOne == true)
        {
            if (dialogueText.text == linesTwo[index])
            {
                NextLineTwo();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = linesTwo[index];
                //StopCoroutine(WriteLineTwo());
            }
        }

    }

    public void StartDialogueOne()
    {
        index = 0;
        StartCoroutine(WriteLineOne());
    }
    public void StartDialogueTwo()
    {
        index = 0;
        StartCoroutine(WriteLineTwo());
    }

    IEnumerator WriteLineOne()
    {
        foreach (char letter in linesOne[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

    }
    IEnumerator WriteLineTwo()
    {
        foreach (char letter in linesTwo[index].ToCharArray())
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }

    }

    public void NextLineOne()
    {
        if(index < linesOne.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLineOne());
        }
        else
        {
            canvasGroup.alpha = 0;
            panel.gameObject.SetActive(false);
        }
    }
    public void NextLineTwo()
    {
        if (index < linesTwo.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLineTwo());
        }
        else
        {
            canvasGroup.alpha = 0;
            panel.gameObject.SetActive(false);
        }
    }
}
