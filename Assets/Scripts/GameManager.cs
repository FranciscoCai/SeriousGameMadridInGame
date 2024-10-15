using NUnit.Framework.Constraints;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string Escena;

    public List<GameObject> ToCheck = new List<GameObject>();
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        for (int i = 0; i < ToCheck.Count; i++)
        {
            MovebleObject toCheck = ToCheck[i].GetComponent<MovebleObject>();
            bool correcto = toCheck.PosicionCorrecta;
            if (correcto == true)
            {
                if(i == ToCheck.Count - 1)
                {
                    SceneManager.LoadScene(Escena);
                }
            }
            else
            {
                break;
            }
        }
    }
}
