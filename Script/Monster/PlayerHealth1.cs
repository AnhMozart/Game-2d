using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float health;
    public float currentHealth;
    private Animator animpl;
    public GameObject player;
    public Slider healthSlider;  // Tham chiếu đến thanh trượt UI

    AudioSource Ads;
    [SerializeField] AudioClip Attack;
    [SerializeField] AudioClip death;

    void Start()
    {
        animpl = GetComponent<Animator>();
        currentHealth = health;
        Ads = GetComponent<AudioSource>();

        if (healthSlider != null)
        {
            healthSlider.maxValue = health;  // Đặt giá trị tối đa cho thanh trượt
            healthSlider.value = currentHealth;  // Đặt giá trị ban đầu cho thanh trượt
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (health < currentHealth)
        {
            Ads.PlayOneShot(Attack);
            currentHealth = health;
            animpl.SetTrigger("Attacked");
        }

        if (health <= 0)
        {
            Ads.PlayOneShot(death);
            animpl.SetBool("isDead", true);
            Debug.Log("player Dead");
            Invoke(nameof(DestroyGameObject),1f);
        }

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;  // Cập nhật thanh máu UI
        }


    }

    private void DestroyGameObject()
    {
        StartCoroutine(DeathDelay(1f));
        Gamesession.islive = false;
        GetComponent<Gamesession>().addScore(100);
    }

    IEnumerator DeathDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }
}
