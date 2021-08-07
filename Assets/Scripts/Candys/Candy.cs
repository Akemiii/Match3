using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public enum CandyType{
        Candy1,
        Candy2,
        Candy3,
        Candy4,
        Candy5,
        Candy6,
        Candy7,
        Candy8,
        Candy9
    }

    public GameController gc;

    public CandyType candyType;

    SpriteRenderer sr;

    public Sprite[] candyList;

    public int PosX;
    public int PosY;


    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
        gc = FindObjectOfType<GameController>();
        CreateCandyType();
    }



    public void CreateCandyType(){

        int typeNum = Random.Range(0,9);

        switch (typeNum)
        {
            case 0:
                candyType = CandyType.Candy1;
                sr.sprite = candyList[0];
            break;

            case 1:
                candyType = CandyType.Candy2;
                sr.sprite = candyList[1];
            break;

            case 2:
                candyType = CandyType.Candy3;
                sr.sprite = candyList[2];
            break;

            case 3:
                candyType = CandyType.Candy4;
                sr.sprite = candyList[3];
            break;

            case 4:
                candyType = CandyType.Candy5;
                sr.sprite = candyList[4];
            break;

            case 5:
                candyType = CandyType.Candy6;
                sr.sprite = candyList[5];
            break;

            case 6:
                candyType = CandyType.Candy7;
                sr.sprite = candyList[6];
            break;

            case 7:
                candyType = CandyType.Candy8;
                sr.sprite = candyList[7];
            break;

            case 8:
                candyType = CandyType.Candy9;
                sr.sprite = candyList[8];
            break;

            default:
                Debug.LogError("Candy Type dosent exist, Check (CreateCandyType) and verify random.range its ok or candyList its ok!");
            break;

        }

    }

    private void OnMouseDown() {
        gc.SelectCandy(this);
    }

    
    public IEnumerator MoveTo(Candy c, float t){

        float curTime = 0f;

        Vector2 currentPos = new Vector2(PosX,PosY);
        Vector2 GoToPosition = new Vector2(c.PosX, c.PosY);

        int posX = c.PosX;
        int posY = c.PosY;

         while (curTime < t)
        {
            transform.position = Vector3.Lerp(currentPos, GoToPosition, (curTime / t));
            curTime += Time.deltaTime;
        
            yield return null;
        }  

        transform.position = GoToPosition;
        PosX = posX;
        PosY = posY;

        yield return null;
    }

    public IEnumerator MoveTo(int x, int y, float t){

        float curTime = 0f;

        Vector2 currentPos = new Vector2(PosX,PosY);
        Vector2 GoToPosition = new Vector2(x, y);

        int posX = x;
        int posY = y;

         while (curTime < t)
        {
            transform.position = Vector3.Lerp(currentPos, GoToPosition, (curTime / t));
            curTime += Time.deltaTime;
        
            yield return null;
        }  

        transform.position = GoToPosition;
        PosX = posX;
        PosY = posY;

        yield return null;
    }


}
