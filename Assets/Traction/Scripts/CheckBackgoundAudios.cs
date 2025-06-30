using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBackgoundAudios : MonoBehaviour
{
    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // For some weird reason when running the scene with multiple players the awake on start does not work
        foreach(var s in audioSources)
        {
            if (!s.isPlaying && s.isActiveAndEnabled)
            {
                //Debug.Log("Audio " + s.name + " was not playing");
                s.Play();
            }
        }
    }
}
