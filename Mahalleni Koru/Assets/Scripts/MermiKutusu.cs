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
    int[] mermiSayısi =
    {
            10,
            20,
            5,
            30,
        };

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public UnityEngine.UI.Image Silahın_resmi;

   public string OlusanSilahınTuru;
   public int OlusanMermiSayısı;
    
    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length );
        OlusanSilahınTuru = silahlar[gelenAnahtar];
        OlusanMermiSayısı = mermiSayısi[Random.Range(0,silahlar.Length)];
        Silahın_resmi.sprite = Silah_resimleri[gelenAnahtar];

       // OlusanSilahınTuru = "Taramali";
        //OlusanMermiSayısı = 30;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
