using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomController : MonoBehaviour
{
    [SerializeField]
    private GameObject H_Image, He_Image, Li_Image, Be_Image, B_Image, C_Image, N_Image, O_Image, F_Image, Ne_Image, Na_Image, Mg_Image, Fe_Image;

    [SerializeField]
    private Image atomImage;

    [SerializeField]
    private int atomOrder = 0;

    private List<string> atomDisplayingList = new List<string> {"H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Fe"};
    
    void Start()
    {
       atomImage = GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void ButtonDown()
    {
        atomOrder++;

        if (atomOrder >= atomDisplayingList.Count)
        {
            atomOrder = atomDisplayingList.Count - 1;
        }

        string currentAtom = atomDisplayingList[atomOrder];

        DisplayAtomImage(currentAtom);
        
        Debug.Log("trying reinfore");
    }

    private void DisplayAtomImage(string atom)
    {
        string atomName = atomDisplayingList[atomOrder];
        Sprite atomSprite = Resources.Load<Sprite>("Sprites/" + atomName);

        if (atomSprite != null)
        {
            atomImage.sprite = atomSprite;
        }
        else
        {
            Debug.LogWarning("Failed to load atom sprite: " + atomName);
        }
    }
    
}
