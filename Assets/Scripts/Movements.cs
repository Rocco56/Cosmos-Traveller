using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables
    
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 1000f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;
    
    
    
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else//if space is not pressed then stop the audio
        {
            StopRotation();
        }
          
    }

        void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
           StartLeftRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
           StartRightRotation();
        }
        else{
           StopParticleEffect();
        }
    }

    
    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);//Vector3.up means (0,1,0)
            if(!audioSource.isPlaying)//if it is false then play the audio
            {
                audioSource.PlayOneShot(mainEngine);
            }
            //Debug.Log("Pressed SPACE -- Thrusting upwards");
            if(!mainEngineParticle.isPlaying)
            {    
                mainEngineParticle.Play();
            }
    }

    void StopRotation()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }


    void StartLeftRotation()
    {
         ApplyRotation(rotationThrust);
            if(!rightThrustParticle.isPlaying)
            {    
                rightThrustParticle.Play();
            }
            //means that it will roatate in left direc i.e is(0,0,1)
            //Debug.Log("Rotating Left");
    }

    void StartRightRotation()
    {
        ApplyRotation(-rotationThrust);
           if(!leftThrustParticle.isPlaying)
            {    
                leftThrustParticle.Play();
            }
            //Debug.Log("Rotating Right");
    }

    void StopParticleEffect()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;// freezing rotation so we can manually rotate //still confusing to me cant get it
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;// unfreezing rotation so the physics system can take over
        /*basic explanation that i understood is that whe we freeze rotation no physical law affects rocket like when u hit obstacle it 
        wont get affected by physical gravity and will not force the rocket to rotate down down this happens till you are manually rotating and 
        when u stop manually rotating the physical gravity comes into place 
        
        */
        //down below is more nicer fix which is not neeeded for now
        //rb.constraints =
           // RigidbodyConstraints.FreezeRotationX | // freezing rotation on the X
            //RigidbodyConstraints.FreezeRotationY | // freezing rotation on the Y
            //RigidbodyConstraints.FreezePositionZ; // freezing position on the Z   
    }

}
