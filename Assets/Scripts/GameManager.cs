using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    [SerializeField] ImagesDatabaseSO imageDatabaseSO;
    [SerializeField] GameObject templateButton;

    [SerializeField] List<Button> allCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> discoveredCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> notDiscoveredCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> selectedCardsButtonsList = new List<Button>();

    bool waitingPlayerChoices = false;

    private void Awake() {
        if (!instance) {
            instance = this;
        }

        SetupGame();
    }

    private void Start() {
        StartCoroutine(GameLoop());
    }

    public void CardButtonClick(Button button) {
        if(!waitingPlayerChoices) {
            return;
        }

        if(selectedCardsButtonsList.Count >= 2) {
            return;
        }

        selectedCardsButtonsList.Add(button);

        if(selectedCardsButtonsList.Count >= 2) {
            waitingPlayerChoices = false;
        }
    }

    

    private void SetupGame() {
        List<Sprite> spriteList = imageDatabaseSO.memoryGameImages.ToList<Sprite>();
        spriteList.AddRange(spriteList);
        List<Sprite> randomSpriteList = new List<Sprite>();

        while(spriteList.Count > 0) {
            Sprite sprite = spriteList[Random.Range(0, spriteList.Count)];
            spriteList.Remove(sprite);
            randomSpriteList.Add(sprite);
        }
        while(randomSpriteList.Count > 0) {
            GameObject buttonGameObject = Instantiate(templateButton);
            allCardsButtonsList.Add(buttonGameObject.GetComponent<Button>());
            notDiscoveredCardsButtonsList.Add(buttonGameObject.GetComponent<Button>());
            buttonGameObject.transform.SetParent(templateButton.transform.parent);
            buttonGameObject.transform.GetChild(0).GetComponent<Image>().sprite = randomSpriteList[0];
            randomSpriteList.RemoveAt(0);
            buttonGameObject.SetActive(true);
        }
    }

    IEnumerator GameLoop() {

        while(true) {
            if(notDiscoveredCardsButtonsList.Count <= 0) {
                //Quebra loop de jogo e finaliza jogo.
            }

            foreach(Button button in notDiscoveredCardsButtonsList) {
                button.interactable = true;
            }

            waitingPlayerChoices = true;
            while(waitingPlayerChoices == true) {
                yield return new WaitForEndOfFrame();
            }

            foreach(Button button in allCardsButtonsList) {
                button.interactable = false;
            }

            if(selectedCardsButtonsList[0].transform.GetChild(0).GetComponent<Image>().sprite == selectedCardsButtonsList[1].transform.GetChild(0).GetComponent<Image>().sprite) {
                //+5 pontos
                discoveredCardsButtonsList.AddRange(selectedCardsButtonsList);
            } else {
                yield return new WaitForSeconds(0.5f);
                foreach(Button button in selectedCardsButtonsList) {
                    button.GetComponent<CardBehaviour>().HideCard();
                }
            }
            selectedCardsButtonsList.Clear();
        }

    }

}
