using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator anim;
    SpriteRenderer img;
    Movement player;
    GameManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        player = GetComponent<Movement>();
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.IsPaused)
        {
            UpdateAnimation();
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    void UpdateAnimation()
    {
        if(player.MoveDirection.x == 0f && player.MoveDirection.y == 0f)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }

        if (player.MoveDirection.x < 0f)
        {
            img.flipX = true;
            anim.SetBool("Moving", true);
        }
        else if(player.MoveDirection.x > 0f)
        {
            img.flipX = false;
            anim.SetBool("Moving", true);
        }

        if (player.knockedOrDash)
        {
            anim.SetBool("Dashing", true);
        }
        else
        {
            anim.SetBool("Dashing", false);
        }
    }
}
