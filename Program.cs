using System;

namespace JogoDaVelha
{
    class Program
    {
        static char[,] board = {
            { ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ' }
        };

        static int jogador1Wins = 0;
        static int jogador2Wins = 0;
        static int draws = 0;

        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain)
            {
                int player = 1;
                bool gameOver = false;

                Console.Clear();
                DisplayWelcomeScreen();

                while (!gameOver)
                {
                    PrintBoard();
                    if (player == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Jogador 1 (X), escolha a linha e coluna (exemplo: 0 0):");
                        string input = Console.ReadLine();
                        if (input == "exit") break;

                        string[] parts = input.Split(' ');

                        if (parts.Length == 2 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int col))
                        {
                            if (row >= 0 && row < 3 && col >= 0 && col < 4 && board[row, col] == ' ')
                            {
                                board[row, col] = 'X';

                                if (CheckWin())
                                {
                                    PrintBoard();
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine("Jogador 1 venceu!");
                                    jogador1Wins++;
                                    gameOver = true;
                                }
                                else if (CheckDraw())
                                {
                                    PrintBoard();
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine("Empate!");
                                    draws++;
                                    gameOver = true;
                                }
                                else
                                {
                                    player = 2;
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Movimento inválido. Tente novamente.");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Entrada inválida. Tente novamente.");
                        }
                    }
                    else if (player == 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        JogadaIA();
                        if (CheckWin())
                        {
                            PrintBoard();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Jogador 2 (IA) venceu!");
                            jogador2Wins++;
                            gameOver = true;
                        }
                        else if (CheckDraw())
                        {
                            PrintBoard();
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("Empate!");
                            draws++;
                            gameOver = true;
                        }
                        else
                        {
                            player = 1;
                        }
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Estatísticas: Jogador 1: {jogador1Wins}, Jogador 2: {jogador2Wins}, Empates: {draws}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Deseja jogar novamente? (S para sim, N para não)");
                string resposta = Console.ReadLine().ToUpper();
                if (resposta != "S")
                {
                    playAgain = false;
                }

                ResetBoard();
            }
        }

        static void DisplayWelcomeScreen()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****************************************");
            Console.WriteLine("        BEM-VINDO AO JOGO DA VELHA!     ");
            Console.WriteLine("*****************************************");
            Console.WriteLine("Jogador 1 será 'X' e Jogador 2 será 'O'.");
            Console.WriteLine("Escolha as coordenadas para jogar, no formato: linha coluna (exemplo: 0 0).");
            Console.WriteLine("Digite 'exit' para sair do jogo.");
            Console.WriteLine("*****************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void PrintBoard()
        {
            Console.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (board[i, j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.Write(board[i, j]);
                    if (j < 3) Console.Write("|");
                }
                Console.WriteLine();
                if (i < 2) Console.WriteLine("------");
            }
        }

        static bool CheckWin()
        {
            // Verifica as linhas
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (board[i, j] == board[i, j + 1] && board[i, j + 1] == board[i, j + 2] && board[i, j] != ' ')
                        return true;
                }
            }

            // Verifica as colunas
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 1; i++)
                {
                    if (board[i, j] == board[i + 1, j] && board[i + 1, j] == board[i + 2, j] && board[i, j] != ' ')
                        return true;
                }
            }

            return false;
        }

        static bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (cell == ' ') return false;
            }
            return true;
        }

        static void JogadaIA()
        {
            Random rand = new Random();
            int row, col;

            do
            {
                row = rand.Next(0, 3);
                col = rand.Next(0, 4);
            } while (board[row, col] != ' ');

            board[row, col] = 'O';
        }

        static void ResetBoard()
        {
            board = new char[3, 4] {
                { ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ' },
                { ' ', ' ', ' ', ' ' }
            };
        }
    }
}
