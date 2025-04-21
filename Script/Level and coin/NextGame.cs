using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextGame : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip nextlevel;
    private AudioSource Ads;


    private void Start()
    {
        Ads = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ads.PlayOneShot(nextlevel);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        int manhientai = SceneManager.GetActiveScene().buildIndex;
        int quaman = manhientai + 1;
        if (quaman == SceneManager.sceneCountInBuildSettings)
        {
            quaman = 0;
        }
        SceneManager.LoadScene(quaman);
        yield return null; // Đợi 1 frame để đảm bảo Gamesession đã sẵn sàng
        FindObjectOfType<Gamesession>().UpdateUI(); ;
    }
}
