using UnityEngine;

public class birbAnims : MonoBehaviour
{
	private moveTest _moveScript;
	private Animator _animator;
	private Vector2 _x;
	[SerializeField] private SpriteRenderer _sprite;

	// Start is called before the first frame update
	void Start()
	{
		_moveScript = GetComponent<moveTest>();
		_animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		_animator.enabled = !GetComponent<moveTest>().IsPaused;
		if (GetComponent<moveTest>().IsPaused) return;

		_x = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
		_animator.SetBool("isGrounded", _moveScript.isGrounded);
		_animator.SetFloat("speed_x", _x.x);
		if (_x.x >= 0.2f)
		{
			_sprite.flipX = false;
		}
		if (_x.x <= -0.2f)
		{
			_sprite.flipX = true;
        }
        _animator.SetBool("Dashing", _moveScript.IsDashing);
		_animator.SetBool("dead", _moveScript.dead);

    }

}
