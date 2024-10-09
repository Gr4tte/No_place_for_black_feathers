using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
	[SerializeField] private Image image;
	[SerializeField] private List<CutsceneScriptableObject> cutscenes;

	private List<Sprite> images;
	private Scenes sceneToLoad;
	private float displayDuration;
	private float fadeInDuration;
	private float fadeOutDuration;
	private float pauseDuration;

	private float fadeInStart;
	private bool fadeInStarted;
	private float fadeOutStart;
	private bool fadeOutStarted;
	private int currentImage;

	void Start()
	{
		CutsceneScriptableObject so = cutscenes[GameManager.instance.playEndCutscene ? 5 : GameManager.instance.currentLevel];
		images = so.images;
		sceneToLoad = so.sceneToLoad;
		displayDuration = so.displayDuration;
		fadeInDuration = so.fadeInDuration;
		fadeOutDuration = so.fadeOutDuration;
		pauseDuration = so.pauseDuration;

		image.CrossFadeAlpha(0, 0, true);

		fadeInStart = Time.time + pauseDuration;
		fadeOutStart = fadeInStart + fadeInDuration + displayDuration;

		fadeInStarted = false;
		fadeOutStarted = false;

		currentImage = 0;
		image.sprite = images[currentImage];
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > fadeOutStart + fadeOutDuration + pauseDuration || Input.GetKeyDown(KeyCode.Space))
		{
			if (currentImage < images.Count - 1)
			{
				image.sprite = images[++currentImage];
				fadeInStart = fadeOutStart + fadeOutDuration + pauseDuration;
				fadeOutStart = fadeInStart + fadeInDuration + displayDuration;

				fadeInStarted = false;
				fadeOutStarted = false;
			}
			else
			{
				if (GameManager.instance.playEndCutscene)
				{
					AudioManager.instance.endingAudioKill();
					AudioManager.instance.mainMenu();

					GameManager.instance.playEndCutscene = false;
				}

				SceneManager.instance.LoadScene(sceneToLoad);
			}
		}
		else
		{
			if (Time.time > fadeInStart && !fadeInStarted)
			{
				image.CrossFadeAlpha(1, fadeInDuration, true);
				fadeInStarted = true;
			}
			else if (Time.time > fadeOutStart && !fadeOutStarted)
			{
				image.CrossFadeAlpha(0, fadeOutDuration, true);
				fadeOutStarted = true;
			}
		}
	}
}
