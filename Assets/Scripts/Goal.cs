using UnityEngine;

public class Goal : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag != "Player" || collider.gameObject.GetComponent<moveTest>().dead) return;

		GameManager.instance.completedLevels = Mathf.Max(GameManager.instance.completedLevels, GameManager.instance.currentLevel);

		if (GameManager.instance.currentLevel == 4)
		{
			GameManager.instance.currentLevel = 0;
			GameManager.instance.playEndCutscene = true;
			SceneManager.instance.LoadScene(Scenes.Cutscene);
			AudioManager.instance.lvl4AudioKill();
			AudioManager.instance.endingAudio();
			return;
		}
		else
		{
			GameManager.instance.currentLevel = 0;
			SceneManager.instance.LoadScene(Scenes.LevelSelector);
		}


		AudioManager.instance.lvl1AudioKill();
		AudioManager.instance.lvl2AudioKill();
		AudioManager.instance.lvl3AudioKill();
		AudioManager.instance.lvl4AudioKill();
		AudioManager.instance.mainMenu();
	}
}
