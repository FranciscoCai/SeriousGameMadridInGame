using System;
using UnityEngine;

public class MovebleObject : MonoBehaviour
{
    [SerializeField] public Vector3 VectorRotate;
    public Vector3 upp;
    public Vector3 vectorActual;
    public float[] RangeFinal;
    public bool PosicionCorrecta = false;
    public GameObject Target;
    void Start()
    {
        if(GameManager.instance != null)
        {
            GameManager.instance.ToCheck.Add(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        vectorActual = gameObject.transform.rotation.eulerAngles;
        if(vectorActual.z >= RangeFinal[0] && vectorActual.z <= RangeFinal[1])
        {
            PosicionCorrecta =true; 
        }
        else
        {
            PosicionCorrecta =false;
        }
    }
}
