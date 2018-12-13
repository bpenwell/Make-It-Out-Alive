using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float ForceMultiplier = 2500;
    public float JumpMultiplier = 3500;
    public float maxSpeed = 10;
    public float moveSpeedMultiplier = 1f;
    public float staminaDrainMult; //Because goddamn i need more stamina!

    //UI
    public Text UI_deaths;

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
    private bool isAlive;
    private float staminaFloat;
    private float timeSinceLastPress;

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
    private float slideDist = 600; //basic multiplier to how far we slide

    //basic projectile we may want to use!
    public GameObject playerProj;
    private float projectileCooldown; //limit how often we can shoot

	// Use this for initialization
	void Start () {
        isAlive = true;
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
        isSliding = false;
        slideDur = -.5f;
        projectileCooldown = 0;
        staminaDrainMult = 1 / (maxSpeed / 10);
        moveSpeedMultiplier = 1; //for now
    }
	
	// Update is called once per frame
	void Update () {
        if (isAlive)
        {
            //If player is out of camera range on the left side, restart game
            if ((Camera.main.transform.position.x - m_renderer.transform.position.x) >= 0 && (Camera.main.transform.position.x - m_renderer.transform.position.x) >= 53)
            {
                if (!m_renderer.isPartOfStaticBatch && isAlive)
                {
                    Camera.main.GetComponent<MoveCamera>().isPlaying = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    AudioSource audio = Camera.main.GetComponent<AudioSource>();
                    audio.clip = deathSound;
                    audio.Play();
                    isAlive = false;
                    Invoke("resetLevel", 1f);
                }
            }

            //If player is out of camera range on the right side, restart game
            if ((Camera.main.transform.position.x - m_renderer.transform.position.x) <= 0 && (Camera.main.transform.position.x - m_renderer.transform.position.x) <= -53)
            {
                if (!m_renderer.isPartOfStaticBatch && isAlive)
                {
                    Camera.main.GetComponent<MoveCamera>().isPlaying = false;
                    GetComponent<SpriteRenderer>().enabled = false;
                    AudioSource audio = Camera.main.GetComponent<AudioSource>();
                    audio.clip = deathSound;
                    audio.Play();
                    isAlive = false;
                    Invoke("resetLevel", 1f);
                }
            }

            isGrounded = m_Feet.Pull_OnGround();
            timeSinceLastPress += Time.deltaTime;

            time_Tracker += Time.deltaTime;
            if (time_Tracker >= (float)60 / music_BPM)
            {
                time_Tracker = 0;
                //camRef.GetComponent<ShakeCamera>().callShake(.1f, .5f);
            }

            //check sliding stuff
            if (isSliding)
            {
                slideDur -= Time.deltaTime;
                m_rb2D.AddForce(new Vector3(15, 0, 0) * slideDist);
                staminaFloat -= 1 * Time.deltaTime;
                UI_Slider.value = (int)staminaFloat;
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
                m_rb2D.AddForce(new Vector3(-525f, 0, 0));
                maxSpeed = 1f;
            }
            if (Input.GetKey(KeyCode.Space) && !isSliding && projectileCooldown <= 0) //shoot a projectile! cant do it while sliding
            {
                GameObject proj = Instantiate(playerProj, gameObject.transform.position, Quaternion.identity);
                proj.GetComponent<ProjBehav>().setup(true, true, 1, .4f);
                projectileCooldown = .4f;
            }

            Debug.Log(m_rb2D.velocity.x);
            if (m_rb2D.velocity.x <= (maxSpeed * moveSpeedMultiplier))
            {
                Vector2 vec = new Vector2(15f, 0);
                m_rb2D.velocity += vec;
            }

            if (Input.GetKeyDown(KeyCode.W) && isGrounded && !isStunned && !isSliding)
            {
                timeSinceLastPress = 0;
                SpaceAlternator = !SpaceAlternator;
                m_Animator.SetBool("Run_Bool", SpaceAlternator);
                //camRef.GetComponent<ShakeCamera>().callShake(.1f, .3f); //first param is how long, second is how hard

                //Decrease stamina
                if (staminaFloat >= 7)
                {
                    staminaFloat -= 7 * staminaDrainMult;
                    UI_Slider.value = (int)staminaFloat;
                    m_rb2D.AddForce(new Vector3(15f, 50f, 0) * JumpMultiplier * moveSpeedMultiplier);

                }
                //If less than 7
                else
                {
                    float staminaDiff = staminaFloat;
                    staminaFloat = 0;
                    //Apply percentage of force to character
                    float percentForceToApply = staminaDiff / 7;
                    m_rb2D.AddForce(new Vector3(15f, 50f, 0) * JumpMultiplier * percentForceToApply * moveSpeedMultiplier);
                }

            }
            else if (Input.GetKeyDown(KeyCode.S) && isGrounded && !isStunned && !isSliding) //Else because we don't want to jump and slide at the same time
            {
                isSliding = true;
                if (staminaFloat >= 7)
                {
                    staminaFloat -= 7 * staminaDrainMult;
                    UI_Slider.value = (int)staminaFloat;
                    slideDur = .4f;
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

            if (Input.GetKeyDown(KeyCode.D) && !isSliding)
            {
                timeSinceLastPress = 0;
                SpaceAlternator = !SpaceAlternator;
                m_Animator.SetBool("Run_Bool", SpaceAlternator);
                //camRef.GetComponent<ShakeCamera>().callShake(.1f, .3f); //first param is how long, second is how hard

                //Decrease stamina
                if (staminaFloat >= 7)
                {
                    staminaFloat -= 7 * staminaDrainMult;
                    UI_Slider.value = (int)staminaFloat;
                    m_rb2D.AddForce(new Vector3(20f, 0, 0) * JumpMultiplier * moveSpeedMultiplier);

                }
                //If less than 7
                else
                {
                    float staminaDiff = staminaFloat;
                    staminaFloat = 0;
                    //Apply percentage of force to character
                    float percentForceToApply = staminaDiff / 7;
                    m_rb2D.AddForce(new Vector3(20f, 0, 0) * JumpMultiplier * percentForceToApply * moveSpeedMultiplier);
                }

            }

            //stamina should increase 10/sec
            if (staminaFloat <= 100 && !isSliding)
            {
                if (timeSinceLastPress <= 1f)
                {
                    staminaFloat += Time.deltaTime * 10f;
                    UI_Slider.value = (int)staminaFloat;
                }

                else
                {
                    staminaFloat += Time.deltaTime * 10f * (2 * timeSinceLastPress);
                    UI_Slider.value = (int)staminaFloat;
                }
            }
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "noteGround" && isAlive)
        {
            Camera.main.GetComponent<MoveCamera>().isPlaying = false;
            GetComponent<SpriteRenderer>().enabled = false;
            AudioSource audio = Camera.main.GetComponent<AudioSource>();
            audio.clip = deathSound;
            audio.Play();
            isAlive = false;
            Invoke("resetLevel", 1f);
        }
    }

    private void resetLevel()
    {
        Scene me;
        me = SceneManager.GetActiveScene();
        if (me.name == "Level1")
        {
            int temp = PlayerPrefs.GetInt("lvl1Deaths");
            temp++;
            PlayerPrefs.SetInt("lvl1Deaths", temp);
            UI_deaths.text = temp.ToString();

        }
        else if (me.name == "Level2")
        {
            int temp = PlayerPrefs.GetInt("lvl2Deaths");
            temp++;
            PlayerPrefs.SetInt("lvl2Deaths", temp);
            UI_deaths.text = temp.ToString();
        }
        SceneManager.LoadScene(me.name);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "proj")
        {
            if(!col.GetComponent<ProjBehav>().playerFriendly && isAlive) //If hit by enemy projectile
            {
                Camera.main.GetComponent<MoveCamera>().isPlaying = false;
                GetComponent<SpriteRenderer>().enabled = false;
                AudioSource audio = Camera.main.GetComponent<AudioSource>();
                audio.clip = deathSound;
                audio.Play();
                isAlive = false;
                Invoke("resetLevel", 1f);
            }
        }
    }
}
