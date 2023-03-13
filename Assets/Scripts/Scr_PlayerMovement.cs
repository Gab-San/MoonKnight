using UnityEngine;

public class Scr_PlayerMovement : MonoBehaviour
{

    Rigidbody2D com_rb2D;
    float unitPerBeat;
    public float velocityMultiplier;
    public float offsetPosition;
    
    void Awake() {
        com_rb2D = GetComponent<Rigidbody2D>();
        offsetPosition = this.transform.position.x;
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

        // Adding the offsetPosition to the PlayerMovement so that it starts from the correct position
        // This can be rewritten so that less calculations must be taken in occurrence each beat 
        com_rb2D.MovePosition(Vector2.right * (offsetPosition + unitPerBeat * (Scr_MusicController.instance.songPosition) * velocityMultiplier) );
    }

}
