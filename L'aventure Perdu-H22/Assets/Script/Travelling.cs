using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travelling : MonoBehaviour
{
    public GameObject[] bornList = new GameObject[6];
    //vitesse de la borne indice vers incide +1
    public float[] vitesseDeplacement = new float[5];
    //rotation de la borne indice vers incide +1
    public float[] vitesseRota = new float[5];
    public GameObject mainCamera;
    public GameObject accueil;
    public float distanceValue;

    //indice de bornes
    int indice;
    GameObject borneSuivante;
    float Vitesse;
    float Rota; 

    //variables initiales, pour reposer notre camera a la position au depart
    Vector3 initPos;
    Vector3 rotaPos;

    //le programme permet d,effectuer un traveling, avec des bornes choisis, avec des vitesses et rotations applique
    // prend la position initiale pour la garder en memoire
    void Awake(){
        initPos = transform.localPosition;
        rotaPos = transform.localEulerAngles;
    }
    //positione a la borne 0
    void OnEnable() {
        indice = 0;

        borneSuivante = bornList[indice];

        transform.localPosition = borneSuivante.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //tant que la derniere borne n'est pas atteinte, fait tourner le programme, sinon l'arrete
        if (indice >= bornList.Length){
                StartCoroutine("CloseTravel");
        }
        else{
            try{
                //augmente la borne suivie
                if (distanceValue > Vector3.Distance(borneSuivante.transform.localPosition, transform.localPosition)){
                    indice += 1;
                    borneSuivante = bornList[indice];
                    Vitesse = vitesseDeplacement[indice -1];
                    Rota = vitesseRota[indice -1];
                }
                deplcementVoid();
                rotationVoid();
            }
            catch{
                
            }
        }

    }

    //stop le travel pour retourner a l'accueil
    IEnumerator CloseTravel(){
        yield return new WaitForSeconds(3);
        transform.localPosition = initPos;
        transform.localEulerAngles = rotaPos;
        mainCamera.SetActive(false);
        accueil.SetActive(true);
    }

    //deplacement sur z
    void deplcementVoid(){
        transform.Translate(0, 0, Vitesse * Time.deltaTime);
    }

    //rotate ver la position voulu
    void rotationVoid(){
        Vector3 direction = borneSuivante.transform.localPosition - transform.localPosition;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, Rota * Time.deltaTime, 0F);

        transform.localRotation = Quaternion.LookRotation(newDirection);
    }
}
