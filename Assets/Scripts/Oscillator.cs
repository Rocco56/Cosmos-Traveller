using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillation : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0,1)] float movementFactor;
    [SerializeField] float period = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon){return;}//to protect our code if period value is zero(cannot divide by zero) it stops the movement of obstacle at a particular position
        float cycles = Time.time/period; //it is to measure time like Time.time give time elapsed and period is a value set by ourselves
        //for ex. 10sec has elapsed and we have set period as 2 that means total of 5 cyles (1cycle =1circle round) in 10sec and each cycle is of 2sec which is our period
        const float tau = Mathf.PI * 2;// constant value which 6.238 1 tau is a complete circle drawn (which is 2pie lol)
        float rawSineWave = Mathf.Sin(cycles * tau);// here we are calculating a value of sine wave which is b/w -1 to 1,this mathf.sin accepts values in radians and gives value b/w -1 to 1
        //hence thats why we multiply total cycle by tau which is radian value of 1 complete circle cycle*tau basically is repitition of sinewave or a round circle
        movementFactor = (rawSineWave +1f)/2f;//rawSinewave gives value b/w -1 and 1 but for movementfactor we need that value to from 0 to 1 
        // hence -1+1 1+1 = 0 /2 2/2= gives 0 to 1
        //here we have used sinewave because its value  repeats like they from -1 to 1 and again 1 to -1 and again repeat similarily to what we want in our motion to be osillative
        // this rawsinewave oscillates movementfactor value between 0 and 1.

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;

    }
}
