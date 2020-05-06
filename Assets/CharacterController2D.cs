using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;// Amount of force added when the player jumps.
    [SerializeField] private float m_dashSpeed = 50f;// Amount of force added when the player dashes.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    //[SerializeField] private LayerMask m_WhatIsWall;                            // A mask determining what a wall is to the character
    [SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	//[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
    [SerializeField] private Transform m_RightCheck;                            // A position marking where to check if the player is hitting smth right.
    [SerializeField] private Transform m_LeftCheck;                             // A position marking where to check if the player is hitting smth left.

	const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
    const float k_LeftRadius = .05f;     // Radius of the overlap circle to determine if hit right
    const float k_RightRadius = .05f;    // Radius of the overlap circle to determine if hit right
    
	const float k_CeilingRadius = .05f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
    private bool doubleJump = false;
    private float timeStamp;
    [SerializeField] private bool m_IsGrounded;            // Whether or not the player is grounded.
    [SerializeField] private int canJump = 0;              // Count of remaining player jumps.
    [SerializeField] private bool wallJump = false;        
    
 
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
        m_IsGrounded = Physics2D.OverlapCircle(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround); //GroundCheck
        wallJump = Physics2D.OverlapCircle(m_RightCheck.position, k_RightRadius, m_WhatIsGround) //Right Check
                || Physics2D.OverlapCircle(m_LeftCheck.position, k_LeftRadius, m_WhatIsGround); //Left Check

        if (wallJump)
        {
            canJump = 1;
        }
        else if(m_IsGrounded && m_Rigidbody2D.velocity.y <= 0){
            canJump = 1;
        }

    }


    public void Move(float move)
    {
      
        //only control the player if grounded or airControl is turned on
        if (m_IsGrounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				//  flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				//  flip the player.
				Flip();
			}
		}
		

        
	}

    

    public void Jump()
    {
        if (canJump >= 1) {
            m_Rigidbody2D.velocity = Vector2.zero;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            canJump--;
        }
    }

    public bool Dash(bool dashCD) {

        if (Input.GetKeyDown(KeyCode.E)) {
            m_Rigidbody2D.AddForce(new Vector2(m_dashSpeed, 0f));
            timeStamp = Time.time + 1;
            dashCD = true;
            return dashCD;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
        m_Rigidbody2D.AddForce(new Vector2(-m_dashSpeed, 0f));
        dashCD = true;
            return dashCD;
        }

        return false;
    }

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
