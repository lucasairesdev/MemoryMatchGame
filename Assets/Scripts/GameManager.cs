using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] ImagesDatabaseSO imageDatabaseSO;
    [SerializeField] GameObject templateButton;

    [SerializeField] List<Button> allCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> discoveredCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> notDiscoveredCardsButtonsList = new List<Button>();
    [SerializeField] List<Button> selectedCardsButtonsList = new List<Button>();

    bool waitingPlayerChoices = false;

    private void Start() {
        SetupGame();
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

        if(notDiscoveredCardsButtonsList.Count <= 0) {
            //Quebra loop de jogo e finaliza jogo.
        }

        foreach(Button button in notDiscoveredCardsButtonsList) {
            button.interactable = true;
        }

        while(waitingPlayerChoices == true) {
            yield return new WaitForEndOfFrame();
        }

        foreach(Button button in allCardsButtonsList) {
            button.interactable = false;
        }

        if(selectedCardsButtonsList[0].transform.GetChild(0).GetComponent<Image>().sprite == selectedCardsButtonsList[1].transform.GetChild(1).GetComponent<Image>().sprite) {
            //+5 pontos
            discoveredCardsButtonsList.AddRange(selectedCardsButtonsList);
            foreach(Button button in discoveredCardsButtonsList) {
                button.interactable = false;
            }

        }
        else{
            foreach(Button button in selectedCardsButtonsList) {
                button.GetComponent<CardBehaviour>().HideCard();
            }
        }

    }
    //Verificar escolhas

    //EndGame()
}
