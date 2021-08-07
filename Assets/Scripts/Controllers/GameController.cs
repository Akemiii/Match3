using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Candy FirstCandy;
    public Candy SecondCandy;

    public Candy candyPrefab;

    public bool IsMoving;
    public Candy[,] Grid;
    
    public int SizeX;
    public int SizeY;
    
    public GameObject selectedIcon;

    void Start()
    {
       Grid = new Candy[SizeX,SizeY];
        CreateGrid();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.K)){
            Debug.Log(Grid[0,0]);
        }
    }

    void CreateGrid(){
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                if(Grid[x,y] == null)
                    Grid[x,y] = CreateCandy(x,y); 
            }
        }
    }


    Candy CreateCandy(int x, int y){
        Candy candy = Instantiate(candyPrefab, new Vector2(x,y),  Quaternion.identity);
        candy.PosX = x;
        candy.PosY = y;
        return candy;
    }

    GameObject selIcon = null;
    public void SelectCandy(Candy c){

        if(IsMoving)
            return;

        if(FirstCandy == null){
            FirstCandy = c;
            selIcon = Instantiate(selectedIcon, new Vector2(c.PosX, c.PosY), Quaternion.identity);
        }else if(c == FirstCandy){
            FirstCandy = null;
            if(selIcon != null)
                Destroy(selIcon);
        }else{
            SecondCandy = c;
            if(selIcon != null)
                Destroy(selIcon);
        }
  
        if(SecondCandy != null)
        {
            if(CheckBoundary(FirstCandy, SecondCandy)){
                MoveCandy(FirstCandy, SecondCandy);
            }
            
            FirstCandy = null;
            SecondCandy = null;
        }
    }

    bool CheckBoundary(Candy c1, Candy c2){

        if(Mathf.Abs(c2.PosX - c1.PosX) == 1 && c2.PosY == c1.PosY){
            return true;
        }

        if(Mathf.Abs(c2.PosY - c1.PosY) == 1 && c2.PosX == c1.PosX){
            return true;
        }

        return false;
    }

    public List<Candy> CheckMatch(Candy c1, Candy c2){
        List<Candy> candyListMatch = new List<Candy>();


        //Verificação candy 1
        List<Candy> C1All = new List<Candy>();
        List<Candy> C1Horizontal = new List<Candy>(); 
        List<Candy> C1Vertical = new List<Candy>(); 

        C1All.Add(c1);

        //Verificação no eixo X para a direita
        for (int i = c1.PosX + 1; i < SizeX; i++)
        {
            Candy c = Grid[i,c1.PosY];
            
            if(c == null){
                break;
            }else{
                if(c1.candyType == c.candyType){

                    C1Horizontal.Add(Grid[i,c1.PosY]);
                }else{
                    break;
                }
            }
        }
        //Verificação no eixo X para a esquerda
        for (int i = c1.PosX - 1; i >= 0; i--)
        {
            Candy c = Grid[i,c1.PosY];
            if(c == null){
                break;
            }else{
                if(c1.candyType == c.candyType){
                    C1Horizontal.Add(Grid[i,c1.PosY]);
                }else{
                    break;
                }
            }
        }
        if(C1Horizontal.Count >= 2)
            C1All.AddRange(C1Horizontal);
        //Vertical para cima
        for (int i = c1.PosY+1; i < SizeY; i++)
        {
            Candy c = Grid[c1.PosX,i];
            if(c == null){
                break;
            }else{
                if(c1.candyType == c.candyType){
                    C1Vertical.Add(Grid[c1.PosX,i]);
                }else{
                    break;
                }
            }
        }
        //Vertical para baixo
        for (int i = c1.PosY-1; i >= 0; i--)
        {
            Candy c = Grid[c1.PosX,i];
            if(c == null){
                break;
            }else{
                if(c1.candyType == c.candyType){
                    C1Vertical.Add(Grid[c1.PosX,i]);
                }else{
                    break;
                }
            }
        }
        if(C1Vertical.Count >= 2)
            C1All.AddRange(C1Vertical);


       
        //Verificação candy 2
        List<Candy> C2All = new List<Candy>();
        List<Candy> C2Horizontal = new List<Candy>(); 
        List<Candy> C2Vertical = new List<Candy>(); 

        C2All.Add(c2);

        //Verificação no eixo X para a direita
        for (int i = c2.PosX + 1; i < SizeX; i++)
        {
            Candy c = Grid[i,c2.PosY];
            if(c == null){
                break;
            }else{
                if(c2.candyType == c.candyType){
                    C2Horizontal.Add(Grid[i,c2.PosY]);
                }else{
                    break;
                }
            }
        }
        //Verificação no eixo X para a esquerda
        for (int i = c2.PosX - 1; i >= 0; i--)
        {
            Candy c = Grid[i,c2.PosY];
            if(c == null){
                break;
            }else{
                
                if(c2.candyType == c.candyType){
                    C2Horizontal.Add(Grid[i,c2.PosY]);
                }else{
                    break;
                }
            }
        }
        if(C2Horizontal.Count >= 2)
            C2All.AddRange(C2Horizontal);
        //Vertical para cima
        for (int i = c2.PosY+1; i < SizeY; i++)
        {
            Candy c = Grid[c2.PosX,i];
            if(c == null){
                break;
            }else{
                          if(c2.candyType == c.candyType){
               C2Vertical.Add(Grid[c2.PosX,i]);
                }else{
                    break;
                }
            }
        }
        //Vertical para baixo
        for (int i = c2.PosY-1; i >= 0; i--)
        {
            Candy c =  Grid[c2.PosX,i];
            if(c == null){
                break;
            }else{
                    
                if(c2.candyType ==c.candyType){
                    C2Vertical.Add(Grid[c2.PosX,i]);
                }else{
                    break;
                }
            }
        }
        if(C2Vertical.Count >= 2)
            C2All.AddRange(C2Vertical);

        if(C2All.Count >= 3){
            candyListMatch.AddRange(C2All);
        }
 
        if(C1All.Count >= 3){
            candyListMatch.AddRange(C1All);
        }

        return candyListMatch;
    }

    public void MoveCandy(Candy c1, Candy c2){
        IsMoving = true;

        StartCoroutine(MoveTo(c1, c2, .5f));
    }

    public IEnumerator MoveTo(Candy c1, Candy c2, float t){
        
        if(c1 != null && c2 != null){
            StartCoroutine(c1.MoveTo(c2, t));
            StartCoroutine(c2.MoveTo(c1, t));
            Grid[c1.PosX, c1.PosY] = c2;
            Grid[c2.PosX, c2.PosY] = c1;
            yield return new WaitForSeconds(.55f);

            List<Candy> candys = CheckMatch(c1,c2);

            if(candys.Count >= 3){
                yield return new WaitForSeconds(.2f);

                RefillGrid(candys);

            }else{
                StartCoroutine(c1.MoveTo(c2, t));
                StartCoroutine(c2.MoveTo(c1, t));
                Grid[c1.PosX, c1.PosY] = c2;
                Grid[c2.PosX, c2.PosY] = c1;
            }

            IsMoving = false;

            yield return null;
        }


    }
    
    void GetColumn(List<Candy> candys){

        foreach (Candy c in candys)
        {
            CandyDown(c.PosX);
        }        
    }

    void ClearCandy(int x, int y){
        Candy c = Grid[x,y];

        if(c != null){
            Grid[x,y] = null;
            Destroy(c.gameObject);
        }

    }

    void ClearGrid(){ 
        for (int i = 0; i < SizeX; i++)
        {
            for (int j = 0; j < SizeY; j++)
            {
                ClearCandy(i,j);
            }
        }
    }

    void ClearCandy(List<Candy> c){
        foreach (Candy item in c)
        {
            ClearCandy(item.PosX, item.PosY);
        }
    }

    bool MatchOnFill(Candy c1){

        List<Candy> C1All = new List<Candy>();
        List<Candy> C1Horizontal = new List<Candy>(); 
        List<Candy> C1Vertical = new List<Candy>(); 

        C1All.Add(c1);

        //Verificação no eixo X para a direita
        for (int i = c1.PosX + 1; i < SizeX; i++)
        {
            if(c1.candyType == Grid[i,c1.PosY].candyType){

                C1Horizontal.Add(Grid[i,c1.PosY]);
            }else{
                break;
            }
        }
        //Verificação no eixo X para a esquerda
        for (int i = c1.PosX - 1; i >= 0; i--)
        {
            if(c1.candyType == Grid[i,c1.PosY].candyType){
            C1Horizontal.Add(Grid[i,c1.PosY]);
            }else{
                break;
            }
        }
        if(C1Horizontal.Count >= 2)
            C1All.AddRange(C1Horizontal);
        //Vertical para cima
        for (int i = c1.PosY+1; i < SizeY; i++)
        {
            if(c1.candyType == Grid[c1.PosX,i].candyType){
            C1Vertical.Add(Grid[c1.PosX,i]);
            }else{
                break;
            }
        }
        //Vertical para baixo
        for (int i = c1.PosY-1; i >= 0; i--)
        {
            if(c1.candyType == Grid[c1.PosX,i].candyType){
                C1Vertical.Add(Grid[c1.PosX,i]);
            }else{
                break;
            }
        }
        if(C1Vertical.Count >= 2)
            C1All.AddRange(C1Vertical);

        if(C1All.Count >= 3){
            return true;
        }else{
            return false;
        }
    }

    void FillGrid(){
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                Candy candy = CreateCandy(x,y);

                Candy c = Grid[x,y];

                while (MatchOnFill(c))
                {
                    ClearCandy(c.PosX, c.PosY);
                    candy = CreateCandy(x,y);
                }
            }
        }
    }

    void CandyDown(int x){
        for (int i = 0; i < SizeY - 1; i++)
        {
            if(Grid[x, i] == null){
                for (int j = i+1; j < SizeY; j++)
                {
                    if(Grid[x,j] != null){
                        StartCoroutine(Grid[x,j].MoveTo(x,i, .1f));
                        Grid[x,i] = Grid[x,j];

                        Grid[x,j] = null;
                        break;
                    }else{
                    }
                }
            }
        }
    }

    void RefillGrid(List<Candy> candys){
        StartCoroutine(RefillGridRoutine(candys));
    }

    IEnumerator RefillGridRoutine(List<Candy> candys){

        StartCoroutine(ClearAndRefillRoutine(candys));
        yield return null;
    }

    IEnumerator ClearAndRefillRoutine(List<Candy> candys){

        yield return new WaitForSeconds(0.25f);

        bool isFinished = false;
        
        while (!isFinished)
        {
            ClearCandy(candys);
        }

        yield return null;
    }




    //Refactor
    List<Candy> FindMatches(int startX, int startY, Vector2 dir, int minLength = 3){
        List<Candy> matches = new List<Candy>();

        return null;
    }







}
