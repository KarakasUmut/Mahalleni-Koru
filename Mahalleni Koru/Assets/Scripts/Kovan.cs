using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kovan : MonoBehaviour
{
    AudioSource KovanSesi;

    void Start()
    {
        KovanSesi = GetComponent<AudioSource>();
        Destroy(gameObject,1f);
    }


    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zemin"))
        {
            KovanSesi.Play();
           
        }
    }
}
