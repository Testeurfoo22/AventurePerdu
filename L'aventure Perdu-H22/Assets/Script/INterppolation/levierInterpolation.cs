using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levierInterpolation : MonoBehaviour
{

    public GameObject Stele;    //la stele
    public GameObject[] puzzleLevierBase = new GameObject[10];  //tableau des 10 levier
    public bool[] puzzleLevierFinal = new bool[10]; // tableau des 10 positions initiales des leviers
    bool[] puzzleLevier = new bool[10]{false, false, false, false, false, false, false, false, false, false}; //tableau final
    Color32 initColor = new Color32(100, 100, 100, 255); //couleur led initiale
    Color32 Openlight = new Color32(255, 255, 0, 255); //couleur led finale
    bool resolu = false;
    // Start is called before the first frame update
    void awake()
    {
        initColor = new Color32(100, 100, 100, 255); 
        Openlight = new Color32(255, 255, 0, 255); 
        resolu = false;
    }
    //si non resolu, si on clic sur un levier, la lancer la methode de changement de couleurs, check si le systeme est resolu
    // Update is called once per frame
    void Update()
    {
        if (!resolu){
            if( Input.GetMouseButtonDown(0) ){
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                
                if( Physics.Raycast( ray, out hit, 10) )
                {
                    try{
                        if (hit.transform.gameObject.name == "Cube"){
                            int index = System.Array.IndexOf(puzzleLevierBase, hit.transform.gameObject);
                            GameObject light = hit.transform.gameObject.transform.parent.Find("Sphere").gameObject;
                            GameObject Levier = hit.transform.gameObject.transform.parent.Find("Cube").gameObject;
                            Levier.GetComponent<AudioSource>().Play();
                            if (puzzleLevier[index] == false){
                                puzzleLevier[index] = true;
                                StartCoroutine(ModifLight(light, 0.1F, Openlight));
                            }
                            else{
                                puzzleLevier[index] = false;
                                StartCoroutine(ModifLight(light, 0.1F, initColor));
                            }

                            resolu = true;
                            for(int x = 0; x < 10; x++){
                                if (puzzleLevier[x] != puzzleLevierFinal[x]){
                                    resolu = false;
                                }
                            }
                            if (resolu){
                                StartCoroutine(Done(0.1F));
                            }
                        }
                    }
                    catch{

                    }
                }
            }
        }
    }
    // change la led au dessus de chaque levier
    IEnumerator ModifLight(GameObject Sphere, float temps, Color32 final){
        float t = 0;
        var led = Sphere.GetComponent<Renderer>();
        var init = led.material.color;
        while (t <= temps){
            var ValInter = Color.Lerp(init, final, t);
            led.material.color = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        led.material.color = final;
    }
    //termine le levier, en changeant de couleurs tout les levier
    IEnumerator Done(float temps){
        gameObject.GetComponent<AudioSource>().Play();
        for (int x = 0; x < 10; x++){
            float t = 0;
            var Basepuzzle = puzzleLevierBase[x].GetComponent<Renderer>();
            while (t <= temps){
                var ValInter = Color.Lerp(initColor, new Color32(0, 0, 0, 255), t);
                Basepuzzle.material.color = ValInter;
                t += Time.deltaTime;
                yield return null;
            }
            Basepuzzle.material.color = new Color32(0, 0, 0, 255);
        }
        yield return new WaitForSecondsRealtime(3);
        Stele.GetComponent<SteleInteraction>().UpdateLight();
    }

}
