using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Pompali : MonoBehaviour
{
    public bool atesEdebilirmi;
    float �ceridenatesEtmeSikligi;
    public float D�sar�danAtesEtmeSiklik;
    public float menzil;
    public Camera cam;

    public AudioSource atesSesi;
    public AudioSource MermiBitti;
    public AudioSource Reload;
    public AudioSource MermiAlma;


    public ParticleSystem atesEfekt;
    public ParticleSystem Mermi�zi;
    public ParticleSystem kanizi;

    Animator animator;
    public string Silah�n_Adi;
    public int ToplamMermiSay�s�;
    public int SarjorKapasitesi;
    int KalanMermi;

    public TextMeshProUGUI toplamMermi_text;
    public TextMeshProUGUI kalanMermi_text;
    
    




    int at�lm�sOlanMermi;

    void Start()
    {
        
        
        KalanMermi = PlayerPrefs.GetInt(Silah�n_Adi + "_Mermi");
        
        SarjorDoldurmaTeknikFonksiyonu("NormalYaz");
        animator = GetComponent<Animator>();
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > �ceridenatesEtmeSikligi && ToplamMermiSay�s� != 0)
            {
                AtesEt();
                �ceridenatesEtmeSikligi = Time.time + D�sar�danAtesEtmeSiklik;
            }
            if (ToplamMermiSay�s� == 0)
            {
                MermiBitti.Play();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ToplamMermiSay�s� < SarjorKapasitesi && KalanMermi != 0)
            {
                if (ToplamMermiSay�s� != 0)
                {

                    SarjorDoldurmaTeknikFonksiyonu("MermiVar");

                }
                else
                {
                    SarjorDoldurmaTeknikFonksiyonu("MermiYok");
                }
                animator.Play("Reload2");
            }


        }
    }



    void AtesEt()
    {
        AtesEtmeTeknikSistemleri();
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, menzil))
        {
            if (hit.transform.gameObject.CompareTag("Dusman"))
            {
                Instantiate(kanizi, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else if (hit.transform.gameObject.CompareTag("devril"))
            {
                Rigidbody rg = hit.transform.gameObject.GetComponent<Rigidbody>();
                //rg.AddForce(transform.forward * 50f);
                rg.AddForce(-hit.normal * 200f);
            }
            else
            {
                Instantiate(Mermi�zi, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }


    }

    

    void SarjorDoldurmaTeknikFonksiyonu(string tur)
    {
        switch (tur)
        {
            case "MermiVar":

                if (KalanMermi <= SarjorKapasitesi)
                {
                    int OlusanToplamDeger = ToplamMermiSay�s� + KalanMermi;

                    if (OlusanToplamDeger > SarjorKapasitesi)
                    {
                        ToplamMermiSay�s� = SarjorKapasitesi;
                        KalanMermi = OlusanToplamDeger - SarjorKapasitesi;
                        PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", KalanMermi);
                    }
                    else
                    {
                        ToplamMermiSay�s� += KalanMermi;
                        KalanMermi = 0;
                        PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", 0);

                    }
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi - ToplamMermiSay�s�;
                    ToplamMermiSay�s� = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", KalanMermi);
                }

                toplamMermi_text.text = ToplamMermiSay�s�.ToString();
                kalanMermi_text.text = KalanMermi.ToString();

                break;

            case "MermiYok":

                if (KalanMermi <= SarjorKapasitesi)
                {
                    ToplamMermiSay�s� = KalanMermi;
                    KalanMermi = 0;
                    PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", 0);
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi;
                    ToplamMermiSay�s� = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", KalanMermi);
                }


                toplamMermi_text.text = ToplamMermiSay�s�.ToString();
                kalanMermi_text.text = KalanMermi.ToString();
                break;

            case "NormalYaz":
                toplamMermi_text.text = ToplamMermiSay�s�.ToString();
                kalanMermi_text.text = KalanMermi.ToString();

                break;






        }
    }

    void Doldur()
    {
        Reload.Play();

    }

    void AtesEtmeTeknikSistemleri()
    {
     
        ToplamMermiSay�s�--;
        toplamMermi_text.text = ToplamMermiSay�s�.ToString();

        atesSesi.Play();
        atesEfekt.Play();
        animator.Play("AtesEt");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
           MermiAlma.Play();
           mermikaydet(other.transform.gameObject.GetComponent<MermiKutusu>().OlusanSilah�nTuru,other.transform.gameObject.GetComponent<MermiKutusu>().OlusanMermiSay�s�);
            MermiKutusuOlustur.mermiKutusu = false;
            Destroy(other.transform.parent.gameObject);
        }

    }
    void mermikaydet(string silaht�r�, int mermisay�s�)
    {
        switch (silaht�r�)
        {
            case "Taramali":
                

                break;

            case "Pompali":
                PlayerPrefs.SetInt("Pompali_Mermi", PlayerPrefs.GetInt("Pompali_Mermi") + mermisay�s�);
                KalanMermi += mermisay�s�;
                PlayerPrefs.SetInt(Silah�n_Adi + "_Mermi", KalanMermi);
                SarjorDoldurmaTeknikFonksiyonu("NormalYaz");

                break;

            case "Magnum":
                PlayerPrefs.SetInt("Magnum_Meri", PlayerPrefs.GetInt("Magnum_Meri") + mermisay�s�);

                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Meri", PlayerPrefs.GetInt("Sniper_Meri") + mermisay�s�);

                break;
        }
    }
}

