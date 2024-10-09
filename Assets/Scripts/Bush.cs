using UnityEngine;

public class Bush : MonoBehaviour
{
	private LevelManager _levelScript;


	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag != "Player") return;

        if (!GameObject.Find("Level Manager").GetComponent<LevelManager>().lvl2CollectibleGot)
        {
            GameObject.Find("birb").GetComponent<moveTest>().death();
        }
    }
}
