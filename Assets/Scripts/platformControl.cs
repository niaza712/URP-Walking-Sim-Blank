using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformControl : MonoBehaviour
{

    public Vector3 myScale;
    public float fallTimer = .6f;
    float xScale;
    float timer = 1;

    // Start is called before the first frame update
    void Start()
    {
        //on start - getting the local scale and stashing it for later
        myScale = transform.localScale;
        xScale = myScale.x;
        zScale = myScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        //add time.deltaTime to timer to make scaling consistent regardless of framerate
        timer += Time.deltaTime;
        //calculate a % of local scale to set the actual X value to
        float xMod = Mathf.Sin(timer); //sine wave
        xMod = Mathf.Clamp(xMod, .2f, 1f); //clamp off the "0" values
        xMod = xScale * xMod; //getting the corresponding % of original scale
        xMod = Mathf.Abs(xMod); //getting absolute value, so no negatives
        //Debug.Log("xScale mod: " + xMod);

        //alternative method using remap float instead of clamp - consistent wave instead of clamped pause in wave like above
        float xMod2 = Mathf.Sin(Time.time);
        xMod2 = RemapFloat(xMod2, -1f, 1f, .2f, 1f);
        xMod2 *= xScale;
        //Debug.Log("alt xScale mod: " + xMod2);

        //plug it in!
        myScale = new Vector3(xMod2, myScale.y, myScale.z);
        transform.localScale = myScale;
    }

    void OnCollisionEnter(Collision collision)
    {
        //run code to make the platform fall
        Debug.Log("collided");
        StartCoroutine(startFall(fallTimer));
    }


    //simple function that remaps a value to a new range from original range
    public static float RemapFloat(float value, float from1, float to1, float from2, float to2)
    {
        float mapped = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        return mapped;
    }

    IEnumerator startFall(float time)
    {
        //code at the top
        //this code runs when you first call the method
        yield return new WaitForSeconds(time);
        //code at the bottom
        //this code runs when X time from the WaitForSeconds line has passed
        Rigidbody myRB = gameObject.GetComponent<Rigidbody>();
        if (myRB != null) { myRB.isKinematic = false; }
        StartCoroutine(killPlatform(3f));
    }

    IEnumerator killPlatform(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }




}
