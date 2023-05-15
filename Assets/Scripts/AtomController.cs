using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private Button Warrior;

    [SerializeField]
    private Image WarriorImage;

    private string savedAtom;

    private List<string> atomDisplayingList = new List<string> {"None", "H", "He", "Li", "Be", "B", "C", "N", "O", "F", "Ne", "Na", "Mg", "Fe", "Ending"};
    private List<string> inventory = new List<string>();


    void Start()
    {
       atomImage = GetComponent<Image>();
       WarriorImage = GetComponent<Image>();
    }

    void Update()
    {
       Ending();
    }

    public void Ending()
    {
        if (Warrior != null)
        {
            if (atomOrder == 15)
            {
                Sprite isis = Resources.Load<Sprite>("Sprites/Ending.png");
                Warrior.gameObject.SetActive(true);
                WarriorImage.sprite = isis;
            }

        else
            {
                Warrior.gameObject.SetActive(false);
            }
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
        StartCoroutine(LoadEndingScene());
        }
    }

    private IEnumerator LoadEndingScene()
    {
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene("EndingScene");
    }

    public void ButtonDown()
    {
        if (atomOrder >= atomDisplayingList.Count)
        {
            atomOrder = atomDisplayingList.Count - 1;
        }

        string currentAtom = atomDisplayingList[atomOrder];

        if (Input.GetKey(KeyCode.K))
        {
            DisplayRecentAtom(savedAtom);
        }

        UpgradeJudge(currentAtom);
    }

    private void UpgradeJudge(string atom)
    {
        
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

    private IEnumerator ShowFailText()
    {
        if (FailText != null)
        {
            FailText.text = "The atom was destroyed";
            FailText.gameObject.SetActive(true);

            yield return new WaitForSeconds(2f);

            FailText.gameObject.SetActive(false);
        }
    }

    public void KeepButtonDown()
    {
        string currentAtom = atomDisplayingList[atomOrder];
        AddToInventory(currentAtom);
        Debug.Log(inventory.Count);
    }

    private void AddToInventory(string atom)
    {
        inventory.Add(atom);
        savedAtom = atom;
    }

    private void DisplayRecentAtom(string newAtom)
    {
        if (!string.IsNullOrEmpty(savedAtom))
        {
            string recentAtom = savedAtom;
            Sprite recentAtomSprite = Resources.Load<Sprite>("Sprites/" + recentAtom);

            if (recentAtomSprite != null)
            {
                atomImage.sprite = recentAtomSprite;
            }
        }

        Debug.Log("K");
    }
}