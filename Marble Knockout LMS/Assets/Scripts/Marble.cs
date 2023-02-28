using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private const float YLowerBound = -1.5f;

    private const float centerRotationOffset = 35f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddRandomForce();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < YLowerBound)
        {
            GameManager.Instance.RemoveMarble(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            AddForceTowardsCenter();
            return;
        }

        AddRandomForce();
    }

    public void AddRandomForce()
    {
        //Get random direction
        var direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

        //Get random speed
        var speed = Random.Range(250f, 700f);

        //Add force to move cell
        rb.AddForce(direction.normalized * speed);
    }

    public void AddForceTowardsCenter()
    {
        //Get random direction
        var direction = Vector3.zero - new Vector3(transform.position.x, 0, transform.position.z);

        //Get random speed
        var speed = Random.Range(250f, 700f);

        //Add force to move cell
        rb.AddForce(direction.normalized * speed);
    }
}
