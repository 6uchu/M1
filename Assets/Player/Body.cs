using UnityEngine;

public class Body : MonoBehaviour
{
    Animator ani;
    Vector2 input;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        ani.SetBool("Run", input.sqrMagnitude > 0);

        if (input.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (input.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
