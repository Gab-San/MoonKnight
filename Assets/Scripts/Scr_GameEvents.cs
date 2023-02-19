using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_GameEvents : MonoBehaviour
{
    // Reference to Blocks
    [Header("Block Event")]
    [SerializeField] List<GameObject> blocksList;
    bool emptyList => (blocksList.Count == 0);
    bool didSwap;
    int blockIndex = 0;
    float nextBeat;

    [Header("Beat Counter UI")]
    GameObject ui_beatText;
    [SerializeField] ScreenSizeOptions resOption; 
    enum ScreenSizeOptions{
        LowRes = 0, // 480p
        MedRes = 1, // 720p
        HighRes = 2 // 1080p
    }
    [SerializeField] TMP_FontAsset beatTextFont;

    void Start(){

        if(!emptyList){
            // Disable all blocks
            foreach(GameObject block in blocksList){
                block.SetActive(false);
            }
            blocksList[blockIndex].SetActive(true); // Activate first block into the list in order to save an if-statement
            didSwap = true; // Set-up bool to calculate next beat
        }
    
        BeatTextInit();
    }

    void Update(){
        if(!emptyList) BlockSwap();
        BeatTextUpd();
    }

    // Appearing and Disappearing Blocks
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

    // Initialization to beat text counter UI by script 
    void BeatTextInit(){
        // Create new Canvas obj
        GameObject ui_beatCanvas = new GameObject("Beat Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        Canvas com_canvas = ui_beatCanvas.GetComponent<Canvas>();
        CanvasScaler com_canvasScaler = ui_beatCanvas.GetComponent<CanvasScaler>();

        com_canvas.renderMode = RenderMode.ScreenSpaceCamera;   // This sets the rendering mode to Camera. This way it is fixed on the camera
        com_canvas.worldCamera = Camera.main;   // This sets the camera to which the canvas is fixed to
        com_canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; // This scales the camera with 'screen resol'

        Vector2 resolution; // Screen resolution var
        Vector2 textRectSize;   // Text Box Size var
        int textFontSize;   // Text Font Size var

        // This switch statement sets values for the various sizes linked to 'screen resol'
        switch (resOption){
            case ScreenSizeOptions.LowRes:  resolution = new Vector2(640, 480); // 480p
                                            textRectSize = new Vector2(150, 75);
                                            textFontSize = 20; break;
            case ScreenSizeOptions.MedRes:  resolution = new Vector2(1280, 720); // 720p
                                            textRectSize = new Vector2(300, 150);
                                            textFontSize = 40; break;
            case ScreenSizeOptions.HighRes: resolution = new Vector2(1920, 1080);   // 1080p
                                            textRectSize = new Vector2(450, 225);
                                            textFontSize = 60; break;
            default:    Debug.LogError("Resolution not found or not set");
                        resolution = new Vector2(0,0);
                        textRectSize = new Vector2(0,0);
                        textFontSize = 0;  break;
        }

        com_canvasScaler.referenceResolution = resolution; // Set screen resol

        // Create Beat Text obj and placing in hierarchy
        ui_beatText = new GameObject("Beat Counter Text", typeof(TextMeshProUGUI));
        ui_beatText.transform.SetParent(ui_beatCanvas.transform, false);

        // Defining Text Box Sizes and Positions
        RectTransform com_textTransform = ui_beatText.GetComponent<RectTransform>(); 
        com_textTransform.anchorMax = new Vector2(0f, 1f);
        com_textTransform.anchorMin = new Vector2(0f, 1f);
        com_textTransform.pivot = new Vector2(0f, 1f);
        // com_textTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 300f, 100f);
        // com_textTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 50f, 200f);
        com_textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textRectSize.x);
        com_textTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textRectSize.y);

        TextMeshProUGUI com_beatText = ui_beatText.GetComponent<TextMeshProUGUI>();
        com_beatText.font = beatTextFont;
        com_beatText.fontStyle = FontStyles.Normal;
        com_beatText.alignment = TextAlignmentOptions.Center;
        com_beatText.fontSize = textFontSize;

    }

      void BeatTextUpd(){
        // Update beat text counter to the beat
        ui_beatText.GetComponent<TextMeshProUGUI>().text = "Beat: " + (Scr_MusicController.instance.songPositionInBeats + 1);

    }
}
