using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 swipe;

    private Touch touch;
    private Rigidbody rb;

    public float minDistance = 0.05f;
    public float playerSpeed = 2f;
    private float tempPlayerSpeed= 2f;

    private bool nextAction = true;
    private bool isGameOver = false;

    private void Start()
    {
        isGameOver = false;
        tempPlayerSpeed = playerSpeed;
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("ChangePlayerSpeed",10,20);
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPos = touch.position;
                nextAction = true;

            }
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                if (!nextAction)
                {
                    return;
                }
                endPos = touch.position;
                if (Vector3.Distance(startPos, endPos) >= minDistance)
                {
                    swipe = endPos - startPos;
                    float x = Mathf.Abs(swipe.x);
                    float y = Mathf.Abs(swipe.y);
                    if (x > y)
                    {
                        if (endPos.x > startPos.x)
                        {
                            Debug.Log("Right");
                            rb.position = new Vector3(rb.position.x + 0.5f, rb.position.y, rb.position.z);
                        }
                        else
                        {
                            Debug.Log("Left");
                            rb.position = new Vector3(rb.position.x - 0.5f, rb.position.y, rb.position.z);
                        }
                        nextAction = false;
                    }
                }
                startPos = touch.position;
            }
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void ChangePlayerSpeed()
    {
        tempPlayerSpeed = Random.Range(playerSpeed - 0.5f, playerSpeed + 1.5f);
    }

    void MovePlayer()
    {
        if (isGameOver)
            return;
        rb.position += Vector3.forward * Time.deltaTime * tempPlayerSpeed;
    }

    #region Collision Detection

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obsticles"))
        {
            isGameOver = true;
            UIManager.gameOver();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Track"))
        {
            TrackManager.instance.ShiftTracks();
        }
    }

    #endregion

    #region Trigger Detection
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            UIManager.gameOver();
            isGameOver = true;
        }
    }
    #endregion
}
