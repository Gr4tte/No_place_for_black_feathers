using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
	public static SceneManager instance;

	public RawImage transitionImage;
	public float FadeDuration;
	private Scenes currentScene;

	public bool IsInLevel
	{
		get => currentScene == Scenes.Level1 || currentScene == Scenes.Level2 || currentScene == Scenes.Level3 || currentScene == Scenes.Level4;
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
			LoadScene(Scenes.Menu);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{

	}

	public void LoadScene(Scenes sceneToLoad)
	{
		if (sceneToLoad == currentScene) return;

		int currentSceneIndex = (int)currentScene;
		int sceneToLoadIndex = (int)sceneToLoad;

		bool useFade = false;
		if (Camera.main != null)
		{
			transitionImage.texture = GetCameraScreenshot();
			useFade = true;
		}

		AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync((sceneToLoadIndex), UnityEngine.SceneManagement.LoadSceneMode.Additive);
		operation.completed += (_) =>
		{
			UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(currentSceneIndex);
			if (scene != null && scene.IsValid() && useFade)
				UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(scene);
			UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(sceneToLoadIndex));

			if (useFade)
				StartCoroutine(FadeOut());
		};

		currentScene = sceneToLoad;
	}

	private Texture2D GetCameraScreenshot()
	{
		Camera camera = Camera.main;

		RenderTexture renderTexture = new RenderTexture(camera.pixelWidth, camera.pixelHeight, 24, RenderTextureFormat.ARGB32);
		renderTexture.antiAliasing = 2;

		camera.targetTexture = renderTexture;
		camera.Render();

		RenderTexture.active = renderTexture;
		Texture2D screenshot = new Texture2D(camera.pixelWidth, camera.pixelHeight, TextureFormat.RGB24, false);
		screenshot.ReadPixels(new Rect(0, 0, camera.pixelWidth, camera.pixelHeight), 0, 0, false);
		screenshot.Apply();

		RenderTexture.active = null;
		camera.targetTexture = null;
		renderTexture.DiscardContents();
		renderTexture.Release();

		return screenshot;
	}

	private IEnumerator FadeOut()
	{
		Color source = Color.white;
		Color destination = new Color(1, 1, 1, 0);
		float t = 0f;
		while (t < FadeDuration)
		{
			transitionImage.color = Color.Lerp(source, destination, t / FadeDuration);
			t += Time.deltaTime;
			//Debug.Log(t);
			yield return null;
		}
	}
}

public enum Scenes
{
	Menu = 1,
	LevelSelector = 2,
	Cutscene = 3,
	Level1 = 4,
	Level2 = 5,
	Level3 = 6,
	Level4 = 7
}
