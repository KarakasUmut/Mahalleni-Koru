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
    float ÝceridenatesEtmeSikligi;
    public float DýsarýdanAtesEtmeSiklik;
    public float menzil;
    public Camera cam;

    public AudioSource atesSesi;
    public AudioSource MermiBitti;
    public AudioSource Reload;
    public AudioSource MermiAlma;


    public ParticleSystem atesEfekt;
    public ParticleSystem MermiÝzi;
    public ParticleSystem kanizi;

    Animator animator;
    public string Silahýn_Adi;
    public int ToplamMermiSayýsý;
    public int SarjorKapasitesi;
    int KalanMermi;

    public TextMeshProUGUI toplamMermi_text;
    public TextMeshProUGUI kalanMermi_text;
    
    




    int atýlmýsOlanMermi;

    void Start()
    {
        
        
        KalanMermi = PlayerPrefs.GetInt(Silahýn_Adi + "_Mermi");
        
        SarjorDoldurmaTeknikFonksiyonu("NormalYaz");
        animator = GetComponent<Animator>();
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > ÝceridenatesEtmeSikligi && ToplamMermiSayýsý != 0)
            {
                AtesEt();
                ÝceridenatesEtmeSikligi = Time.time + DýsarýdanAtesEtmeSiklik;
            }
            if (ToplamMermiSayýsý == 0)
            {
                MermiBitti.Play();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ToplamMermiSayýsý < SarjorKapasitesi && KalanMermi != 0)
            {
                if (ToplamMermiSayýsý != 0)
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
                Instantiate(MermiÝzi, hit.point, Quaternion.LookRotation(hit.normal));
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
                    int OlusanToplamDeger = ToplamMermiSayýsý + KalanMermi;

                    if (OlusanToplamDeger > SarjorKapasitesi)
                    {
                        ToplamMermiSayýsý = SarjorKapasitesi;
                        KalanMermi = OlusanToplamDeger - SarjorKapasitesi;
                        PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", KalanMermi);
                    }
                    else
                    {
                        ToplamMermiSayýsý += KalanMermi;
                        KalanMermi = 0;
                        PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", 0);

                    }
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi - ToplamMermiSayýsý;
                    ToplamMermiSayýsý = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", KalanMermi);
                }

                toplamMermi_text.text = ToplamMermiSayýsý.ToString();
                kalanMermi_text.text = KalanMermi.ToString();

                break;

            case "MermiYok":

                if (KalanMermi <= SarjorKapasitesi)
                {
                    ToplamMermiSayýsý = KalanMermi;
                    KalanMermi = 0;
                    PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", 0);
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi;
                    ToplamMermiSayýsý = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", KalanMermi);
                }


                toplamMermi_text.text = ToplamMermiSayýsý.ToString();
                kalanMermi_text.text = KalanMermi.ToString();
                break;

            case "NormalYaz":
                toplamMermi_text.text = ToplamMermiSayýsý.ToString();
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
     
        ToplamMermiSayýsý--;
        toplamMermi_text.text = ToplamMermiSayýsý.ToString();

        atesSesi.Play();
        atesEfekt.Play();
        animator.Play("AtesEt");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
           MermiAlma.Play();
           mermikaydet(other.transform.gameObject.GetComponent<MermiKutusu>().OlusanSilahýnTuru,other.transform.gameObject.GetComponent<MermiKutusu>().OlusanMermiSayýsý);
            MermiKutusuOlustur.mermiKutusu = false;
            Destroy(other.transform.parent.gameObject);
        }

    }
    void mermikaydet(string silahtürü, int mermisayýsý)
    {
        switch (silahtürü)
        {
            case "Taramali":
                

                break;

            case "Pompali":
                PlayerPrefs.SetInt("Pompali_Mermi", PlayerPrefs.GetInt("Pompali_Mermi") + mermisayýsý);
                KalanMermi += mermisayýsý;
                PlayerPrefs.SetInt(Silahýn_Adi + "_Mermi", KalanMermi);
                SarjorDoldurmaTeknikFonksiyonu("NormalYaz");

                break;

            case "Magnum":
                PlayerPrefs.SetInt("Magnum_Meri", PlayerPrefs.GetInt("Magnum_Meri") + mermisayýsý);

                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Meri", PlayerPrefs.GetInt("Sniper_Meri") + mermisayýsý);

                break;
        }
    }
}

