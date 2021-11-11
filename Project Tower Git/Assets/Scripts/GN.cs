using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GN : MonoBehaviour
{
    public GameObject lX;//линия Х
    public GameObject lY;//линия Y
    public Transform lZ;//точка берушийся за нулевой крдинату для генерации всего

    public Transform[] dot;//точки на Х ,точки на Y
    //расположить точки в таком поорядке
    // 0 Y1
    // 1 X1
    // 2 Y2
    // 3 X2
    // это нужно для логичного распределения размера плотформ создающую коробки 

    //просто удобная кнстанта для меня )))
    int n = 0;

    public GameObject pol;//пол
    public GameObject fon;//фон

    
    
    void Start()
    { 
        // размер линии или каробки
        int SizeX = 0;// по Х
        int SizeY = 0;// по У

        //генерация лакации 
        gen_size(ref SizeX, ref SizeY);
        gen_box( SizeX,  SizeY);
        //Invoke("test(ref SizeX, ref SizeY)", 2f);
        test(SizeX,SizeY);
        //gen_fon(ref SizeX, ref SizeY);
        //gen_Mpol(ref SizeX, ref SizeY);

    }

    public void gen_size(ref int SizeX, ref int SizeY)
    {
        //X
        SizeX = 4;//Random.Range(4, 10);
        SizeX *= 5;
        lX.transform.localScale = new Vector2(transform.localScale.x * SizeX, transform.localScale.y + n);
        if (SizeX%2!=0) 
        {
            SizeX -= 1;
        }
        //Y
        SizeY = 4; //Random.Range(4, 6);
        SizeY *= 5;
        if (SizeX % 2 != 0)
        {
            SizeX -= 1;
        }
        lY.transform.localScale = new Vector2(transform.localScale.x + n, transform.localScale.y * SizeY);
    }


    public void gen_box( int SizeX, int SizeY)
    {
        GameObject[] GPol = new GameObject[4];

        int a = 0;

        for (int i = 0; i < 4; i++)
        {
            GPol[i] = Instantiate(pol, dot[i].position, Quaternion.identity);

            if (i % 2 == 0)
            {
                GPol[i].transform.localScale = new Vector2(transform.localScale.x * SizeX, transform.localScale.y);
            }
            else
            {
                GPol[i].transform.localScale = new Vector2(transform.localScale.x * SizeY, transform.localScale.y);
            }
            GPol[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, a));
            a += -90;

        }

    }
    //1
    public void test(int SizeX, int SizeY)
    {
        //фигня №2 принцип "цепь маркова" точнее как у меня оно получилось и как я её понял
        //МАТИМАТИКА 
        //для удобства приравниваем к Х и У
        int y = SizeY;
        int x = SizeX;
        //делим пополам , уменьщаем на 1 и умнажем на -1 чтобы попасть в левый нижний угол в клетки 
        y /= 2;
        x /= 2;
        y -= 1;
        x -= 1;
        y *= -1;
        x *= -1;

        int a = 2/SizeX;
        int ti = pol_start(SizeX , SizeY);
        int ggg;
        for (int ix = x; ix < SizeY / 2; ix++) 
        { 

            var cell = Instantiate(fon, lZ);
            cell.transform.localPosition = new Vector3(ix, y, 0);
        }
        /*
        int y = SizeY;
        int x = 0;
        y /= 2;
        y -= 1;
        y *= -1;

        for (int iy = y; iy < SizeY / 2; iy++) 
        {
            var cell = Instantiate(fon, lZ);
            cell.transform.localPosition = new Vector3(x, y, 0);
        }



        //фигня №1 принцип "попытка сделать"цепь маркова" или дмк - долго мутерно кастыльно"
        /*
        //МАТИМАТИКА 
        //для удобства приравниваем к Х и У
        int y = SizeY;
        int x = SizeX;
        //делим пополам , уменьщаем на 1 и умнажем на -1 чтобы попасть в левый нижний угол в клетки 
        y /= 2;
        x /= 2;
        y -= 1;
        x -= 1;
        y *= -1;
        x *= -1;

        int c=0;

        for(int iy = y; iy < SizeY / 2; iy++) 
        {
            c++;
            for (int ix=x;ix<SizeX/2;ix++)
            {
                if (c%4==0)
                {
                    var cell = Instantiate(pol, lZ);
                    cell.transform.localPosition = new Vector3(ix, iy, 0);
                }
                else 
                {
                    var cell= Instantiate(fon, lZ);
                    cell.transform.localPosition = new Vector3(ix, iy, 0);
                }
                
            }
        }
        */

    }

    public int pol_start(int SizeX, int SizeY) 
    {
        int a = 1;
        while (a % SizeX != 0) 
        {
            a++;
        }
        a -= 1;
        return a;
    }

    void Update()
        {
        
        }
}
