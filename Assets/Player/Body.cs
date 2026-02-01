using UnityEngine;

public class Body : MonoBehaviour
{
    Animator anim;
    Vector2 input;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input = input.normalized;

        anim.SetBool("Run", input.sqrMagnitude > 0);

        if (input.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (input.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
