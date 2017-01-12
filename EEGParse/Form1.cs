using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEGParse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void открытьСостоянияCRVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog()==DialogResult.OK)
            {



    





                foreach (var item in openFileDialog1.FileNames)
                {

                    StreamReader sr = new StreamReader(item);

                    for (int i = 0; i < 5; i++)
                    {
                        sr.ReadLine(); //пять не нужных строк служебной информации
                    }


                    FileInfo myfileinfo = new FileInfo(item);





                  //  MessageBox.Show(myfileinfo.Name);

                    string Dir1 = myfileinfo.Directory + "/состояние 1/";
                    string Dir2= myfileinfo.Directory + "/состояние 2/";

                    Directory.CreateDirectory(Dir1);
                    Directory.CreateDirectory(Dir2);

                    
                   StreamWriter sw_otv1 = new StreamWriter( Dir1 + myfileinfo.Name +"_1_obr.csv");

                   StreamWriter sw_otv2 = new StreamWriter(Dir2 + myfileinfo.Name + "_2_obr.csv");

                    List<double> otv1 = new List<double>();
                    List<double> otv2 = new List<double>();


                    string line = "123";
                    while (line!=null)
                    {

                        line = sr.ReadLine();
                        if (line!=null)
                        {

                            otv1.Add(double.Parse(line.Split('\t')[0]));
                            otv2.Add(double.Parse(line.Split('\t')[1]));
                        }
                        
                    }




                    //так массивы сформированы.... отступаем 500 отчетов слева и ищем гломальный максимум у следующих 1000 отчетов

                    int maxIndex = 500;
                    for (int i = 500; i < 1500; i++)
                    {
                        if (otv1[i]>otv1[maxIndex])
                        {
                            maxIndex = i;
                        }
                    }

                    //нашли индекс у максимального элемента


                //теперь нужно тупо обрезать 500 элементов до и 500 элементов после

                    otv1.RemoveRange(0, maxIndex - 500);

                    maxIndex = 500;


                    //if ((maxIndex + 500)>(otv1.Count - 1))
                    //{
                    //    MessageBox.Show("Не могу правильно удалить, так как сигнал слишком короткий");
                    //} else 
                        
                        
                        
                        otv1.RemoveRange(maxIndex + 500, otv1.Count - 1000);



                    maxIndex = 500;
                    for (int i = 500; i < 1500; i++)
                    {
                        if (otv2[i] > otv2[maxIndex])
                        {
                            maxIndex = i;
                        }
                    }




                    otv2.RemoveRange(0, maxIndex - 500);


                    maxIndex = 500;

                    //if ((maxIndex + 500) > (otv2.Count - 1))
                    //{
                    //    MessageBox.Show("Не могу правильно удалить, так как сигнал слишком короткий");
                    //}
                    //else
                        
                        otv2.RemoveRange(maxIndex + 500, otv2.Count - 1000);






                    foreach (var item1 in otv1)
                    {
                        sw_otv1.WriteLine(item1);
                    }

                    foreach (var item2 in otv2)
                    {
                        sw_otv2.WriteLine(item2);
                    }



                  sw_otv1.Close();
                  sw_otv2.Close();
                    sr.Close();
                }




            }



        }
    }
}
