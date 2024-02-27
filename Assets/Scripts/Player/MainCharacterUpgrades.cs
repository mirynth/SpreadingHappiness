<<<<<<< HEAD:Assets/Scripts/MainCharacterUpgrades.cs
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
        [SerializeField] private Selectable firstSelected;
        int boba;

        public List<UpgradeData> upgrades;
        List<int> upgrade_order;

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

        private void Awake()
        {
            upgrades = new List<UpgradeData>
            {
                new UpgradeData(Resources.Load<Sprite>("Art/Upgrades/blueberry"), "Blueberry", "Increase speed", (MainCharacterController controller) => 
                { controller.Upgrade_Speed(); }),
                new UpgradeData(Resources.Load<Sprite>("Art/Upgrades/lemon"), "Lemon", "Increases defence", (MainCharacterController controller) => 
                { controller.Upgrade_Defence();  }),
                new UpgradeData(Resources.Load<Sprite>("Art/Upgrades/burger"), "Burger", "Increases regeneration", (MainCharacterController controller) => 
                { controller.Upgrade_Regen();  }),
                new UpgradeData(Resources.Load<Sprite>("Art/Upgrades/elixir"), "Elixir", "Increase your maximum health", (MainCharacterController controller) => 
                { controller.Upgrade_MaxHealth(); })
            };
        }

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
            if (firstSelected != null)
            {
                firstSelected.Select();
            }
            List<int> nameListChecker = new List<int>();
            int randomNummer;

            upgrade_order = new List<int>();

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

                    transformOption.Find("Image").GetComponentInChildren<Image>().sprite = upgrades[randomNummer].sprite;
                    transformOption.Find("Name").GetComponentInChildren<TMP_Text>().text = upgrades[randomNummer].name + " (" + upgrades[randomNummer].UpgradeCount() + ")";
                    transformOption.Find("Description").GetComponentInChildren<TMP_Text>().text = upgrades[randomNummer].description;

                    nameListChecker.Add(randomNummer);
                    upgrade_order.Add(randomNummer);
                }
            }
        }

        public void Buying(int option)
        {
            if (option < 4)
            {
                /*
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
                */
                upgrades[upgrade_order[option - 1]].Upgrade(MainCharacterController.instance);

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