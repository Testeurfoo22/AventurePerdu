using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    private int TimeDirection;
    private int TimeForce;
    WindZone wind;

    //ce programme permet de changer l,intensite et la direction du vent sur un temps donne, visible sur le long terme.
    // Start is called before the first frame update
    void Start()
    {
        wind = gameObject.GetComponent<WindZone>();
        wind.windMain = 0.1F;
        wind.windTurbulence = 0.1F;

        Init();
    }

    // Update is called once per frame
    void Init(){
        StartCoroutine(setDirection());
        StartCoroutine(setForce());
    }

    //Change la direction du vent
    IEnumerator setDirection(){
        while(true){
            yield return new WaitForSeconds(Random.Range(2, 5) * 60);
            transform.localEulerAngles = new Vector3(Random.Range(0, 181), Random.Range(0, 361), 0F);
            yield return null;
        }
    }

    //change la force du vent 
    IEnumerator setForce(){
        while(true){
            yield return new WaitForSeconds(Random.Range(2, 5) * 60);
            float Force = Random.Range(0.0F, 1.0F);
            wind.windMain = Force;
            wind.windTurbulence = Force;
            yield return null;
        }
    }
}
