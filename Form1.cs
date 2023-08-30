using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tarea_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string ConvertHaD(string CadHex)
        {
            int dec;
            string cadenaDec;
            dec = Convert.ToInt32(CadHex, 16);
            cadenaDec = dec.ToString();
            return cadenaDec;
        }

        private string ConvertDaH(string dec)
        {
            int decimalNumber;
            if (!int.TryParse(dec, out decimalNumber))
            {
                // Manejar el caso en el que la cadena no sea un número entero válido
                return "0";
            }

            if (decimalNumber < 1)
                return "0";

            int hex = decimalNumber;
            string hexStr = string.Empty;

            while (decimalNumber > 0)
            {
                hex = decimalNumber % 16;

                if (hex < 10)
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 48).ToString());
                else
                    hexStr = hexStr.Insert(0, Convert.ToChar(hex + 55).ToString());

                decimalNumber /= 16;
            }

            return hexStr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i,y,x;
            string aux="", numHex="", numDec="", Strs="";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Archivos de texto (*.txt)|*.txt";
            openFileDialog1.Title = "Seleccionar archivo de texto";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                StreamReader reader = new StreamReader(fileName);
                string line;
                //for(x=0;x<400;x++)
                while ((line = reader.ReadLine()) != null)
                {
                    //line = reader.ReadLine();
                    i = 0;
                    for (y = 0; y<8; y++)
                    {
                        while(line[i] != ':' && line[i] != '/')
                        {
                            aux += line[i];
                            i++;
                        }
                        i++;
                        numHex += ConvertHaD(aux) + ':';
                        aux = "";
                    }
                    for(y=0; y<2; y++)
                    {
                        while (line[i] != ',') i++;
                        i++;
                    }
                    while (line[i] != ',')
                    {
                        Strs += line[i];
                        i++;
                    }
                    for (y = 0; y < 3; y++)
                    {
                        while (line[i] != ',') i++;
                        i++;
                    }
                    for(y=0;y<4;y++)
                    {
                        while (line[i] != '.' && i!=line.Length-1)
                        {
                            aux += line[i];
                            i++;
                        }
                        i++;
                        numDec += ConvertDaH(aux) + ':';
                        aux = "";
                    }
                    richTextBox1.Text += Strs + ":" + numHex + numDec + "\n\n";
                    aux = numDec = Strs = numHex = "";
                }
                reader.Close();
            }
        }
    }
}
