using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    #region Variables
    [Header("Variables")]

    public static bool pizzaTaken;
    

    [Header("References")]
    [SerializeField] private GameObject Player;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Material pizzaMat;

    #endregion
    void Start()
    {
        pizzaTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == Player)
        {
            pizzaTaken = true;
            Instantiate(ps);
            ps.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            StartCoroutine(PizzaHell());
        }
    }

    private IEnumerator PizzaHell()
    {
        pizzaMat.SetInt("_pizzaCollected", 1);
        pizzaMat.SetFloat("_evilness", 1);
        yield return new WaitForSeconds(0.5f);
        pizzaMat.SetInt("_pizzaCollected", 0);
        yield return new WaitForSeconds(0.1f);
        pizzaMat.SetInt("_pizzaCollected", 1);
        yield return new WaitForSeconds(0.05f);
        pizzaMat.SetInt("_pizzaCollected", 0);
        yield return new WaitForSeconds(0.05f);
        pizzaMat.SetInt("_pizzaCollected", 1);
        yield return new WaitForSeconds(0.01f);
        pizzaMat.SetInt("_pizzaCollected", 0);


    }

}
