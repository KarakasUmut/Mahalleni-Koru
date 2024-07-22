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
    int[] mermiSay�si =
    {
            10,
            20,
            5,
            30,
        };

    public List<Sprite> Silah_resimleri = new List<Sprite>();
    public UnityEngine.UI.Image Silah�n_resmi;

   public string OlusanSilah�nTuru;
   public int OlusanMermiSay�s�;
    
    void Start()
    {
        int gelenAnahtar = Random.Range(0, silahlar.Length );
        OlusanSilah�nTuru = silahlar[gelenAnahtar];
        OlusanMermiSay�s� = mermiSay�si[Random.Range(0,silahlar.Length)];
        Silah�n_resmi.sprite = Silah_resimleri[gelenAnahtar];

       // OlusanSilah�nTuru = "Taramali";
        //OlusanMermiSay�s� = 30;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
