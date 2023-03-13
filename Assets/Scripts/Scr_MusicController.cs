using UnityEngine;

// 1 BPM = 1/60 Hz (s^-1)
// 120 BPM = 120/60 Hz (s^-1)
public class Scr_MusicController : MonoBehaviour
{
    public static Scr_MusicController instance; 


    public bool gameHasStarted;
    public AudioSource au_musicSource; // Used into BeatReference
    [SerializeField] float bpm;
    public float secPerBeat; // Used into BeatReference
    float songStart;
    public float songPosition; // Used into PlayerMovement
    public float songPositionInBeats; // Used into GameEvents
    
    void Awake() {
        instance = this;     
        au_musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f/bpm; // 1bpm = 1/60 [Hz] so 1 secPerBeat = 1/60bpm = 60s/60 
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameHasStarted){
            if(Input.anyKeyDown){
                songStart = (float) (AudioSettings.dspTime);
                au_musicSource.Play();
                gameHasStarted = true;
            }
        }

        if(!au_musicSource.isPlaying){
            return;
        }
        songPosition = (float) (AudioSettings.dspTime - songStart);
        songPositionInBeats = Mathf.FloorToInt( songPosition / secPerBeat);
    }
}
