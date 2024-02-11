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

        public List<UpgradeData> upgrades;
        List<int> upgrade_order;

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

            List<int> nameListChecker = new List<int>();
            int randomNummer;

            upgrade_order = new List<int>();

            //3 is how many Taes are shown on the UI
            while (nameListChecker.Count != 3)
            {
                randomNummer = Random.Range(0, upgrades.Count);
                if (nameListChecker.Count != upgrades.Count - 1)
                    randomNummer = Random.Range(0, upgrades.Count);
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
