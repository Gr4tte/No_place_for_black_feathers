using UnityEngine;

public class Hazard : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag != "Player") return;

		GameObject.Find("birb").GetComponent<moveTest>().death();
	}
}
