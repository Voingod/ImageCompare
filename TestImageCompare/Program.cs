#define Release

using System;
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

                if (ImageNamesIn.Length < ImageNamesOut.Length)
                    Download(ImageNamesIn, ImageNamesOut, pathToDirctoryIn, pathToDirctoryOut);
                else
                    Download(ImageNamesOut, ImageNamesIn, pathToDirctoryOut, pathToDirctoryIn);

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
                    for (int j = Bmp1.Width; j < Bmp1.Height; j++)
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

        static void Download(FileInfo[] pathToDirctoryIn, FileInfo[] pathToDirctoryOut, string ImageNamesIn, string ImageNamesOut)
        {
            Bitmap Bmp2=null;
            for (int j = 0; j < pathToDirctoryIn.Length; j++)
            {
                bool flag = true;
                Bitmap Bmp1 = new Bitmap(ImageNamesIn + "\\" + pathToDirctoryIn[j], true);
                for (int i = 0; i < pathToDirctoryOut.Length; i++)
                {
                    Bmp2 = new Bitmap(ImageNamesOut + "\\" + pathToDirctoryOut[i], true);
                    if (Equality(Bmp1, Bmp2))
                    { 
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Bmp1.Dispose();
                    Console.WriteLine(ImageNamesIn + "\\" + pathToDirctoryIn[j]);
                    File.Move(ImageNamesIn + "\\" + pathToDirctoryIn[j], ImageNamesOut + "\\" + pathToDirctoryIn[j]);
                }
            }
        }

    }

}
