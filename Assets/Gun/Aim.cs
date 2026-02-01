using UnityEngine;

public class Aim : MonoBehaviour
{
    Vector2 dir;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if (dir.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (angle > 90f || angle < -90f){
                transform.localScale = new Vector3(1, -1, 1);
            }
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
