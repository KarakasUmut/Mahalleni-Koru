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
    float İceridenatesEtmeSikligi;
    public float DısarıdanAtesEtmeSiklik;
    public float menzil;
    public Camera cam;

    public AudioSource atesSesi;
    public AudioSource MermiBitti;
    public AudioSource Reload;
    public AudioSource MermiAlma;


    public ParticleSystem atesEfekt;
    public ParticleSystem Mermiİzi;
    public ParticleSystem kanizi;

    Animator animator;
    public string Silahın_Adi;
    public int ToplamMermiSayısı;
    public int SarjorKapasitesi;
    int KalanMermi;

    public TextMeshProUGUI toplamMermi_text;
    public TextMeshProUGUI kalanMermi_text;
    
    




    int atılmısOlanMermi;

    void Start()
    {
        
        
        KalanMermi = PlayerPrefs.GetInt(Silahın_Adi + "_Mermi");
        
        SarjorDoldurmaTeknikFonksiyonu("NormalYaz");
        animator = GetComponent<Animator>();
        
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (atesEdebilirmi && Time.time > İceridenatesEtmeSikligi && ToplamMermiSayısı != 0)
            {
                AtesEt();
                İceridenatesEtmeSikligi = Time.time + DısarıdanAtesEtmeSiklik;
            }
            if (ToplamMermiSayısı == 0)
            {
                MermiBitti.Play();
            }

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ToplamMermiSayısı < SarjorKapasitesi && KalanMermi != 0)
            {
                if (ToplamMermiSayısı != 0)
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
                Instantiate(Mermiİzi, hit.point, Quaternion.LookRotation(hit.normal));
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
                    int OlusanToplamDeger = ToplamMermiSayısı + KalanMermi;

                    if (OlusanToplamDeger > SarjorKapasitesi)
                    {
                        ToplamMermiSayısı = SarjorKapasitesi;
                        KalanMermi = OlusanToplamDeger - SarjorKapasitesi;
                        PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", KalanMermi);
                    }
                    else
                    {
                        ToplamMermiSayısı += KalanMermi;
                        KalanMermi = 0;
                        PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", 0);

                    }
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi - ToplamMermiSayısı;
                    ToplamMermiSayısı = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", KalanMermi);
                }

                toplamMermi_text.text = ToplamMermiSayısı.ToString();
                kalanMermi_text.text = KalanMermi.ToString();

                break;

            case "MermiYok":

                if (KalanMermi <= SarjorKapasitesi)
                {
                    ToplamMermiSayısı = KalanMermi;
                    KalanMermi = 0;
                    PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", 0);
                }
                else
                {
                    KalanMermi -= SarjorKapasitesi;
                    ToplamMermiSayısı = SarjorKapasitesi;
                    PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", KalanMermi);
                }


                toplamMermi_text.text = ToplamMermiSayısı.ToString();
                kalanMermi_text.text = KalanMermi.ToString();
                break;

            case "NormalYaz":
                toplamMermi_text.text = ToplamMermiSayısı.ToString();
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
     
        ToplamMermiSayısı--;
        toplamMermi_text.text = ToplamMermiSayısı.ToString();

        atesSesi.Play();
        atesEfekt.Play();
        animator.Play("AtesEt");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mermi"))
        {
           MermiAlma.Play();
           mermikaydet(other.transform.gameObject.GetComponent<MermiKutusu>().OlusanSilahınTuru,other.transform.gameObject.GetComponent<MermiKutusu>().OlusanMermiSayısı);
            MermiKutusuOlustur.mermiKutusu = false;
            Destroy(other.transform.parent.gameObject);
        }

    }
    void mermikaydet(string silahtürü, int mermisayısı)
    {
        switch (silahtürü)
        {
            case "Taramali":
                

                break;

            case "Pompali":
                PlayerPrefs.SetInt("Pompali_Mermi", PlayerPrefs.GetInt("Pompali_Mermi") + mermisayısı);
                KalanMermi += mermisayısı;
                PlayerPrefs.SetInt(Silahın_Adi + "_Mermi", KalanMermi);
                SarjorDoldurmaTeknikFonksiyonu("NormalYaz");

                break;

            case "Magnum":
                PlayerPrefs.SetInt("Magnum_Meri", PlayerPrefs.GetInt("Magnum_Meri") + mermisayısı);

                break;

            case "Sniper":
                PlayerPrefs.SetInt("Sniper_Meri", PlayerPrefs.GetInt("Sniper_Meri") + mermisayısı);

                break;
        }
    }
}

