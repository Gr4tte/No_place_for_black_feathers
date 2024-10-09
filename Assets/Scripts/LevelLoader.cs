using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
	[SerializeField] private List<Button> buttons;

	private void Start()
	{
		for (int i = 0; i < buttons.Count; i++)
		{
			bool isUnlocked = i < GameManager.instance.completedLevels + 1;
			buttons[i].image.color = isUnlocked ? new(1, 1, 1, 1) : new(0.3f, 0.3f, 0.3f, 1);
        }
    }
    public void LoadLevel(int level)
    {
        if (level > GameManager.instance.completedLevels + 1) return;

        GameManager.instance.currentLevel = level;
        SceneManager.instance.LoadScene(Scenes.Cutscene);
        AudioManager.instance.mainMenuKill();

        if (level == 1){AudioManager.instance.lvl1Audio();}
        if (level == 2){AudioManager.instance.lvl2Audio();}
        if (level == 3){AudioManager.instance.lvl3Audio();}
        if (level == 4){AudioManager.instance.lvl4Audio();}
    }
}
