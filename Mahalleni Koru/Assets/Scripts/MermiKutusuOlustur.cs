using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MermiKutusuOlustur : MonoBehaviour
{
    public List<GameObject> MermiKutusuPoint = new List<GameObject>();
    public GameObject Mermi_Kutusu;
    public static bool mermiKutusu;
    public float Kutu_C�kma_S�resi;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mermi_Kutusu_Yap());
        mermiKutusu = false;
    }

    IEnumerator Mermi_Kutusu_Yap()
    {
        while (true)
        { 
            yield return null;

            
            if (!mermiKutusu)
            {
                yield return new WaitForSeconds(5f);
                int randomSay� = Random.Range(0, 4);
                Instantiate(Mermi_Kutusu, MermiKutusuPoint[randomSay�].transform.position, MermiKutusuPoint[randomSay�].transform.rotation);
                mermiKutusu = true;
            }
        }
       
       
        

        /* while (true)
         {
             yield return new WaitForSeconds(5f);
             int randomSay� = Random.Range(0,3);
             Instantiate(Mermi_Kutusu, MermiKutusuPoint[0].transform.position, MermiKutusuPoint[0].transform.rotation);
             mermiKutusu = true;


         }*/

    }
    
}
