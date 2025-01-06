using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPInteraction : MonoBehaviour
{
    public float distance; //distance pour utiliser la Tp
    public float temps; //temps d,interpolation
    public GameObject TargetObject; //sphere avec les 3 spheres
    Vector3 Modif = new Vector3(0F, 0F, 0F); //vector3 ephemere
    float t = 0;    //t ephemere
    bool isRotate = false;  //bool pour savoir si rotation il y a
    //bool isActive = false;

    public Vector3[] listpositionTp = new Vector3[3];   //liste des positions specifiques des de la grande spheres
    public GameObject[] listTpGame = new GameObject[3];     //liste des petites spheres
    public GameObject[] listTpWorld = new GameObject[3];    //liste des Tp correspondantes
    public GameObject CameraPlayer;
    int currentState = 0;   //etat actuel
    Color32 initColor = new Color32(100, 100, 100, 255); //couleur initiales des petites spheres
    Color32 FinalColor = new Color32(0, 0, 0, 255); //couleur finales des petites spheres
    Color32 initColorCam = new Color32(0, 0, 0, 0); //couleur initiales de la cam
    Color32 FinalColorCam = new Color32(0, 0, 0, 255); //couleur finales de la cam
    // Start is called before the first frame update
    void awake()
    {

    }
    //si on est assez proche, on peut faire tourner la stele de Tp et lock notre deeplacement.
    // Update is called once per frame
    void Update()
    {
        if (distance >= Vector3.Distance(CameraPlayer.transform.position, transform.position)){
            if( Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                try{
                    if( Physics.Raycast( ray, out hit, 100) )
                    {
                        if (hit.transform.gameObject.name == listTpGame[currentState].name){
                            StartCoroutine(TP());
                        }
                    }
                }
                catch{

                }
            }

            if (Input.GetKeyDown(KeyCode.A)){
                StartCoroutine(wait(listpositionTp, currentState, -1));
            }
            if (Input.GetKeyDown(KeyCode.D)){
                StartCoroutine(wait(listpositionTp, currentState, +1));
            }
        }
    }
    //change les couleurs adequates et change la position de notre personnage
    IEnumerator TP(){
        yield return (colorCam(initColorCam, FinalColorCam));
        yield return ColorChange(initColor, FinalColor);
        listTpGame[currentState ].GetComponent<AudioSource>().Play();
        if (listTpGame[currentState].name != gameObject.name){
            CameraPlayer.transform.position = 
                new Vector3(listTpWorld[currentState].transform.position.x - listTpWorld[currentState].transform.forward.x * 1.5F, 
                    CameraPlayer.transform.position.y, 
                        listTpWorld[currentState].transform.position.z + listTpWorld[currentState].transform.forward.x * 1.5F);
            CameraPlayer.transform.localEulerAngles = listTpWorld[currentState].transform.localEulerAngles + new Vector3(0F, 90F, 0F);
        }
        yield return (colorCam(FinalColorCam, initColorCam));
        yield return ColorChange(FinalColor, initColor);
        yield return null;
    }
    //change la couleur de la cam
    IEnumerator colorCam(Color32 init, Color32 final){
        t = 0; 
        var BaseTP = CameraPlayer.transform.Find("Plane").gameObject.GetComponent<SpriteRenderer>();
        while (t <= temps){
            var ValInter = Color.Lerp(init, final, t);
            BaseTP.color = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        BaseTP.color = final;
    }
    //change la couleur de la sphere de Tp
    IEnumerator ColorChange(Color32 init, Color32 final){
        t = 0;
        var BaseTP = listTpGame[currentState].GetComponent<Renderer>();
        while (t <= temps){
            var ValInter = Color.Lerp(init, final, t);
            BaseTP.material.color = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        BaseTP.material.color = final;
    }
    //fait attendre jusqu'a la rotation s;est termine;
    IEnumerator wait(Vector3[] listTp, int State, int misedANiveau){

        yield return new WaitUntil(() => isRotate == false);
        currentState = (State + misedANiveau + 3) % 3;
        StartCoroutine(Rotate(listpositionTp[(currentState - misedANiveau + 3) % 3], listpositionTp[currentState]));
    }
    //fait tourner la platforme de TP
    IEnumerator Rotate(Vector3 Depart, Vector3 Final){
        isRotate = true;
        t = 0;
        gameObject.GetComponent<AudioSource>().Play();
        while (t <= temps){
            Vector3 ValInter = Vector3.Lerp(Depart, Final, t);
            TargetObject.transform.localEulerAngles = ValInter;
            Modif = TargetObject.transform.localEulerAngles;
            t += Time.deltaTime;
            yield return null;
        }
        Modif = Final;
        TargetObject.transform.localEulerAngles = Modif;
        isRotate = false;
    }
}
