using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform weaponTrans;
    public float speed = 4f;
    public Rigidbody2D rb;
    public Animator anim;
    public Camera cam;
    Vector2 movement;

    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
        if(mousePos.x > transform.position.x)
        {
            anim.SetBool("IsLookingRight", true);
            anim.SetBool("IsLookingLeft", false);
        }
        else
        {
            anim.SetBool("IsLookingLeft", true);
            anim.SetBool("IsLookingRight", false);
        }

        Vector3 aimDir = (mousePos - weaponTrans.position).normalized;
        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        angle += 180f;
        weaponTrans.eulerAngles = new Vector3(0, 0, angle);

        Vector2 scale = weaponTrans.localScale;
        if (mousePos.x > weaponTrans.position.x)
        {
            scale.y = -1;
        }
        else
        {
            scale.y = 1;
        }
        weaponTrans.localScale = scale;
    }

    private void FixedUpdate()
    {
        movement = movement.normalized;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
