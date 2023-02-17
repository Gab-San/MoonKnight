using UnityEngine;

public class Scr_MusicController : MonoBehaviour
{
    
    AudioSource au_musicSource;
    [SerializeField] float bpm;
    float secPerBeat;
    float songStart;
    float songPosition;
    public float songPositionInBeats;
    public static Scr_MusicController instance; 

    void Awake() {
       instance = this;     
    }
    
    // Start is called before the first frame update
    void Start()
    {
        au_musicSource = GetComponent<AudioSource>();
        songStart = (float) (AudioSettings.dspTime);
        secPerBeat = 60f/bpm;
        au_musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float) (AudioSettings.dspTime - songStart);
        songPositionInBeats = Mathf.FloorToInt(songPosition / secPerBeat);
    }
}
