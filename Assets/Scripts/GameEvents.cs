using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Reference to Blocks
    [SerializeField] List<GameObject> blocksList = new List<GameObject>();
    bool didSwap;
    int blockIndex = 0;
    float nextBeat;
    
    // Appearing and Disappearing Blocks
    void Start(){
        foreach(GameObject block in blocksList){
            block.SetActive(false);
        }
        blocksList[blockIndex].SetActive(true);
        didSwap = true;
    }

    void BlockSwap(){
        float songPos = Scr_MusicController.instance.songPositionInBeats;

        if(didSwap){
            nextBeat = songPos + 4;
            didSwap = false;
        }

        Debug.Log(nextBeat + "\n" + songPos);

        if(songPos >= nextBeat && !didSwap){
            blocksList[blockIndex].SetActive(false);
            Debug.Log("Block Index: " + blockIndex + "\n" + blocksList.Count);
            blockIndex++;
            if(blockIndex >= blocksList.Count){
                blockIndex = 0;
            }
            blocksList[blockIndex].SetActive(true);
            didSwap = true;
        }
    }

    void Update(){
        BlockSwap();
    }
}
