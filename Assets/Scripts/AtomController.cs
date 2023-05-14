using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtomController : MonoBehaviour
{
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

    [SerializeField]
    private Text[] inventorySlots;

    private List<string> atomDisplayingList = new List<string> {"None", "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Fe"};
    private List<string> inventory = new List<string>(); // 추가된 부분


    void Start()
    {
       atomImage = GetComponent<Image>();
       UpdateInventoryUI();
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

    private IEnumerator ShowFailText()
    {
        if (FailText != null)
        {
            FailText.text = "The atom was destroyed";
            FailText.gameObject.SetActive(true);
            Debug.Log("Atom Reset");

            yield return new WaitForSeconds(2f); // 실패 텍스트를 2초 동안 표시

            FailText.gameObject.SetActive(false);
        }
    }

    public void KeepButtonDown()
    {
        string currentAtom = atomDisplayingList[atomOrder];
        AddToInventory(currentAtom);
    }

    private void AddToInventory(string atom) // 추가된 함수
    {
        inventory.Add(atom);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI() // 추가된 함수
    {
        if (inventorySlots.Length != inventory.Count)
        {
            Debug.LogWarning("Inventory slot count does not match inventory size.");
            return;
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlots[i].text = inventory[i];
        }
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
            Debug.Log("Succes Rate:" + reinforceSuccessRate);
            reinforceSuccessRate = 80;

            StartCoroutine(ShowFailText());
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