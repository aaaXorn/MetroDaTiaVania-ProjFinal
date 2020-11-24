using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class ItemsManager
{
    public int ID;
    public string Name;
    public string Description;
    public Sprite Icon;

}

[System.Serializable]
public class SlotsManager
{
    public int Number;
    public int ItemID;
    public Sprite Imagem;
    public GameObject Slot;
}

public class InventorySystem : MonoBehaviour
{

    public int ClickedSlotNumber;

    public ItemsManager[] Items;
    public SlotsManager[] Slots;

    public Sprite NullItem;

    public bool ClickedItem;
    public bool CreatedItem;
    public GameObject MoveObject;

    public void Start()
    {
        GameObject[] SSlots = new GameObject[Slots.Length];
        for (int x = 0; x < SSlots.Length; x++)
        {
            SSlots[0] = GameObject.Find("Image (0)");
            SSlots[1] = GameObject.Find("Image (1)");
            SSlots[2] = GameObject.Find("Image (2)");
            SSlots[3] = GameObject.Find("Image (3)");
            SSlots[4] = GameObject.Find("Image (4)");
            SSlots[5] = GameObject.Find("Image (5)");
            SSlots[6] = GameObject.Find("Image (6)");
            SSlots[7] = GameObject.Find("Image (7)");
            SSlots[8] = GameObject.Find("Image (8)");
            SSlots[9] = GameObject.Find("Image (9)");
            SSlots[10] = GameObject.Find("Image (10)");
            SSlots[11] = GameObject.Find("Image (11)");
            SSlots[12] = GameObject.Find("Image (12)");
            SSlots[13] = GameObject.Find("Image (13)");
            SSlots[14] = GameObject.Find("Image (14)");
            SSlots[15] = GameObject.Find("Image (15)");
            SSlots[16] = GameObject.Find("Image (16)");
            SSlots[17] = GameObject.Find("Image (17)");
            SSlots[18] = GameObject.Find("Image (18)");
            SSlots[19] = GameObject.Find("Image (19)");
            SSlots[20] = GameObject.Find("Image (20)");
            SSlots[21] = GameObject.Find("Image (21)");
            SSlots[22] = GameObject.Find("Image (22)");
            SSlots[23] = GameObject.Find("Image (23)");
            SSlots[24] = GameObject.Find("Image (24)");
            SSlots[25] = GameObject.Find("Image (25)");
            SSlots[26] = GameObject.Find("Image (26)");
            SSlots[27] = GameObject.Find("Image (27)");
        }



        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].Slot = SSlots[x];
        }

        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].Imagem = Items[Slots[x].ItemID].Icon;
            Slots[x].Slot.GetComponent<Image>().sprite = Slots[x].Imagem;
        }

    }

    public void Update()
    {
        for (int x = 0; x < Slots.Length; x++)
        {
            if (Slots[x].ItemID == 0)
            {
                Slots[x].Imagem = NullItem;
            }
            else
            {
                Slots[x].Imagem = Items[Slots[x].ItemID].Icon;
            }

            Slots[x].Slot.GetComponent<Image>().sprite = Slots[x].Imagem;
        }

        if (ClickedItem == true)
        {
            MoveObject.SetActive(true);
            MoveObject.GetComponent<Image>().sprite = Slots[ClickedSlotNumber].Imagem;
            MoveObject.transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void Clicked()
    {
        if (ClickedItem == false)
        {
            ClickedSlotNumber = int.Parse(this.name);
            if (Slots[ClickedSlotNumber].ItemID > 0)
            {
                ClickedItem = true;
                Slots[ClickedSlotNumber].Slot.gameObject.SetActive(false);
            }
        }
        else
        {
            Slots[ClickedSlotNumber].Slot.gameObject.SetActive(true);
            ClickedItem = false;
            int ItemID1 = Slots[ClickedSlotNumber].ItemID;
            Sprite Imagem1 = Slots[ClickedSlotNumber].Imagem;

            int ClickedSlotNumber2 = int.Parse(this.name);
            int ItemID2 = Slots[ClickedSlotNumber2].ItemID;
            Sprite Imagem2 = Slots[ClickedSlotNumber2].Imagem;

            Slots[ClickedSlotNumber].ItemID = ItemID2;
            Slots[ClickedSlotNumber].Imagem = Imagem2;

            Slots[ClickedSlotNumber2].ItemID = ItemID1;
            Slots[ClickedSlotNumber2].Imagem = Imagem1;

            CreatedItem = false;
            MoveObject.SetActive(false);
        }
    }

}