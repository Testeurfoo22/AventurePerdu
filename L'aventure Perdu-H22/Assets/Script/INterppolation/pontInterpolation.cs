using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pontInterpolation : MonoBehaviour
{
    public float Initpos;   //position initiales
    public float FinalPos;  //position finales
    public float Temps;     //temps interpolation
    // Start is called before the first frame update
    void Awake() {
        transform.localPosition = new Vector3(transform.localPosition.x, Initpos, transform.localPosition.z);
    }
    void Update()
    {
    }
    //change la position du pont sur un temps t
    // Update is called once per frame
    public IEnumerator Pont(){
        yield return new WaitForSecondsRealtime(3);
        float t = 0;
        Vector3 Modif = transform.localPosition;
        gameObject.GetComponent<AudioSource>().Play();
        while (t <= Temps){
            float ValInter = Mathf.Lerp(Initpos, FinalPos, t);
            Modif.y = ValInter;
            transform.localPosition = Modif;
            t += Time.deltaTime;
            yield return null;
        }
        Modif.y = FinalPos;
        transform.localPosition = Modif;
    }
}
