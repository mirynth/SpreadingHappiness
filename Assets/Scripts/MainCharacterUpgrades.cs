using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainCharacterUpgrades : MonoBehaviour
    {
        int boba;

        public List<Sprite> imageList;

        List<string> nameList = new List<string>
        {
        "Blueberry",
        "lemon",
        "acai berry",
        "Vitali"
        };

        List<string> descriptionList = new List<string>
        {
        "increase your movement speed",
        "increase your defense",
        "heals you",
        "increase max health"
        };

        public void BobaChanged(int BobaBits)
        {
            boba = BobaBits;

            if (BobaBits % 90 == 0)
            {
                Buying(4);
            }
            else if (BobaBits % 30 == 0)
            {
                Upgrades();
            }
        }

        public void Upgrades()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            transform.gameObject.SetActive(true);
            Time.timeScale = 0f;

            List<int> nameListChecker = new List<int>();
            int randomNummer;

            //3 is how many Taes are shown on the UI
            while (nameListChecker.Count != 3)
            {
                randomNummer = Random.Range(0, nameList.Count);
                if (nameListChecker.Count != nameList.Count - 1)
                    randomNummer = Random.Range(0, nameList.Count);
                else
                {
                    for (int i = 0; i < nameListChecker.Count; i++)
                    {
                        if (!nameListChecker.Contains(i))
                            randomNummer = i;
                    }
                }

                if (!nameListChecker.Contains(randomNummer))
                {
                    int TeaNumber = nameListChecker.Count + 1;
                    Transform transformOption = transform.Find("Tea" + TeaNumber);

                    transformOption.Find("Image").GetComponentInChildren<Image>().sprite = imageList[randomNummer];
                    transformOption.Find("Name").GetComponentInChildren<TMP_Text>().text = nameList[randomNummer];
                    transformOption.Find("Description").GetComponentInChildren<TMP_Text>().text = descriptionList[randomNummer];

                    nameListChecker.Add(randomNummer);
                }
            }
        }

        public void Buying(int option)
        {
            if (option < 4)
            {
                Transform transformOption = transform.Find("Tea" + option);

                string OptionText = transformOption.Find("Name").GetComponentInChildren<TMP_Text>().text;
                Debug.Log("Bought: " + OptionText);

                //getting upgrades
                switch (OptionText)
                {
                    case "Blueberry":
                        MainCharacterController.instance.hSpeed += 1;
                        MainCharacterController.instance.vSpeed += 1;
                        break;
                    case "lemon":
                        // code block
                        break;
                    case "acai berry":
                        // code block
                        break;
                    case "Vitali":
                        // code block
                        break;
                }


                boba -= 30;
            }
            else
            {
                //Get special Boba
                boba -= 90;
            }

            MainCharacterController.instance.BobaBits = boba;

            //Update Boba Text Counter
            UIEvents.OnPlayerBobaCountChanged(boba);

            Skip();
        }

        public void Skip()
        {
            transform.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
