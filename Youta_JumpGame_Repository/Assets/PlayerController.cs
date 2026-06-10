using UnityEngine;
using UnityEngine.SceneManagement; // リセット用

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; // スタート地点を記録
    }

    void Update()
    {
        // ① 移動
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        rb.AddForce(new Vector3(moveX, 0, moveZ) * speed);

        // ⑥ ジャンプ制限
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // ⑤ 落下したらリセット
        if (transform.position.y < -5f)
        {
            transform.position = startPosition;
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true; // 地面に触れたらジャンプ可能に
    }

    // ④ ゴール判定（Trigger用）
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Goal")
        {
            Debug.Log("クリア！");
        }
    }
}
