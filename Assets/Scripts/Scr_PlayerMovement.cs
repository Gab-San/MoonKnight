using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    Rigidbody2D com_rb2D;
    float unitPerBeat;
    public float velocity;
    
    void Awake() {
        com_rb2D = GetComponent<Rigidbody2D>();
    }

    void Start(){

        // secPerBeat [Hz = 1/s]
        // unitPerBeat is a normalized velocity (1/x [m/s]) 1f per beat
        // unitPerBeat times a number gives the real velocity at which the player moves
        unitPerBeat = 1 / Scr_MusicController.instance.secPerBeat; 
    }

    void Update() {
        if(!Scr_MusicController.instance.gameHasStarted){
            return;
        }
        com_rb2D.MovePosition(Vector2.right * unitPerBeat * (Scr_MusicController.instance.songPosition) * velocity);
    }

}
