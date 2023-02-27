using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marble : MonoBehaviour
{
    private const float YLowerBound = -1.5f;

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
        //TODO: Add a compare tag and if it hit a barrier then make the direction of the force be towards the center with minor variation

        AddRandomForce();
    }

    public void AddRandomForce()
    {
        //Get random direction
        var direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));

        //Get random speed
        var speed = Random.Range(150f, 500f);

        //Add force to move cell
        rb.AddForce(direction.normalized * speed);
    }
}
