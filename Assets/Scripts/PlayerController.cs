using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public GameObject PlayerParent;
    public GameObject capsuleText;
    public GameObject Bridge;
    public Animator BridgeRotation;
    public Animator playerAnim;
    public Text capsuleCounter;
    public Text countdownText;
    public Camera Camera;
    float xLimit;
    float yLimit;
    float zLimit;
    float moveSpeed;
    float capsuleCount = 4f;
    float countdown;
    float currentTime = 0f;
    float startingTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10.0f;
        xLimit = 5;
        zLimit = 5;
        currentTime = startingTime;
        Camera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (currentTime > 0)
        {
            countdownText.text = "Timer : " + currentTime.ToString("0");
        }
        else if (currentTime <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
        if (transform.position.y < -2)
        {
            SceneManager.LoadScene("Lose");
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            playerAnim.SetBool("runningState", true);
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetBool("runningState", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 270, 0);
            playerAnim.SetBool("runningState", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("runningState", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 180, 0);
            playerAnim.SetBool("runningState", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("runningState", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Euler(0, 90, 0);
            playerAnim.SetBool("runningState", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("runningState", false);
        }
        capsuleText.GetComponent<Text>().text = ("Capsules Left : " + capsuleCount);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
            Camera.transform.rotation = Quaternion.Euler(20, 0, 0);
            Camera.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z - 5);
		}
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Capsule")
        {
            Destroy(other.gameObject);
            capsuleCount -= 1;
        }
        if (other.gameObject.tag == "Cone" && capsuleCount == 0)
		{
            BridgeRotation.SetBool("Rotation", true);
		}
        if (other.gameObject.tag == "Crate" && capsuleCount == 0 && currentTime != 0)
		{
            SceneManager.LoadScene("Win");
		}
    }
}
