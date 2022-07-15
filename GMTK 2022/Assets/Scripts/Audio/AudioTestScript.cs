using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTestScript : MonoBehaviour
{
    AudioScript test;
    [SerializeField] AudioClip intro;
    [SerializeField] AudioClip loop;
    [SerializeField] AudioSource source;
    [SerializeField] bool shouldLoop;
    // Start is called before the first frame update
    void Start()
    {
        test = gameObject.AddComponent<AudioScript>();
        test.PlayTrackAfterIntro(source, intro, loop, 0.2f, shouldLoop);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
