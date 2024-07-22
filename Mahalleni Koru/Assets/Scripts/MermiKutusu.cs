using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MermiKutusu : MonoBehaviour
{
    string[] silahlar =
        {
            "Magnum",
            "Pompali",
            "Sniper",
            "Taramali",
        };
    int[] mermiSayýsi =
    {
            10,
            20,
            5,
            30,
        };

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public UnityEngine.UI.Image Silahýn_resmi;

   public string OlusanSilahýnTuru;
   public int OlusanMermiSayýsý;
    
    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length );
        OlusanSilahýnTuru = silahlar[gelenAnahtar];
        OlusanMermiSayýsý = mermiSayýsi[Random.Range(0,silahlar.Length)];
        Silahýn_resmi.sprite = Silah_resimleri[gelenAnahtar];

       // OlusanSilahýnTuru = "Taramali";
        //OlusanMermiSayýsý = 30;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
