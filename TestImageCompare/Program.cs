#define Release

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TestImageCompare
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                Console.Write("Enter the first path to directory with images: ");
                string pathToDirctoryIn = Console.ReadLine();
                Console.Write("Enter the second path to directory with images: ");
                string pathToDirctoryOut = Console.ReadLine();

                Console.WriteLine("\nProgram is working\n");

                

                var ImageNamesIn = new DirectoryInfo(pathToDirctoryIn).GetFiles();
                var ImageNamesOut = new DirectoryInfo(pathToDirctoryOut).GetFiles();

                string[] ImageIn = new string[ImageNamesIn.Length];
                string[] ImageOut = new string[ImageNamesOut.Length];

                for (int i = 0; i < ImageIn.Length; i++)
                    ImageIn[i] = ImageNamesIn[i].ToString();
                for (int i = 0; i < ImageOut.Length; i++)
                    ImageOut[i] = ImageNamesOut[i].ToString();                


                if (ImageIn.Length < ImageOut.Length)
                    Download(ImageIn, ImageOut, pathToDirctoryIn, pathToDirctoryOut);
                else
                    Download(ImageOut, ImageIn, pathToDirctoryOut, pathToDirctoryIn);

                Console.WriteLine();
                Console.WriteLine("Finish");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
               
            }
            Console.ReadLine();
        }

        static bool Equality(Bitmap Bmp1, Bitmap Bmp2)
        {
            if (Bmp1.Size == Bmp2.Size)
            {
                for (int i = 0; i < Bmp1.Width; i++)
                {
                    for (int j = 0; j < Bmp1.Height; j++)
                    {
                        if (Bmp1.GetPixel(i, j) != Bmp2.GetPixel(i, j))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }

            else
            {
                return false;
            }
        }

        static void Download(string[] pathToDirctoryIn, string[] pathToDirctoryOut, string ImageNamesIn, string ImageNamesOut)
        {
            Bitmap Bmp2=null;
            for (int j = 0; j < pathToDirctoryIn.Length; j++)
            {
                Console.Write(ImageNamesIn + "\\" + pathToDirctoryIn[j]);
                bool flag = false;
                
                Bitmap Bmp1 = new Bitmap(ImageNamesIn + "\\" + pathToDirctoryIn[j], true);
                for (int i = 0; i < pathToDirctoryOut.Length; i++)
                {
                    Console.WriteLine(" "+ImageNamesOut + "\\" + pathToDirctoryOut[i]);
                    Bmp2 = new Bitmap(ImageNamesOut + "\\" + pathToDirctoryOut[i], true);
                    if (Equality(Bmp1, Bmp2))
                    {
                        
                        Console.WriteLine(" "+true, Console.ForegroundColor = ConsoleColor.Green);
                        Console.ResetColor();
                        flag = true;
                        //continue;
                          break;
                    }
                    else
                    {
                        Console.WriteLine(" " + false, Console.ForegroundColor = ConsoleColor.Red);
                        Console.ResetColor();
                    }
                       
                }
                Console.WriteLine(flag);
                Console.WriteLine();
                if (!flag)
                {
                    string data = DateTime.Now.Millisecond.ToString();
                    Bmp1.Dispose();
                    File.Move(ImageNamesIn + "\\" + pathToDirctoryIn[j], ImageNamesOut + "\\" + data + pathToDirctoryIn[j]);
                    Console.WriteLine("Moving: "+ImageNamesIn + "\\" + pathToDirctoryIn[j], Console.ForegroundColor = ConsoleColor.Blue);
                    Console.ResetColor();
                }
            }
        }

    }

}
