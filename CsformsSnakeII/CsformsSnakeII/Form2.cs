using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsformsSnakeII
{
    public partial class Form2 : Form
    {
     
        public Form2()
        {
            InitializeComponent();
        }
        //******************************DEFINATONS****************************
        int snakeLenght = 4;
        int[,] snakeLimbs = new int[2, 200];
        int yon = 3;
        int oldYon = 3;
        int foodX, foodY,wait;
        Panel[,] panels = new Panel[20, 10];
        Image img1 = new Bitmap(@".\food.png");
        Image img2 = new Bitmap(@".\kafa1.png");
        Image img3 = new Bitmap(@".\kafa2.png");
        Image img4 = new Bitmap(@".\kafa3.png");
        Image img5 = new Bitmap(@".\kafa4.png");
        Image img6 = new Bitmap(@".\govde1.png");

        //******************************FUNCTIONS*****************************
        void lifeControl()
        {
            for (int i = 1; i < snakeLenght; i++)
            {
                if (snakeLimbs[0, i] == snakeLimbs[0, 0] && snakeLimbs[1, i] == snakeLimbs[1, 0])
                {
                    int snakeLenght = 4;
                    timer1.Enabled = false;
                    timer2.Enabled = true;
                    lblGameOver.Visible = true;
                }
            }
        }
        void imageInator()
        {
            for (int i = 0; i < snakeLenght; i++)
            {
                if (i == 0)//bas
                {
                    if (yon == 4)//top
                    {
                        panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = img2;
                    }
                    else if (yon == 2)//down
                    {
                        panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = img3;
                    }
                    else if (yon == 1)//left
                    {
                        panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = img5;
                    }
                    else//right
                    {
                        panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = img4;
                    }
                }
                else
                {
                    panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = img6;
                }
            }  
        }
        void eating()
        {
            if (foodX==snakeLimbs[0,0]&&foodY==snakeLimbs[1,0])
            {
                snakeLenght++;
                foodCreator();
            }
        }
        void foodCreator()
        {
            bool check = false;
            Random rand = new Random();
            do
            {
                check = false;
                foodX = rand.Next(0, 20);
                foodY = rand.Next(0, 10);
                for (int i = 0; i < snakeLenght; i++)
                {
                    if (snakeLimbs[0, i] == foodX && snakeLimbs[1, i] == foodY)
                    {
                        check = true;
                    }
                }
            } while (check);
            panels[foodX, foodY].BackgroundImage = img1;
        }
        void haraket()
        {
            for (int i = snakeLenght; i >= 0; i--)
            {
                panels[snakeLimbs[0, i], snakeLimbs[1, i]].BackgroundImage = null;
                if (i!=0)//aktarma
                {
                    snakeLimbs[0, i] = snakeLimbs[0, i - 1];
                    snakeLimbs[1, i] = snakeLimbs[1, i - 1];
                }
                else//0. eleman oluşturma
                {
                    panels[snakeLimbs[0, 0], snakeLimbs[1, 0]].BackgroundImage = img2;
                    if (yon == 1)
                    {
                        snakeLimbs[0, 0] = (20 + snakeLimbs[0, 1] + 1 ) % 20;
                        snakeLimbs[1, 0] = snakeLimbs[1, 1];
                        oldYon = 1;
                    }
                    else if (yon == 2)
                    {
                        snakeLimbs[0, 0] = snakeLimbs[0, 1];
                        snakeLimbs[1, 0] = ( 10 + snakeLimbs[1, 1] + 1 ) % 10;
                        oldYon = 2;
                    }
                    else if (yon == 3)
                    {
                        snakeLimbs[0, 0] = ( 20 + snakeLimbs[0, 1] - 1 ) % 20;
                        snakeLimbs[1, 0] = snakeLimbs[1, 1];
                        oldYon = 3;
                    }
                    else
                    {
                        snakeLimbs[0, 0] = snakeLimbs[0, 1];
                        snakeLimbs[1, 0] = ( 10 + snakeLimbs[1, 1] - 1 ) % 10;
                        oldYon = 4;
                    }
                }
            }
        }
        void baslangıc()
        {
            for (int i = 0; i < 4; i++)
            {
                snakeLimbs[0, i] = i+ 10;
                snakeLimbs[1, i] =  4;
            }
        }
      
        //*******************FORM COMPANENTS*********************************
        private void Form2_Load(object sender, EventArgs e)
        {
            int pnlLenght = 30;
            lblGameOver.Visible = false;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    panels[i, j] = new Panel();
                    int pnlTop = j * pnlLenght + 50;
                    int pnlLeft = i * pnlLenght + 10;
                    panels[i, j].Top = pnlTop;
                    panels[i, j].Left = pnlLeft;
                    panels[i,j].Width = pnlLenght;
                    panels[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    panels[i, j].Height = pnlLenght;
                    panels[i, j].BackColor = Color.DarkSeaGreen;
                    this.Controls.Add(panels[i, j]);

                }
            }
            baslangıc();
            imageInator();
            foodCreator();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblScore.Text = snakeLenght.ToString();
            haraket();
            imageInator();
            lifeControl();
            eating();
            imageInator();
        }

        private void btnTop_Click(object sender, EventArgs e)
        {
            btnDown.Enabled = false;
            btnTop.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;
            yon = 4;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            btnDown.Enabled = true;
            btnTop.Enabled = true;
            btnLeft.Enabled = true;
            btnRight.Enabled = false;
            yon = 3;

        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            btnDown.Enabled = true;
            btnTop.Enabled = true;
            btnLeft.Enabled = false;
            btnRight.Enabled = true;
            yon = 1;
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            btnDown.Enabled = true;
            btnTop.Enabled = false;
            btnLeft.Enabled = true;
            btnRight.Enabled = true;
            yon = 2;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            wait++;
            if (wait>1)
            {
                Form1 yeni = new Form1();
                yeni.Show();
                this.Hide();
                wait = 0;
                timer2.Enabled = false;
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            ControlPanel.Visible = false;
            if (e.KeyCode == Keys.D||e.KeyCode==Keys.Right)
            {
                if (oldYon!=3)
                {
                    btnDown.Enabled = true;
                    btnTop.Enabled = true;
                    btnLeft.Enabled = false;
                    btnRight.Enabled = true;
                    yon = 1;
                }
            }
            else if (e.KeyCode == Keys.W)
            {
                if (oldYon!=2)
                {
                    btnDown.Enabled = false;
                    btnTop.Enabled = true;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                    yon = 4;
                }
            }
            else if (e.KeyCode == Keys.S)
            {
                if (oldYon!=4)
                {
                    btnDown.Enabled = true;
                    btnTop.Enabled = false;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = true;
                    yon = 2;
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if (oldYon!=1)
                {
                    btnDown.Enabled = true;
                    btnTop.Enabled = true;
                    btnLeft.Enabled = true;
                    btnRight.Enabled = false;
                    yon = 3;
                }

            }

            /////////////////////////
        }

    }
}
