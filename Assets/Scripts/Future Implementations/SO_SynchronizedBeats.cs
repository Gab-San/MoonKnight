using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BeatRecorder", fileName = "_Event_ Beats")]
public class SO_SynchronizedBeats : ScriptableObject
{
    [SerializeField] List<Vector2> beatsPositions;
    [SerializeField] Color beatReferenceColor;

    public void AddBeat(Vector2 beat_position, float velocity){}
    public void RemoveBeat(Vector2 beat_position){}
    void NormalizeBeatPosition(Vector2 beat_position, float velocity){}
    void CalculateBeatPosition(float velocity){}
}
