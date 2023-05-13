using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomController : MonoBehaviour
{
    // [SerializeField]
    // private GameObject H_Image, He_Image, Li_Image, Be_Image, B_Image, C_Image, N_Image, O_Image, F_Image, Ne_Image, Na_Image, Mg_Image, Fe_Image;

    [SerializeField]
    private Image atomImage;

    [SerializeField]
    private int atomOrder = 0;

    [SerializeField]
    private int reinforceSuccessRate = 80;

    [SerializeField]
    private int decreaseRate = 7;

    [SerializeField]
    private Text FailText;

    private List<string> atomDisplayingList = new List<string> {"None", "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Fe"};

    void Start()
    {
       atomImage = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void ButtonDown()
    {

        if (atomOrder >= atomDisplayingList.Count)
        {
            atomOrder = atomDisplayingList.Count - 1;
        }

        string currentAtom = atomDisplayingList[atomOrder];

        UpgradeJudge(currentAtom);
        
    }

    private void UpgradeJudge(string atom)
    {
        Debug.Log("trying reinfore");

        string atomName = atomDisplayingList[atomOrder];
        Sprite atomSprite = Resources.Load<Sprite>("Sprites/" + atomName); 
        
        int randomValue = Random.Range(1, 79);

        if (randomValue > reinforceSuccessRate || reinforceSuccessRate <= 0)
         {
            atomOrder = 0;
            atomName = atomDisplayingList[0];
            Debug.Log("Atom is Broken!");
            Debug.Log("Succes Rate:" + reinforceSuccessRate);
            reinforceSuccessRate = 80;
         }
    
        if(randomValue <= reinforceSuccessRate)
        {

            if (atomSprite != null)
            {
                atomOrder++;
                atomImage.sprite = atomSprite;
            }

            reinforceSuccessRate -= decreaseRate;
            Debug.Log("Succes Rate:" + reinforceSuccessRate);
        }
    }
}