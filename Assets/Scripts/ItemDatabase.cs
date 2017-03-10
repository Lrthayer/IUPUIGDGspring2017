﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System.Linq;

public class ItemDatabase : MonoBehaviour {
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	public Sprite[] iconArray;

	void Start()
	{
		//Gets code from Json File
		itemData = JsonMapper.ToObject (File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();


	}


	public Item FetchItemByID(int id)
	{
		for (int i = 0; i < database.Count; i++)
			if (database [i].ID == id)
				return database [i];
			return null;
	
	}

	void ConstructItemDatabase()
	{
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["stats"]["attack"],
				(int)itemData[i]["stats"]["defence"], (int)itemData[i]["stats"]["cooldown"], itemData[i]["description"].ToString(),
				(bool)itemData[i]["stackable"], (int)itemData[i]["rarity"], itemData[i]["slug"].ToString()));
		}
	}

}


public class Item
{
	public int ID { get; set;}
	public string Title { get; set;}
	public int Attack { get; set; }
	public int Defence { get; set; }
	public int Cooldown { get; set; }
	public string Description { get; set;}
	public bool Stackable { get; set; }
	public int Rarity { get; set; }
	public string Slug { get; set;}
	public Sprite Sprite { get; set;}


	public Item(int id, string title, int attack, int defence, int cooldown, string description, bool stackable, int rarity, string slug)
	{
		this.ID = id;
		this.Title = title;
		this.Attack = attack;
		this.Defence = defence;
		this.Cooldown = cooldown;
		this.Description = description;
		this.Stackable = stackable;
		this.Rarity = rarity;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug); 
	}

	public Item()
	{
		this.ID = -1;
	}

}