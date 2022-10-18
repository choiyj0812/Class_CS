using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace Class_CS
{
    class Program
    {
        static byte[,] background = new byte[22, 12]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
        };

        static byte[,,] block_L = new byte[4, 4, 4]
        {
            {
                {0, 0, 0, 0},
                {0, 1, 0, 0},
                {0, 1, 1, 1},
                {0, 0, 0, 0}
            },
            {
                {0, 0, 0, 0},
                {0, 1, 1, 0},
                {0, 1, 0, 0},
                {0, 1, 0, 0}
            },
            {
                {0, 0, 0, 0},
                {1, 1, 1, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 0}
            },
            {
                {0, 0, 1, 0},
                {0, 0, 1, 0},
                {0, 1, 1, 0},
                {0, 0, 0, 0}
            }
        };

        static int x = 3, y = 3;
        static int count = 0;
        static int rotate = 0;
        static void Main(string[] args)
        {
            ConsoleKeyInfo key_value;
            String ch;

            Console.Clear();
            Draw_Background();
            Draw_Background_value();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key_value = Console.ReadKey(true);
                    ch = key_value.Key.ToString();

                    if (ch == "A")
                    {
                        if (overlap_check(-1, 0) == 0)
                        {
                            Delete_Block();
                            x--;
                            Make_Block();
                        }
                    }
                    else if (ch == "S")
                    {
                        if (overlap_check(0, 1) == 0)
                        {
                            Delete_Block();
                            y++;
                            Make_Block();
                        }
                    }
                    else if (ch == "D")
                    {
                        if (overlap_check(1, 0) == 0)
                        {
                            Delete_Block();
                            x++;
                            Make_Block();
                        }
                    }
                    else if (ch == "R")
                    {
                        if (overlap_check_rotate() == 0) {
                            Delete_Block();
                            rotate++;
                            if (rotate > 3) rotate = 0;
                            Make_Block();
                        }
                    }
                }
                ////////////////////////////////////////////////
                if(count == 100)
                {
                    count = 0;

                    if (overlap_check(0, 1) == 0)
                    {
                        Delete_Block();
                        y++;
                        Make_Block();
                    }
                    else
                    {
                        Insert_Block();
                        Draw_Background_value();
                        for(int i = 1; i < 21; i++)
                        {
                            Line_Check(i);
                        }
                        
                        x = y = 3;
                        rotate = 0;
                    }
                }
                ////////////////////////////////////////////////
                count++;
                Thread.Sleep(10);
            }
        }

        static void Draw_Background()
        {
            for (int j = 0; j < 22; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (background[j, i] == 1)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("*");
                    }
                    else
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("-");
                    }
                }
            }
        }

        static void Draw_Background_value()
        {
            for (int j = 0; j < 22; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (background[j, i] == 1)
                    {
                        Console.SetCursorPosition(i + 15, j);
                        Console.Write("1");
                    }
                    else
                    {
                        Console.SetCursorPosition(i + 15, j);
                        Console.Write("0");
                    }
                }
            }
        }

        static void Make_Block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        Console.SetCursorPosition(x + i, y + j);
                        Console.Write("*");
                    }
                }
            }
        }

        static void Delete_Block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        Console.SetCursorPosition(x + i, y + j);
                        Console.Write("-");
                    }
                }
            }
        }

        static int overlap_check(int offset_x, int offset_y)
        {
            int overlap_count = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1 && background[j + y + offset_y, i + x + offset_x] == 1)
                    {
                        overlap_count++;
                    }
                }
            }
            return overlap_count;
        }

        static int overlap_check_rotate()
        {
            int overlap_count = 0;
            int tmp_rotate = rotate;

            tmp_rotate++;
            if (tmp_rotate == 4) tmp_rotate = 0;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[tmp_rotate, j, i] == 1 && background[j + y, i + x] == 1)
                    {
                        overlap_count++;
                    }
                }
            }
            return overlap_count;
        }

        static void Insert_Block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        background[j + y, i + x] = 1;
                    }
                }
            }
        }

        static void Line_Check(int line_num)
        {
            int block_num = 0;
            for(int i=0;i<10; i++)
            {
                if(background[line_num, i + 1] == 1)
                {
                    block_num++;
                }
            }

            if (block_num == 10)
            {
                for (int j = line_num; j > 1; j--) {
                    for (int i = 0; i < 10; i++)
                    {
                        background[j, i + 1] = background[j - 1, i + 1];
                    }
                }

                Draw_Background();
                Draw_Background_value();
            }
        }
    }
}