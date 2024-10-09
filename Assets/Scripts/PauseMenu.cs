using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private LevelManager levelManager;

	[SerializeField] private GameObject pauseMenu;
	private bool isPaused;
	private moveTest _moveTestScript;

	// Update is called once per frame
	void Update()
	{
		pauseMenu.SetActive(isPaused);

		if ((Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape)) && !levelManager.isInDialogue)
		{
			isPaused = !isPaused;
			FindBirb();
			_moveTestScript.pauseForBirb(isPaused);
		}
	}

	public void Continue()
	{
		isPaused = false;
		FindBirb();
		_moveTestScript.pauseForBirb(isPaused);
	}

	public void Exit()
	{
		AudioManager.instance.lvl1AudioKill();
		AudioManager.instance.lvl2AudioKill();
		AudioManager.instance.lvl3AudioKill();
		AudioManager.instance.lvl4AudioKill();
		AudioManager.instance.mainMenu();

		GameManager.instance.currentLevel = 0;
		SceneManager.instance.LoadScene(Scenes.LevelSelector);
	}

	public void SkipLevel()
	{
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

	private void FindBirb()
	{
		if (_moveTestScript != null) return;

		_moveTestScript = GameObject.Find("birb").GetComponent<moveTest>();
	}
}
