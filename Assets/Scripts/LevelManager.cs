using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isInDialogue = false;
	public bool lvl2CollectibleGot = false;
	public bool hasFallDamage;

    [SerializeField] private GameObject playerPrefab;
	[SerializeField] private Transform spawnPoint;

	[SerializeField] private List<GameObject> backgroundLayers;

	[SerializeField] private Vector2 backgroundOffset;
	[SerializeField] private Vector2 backgroundParallax;

	private GameObject player;

	void Start()
	{
		player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
		player.name = "birb";
	}

	private void Update()
	{
		for (int i = 0; i < backgroundLayers.Count; i++)
		{
			backgroundLayers[i].transform.position = new Vector3(
				player.transform.position.x - player.transform.position.x / (backgroundParallax.x * (i + 1)) + backgroundOffset.x,
				player.transform.position.y - player.transform.position.y / (backgroundParallax.y * (i + 1)) + backgroundOffset.y,
				backgroundLayers[i].transform.position.z);
		}
	}

	public void Reset()
	{
		player.transform.position = spawnPoint.position;
	}
}
