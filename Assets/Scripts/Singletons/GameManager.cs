using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public int completedLevels = 0;
	public int currentLevel = 0;
	public bool playEndCutscene;

	[HideInInspector]
	public int maxDashes
	{
		get
		{
			return levelMovementInfo[completedLevels].maxDashes;
		}
		private set { }
	}

	public int maxJumps
	{
		get
		{
			return levelMovementInfo[completedLevels].maxJumps;
		}
		private set { }
	}

	[SerializeField] private LevelMovementInfo[] levelMovementInfo = new LevelMovementInfo[4];

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	[System.Serializable]
	public struct LevelMovementInfo
	{
		public int maxDashes;
		public int maxJumps;
	}
}
