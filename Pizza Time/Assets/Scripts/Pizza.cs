using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    #region Variables
    [Header("Variables")]

    public static bool pizzaTaken;
    

    [Header("References")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject PizzaObj;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] AnimationCurve evilnessCurve;

    [SerializeField] private Material pizzaMat;
    

    [SerializeField] private PlayerController pc;

    [SerializeField] private Shake shake;

    #endregion
    void Start()
    {
        pizzaTaken = false;
        pizzaMat.SetFloat("_evilness", 0);

        PizzaObj = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player && pizzaTaken == false)
        {
            pizzaTaken = true;  
            Instantiate(ps, transform.position, Quaternion.identity);
            ps.transform.position = gameObject.transform.position;
            StartCoroutine(FadeEvilness());
            PizzaObj.SetActive(false);
            //gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    //public IEnumerator PizzaHell()
    //{
    //    pizzaTaken = true;
    //}

    public IEnumerator FadeEvilness()
    {
        pizzaMat.SetFloat("_evilness", 1);
        yield return new WaitForSeconds(0.3f);
        float evilness = 1;
        while (evilness > 0)
        {
            evilness -= Time.deltaTime;
            pizzaMat.SetFloat("_evilness", evilnessCurve.Evaluate(evilness));
            print(evilness);
            yield return null;
        }
        Destroy(gameObject);
    }
}
