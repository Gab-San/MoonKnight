using UnityEngine;

public class Scr_BeatReference : MonoBehaviour
{
    Scr_MusicController scr_MusicController;
    AudioSource au_audioSource;
    [SerializeField] float velocity;

    void Awake(){
        scr_MusicController =  Scr_MusicController.instance;
        au_audioSource = GetComponent<AudioSource>();
    }

}
