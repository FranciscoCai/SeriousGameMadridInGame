using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip[] audioClips;

    public GameObject radio;

    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (radio == GetClickedObject(out RaycastHit hit) && currentClipIndex < audioClips.Length - 1)
            {
                currentClipIndex++;
                Debug.Log("Puta");
            }
            else
            {
                currentClipIndex = 0; // Vuelve al primer clip si es el último.
            }
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();

        }
        
        
        
        if (!audioSource.isPlaying)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }
    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject()) { target = hit.collider.gameObject; }
        }
        return target;
    }

    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}
