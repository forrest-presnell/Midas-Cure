using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class King_Midas : MonoBehaviour
{
    [Header("Inscribed")]
    public float speed = 30;
    public GameObject leftArm;
    public GameObject rightArm;
    public Material goldMaterial;

    private bool glovedUp = false;
    private Rigidbody rb;
    private float xAxis;
    private float zAxis;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.G))
        {
            switchGlove();
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = new Vector3(xAxis * speed, rb.velocity.y, zAxis * speed);
        }
    }

    void switchGlove()
    {
        glovedUp = !glovedUp;
        ((Behaviour)leftArm.GetComponent("Halo")).enabled = glovedUp;
        ((Behaviour)rightArm.GetComponent("Halo")).enabled = glovedUp;
    }

    void OnTriggerStay(Collider other)
    {
        string[] ignoreNames = { "Floor", "Door_Entrance", "Door_Exit"};
        if (glovedUp && !ignoreNames.Contains(other.gameObject.name))
        {
            MeshRenderer otherRenderer = other.GetComponentInParent<MeshRenderer>();
            if (otherRenderer.sharedMaterial == goldMaterial) return;
            otherRenderer.material = goldMaterial;

            Rigidbody otherRb = other.GetComponentInParent<Rigidbody>();
            if (otherRb != null)
            {
                otherRb.mass = 1000000;
            }
        }
    }
}
