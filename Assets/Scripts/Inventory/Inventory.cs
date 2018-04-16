using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour {

	GameObject inventoryPanel;
	GameObject itemPanel;
	GameObject slotPanel;
	GameObject itemSlotPanel;
	ItemDatabase database;
	public GameObject inventorySlot;
	public GameObject inventoryItem;

	public int invSlotAmount;
	public int itemSlotAmount;
	public List<Item> items = new List<Item>();
	public List<GameObject> invSlots = new List<GameObject>();
	public List<GameObject> itemSlots = new List<GameObject>();

	private Inventory inv;
	private Tooltip tooltip;


	void Start()
	{
		inv = GameObject.Find ("Inventory").GetComponent<Inventory> ();
		tooltip = inv.GetComponent<Tooltip> ();

		database = GetComponent<ItemDatabase>();

		inventoryPanel = GameObject.Find("Inventory Panel");
		slotPanel = inventoryPanel.transform.Find ("Slot Panel").gameObject;
		for (int i = 0; i < invSlotAmount; i++) {
			items.Add (new Item ());
			invSlots.Add (Instantiate (inventorySlot));
			invSlots [i].GetComponent<InventorySlot> ().slotId = i;
			invSlots [i].transform.SetParent (slotPanel.transform, false);
		}

		itemPanel = GameObject.Find("Item Panel");
		itemSlotPanel = itemPanel.transform.Find ("Item Slot Panel").gameObject;
		for (int i = 0; i < itemSlotAmount; i++) {
			items.Add (new Item ());
			invSlots.Add (Instantiate (inventorySlot));
			invSlots [i + invSlotAmount].GetComponent<InventorySlot> ().slotId = i+invSlotAmount;
			invSlots [i + invSlotAmount].transform.SetParent (itemSlotPanel.transform, false);

		}


		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
		AddItem (4);
		AddItem (5);

		inventoryPanel.SetActive (false);
		itemPanel.SetActive (false);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.I)) {
			inventoryPanel.SetActive (!inventoryPanel.activeInHierarchy);
			itemPanel.SetActive (!itemPanel.activeInHierarchy);
			tooltip.Deactivate();
		}
	}

	public void AddItem(int id)
	{
		Item itemToAdd = database.FetchItemByID (id);

			for (int i = 0; i < items.Count; i++) {
				if (items [i].ID == -1) {
					items [i] = itemToAdd;
					GameObject itemObj = Instantiate (inventoryItem);
					itemObj.GetComponent<ItemData>().item = itemToAdd;
					itemObj.GetComponent<ItemData> ().amount = 1;
					itemObj.GetComponent<ItemData> ().slotNumber = i;
					itemObj.transform.SetParent (invSlots [i].transform, false);
					itemObj.transform.position = invSlots [i].transform.position;
					itemObj.GetComponent<Image> ().sprite = itemToAdd.Sprite;
					itemObj.name = itemToAdd.Title;
					break;
				}
			}
	}

	bool CheckIfItemIsInInventory(Item item)
	{
		for (int i = 0; i < items.Count; i++)
			if (items [i].ID == item.ID) {
				return true;
			}
		return false;
	}
}