using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteleInteraction : MonoBehaviour
{
    public GameObject[] light1 = new GameObject[2]; //tableau de 2 lights
    public GameObject[] light2 = new GameObject[2]; //tableau de 2 autres lights
    bool[] Status = new bool[2]{false, false};  // status de bool, pour les lights
    public Color32 initColor;   // couleurs initiales
    public Color32 finalColor;  //couleurs finales
    public GameObject baseMid;  //base du milieu
    public Vector3 blockYbase = new Vector3(0F, 0F, 0F);    //position y de la base initiales
    public Vector3 blockYFinal = new Vector3(0F, 0F, 0F);   //position finales y
    public float temps; //temps interpolation
    public GameObject Pont;     //le pont
    public float initIntensity;     //intensite initiale
    public float finalIntensity;    //intensite finale
    bool clicked;
    // Start is called before the first frame update
    void Awake()
    {
        //initialise les lights;
        Status = new bool[]{false, false};
        for (int x = 0; x < 2; x++){
            light1[x].GetComponent<Light>().color = initColor;
            light2[x].GetComponent<Light>().color = initColor;
            light1[x].GetComponent<Light>().intensity = initIntensity;
            light2[x].GetComponent<Light>().intensity = initIntensity;
        }
        baseMid.transform.localPosition = blockYbase;
        clicked = false;
    }
    //si le nouton du milieu c;est pas clique, si on clic sur le bouton lance la fonction de de placement du pont;
    // Update is called once per frame
    void Update()
    {
        if (!clicked){
            if( Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                try{
                    if( Physics.Raycast( ray, out hit, 10) )
                    {
                        if (hit.transform.gameObject.name == "Clic"){
                            clicked = true;
                            StartCoroutine(Pont.GetComponent<pontInterpolation>().Pont());
                        }
                    }
                }
                catch{

                }
            }
        }
    }
    //change les lights en fonction des puzzles et leviers
    public void UpdateLight(){
        for (int x = 0; x < 2; x++){
            if (Status[x] == false){
                Status[x] = true;
                if(x == 0){
                    StartCoroutine(Light(light1));
                }
                else{
                    StartCoroutine(Light(light2));
                }
                break;
            }
        }
        bool finished = true;
        for (int x = 0; x < 2; x++){
            if (Status[x] == false){
                finished = false;
            }
        }
        if (finished){
            StartCoroutine(SteleMouvement());
        }
    }
    //changes les lights de couleurs et l,intensite
    IEnumerator Light(GameObject[] lightTab){
        for (int x = 0; x < 2; x++){
            float t = 0;
            var light = lightTab[x].GetComponent<Light>();
            var intensity = lightTab[x].GetComponent<Light>();
            while (t <= temps){
                var ValInter = Color.Lerp(initColor, finalColor, t);
                light.color = ValInter;
                var ValInterintensity = Mathf.Lerp(initIntensity, finalIntensity, t);
                intensity.intensity = ValInterintensity;
                t += Time.deltaTime;
                yield return null;
            }
            light.color = finalColor;
            intensity.intensity = finalIntensity;
        }
    }
    //fait monter la stele quand les lights sont allumees
    IEnumerator SteleMouvement(){
        yield return new WaitForSecondsRealtime(4);
        float t = 0;
        gameObject.GetComponent<AudioSource>().Play();
        while (t <= temps){
            Vector3 ValInter = Vector3.Lerp(blockYbase, blockYFinal, t);
            baseMid.transform.localPosition = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        baseMid.transform.localPosition = blockYFinal;
    }
}
