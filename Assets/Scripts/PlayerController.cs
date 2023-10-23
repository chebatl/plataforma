using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private GameController _gameController;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    public Transform groundCheck;
    private bool onGround;
    private float horizontalSpeed;
    private float verticalSpeed;
    public float speed;
    public float jumpForce;
    public bool isFacingLeft;
    public LayerMask groundLayer;
    private bool doubleJump;
    public int extraJumps;
    private int _extraJumps;
    public GameObject carrotPrefab;
    public float shootSpeedX, shootSpeedY;
    private bool isShootig = false;
    public float shootDelay;
    public Transform gunPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _extraJumps = extraJumps;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = Input.GetAxisRaw("Horizontal");
        verticalSpeed = playerRb.velocity.y;
        playerRb.velocity = new Vector2(horizontalSpeed * speed, verticalSpeed);
        Jump();
        VerifyAndFlip();
        Shoot();
    }

    private void FixedUpdate() {
        onGround = Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
    }

    private void LateUpdate() {
        playerAnimator.SetInteger("horizontalSpeed", (int)horizontalSpeed);
        playerAnimator.SetFloat("verticalSpeed", verticalSpeed);
        playerAnimator.SetBool("onGround", onGround);
    }

    private void Jump()
    {
        doubleJump1(20);

        //doubleJump2(1000);
    }

    private void doubleJump1(int auxJumpForce)
    {
        bool canJump = onGround;

        if (Input.GetButtonDown("Jump"))
        {
            if (canJump)
            {
                doubleJump = true;
                playerRb.velocity = new Vector2(playerRb.velocity.x, auxJumpForce);
            }
            else if (doubleJump)
            {
                doubleJump = false;
                playerRb.velocity = new Vector2(playerRb.velocity.x, auxJumpForce - 4);
            }
        }
    }

    private void doubleJump2(int auxJumpForce)
    {

        if(onGround) _extraJumps = extraJumps;
        if (_extraJumps == 0 && Input.GetButtonDown("Jump") && !onGround)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 0f);
            playerRb.AddForce(new Vector2(0, auxJumpForce - 300));
            _extraJumps--;
        }

        if (_extraJumps > 0 && Input.GetButtonDown("Jump"))
        {
            playerRb.AddForce(new Vector2(0, auxJumpForce));
            _extraJumps--;
        }
    }

    private void VerifyAndFlip()
    {
        if (isFacingLeft && horizontalSpeed > 0)
        {
            Flip();
        }
        if (!isFacingLeft && horizontalSpeed < 0)
        {
            Flip();
        }
    }

    private void Flip(){
        isFacingLeft = !isFacingLeft;
        float x = transform.localScale.x * (-1);
        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        shootSpeedX *= -1;
    }

    private void Shoot(){
        if(!isShootig && Input.GetButton("Fire1") && _gameController.getAmmo() > 0){
            isShootig = true;
            _gameController.changeAmmo(-1);
            StartCoroutine(shootDelayCoroutine());
            GameObject temp = Instantiate(carrotPrefab);
            temp.transform.position = gunPosition.position;
            //temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootSpeedX, 0));
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeedX, 0);
            Destroy(temp, 1.5f);
        }

        if(!isShootig && Input.GetButton("Fire2") && _gameController.getAmmo() > 0){
            isShootig = true;
            _gameController.changeAmmo(-1);
            StartCoroutine(shootDelayCoroutine());
            GameObject temp = Instantiate(carrotPrefab);
            temp.transform.position = gunPosition.position;
            temp.GetComponent<Rigidbody2D>().gravityScale = 2;
            //temp.GetComponent<Rigidbody2D>().AddForce(new Vector2(shootSpeedX, shootSpeedY));
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeedX - 7, shootSpeedY);
        }
    }

    IEnumerator shootDelayCoroutine(){
        yield return new WaitForSeconds(shootDelay);
        isShootig = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag(TagsType.COLLECTABLE.ToString())){

            Collectable collectable = collider.gameObject.GetComponent<Collectable>();

            if(CollectableType.AMMO.ToString().Equals(collectable.collectableName)){
                _gameController.changeAmmo(collectable.quantity);
            }

            if(CollectableType.EGG.ToString().Equals(collectable.collectableName)){
                print("egg");
            }


            Destroy(collider.gameObject);
        }
    }

}
