using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        PlayerPrefs.SetInt("Pompali_Mermi", 50);
        if (!PlayerPrefs.HasKey("OyunBasladimi")) 
        {
            PlayerPrefs.SetInt("Taramali_Mermi", 100);
            
            // PlayerPrefs.SetInt("Magnum_Meri", 30);
            // PlayerPrefs.SetInt("Sniper_Meri", 20);

            PlayerPrefs.SetInt("OyunBasladimi", 1);
        }
      

    }














}

    
   

    
    
       
       

    
