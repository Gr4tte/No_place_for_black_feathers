using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "ScriptableObjects/Cutscene", order = 1)]
public class CutsceneScriptableObject : ScriptableObject
{
    public Scenes sceneToLoad;

    public List<Sprite> images;
    public float displayDuration;
    public float fadeInDuration;
    public float fadeOutDuration;
    public float pauseDuration;
}
