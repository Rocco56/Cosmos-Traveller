using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    

    bool isTransitioning = false;//used to stop extra audio that comes from crashing object more than one time 
    //here transitiong defines for eg if rocket crashes and the level is reloaded and when rocket lands on landing pad next level is loaded
    //basically trasitioning from one to other.
    bool collisionDisable = false;
 

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;//toggle collision
        }

    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable){ return; }
        
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start line");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                //Debug.Log("Obstacle!!");
                break;
        }
    }
    
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        //todo add SFX on crash
        //todo add Particle effect on crash
        GetComponent<Movements>().enabled = false;
        Invoke("ReloadLevel",levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        //todo add SFX on success
        //todo add Particle effect on success
        GetComponent<Movements>().enabled = false;
        Invoke("LoadNextLevel",levelLoadDelay);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)//SceneManager.sceneCount.....gives total no scenes
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }
}
