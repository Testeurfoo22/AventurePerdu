using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCaverneInteraction : MonoBehaviour
{
    public float temps;     //variable temps, duree des interpolation
    public GameObject TpDoor;   //Object ou on doit etre tp
    public Vector3 distanceTp = new Vector3(0F, 0F, 1.5F);  //distance de deplacement
    Color32 initColorCam = new Color32(0, 0, 0, 0);     //couleur initiale de la cam
    Color32 FinalColorCam = new Color32(0, 0, 0, 255);  //couleur finale
    public GameObject CameraPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //check quand le player entre en contact avec notre collider
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MainCamera"){
            StartCoroutine(TP());
        }
    }

    //Tp(), change la couleur, lance le son de Tp et change la position du player
    IEnumerator TP(){
        yield return (colorCam(initColorCam, FinalColorCam));
        gameObject.GetComponent<AudioSource>().Play();
        CameraPlayer.transform.position = new Vector3(TpDoor.transform.position.x + TpDoor.transform.forward.x * distanceTp.z,
            distanceTp.y, TpDoor.transform.position.z + TpDoor.transform.forward.z * distanceTp.z);
        CameraPlayer.transform.localEulerAngles = TpDoor.transform.localEulerAngles;
        yield return (colorCam(FinalColorCam, initColorCam));
        yield return null;
    }

    //change la couleur de la cam
    IEnumerator colorCam(Color32 init, Color32 final){
        float t = 0; 
        var BaseTP = CameraPlayer.transform.Find("Plane").gameObject.GetComponent<SpriteRenderer>();
        while (t <= temps){
            var ValInter = Color.Lerp(init, final, t);
            BaseTP.color = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        BaseTP.color = final;
    }
}
