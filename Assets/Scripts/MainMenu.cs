using UnityEngine;

public class MainMenu : MonoBehaviour
{
	public void StartGame()
    {
        SceneManager.instance.LoadScene(GameManager.instance.completedLevels < 1 ? Scenes.Cutscene : Scenes.LevelSelector);
        AudioManager.instance.mainMenu();
        AudioManager.instance.lvl1AudioKill();
        AudioManager.instance.lvl2AudioKill();
        AudioManager.instance.lvl3AudioKill();
        AudioManager.instance.lvl4AudioKill();
    }
}
