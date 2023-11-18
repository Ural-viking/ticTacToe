using System;
using System.Data;


namespace ticTacToe
{
    internal class Program
    {
        // игровое поле
        static char[,] board = new char[3, 3];

        // переменная очереди хода (1 или 2)
        static int numberPlayer = 1;

        static void Main(string[] args)
        {
            //делаем игровое поле
            addBoard();

            //запускаем игру
            playGame();
        }

        static void addBoard() {
            //пустые поля будут отмечены знаком -
            for (int line = 0; line < 3; line++) {
                for (int column = 0; column < 3; column++) {
                    board[line, column] = '-';
                }
            }
        }
        static void printBoard()
        {
            Console.WriteLine("  0 1 2");
            for (int line = 0; line < 3; line++)
            {
                Console.Write(line.ToString() + " ");
                for (int column = 0; column < 3; column++)
                {
                    Console.Write(board[line, column] + " ");
                }
                Console.WriteLine();
            }
        }
        static bool CheckWin(int player)
        {
            char symbol = player == 1 ? 'X' : 'O';
            // Проверяем горизонтали
            for (int line = 0; line < 3; line++)
            {
                if (board[line, 0] == symbol && board[line, 1] == symbol && board[line, 2] == symbol)
                {
                    return true;
                }
            }
            // Проверяем вертикали
            for (int column = 0; column < 3; column++)
            {
                if (board[0, column] == symbol && board[1, column] == symbol && board[2, column] == symbol)
                {
                    return true;
                }
            }
            // Проверяем диагонали
            if ((board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol) ||
                (board[2, 0] == symbol && board[1, 1] == symbol && board[0, 2] == symbol))
            {
                return true;
            }
            return false;
        }

        static bool IsBoardFull()
        {
            // Проверяем, остались ли на игровом поле незаполненные клетки
            for (int line = 0; line < 3; line++)
            {
                for (int column = 0; column < 3; column++)
                {
                    if (board[line, column] == '-')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static void playGame() {
            bool gameover = false;
            bool wrongMove = false;

            while (!gameover) {
                // Очищаем консоль перед каждым ходом
                Console.Clear();

                // Вывод поля на экран
                printBoard();

                // неправильный ход
                if (wrongMove) {
                    Console.WriteLine("Некорректный ход. Попробуйте еще раз.");
                    wrongMove = false;
                }

                // игрок вводит координаты
                Console.WriteLine("Игрок " + numberPlayer + ", ваш ход. Введите координаты через пробел: ");
                string[] input = Console.ReadLine().Split();

                if (input.Length == 2 && int.TryParse(input[0], out int line) && int.TryParse(input[1], out int column) && line >= 0 && line <= 2 && column >= 0 && column <= 2)
                {
                    // Проверяем, что выбранная клетка пуста
                    if (board[line, column] == '-')
                    {
                        board[line, column] = numberPlayer == 1 ? 'X' : 'O';
                    }
                    // Выбранная клетка уже занята
                    else {
                        wrongMove = true;
                    }

                    // Проверяем, не победил ли игрок
                    if (CheckWin(numberPlayer))
                    {
                        Console.Clear();
                        printBoard();
                        Console.WriteLine("Игрок " + numberPlayer + " победил!");
                        gameover = true;
                    }
                    else if (IsBoardFull())
                    {
                        // Проверяем, есть ли свободные клетки
                        Console.Clear();
                        printBoard();
                        Console.WriteLine("Ничья!");
                        gameover = true;
                    }
                    else
                    {
                        // Меняем игрока каждый ход
                        numberPlayer = numberPlayer == 1 ? 2 : 1;
                    }
                }
                else
                {
                    // Некорректный ввод координат
                    wrongMove = true;
                }
            }

            Console.WriteLine("Игра окончена. Нажмите любую клавишу, чтобы выйти.");
            Console.ReadKey();
        }

    }
}