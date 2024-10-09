using UnityEngine;

public class moveTest : MonoBehaviour
{
	private Rigidbody2D _rb;

	public bool isGrounded = false;
	[SerializeField] private LayerMask _groundmask;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private float _jumpForce;
	[SerializeField] private float _speed;
	[SerializeField] private float _groundDrag;
	[SerializeField] private float _airDrag;
	private bool _paused = false;
    private bool _wasGrounded;
    private bool _wasFalling;
	public bool dead = false;
    private float _startOfFall;
    [SerializeField] private float _minimumFall;
	private float _deathTimer;

    public bool IsPaused
	{
		get
		{
			return _paused;
		}
		private set { }
	}
	private Vector2 _rbVelocity;

	[SerializeField] private float dashDuration;
	[SerializeField] private Vector2 dashSpeed;

	private float lastDirection = 1;
	private Vector2 dashDirection;
	private float dashDurationLeftAfterPause = float.NegativeInfinity;
	private float jumpStartTime = 0;
	private float dashEnd = 0;
	private bool isDashing = false;
	public bool IsDashing
	{
		get
		{
			return isDashing;
		}
		private set { }
	}

	[SerializeField] private int jumpsLeft;
	[SerializeField] private int dashesLeft;

	private int maxJumps;
	private int maxDashes;

	void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		maxJumps = GameManager.instance.maxJumps;
		maxDashes = GameManager.instance.maxDashes;
	}

	// Update is called once per frame
	void Update()
	{
		if (_paused) return;

		if (Time.time > dashEnd && isDashing && !_paused)
		{
			isDashing = false;
			dashesLeft--;
        }

        if (!dead)
        {
            _movement();
			_jumping();
			_dashing();
		}

		_isGrounded();
		_fall();
		if (dead)
        {
			_extraDeath();
        }
	}

	private void _dashing()
	{
		if (Input.GetKeyDown(KeyCode.LeftShift) && dashesLeft > 0 && !isDashing)
		{
			dashDirection = new Vector2(
				Input.GetAxis("Horizontal") >= 0 ? 1 : -1,
				Input.GetAxis("Vertical") >= 0 ? 1 : -1);

			dashDirection.x = Input.GetAxis("Horizontal") == 0 ? lastDirection : dashDirection.x;

			dashEnd = Time.time + dashDuration;
			isDashing = true;
		}
	}

	private void _movement()
	{
		_rb.velocity = isDashing ?
			dashSpeed * dashDirection :
			new Vector2(Input.GetAxis("Horizontal") * _speed, _rb.velocity.y);

		lastDirection = Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") : lastDirection;
		lastDirection = lastDirection > 0 ? 1 : lastDirection;
		lastDirection = lastDirection < 0 ? -1 : lastDirection;
    }

    private void _isGrounded()
    {
        Vector2 groundchecksize = new Vector2(_groundCheck.localScale.x, _groundCheck.localScale.y) / 2;

        Collider2D collider2D = Physics2D.OverlapBox(_groundCheck.position, groundchecksize, 0, _groundmask);
        isGrounded = Physics2D.OverlapBox(_groundCheck.position, groundchecksize, 0, _groundmask);

        jumpsLeft = isGrounded ? maxJumps : Mathf.Min(jumpsLeft, maxJumps - 1);
        dashesLeft = isGrounded ? maxDashes : dashesLeft;
    }

	private void _fall()
    {
		if (!_wasFalling && _isFalling) _startOfFall = transform.position.y;

		float _fallDistance = _startOfFall - transform.position.y;
		if (!_wasGrounded && isGrounded)
        {
			if (_fallDistance > _minimumFall)
            {
				if (GameObject.Find("Level Manager").GetComponent<LevelManager>().hasFallDamage)
                {
					death();
				}
			}
        }
        _wasGrounded = isGrounded;
		_wasFalling = _isFalling;
    }
    bool _isFalling { get { return (!isGrounded && _rb.velocity.y < 0); } }
	private void _jumping()
	{
		if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && !isDashing && Time.time > jumpStartTime)
		{
			_rb.velocity = new Vector2(_rb.velocity.x, 0);
			Vector2 force = new Vector2(0, _jumpForce);
			_rb.AddForce(force, ForceMode2D.Impulse);
			jumpsLeft--;
		}
	}

	public void pauseForBirb(bool _ispaused)
	{
		if (_ispaused)
		{
			dashDurationLeftAfterPause = isDashing ? dashEnd - Time.time : float.NegativeInfinity;
			_paused = true;
			_rbVelocity = _rb.velocity;
			_rb.velocity = new Vector2(0, 0);
			_rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
		else
		{
			jumpStartTime = Time.time + 0.1f;
			dashEnd = dashDurationLeftAfterPause > 0 ? Time.time + dashDurationLeftAfterPause : 0;
			_paused = false;
			_rb.velocity = _rbVelocity;
			_rb.constraints = RigidbodyConstraints2D.None;
			_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void death()
    {
        dead = true;
        _deathTimer = 1;
        _rb.constraints = RigidbodyConstraints2D.None;
		_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void _extraDeath()
    {
        _deathTimer -= Time.deltaTime;
        if (_deathTimer <= 0)
        {
			transform.rotation = new Quaternion();
			GameObject.Find("Level Manager").GetComponent<LevelManager>().Reset();
            dead = false;
        }
    }
}

