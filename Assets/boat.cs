using UnityEngine;

public class boat : MonoBehaviour {

    public float turnSpeed = 1000f;
    public float accelerationSpeed = 1000f;

    private Rigidbody player;

    // Use this for initialization
    void Start() {
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        player.AddTorque(0f, h * turnSpeed * Time.deltaTime, 0f);
        player.AddForce(transform.forward * v * accelerationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Terrain") {
            Debug.Log("land");

        }

    }
}
