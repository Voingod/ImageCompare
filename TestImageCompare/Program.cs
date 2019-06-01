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
                string path1 = @"D:\Test1";
                string path2 = @"D:\Test1\Test2";

                //string path1 = @"D:\ХВидео\Катя\";
                //string path2 = @"D:\ХВидео\Катя\Катя";

                var AllPath1 = new DirectoryInfo(path1).GetFiles();
                var AllPath2 = new DirectoryInfo(path2).GetFiles();

                if (AllPath1.Length < AllPath2.Length)
                {
                    Download(AllPath1, AllPath2, path1, path2);
                    Console.WriteLine("one");
                }
                else
                {
                    Download(AllPath2, AllPath1, path2, path1);
                    Console.WriteLine("two");
                }
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

        static void Download(FileInfo[] AllPath1, FileInfo[] AllPath2, string path1, string path2)
        {
#if !Release
            for (int i=0;i<AllPath1.Length; i++)
            {
                Console.Write(i);
                Bitmap Bmp1 = new Bitmap(path1 + "\\" + AllPath1[i], true);
                for (int j=0;j<AllPath2.Length;j++)
                {
                    Console.WriteLine(j);
                    Bitmap Bmp2 = new Bitmap(path2 + "\\" + AllPath2[j], true);
                    if (!Equality(Bmp1, Bmp2))
                    {
                        //  Bmp2.Dispose();
                        //  Bmp1.Dispose();
                        //Console.WriteLine(path2 + "\\" + AllPath2[j]);
                        break;
                       // File.Move(path2 + "\\" + AllPath2[j], path1 + "\\" + AllPath2[j]);
                    }
                    
                }
            }
#elif !Debug

            Bitmap Bmp2=null;
            for (int j = 0; j < AllPath1.Length; j++)
            {

                bool flag = true;
                Bitmap Bmp1 = new Bitmap(path1 + "\\" + AllPath1[j], true);
                for (int i = 0; i < AllPath2.Length; i++)
                {

                    Bmp2 = new Bitmap(path2 + "\\" + AllPath2[i], true);
                    if (Equality(Bmp1, Bmp2))
                    { 
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Bmp1.Dispose();
                    Console.WriteLine(path1 + "\\" + AllPath1[j]);
                    File.Move(path1 + "\\" + AllPath1[j], path2 + "\\" + AllPath1[j]);
                }
            }
 
#endif
        }

    }

}
