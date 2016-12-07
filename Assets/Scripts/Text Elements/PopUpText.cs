using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PopUpText : MonoBehaviour {


    void Start()
    {
        //set the parent to the ui group
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(.8f);
        Destroy(this.gameObject);
    }

}
