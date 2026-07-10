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

        PizzaObj = transform.GetChild(0).gameObject;
        pizzaMat.SetInt("_pizzaCollected", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player && pizzaTaken == false)
        {
            StartCoroutine(PizzaHell());   
            Instantiate(ps);
            ps.transform.position = gameObject.transform.position;
            Destroy(PizzaObj);
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    public IEnumerator PizzaHell()
    {
        pizzaTaken = true;
        
        StartCoroutine(shake.Shaking());
        StartCoroutine(FadeEvilness());

        pizzaMat.SetInt("_flash", 1);
        yield return new WaitForSeconds(0.1f);


        pizzaMat.SetInt("_flash", 0);
        pizzaMat.SetInt("_pizzaCollected", 1);

        yield return new WaitForSeconds(3f);

        pizzaMat.SetInt("_pizzaCollected", 0);

        Destroy(gameObject);
    }

    public IEnumerator FadeEvilness()
    {
        pizzaMat.SetFloat("_evilness", 1);
        yield return new WaitForSeconds(1f);
        float evilness = 1;
        while (evilness > 0)
        {
            evilness -= Time.deltaTime / 3;
            pizzaMat.SetFloat("_evilness", evilnessCurve.Evaluate(evilness));
            print(evilness);
            yield return null;
        }
    }
}
