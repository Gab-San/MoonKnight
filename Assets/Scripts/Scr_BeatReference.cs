using UnityEngine;
/*
Lets have in mind that 1 BPM = 1/60 Hz (s^-1)
*/
public class Scr_BeatReference : MonoBehaviour
{
    Scr_MusicController scr_MusicController;
    Scr_PlayerMovement scr_Player;
    AudioSource au_audioSource;
    [Header("Beat Reference Parameters")]
    static bool gameWasStarted;
    static float songtimeLength;
    static float secondsPerBeat;
    static float velocity;
    static float offsetPosition;
    [SerializeField] float fourthBeatDoubleLineoffset;
    [Header("Gizmos Modification")]
    [SerializeField] Color[] colors = new Color[2];

    void Awake(){
        scr_MusicController = Scr_MusicController.instance;
        scr_Player = GameObject.Find("Player").GetComponent<Scr_PlayerMovement>();
        au_audioSource = scr_MusicController.au_musicSource;
    }

    void Start(){
        // Sets the low left corner in 0 0 0
        Camera.main.transform.position = new Vector3 (Camera.main.aspect * Camera.main.orthographicSize ,Camera.main.orthographicSize, Camera.main.transform.position.z);
        songtimeLength = au_audioSource.clip.length;
        secondsPerBeat = scr_MusicController.secPerBeat;
        velocity = scr_Player.velocityMultiplier;
        offsetPosition = scr_Player.offsetPosition;
        gameWasStarted = true;
    }
 

    void OnDrawGizmos() {
        if(!gameWasStarted){
            return;
        }
    
        // Drawing Horizontal Line
        Gizmos.color = colors[0];
        for(int i = 0; i <= 2 * Camera.main.orthographicSize; i++){
            // By doing songLength/secondsPerBeat * velocity i'm multiplying time (songLength) per velocity (xf[m]/secondsPerBeat [s])
            Gizmos.DrawLine(Vector3.up * i, new Vector3(songtimeLength* velocity/secondsPerBeat, i, 0f)); 
        }
        // Drawing Vertical Line
        Gizmos.color = colors[1];
        // Song Length in Beats is total length of the song divided by the seconds it take for one beat to complete
        for(int i = 0; i < songtimeLength/secondsPerBeat; i++){
            float startingDrawPosition = i * velocity + offsetPosition; // Added starting offset position to the beatreference
            Gizmos.DrawLine(Vector3.right * startingDrawPosition, new Vector3(startingDrawPosition, 2* Camera.main.orthographicSize ,0f));
            if((i + 1) % 4 == 0){
                Gizmos.DrawLine(Vector3.right * (startingDrawPosition + fourthBeatDoubleLineoffset), new Vector3(startingDrawPosition + fourthBeatDoubleLineoffset, 2* Camera.main.orthographicSize ,0f));
            }
        }
    }
}
