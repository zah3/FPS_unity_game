using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {

    public float amount;
    public float smoothness;
    public float max_amount;  //niepotrzebne w moim przypadku
    public Vector3 initial_position;

    void Start()
    {
        initial_position = transform.localPosition;
    }
    
    void Update () {
        float movmentX = -Input.GetAxis("Mouse X") * amount;
        float movmentY = -Input.GetAxis("Mouse Y") * amount;
        Vector3 final_positiion = new Vector3(movmentX, movmentY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final_positiion + initial_position, Time.deltaTime * smoothness);
        movmentX = Mathf.Clamp(movmentX, -max_amount, max_amount);
        movmentY = Mathf.Clamp(movmentY, -max_amount, max_amount);
    }
}
