using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject gate;


    AudioSource Ads;
    [SerializeField] AudioClip Bum;


    // Start is called before the first frame update
    void Start()
    {
        Ads = GetComponent<AudioSource>();
        gate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            gate.SetActive(true);
            Ads.PlayOneShot(Bum);
        }
    }
}
