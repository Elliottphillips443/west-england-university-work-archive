using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpritePicker : MonoBehaviour
{
    public SpriteRenderer s_rend;
    public List<Sprite> possible_sprites;

	// Use this for initialization
	void Start ()
    {
        s_rend.sprite = possible_sprites[Random.Range(0, possible_sprites.Count)];
	}
}
