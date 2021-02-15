using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public GameObject expl;
    public float speedModifier;

    Vector3 oldPos;
    Vector3 movement;

    Animator animator;
    Touch touch;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void Run()
    {
        animator.SetBool("Running", true);
        StartCoroutine(Accleration());
    }

    public IEnumerator Deceleration()
    {
        for (float accleration = 1; accleration >= 0; accleration -= 0.01f)
        {
            animator.SetFloat("PosX", accleration);
            animator.SetFloat("PosY", accleration);
            yield return new WaitForSeconds(0.01f);
        }
        animator.SetBool("Victory", true);
    }

    IEnumerator Accleration()
    {
        for (float accleration = 0; accleration <= 1; accleration += 0.01f)
        {
            animator.SetFloat("PosX", accleration);
            animator.SetFloat("PosY", accleration);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.gameIsActive)
            Movement();

    }

    void Movement()
    {
        if (Input.touchCount > 0)
        {

            oldPos = transform.position;
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {              
                float xMov = transform.position.x + touch.deltaPosition.x * speedModifier*Time.fixedDeltaTime;
                float zMov = transform.position.z + Mathf.Clamp(touch.deltaPosition.y * speedModifier*Time.fixedDeltaTime, 0, 15);
                movement = oldPos -new Vector3(xMov, transform.position.y,zMov);
            }
        }
        rb.velocity = -movement + Vector3.forward * 5f;
        movement = Vector3.zero;
    }

    IEnumerator explDisappear(float time)
    {
        yield return new WaitForSeconds(time-0.5f);
        expl.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Danger")
        {
            rb.velocity = Vector3.zero;
            expl.SetActive(true);
            GameManager.Instance.GameEnded();
            StartCoroutine(explDisappear(expl.GetComponent<ParticleSystem>().main.duration));
            animator.SetBool("Lose",true);
        }
    }
}
