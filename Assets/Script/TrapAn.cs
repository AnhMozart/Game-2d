using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrap2 : MonoBehaviour
{
    [SerializeField] GameObject VatPhamAn;
    [SerializeField] string triggeringTag = "Player";
    void Start()
    {
        // ẩn vật phẩm ngay khi bắt đầu game
        VatPhamAn.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(triggeringTag))
        {
            VatPhamAn.SetActive(true);
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggeringTag))
        {
            VatPhamAn.SetActive(false);
        }
    }
}
