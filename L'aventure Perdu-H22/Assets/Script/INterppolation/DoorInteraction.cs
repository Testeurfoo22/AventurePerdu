using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public float ValueInit; //angle de depart
    public float ValueFin;  //angle final
    public float temps; //temps interaction
    public GameObject TargetObject; //object de la porte
    Vector3 Modif = new Vector3(0F, 0F, 0F); // vecteur ephemere
    float t = 0;
    bool isRotate = false;
    public Item itemUnlock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //check quand le player entre dans la zone du collider
    private void OnTriggerEnter(Collider collision) {
        if ((itemUnlock.ToString() == "NULL") || inventaire.Present(itemUnlock) != -1){
            if ((collision.gameObject.tag == "MainCamera")){
                StartCoroutine(wait(ValueInit, ValueFin, temps));
            } 
        }  
    }
    //check quand le player sort dans la zone du collider
    private void OnTriggerExit(Collider collision) {
        if ((itemUnlock.ToString() == "NULL") || inventaire.Present(itemUnlock) != -1){
            if (collision.gameObject.tag == "MainCamera"){
                StartCoroutine(wait(ValueFin, ValueInit, temps));
            }   
        }  
    }
    //attends que la rotation s'est termine
    IEnumerator wait(float Depart, float Final, float Temps){

        yield return new WaitUntil(() => isRotate == false);
        StartCoroutine(Rotate(Depart, Final, Temps));
    }
    //fait tourner la porte, et lance un son de porte
    IEnumerator Rotate(float Depart, float Final, float Temps){
        isRotate = true;
        t = 0;
        Modif = TargetObject.transform.localEulerAngles;
        gameObject.GetComponent<AudioSource>().Play();
        while (t <= temps){
            float ValInter = Mathf.Lerp(Depart, Final, t);
            Modif.y = ValInter;
            TargetObject.transform.localEulerAngles = Modif;
            t += Time.deltaTime;
            yield return null;
        }
        Modif.y = Final;
        TargetObject.transform.localEulerAngles = Modif;
        isRotate = false;
    }
}
