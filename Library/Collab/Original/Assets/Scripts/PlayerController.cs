using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float ForceMultiplier = 2500;
    public float JumpMultiplier = 3500;

    //Sounds
    public AudioClip deathSound;

    //Self-made scripts
    private DetectGround m_Feet;
    private SpriteRenderer m_renderer;
    private Animator m_Animator;
    private Rigidbody2D m_rb2D;
    //This bool will switch whenever the propel button is pressed
    private bool SpaceAlternator;
    private bool isStunned;
    private bool isGrounded;
    private float staminaFloat;
    private float timeSinceLastPress;
    private float maxSpeed;

    //Our music so far has been 120 BPM. We will use this to constantly shake camera
    private int music_BPM = 120;
    private float time_Tracker;


    //UI
    private Slider UI_Slider;

    //Ref for camera because it's easy
    public GameObject camRef;

    //sliding stuff
    private float slideDur;
    private bool isSliding;
    private float slideDist = 500; //basic multiplier to how far we slide

    //basic projectile we may want to use!
    public GameObject playerProj;
    private float projectileCooldown; //limit how often we can shoot

	// Use this for initialization
	void Start () {
        time_Tracker = 0;
        timeSinceLastPress = 0;
        m_Feet = gameObject.GetComponentInChildren<DetectGround>();
        UI_Slider = GameObject.FindGameObjectWithTag("UI_Slider").GetComponent<Slider>();
        m_Animator = gameObject.GetComponent<Animator>();
        m_rb2D = gameObject.GetComponent<Rigidbody2D>();
        SpaceAlternator = false;
        isStunned = false;
        isGrounded = true;
        staminaFloat = 60f;
        m_renderer = GetComponent<SpriteRenderer>();
        maxSpeed = 10f;
        isSliding = false;
        slideDur = -1f;
        projectileCooldown = 0;
    }
	
	// Update is called once per frame
	void Update () {
        maxSpeed = 10f;
        //If player is out of camera range on the left side, restart game
        if ((Camera.main.transform.position.x - m_renderer.transform.position.x) >= 0 && (Camera.main.transform.position.x - m_renderer.transform.position.x) >= 53)
        {
            if (!m_renderer.isPartOfStaticBatch)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                AudioSource audio = Camera.main.GetComponent<AudioSource>();
                audio.clip = deathSound;
                audio.Play();
                Invoke("resetLevel", 2f);
            }
        }

        //If player is out of camera range on the right side, restart game
        if ((Camera.main.transform.position.x - m_renderer.transform.position.x) <= 0 && (Camera.main.transform.position.x - m_renderer.transform.position.x) <= -53)
        {
            if (!m_renderer.isPartOfStaticBatch)
            {
                GetComponent<SpriteRenderer>().enabled = false;
                AudioSource audio = Camera.main.GetComponent<AudioSource>();
                audio.clip = deathSound;
                audio.Play();
                Invoke("resetLevel", 2f);
            }
        }

        isGrounded = m_Feet.Pull_OnGround();
        timeSinceLastPress += Time.deltaTime;
        
        time_Tracker += Time.deltaTime;
        if(time_Tracker >= (float)60/music_BPM){
            time_Tracker = 0;
            //camRef.GetComponent<ShakeCamera>().callShake(.1f, .5f);
        }

        //check sliding stuff
        if(isSliding)
        {
            slideDur -= Time.deltaTime;
            m_rb2D.AddForce(new Vector3(15, 0, 0) * slideDist);
            if (slideDur < 0)
            {
                isSliding = false;
                gameObject.transform.Rotate(Vector3.forward * -90);
                
            }
        }

        //TODO:
        //Camera should line character up on the screen according to staminaFloat's number
        //code goes here...

        //If eligible for propelling; can't propel if stamina is 0 or stunned
        /*if (Input.GetKeyDown(KeyCode.Space) && staminaFloat != 0 && !isStunned)
        {
            timeSinceLastPress = 0;
            SpaceAlternator = !SpaceAlternator;
            m_Animator.SetBool("Run_Bool", SpaceAlternator);
            //TODO:
            //Apply force to character
            //I'm thinking that we want a short burst of intense force
            //We should apply camera shake as well
            //code @ line 54 & 64

            //Sam's gonna try and apply screen shake
            //camRef.GetComponent<ShakeCamera>().callShake(.1f, .3f); //first param is how long, second is how hard

            //Decrease stamina
            if(staminaFloat >= 7)
            {
                staminaFloat -= 7;
                UI_Slider.value = (int)staminaFloat;
                m_rb2D.AddForce(transform.right * ForceMultiplier);

            }
            //If less than 7
            else
            {
                float staminaDiff = staminaFloat;
                staminaFloat = 0;
                //Apply percentage of force to character
                float percentForceToApply = staminaDiff / 7;
                m_rb2D.AddForce(transform.right * ForceMultiplier * percentForceToApply);
            }
        }
        */
        projectileCooldown -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            m_rb2D.AddForce(new Vector3(-50f, 0, 0));
            maxSpeed = 3f;
        }
        if(Input.GetKey(KeyCode.Space) && !isSliding && projectileCooldown <= 0) //shoot a projectile! cant do it while sliding
        {
            GameObject proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
            proj.GetComponent<ProjBehav>().setup(true, true, 1, .7f);
            projectileCooldown = .3f;
        }

        Debug.Log(m_rb2D.velocity.x);
        if (m_rb2D.velocity.x <= maxSpeed)
        {
            Vector2 vec = new Vector2(15f, 0);
            m_rb2D.velocity += vec;
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isStunned && !isSliding){
            timeSinceLastPress = 0;
            SpaceAlternator = !SpaceAlternator;
            m_Animator.SetBool("Run_Bool", SpaceAlternator);
            //camRef.GetComponent<ShakeCamera>().callShake(.1f, .3f); //first param is how long, second is how hard

            //Decrease stamina
            if (staminaFloat >= 7)
            {
                staminaFloat -= 7;
                UI_Slider.value = (int)staminaFloat;
                m_rb2D.AddForce(new Vector3(15f, 50f, 0) * JumpMultiplier);

            }
            //If less than 7
            else
            {
                float staminaDiff = staminaFloat;
                staminaFloat = 0;
                //Apply percentage of force to character
                float percentForceToApply = staminaDiff / 7;
                m_rb2D.AddForce(new Vector3(15f, 50f, 0) * JumpMultiplier * percentForceToApply);
            }

        }
        else if(Input.GetKeyDown(KeyCode.S) && isGrounded && !isStunned && !isSliding) //Else because we don't want to jump and slide at the same time
        {
            isSliding = true;
            if (staminaFloat >= 7)
            {
                staminaFloat -= 7;
                UI_Slider.value = (int)staminaFloat;
                slideDur = .7f;
                //m_rb2D.AddForce(new Vector3(15f, 0, 0) * JumpMultiplier);

            }
            //If less than 7
            else
            {
                float staminaDiff = staminaFloat;
                staminaFloat = 0;
                //Apply percentage of force to character
                slideDur = staminaDiff / 7;
                
                //m_rb2D.AddForce(new Vector3(15f, 50f, 0) * JumpMultiplier * percentForceToApply);
            }
            gameObject.transform.Rotate(Vector3.forward * 90);
        }

        if(Input.GetKeyDown(KeyCode.D) && !isStunned && !isSliding){
            timeSinceLastPress = 0;
            SpaceAlternator = !SpaceAlternator;
            m_Animator.SetBool("Run_Bool", SpaceAlternator);
            //camRef.GetComponent<ShakeCamera>().callShake(.1f, .3f); //first param is how long, second is how hard

            //Decrease stamina
            if (staminaFloat >= 7)
            {
                staminaFloat -= 7;
                UI_Slider.value = (int)staminaFloat;
                m_rb2D.AddForce(new Vector3(20f, 0, 0) * JumpMultiplier);

            }
            //If less than 7
            else
            {
                float staminaDiff = staminaFloat;
                staminaFloat = 0;
                //Apply percentage of force to character
                float percentForceToApply = staminaDiff / 7;
                m_rb2D.AddForce(new Vector3(20f, 0, 0) * JumpMultiplier * percentForceToApply);
            }

        }

        //stamina should increase 10/sec
        if (staminaFloat <= 100) {
            if (timeSinceLastPress <= 1f)
            {
                staminaFloat += Time.deltaTime * 10f;
                UI_Slider.value = (int)staminaFloat;
            }

            else
            {
                staminaFloat += Time.deltaTime * 10f * (2*timeSinceLastPress);
                UI_Slider.value = (int)staminaFloat;
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "noteGround")
        {
            AudioSource audio = Camera.main.GetComponent<AudioSource>();
            audio.clip = deathSound;
            audio.Play();
            Invoke("resetLevel", 2f);
        }
    }

    private void resetLevel()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Scene me;
        me = SceneManager.GetActiveScene();
        SceneManager.LoadScene(me.name);
    }
}
