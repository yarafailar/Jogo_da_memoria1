
using System;
using System.Numerics;

namespace Jogo_da_memoria1
{
    public class Program
    {
        public static void PrintMatrix(int[,] tela)
        {
            Console.WriteLine("   ");
            for (int j = 0; j < 4; j++)
            {
                Console.Write("   {0} ", j + 1);
            }
            Console.WriteLine("\n  -----------------");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("{0} |", i + 1);
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(" {0} |", tela[i, j]);
                }
                Console.WriteLine("\n  -----------------");
            }
            Console.WriteLine();
        }
        public static void Main(string[] args)
        {
            int[,] jogo = new int[4, 4];
            int[,] tela = new int[4, 4];
            Console.WriteLine("Entre com o nome do Player 1:");
            String nomeP1 = Console.ReadLine();
            Console.WriteLine("Entre com o nome do Player 2:");
            String nomeP2 = Console.ReadLine();

            Player p1 = new Player(nomeP1);
            Player p2 = new Player(nomeP2);

            //Para criar números aleatórios
            Random gerador = new Random();

            for (int i = 1; i <= 8; i++) //Atribui os pares de números às posições
            {
                //Escolhe a posição do primeiro número do par
                int lin, col;
                for (int j = 0; j < 2; j++)
                {
                    do
                    {
                        lin = gerador.Next(0, 4);
                        col = gerador.Next(0, 4);

                    } while (jogo[lin, col] != 0);
                    jogo[lin, col] = i;
                }
            }
            //Sorteio do jogador que começa 
            int jogador = gerador.Next(1, 3);
            int acertos = 0;
            bool continuarjogando = true;

            do
            {
                //impressão da matrix
                Program.PrintMatrix(tela);

                Console.WriteLine("{0} É A SUA VEZ!",
                jogador == 1 ? p1.Name : p2.Name);

                int lin1;
                int col1;

                do
                {
                    do
                    {
                        //Pedir as posições do primeiro número
                        Console.WriteLine("Escolha uma linha para jogar [1, 4]: ");
                        lin1 = int.Parse(Console.ReadLine());
                        lin1--;
                    } while (lin1 < 0 || lin1 >= 4);

                    do
                    {
                        Console.WriteLine("Escolha uma coluna para jogar [1, 4]: ");
                        col1 = int.Parse(Console.ReadLine());
                        col1--;
                    } while (col1 < 0 || col1 >= 4);
                } while (tela[lin1, col1] != 0);

                tela[lin1, col1] = jogo[lin1, col1];

                //impressão da matrix
                Program.PrintMatrix(tela);

                int col2, lin2;
                do
                {
                    do
                    {
                        //Pedir as posições do segundo número
                        Console.WriteLine("Escolha uma linha para jogar [1, 4]: ");
                        lin2 = int.Parse(Console.ReadLine());
                        lin2--;
                    } while (lin2 < 0 || lin2 >= 4);

                    do
                    {
                        Console.WriteLine("Escolha uma coluna para jogar [1, 4]: ");
                        col2 = int.Parse(Console.ReadLine());
                        col2--;
                    } while (col2 < 0 || col2 >= 4);
                } while (tela[lin2, col2] != 0);

                tela[lin2, col2] = jogo[lin2, col2];

                //impressão da matrix
                Program.PrintMatrix(tela);

                //Em caso de acerto, a matriz tela permanece como está.
                //Em caso de erro, precisamos voltar as posições para zero.
                if (jogo[lin1, col1] != jogo[lin2, col2])
                {
                    //Trocar o jogador
                    jogador = (jogador % 2) + 1;

                    tela[lin1, col1] = 0;
                    tela[lin2, col2] = 0;
                }
                else //caso tenha acertado o par
                {
                    if (jogador == 1)
                        p1.Score += 1;
                    else
                        p2.Score += 1;

                    acertos++;
                }
                int saida;

                if (jogo[lin1, col1] != jogo[lin2, col2])
                {
                    Console.WriteLine("Caso deseja sair, insira 0.Caso contrário, digite outro numero qualquer:\n");
                    saida = int.Parse(Console.ReadLine());

                    if (saida == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Você saiu!!");
                        continuarjogando = false;
                    }
                }


            } while (acertos < 8 && continuarjogando);
            if (acertos == 8)
            {
                Console.WriteLine(p1.ToString());

                Console.WriteLine();//quebra linha

                Console.WriteLine(p2.ToString());

                Console.WriteLine("PARABÉNS, VOCÊ COMPLETOU O JOGO!");
            }
        }
    }
}