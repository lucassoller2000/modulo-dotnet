using System;

namespace PiorJogoDoMundo.App
{
    class Program
    {
        static void Main(string[] args)
        {
            int linhas =10;
            int colunas =20;
            var matriz = new char[linhas, colunas] ;
            
            int x=2;
            int y=2;
           
            
            do
            {

                for(int i=0; i<linhas; i++){
                    for(int j=0; j<colunas; j++){
                        if(i==0){
                            matriz[i,j] ='x';
                        }
                        if(i==linhas-1){
                            matriz[i,j] ='x';
                        }
                        if(j==0){
                            matriz[i,j] ='x';
                        }
                        if(j==colunas-1){
                            matriz[i,j] ='x';
                        }
                        Console.Write(matriz[i,j]);
                    }
                    Console.WriteLine();
                }
                matriz.SetValue(' ', x, y);

                var teclaPressionada = Console.ReadKey(true).Key;
                
                if (teclaPressionada == ConsoleKey.UpArrow)  x -=1;
                if (teclaPressionada == ConsoleKey.DownArrow) x += 1;
                if (teclaPressionada == ConsoleKey.LeftArrow) y -=1;
                if (teclaPressionada == ConsoleKey.RightArrow) y +=1;
                
                x = Math.Clamp(y, 1, 18);
                y = Math.Clamp(x, 1, 8);
                matriz.SetValue('o', x, y);
                Console.SetCursorPosition(0, Console.CursorTop-10);

            } while (true);
        }
    }
}
