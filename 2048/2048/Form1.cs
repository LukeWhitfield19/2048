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

namespace _2048
{
    public partial class Form1 : Form
    {
        Label[,] labels = new Label[4, 4];
        Random rand = new Random();
        public Form1()
        {

            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
            KeyPreview = true;
            //list inefficently like this as attempting to do it the main class throws an error
            labels[0, 0] = label_1;
            labels[0, 1] = label_2;
            labels[0, 2] = label_3;
            labels[0, 3] = label_4;
            labels[1, 0] = label_5;
            labels[1, 1] = label_6;
            labels[1, 2] = label_7;
            labels[1, 3] = label_8;
            labels[2, 0] = label_9;
            labels[2, 1] = label_10;
            labels[2, 2] = label_11;
            labels[2, 3] = label_12;
            labels[3, 0] = label_13;
            labels[3, 1] = label_14;
            labels[3, 2] = label_15;
            labels[3, 3] = label_16;

            Intialise();

        }


        public Dictionary<string, Color> colours = new Dictionary<string, Color>
        {
            { "", Color.Gainsboro },
            { "2", Color.White},
            { "4", Color.AntiqueWhite},
            { "8", Color.BurlyWood},
            { "16", Color.LightSalmon},
            { "32", Color.IndianRed},
            { "64", Color.Red},
            { "128", Color.Khaki},
            { "256", Color.PeachPuff},
            { "512", Color.Gold},
            { "1024", Color.Yellow},
            { "2048", Color.Blue},

        };    

        public void Intialise() //sets the board up with two squares set to "2"
        {           
            int rnd1 = rand.Next(1, 17);
            int rnd2 = rand.Next(1, 17);
            int i = 0;


            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    i++;
                    if (labels[y, x].Text == "" && i == rnd1)
                    {
                        labels[y, x].BackColor = colours["2"];
                        labels[y, x].Text = "2";
                    }

                    if (labels[y, x].Text == "" && i == rnd2)
                    {
                        labels[y, x].BackColor = colours["2"];
                        labels[y, x].Text = "2";
                    }

                }

            }

        }

        public void GameWin()
        {
           DialogResult result = MessageBox.Show("Congratulations, You've Won! Click yes if you'd like to restart", "Restart", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Intialise();
                //restarts the game
            }
            else
            {
                KeyDown += new KeyEventHandler(Form1_KeyDown);
                KeyPreview = false;
                //disable keyinputs so player can't move tiles further
            }
        }

        public void Userinput(object sender, KeyEventArgs e) //calls the corrospending function to the key the player pressed
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                      Up();
                    break;
                case Keys.A:
                     Lft();
                    break;
                case Keys.S:
                    Down();
                    break;
                case Keys.D:
                    Rht();
                    break;
            }

        }

        public void Rht()
        {            

            int rnd = rand.Next(0, 17);
            for (int y = 0; y < 4; y++)
            {
                int x = 0;
                int i = 0;
                while (x < 3)
                {   //if statement for merging 
                    if (labels[y, x].Text != "" && labels[y, x + 1].Text == labels[y, x].Text) 
                    {
                        labels[y, x].Text = "";

                        labels[y, x + 1].Text = Convert.ToString(Convert.ToInt32(labels[y, x + 1].Text) * 2);

                        if (labels[y,x + 1].Text == "2048")
                        {
                            GameWin();
                        }

                        //generate a new random tile after succesful merge
                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }
                    }
                    //statement for moving tile to the edge/next avaliable space              
                    else if (labels[y, x].Text != "" && labels[y, x + 1].Text == "")
                    {
                        i = 0;
                        labels[y, x + 1].Text = labels[y, x].Text;
                        labels[y, x].Text = "";
                        //generate new random tile
                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }

                    }
                    x++;
                    //reset all the labels colours to the correct ones
                    for (int q = 0; q < 4; q++)
                    {
                        for (int w = 0; w < 4; w++)
                        {
                            labels[q, w].BackColor = colours[labels[q, w].Text];
                        }
                    }
                }
            }
        }

        public void Lft()
        {
                         
            int i = 0;
            int rnd = rand.Next(0, 17);
            for (int y = 0; y < 4; y++)
            {
                int x = 3;
                while (x > 0)
                {
                    if (labels[y, x].Text != "" && labels[y, x - 1].Text == labels[y, x].Text)
                    {
                        labels[y, x].Text = "";
                        
                        labels[y, x - 1].Text = Convert.ToString(Convert.ToInt32(labels[y, x - 1].Text) * 2);

                        if (labels[y, x - 1].Text == "2048")
                        {
                            GameWin();
                        }

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }
                    }
                    else if (labels[y, x].Text != "" && labels[y, x - 1].Text == "")
                    {
                        i = 0;
                        labels[y, x - 1].Text = labels[y, x].Text;
                        labels[y, x].Text = "";

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }
                    }
                    x--;

                    for (int q = 0; q < 4; q++)
                    {
                        for (int w = 0; w < 4; w++)
                        {
                            labels[q, w].BackColor = colours[labels[q, w].Text];
                        }
                    }
                }
            }
        }

        public void Up()
        {           

            int i = 0;
            int rnd = rand.Next(0, 17);
            for (int x = 0; x < 4; x++)
            {
                int y = 3;
                while (y > 0)
                {
                    if (labels[y, x].Text != "" && labels[y - 1, x].Text == labels[y, x].Text)
                    {
                        labels[y, x].Text = "";
                        
                        labels[y - 1, x].Text = Convert.ToString(Convert.ToInt32(labels[y - 1, x].Text) * 2);

                        if (labels[y - 1, x].Text == "2048")
                        {
                            GameWin();
                        }

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }
                    }
                    else if (labels[y, x].Text != "" && labels[y - 1, x].Text == "")
                    {
                        i = 0;
                        labels[y - 1, x].Text = labels[y, x].Text;
                        labels[y, x].Text = "";

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }

                    }
                    y--;

                    for (int q = 0; q < 4; q++)
                    {
                        for (int w = 0; w < 4; w++)
                        {
                            labels[q, w].BackColor = colours[labels[q, w].Text];
                        }
                    }
                }
            }
        }

        public void Down()
        {            
            int i = 0;
            int rnd = rand.Next(0, 17);
            for (int x = 0; x < 4; x++)
            {
                int y = 0;
                while (y < 3)
                {
                    if (labels[y, x].Text != "" && labels[y + 1, x].Text == labels[y, x].Text)
                    {
                        labels[y, x].Text = "";
                        
                        labels[y + 1, x].Text = Convert.ToString(Convert.ToInt32(labels[y + 1, x].Text) * 2);

                        if (labels[y + 1, x].Text == "2048")
                        {
                            GameWin();
                        }

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }

                    }
                    else if (labels[y, x].Text != "" && labels[y + 1, x].Text == "")
                    {
                        i = 0;
                        labels[y + 1, x].Text = labels[y, x].Text;
                        labels[y, x].Text = "";

                        for (int a = 0; a < 4; a++)
                        {

                            for (int b = 0; b < 4; b++)
                            {
                                i++;
                                if (labels[a, b].Text == "" && i == rnd)
                                {
                                    labels[a, b].Text = "2";
                                    labels[a, b].BackColor = colours["2"];
                                }
                            }
                        }

                    }
                    y++;

                    for (int q = 0; q < 4; q++)
                    {
                        for (int w = 0; w < 4; w++)
                        {
                            labels[q, w].BackColor = colours[labels[q, w].Text];
                        }
                    }
                }
            }
        }
      

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            Userinput(sender, e);
        }
    }            
}

    


    
        

        

        
    


