using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Npcstuff : MonoBehaviour
{
    #region Variables
    [Header("Variables")]

    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] npcModels;

    [SerializeField] public List<GameObject> ingameNPCs;

    [SerializeField] private int[] numberGen;

    [Header("References")]
    [SerializeField] private GameObject Player;



        #endregion

    private void Awake()
    {
        

        bool duplicates = true;

        int iterations = 0;

        while (duplicates)
        {
            for (int i = 0; i < 10; i++)
            {
                numberGen[i] = Random.Range(0, spawnPoints.Length);

                
            }

            if (numberGen.Distinct().Count() == numberGen.Length)
            {
                duplicates = false;
            }

            iterations++;
            if (iterations > 100000)
            {
                print("were fucked");
                break;
            }
        }

        

        
    }

    void Start()
    {
        ingameNPCs = new List<GameObject>();

        for (int i = 0; i < numberGen.Length; i++)
        {
            var pos = spawnPoints[numberGen[i]];
            ingameNPCs.Add(Instantiate (npcModels[i], pos.transform.position, Quaternion.identity));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject npcs in ingameNPCs)
        {

            npcs.transform.LookAt(Player.transform.position);
        }
    }
}
