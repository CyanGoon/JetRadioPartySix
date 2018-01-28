using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fungalTowerBehaviour : MonoBehaviour
{

    public GameObject player;
    private Rigidbody rb;
    public AudioClip[] resourceAudioClips;
    AudioSource audioSource;
    public AudioClip whisper;
    int delay = 0;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        delay = 0;
    }

    private void Awake()
    {
        resourceAudioClips = Resources.LoadAll<AudioClip>("audio");
    }

    // Update is called once per frame
    void Update()
    {
        if (delay > 0)
        {
            delay -= 1;
        }
        float distance = Vector3.Distance(player.transform.position, rb.transform.position);
        {
            if (delay <= 0)
            {
                if (distance <= 6)
                {
                    int random = Random.Range(0, resourceAudioClips.Length);
                    whisper = resourceAudioClips[random];
                    audioSource.PlayOneShot(whisper, 1.8F/(distance*2));
                    delay = 180;
                }
            }
            
        }
      
    }

    public void setTarget(GameObject target)
    {

        player = target;

    }
}