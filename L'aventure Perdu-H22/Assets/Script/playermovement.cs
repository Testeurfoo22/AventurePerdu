using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float vitesse = 10F ;
    public float acceleration = 5F;
    public float rotation = 10F;
    public bool controlVericalInversion = false;
    public bool controlHorizontalInversion = false;
    public float distanceTerrain = 2F;
    static bool isMovable = true;
    // Start is called before the first frame update
    void Start()
    {
        //Do Nothing
    }

    // Update is called once per frame
    void Update()
    {
        if(isMovable){
            GestionBoutonDroit();

            if (!Cursor.visible){
                transform.localRotation = GestionRotation();
            }

            transform.Translate(Deplacement() * Time.deltaTime * Acceleration());

            if (Terrain.activeTerrain){
                transform.position = GestionTerrain();
            }
        }
    }

    public static IEnumerator Freeze(int Temps){
        isMovable = false;
        yield return new WaitForSecondsRealtime(Temps);
        isMovable = true;
    }
    
    //permet d'eviter la collision avec le terrain
    Vector3 GestionTerrain(){
        float hauteurTerrain =
            Terrain.activeTerrain.transform.position.y +
            Terrain.activeTerrain.SampleHeight(transform.position) +
            distanceTerrain;

        return new Vector3(
            transform.position.x,
            Mathf.Max(transform.position.y, hauteurTerrain),
            transform.position.z);
    }

    //gere la rotation de la souris ssi le cursur est bloque 
    Quaternion GestionRotation(){
        float rotationX = Input.GetAxis("Mouse X");
        float rotationY = Input.GetAxis("Mouse Y");
        //Limite de rota
        float limite = 1F;
        rotationX = Mathf.Clamp(rotationX, -limite, limite);
        rotationY = Mathf.Clamp(rotationY, -limite, limite);

        //permet de langement d'axe
        if (controlHorizontalInversion){
            rotationX = -rotationX;
        }
        if (controlVericalInversion){
            rotationY = -rotationY;
        }
        //rota souris
        Vector3 rotationSouris = new Vector3(rotationY, rotationX, 0F) * rotation;
        //rota de la cam
        Vector3 rotationPresente = transform.eulerAngles;
        //rota resultante
        return Quaternion.Euler(rotationPresente + rotationSouris);
    }

    //permet de lock la souris dans l'ecran de jeu
    void GestionBoutonDroit(){
        if (Input.GetMouseButtonDown(1)){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButtonUp(1)){
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //renvoie un vecteur de deplacement a partir des touches suivantes.
    Vector3 Deplacement(){
        Vector3 deplacement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)){
            deplacement += Vector3.forward; 
        }
        if (Input.GetKey(KeyCode.S)){
            deplacement += Vector3.back; 
        }

        if (Input.GetKey(KeyCode.A)){
            deplacement += Vector3.left; 
        }
        if (Input.GetKey(KeyCode.D)){
            deplacement += Vector3.right; 
        }

        if (Input.GetKey(KeyCode.Q)){
            deplacement += Vector3.up; 
        }
        if (Input.GetKey(KeyCode.E)){
            deplacement += Vector3.down; 
        }

        return deplacement;
    }

    //retourne la vitesse finale, avec ou sans acceleration
    float Acceleration(){

        if (Input.GetKey(KeyCode.LeftShift)){
            return vitesse * acceleration;
        }
        return vitesse;
    }
}
