using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class learningCurve : MonoBehaviour
{
    public int currentAge = 17;
    public int addedAge = 5;
    // Start is called before the first frame update
    void Start()
    {
        computeAge();
        Debug.Log(30 + 1);

        Debug.Log(currentAge + 1);
    }
    /// <summary>
    /// Adds last known age plus years passed
    /// </summary>
    void computeAge()
    {
        Debug.Log(currentAge + addedAge);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
