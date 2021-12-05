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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

namespace agent_town
{
    public partial class Form1 : Form
    {
        string time_start;
        string time_end;
        int[,] town = new int[7, 7] { {1, 1, 1, 1, 1, 1, 1 }, {1, 1, 0, 0, 1, 0, 1 }, {1, 0, 0, 0, 0, 0, 1 }, {1, 1, 0, 0, 1, 0, 1 }, 
            {1, 0, 0, 0, 0, 0, 1 }, {1, 0, 1, 0, 0, 1, 1 }, {1, 1, 1, 1, 1, 1, 1 }};
        int x = 101;
        int y = 201;
        int[,] visited = new int[5, 5] { { 999, 0, 0, 999, 0 }, { 0, 0, 0, 0, 0 }, { 999, 0, 0, 999, 0 }, { 0, 0, 0, 0, 0 }, { 0, 999, 0, 0, 999 } };
        int counter = 0;
        
        

        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AcceptButton = move;
        }

        Boolean patame = true;
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if(patame == true){
            agent.Location = new Point(101, 201);
            move.Visible = true ;
            time_start = DateTime.Now.ToLongTimeString();
            x = 101;
            y = 201;
            this.timer1.Interval = 500;
            this.timer1.Start();
            patame = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green) && (home.ForeColor == System.Drawing.Color.Green))
            {
                timer1.Stop();
               
            }
        

         counter++;
            myLabel.Text = counter.ToString();
            Random rnd = new Random();
            int path = rnd.Next(1, 4);
            int direction = rnd.Next(1, 3);
            int way = rnd.Next(1, 2);
            if (agent.Location == new Point(101, 201)) //denksia apo spiti
            {
                visited[2, 1] += 1; // +1 kathe fora pou paei sto tetragono
                label3.Text = DateTime.Now.ToLongTimeString();
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green))
                {
                    home.ForeColor = System.Drawing.Color.Green;
                    move.Visible = false;
                    label5.Text = DateTime.Now.ToLongTimeString();
                }
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green) && (home.ForeColor == System.Drawing.Color.Green))
                {
                    time_end = DateTime.Now.ToLongTimeString();

                    var result = MessageBox.Show("Ο πρακτορας επισκέφθηκε ολους τις απαραιτητες τοποθεσιες.Πατήστε ΟΚ για να δείτε τα αποτελέσματα του πράκτορα και να κλείσετε την εφαρμογή.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        string createText = "Αποτελέσματα του πράκτορα:" + Environment.NewLine + "Ωρα εκκινησης: " + time_start + Environment.NewLine + "Ωρα τελους: " + time_end + Environment.NewLine + "Αριθμος κινησεων: " + counter.ToString();
                        File.WriteAllText("Results.txt", createText);
                        Process.Start("Results.txt");



                        Application.Exit();
                    }
                }
                if (((visited[1, 1] < visited[2, 2]) && (visited[1, 1] < visited[3, 1])) || ((visited[3, 1] == visited[2, 2]) && (visited[1, 1] < visited[2, 2]))) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if (((visited[2, 2] < visited[1, 1]) && (visited[2, 2] < visited[3, 1])) || ((visited[3, 1] == visited[1, 1]) && (visited[2, 2] < visited[1, 1]))) // ποιο από τα 3 γειτονικά είναι μικρότερο αν είναι διαφορετικοί αριθμοί ή αν 2 είναι ίσα αν το ένα είναι μικρότερο
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if (((visited[3, 1] < visited[1, 1]) && (visited[3, 1] < visited[2, 2])) || ((visited[1, 1] == visited[2, 2]) && (visited[3, 1] < visited[3, 1])))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (((visited[3, 1] == visited[1, 1]) && (visited[1, 1] < visited[2, 2])) || ((visited[3, 1] == visited[2, 2]) && (visited[2, 2] < visited[1, 1])) || ((visited[1, 1] == visited[2, 2]) && (visited[1, 1] < visited[3, 1]))) // αν 2 είναι ίσα και είναι μικρότερα από το άλλο
                {
                    if ((visited[3, 1] == visited[1, 1]) && (visited[1, 1] < visited[2, 2]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[3, 1] == visited[2, 2]) && (visited[2, 2] < visited[1, 1]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[1, 1] == visited[2, 2]) && (visited[1, 1] < visited[3, 1]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                }
                else if ((visited[1, 1] == visited[2, 2]) && (visited[1, 1] == visited[3, 1]) && (visited[2, 2] == visited[3, 1]))
                {
                    switch (direction)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(101, 1)) // έξω right από την τράπεζα
            {
                visited[0, 1] += 1;
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green))
                {
                    bank.ForeColor = System.Drawing.Color.Green;
                    visited[1, 0] = 999;
                    visited[0, 1] = 999;
                }
                if (visited[0, 2] < visited[1, 1])
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if (visited[1, 1] < visited[0, 2])
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (visited[1, 1] == visited[0, 2])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(201, 1)) //έξω left απο το cafe
            {
                visited[0, 2] += 1;
                cafe.ForeColor = System.Drawing.Color.Green;
                visited[0, 2] = 999;
                visited[0, 4] = 999;
                if (visited[0, 1] < visited[1, 2])
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if (visited[1, 2] < visited[0, 1])
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (visited[0, 1] == visited[1, 2])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(401, 1)) //έξω right απο το cafe
            {
                visited[0, 4] += 1;
                cafe.ForeColor = System.Drawing.Color.Green;
                visited[0, 2] = 999;
                visited[0, 4] = 999;
                agent.Location = new Point(x, y + 100); //down
                y = y + 100;
            }
            else if (agent.Location == new Point(1, 101)) //έξω down από την τράπεζα και up απο το σπίτι του
            {
                visited[1, 0] += 1;
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green))
                {
                    bank.ForeColor = System.Drawing.Color.Green;
                    home.ForeColor = System.Drawing.Color.Green;
                    visited[1, 0] = 999;
                    visited[0, 1] = 999;
                    move.Visible = false;
                    label5.Text = DateTime.Now.ToLongTimeString();
                }
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green) && (home.ForeColor == System.Drawing.Color.Green))
                {
                    time_end = DateTime.Now.ToLongTimeString();

                    var result = MessageBox.Show("Ο πρακτορας επισκέφθηκε ολους τις απαραιτητες τοποθεσιες.Πατήστε ΟΚ για να δείτε τα αποτελέσματα του πράκτορα και να κλείσετε την εφαρμογή.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        string createText = "Αποτελέσματα του πράκτορα:" + Environment.NewLine + "Ωρα εκκινησης: " + time_start + Environment.NewLine + "Ωρα τελους: " + time_end + Environment.NewLine + "Αριθμος κινησεων: " + counter.ToString();
                        File.WriteAllText("Results.txt", createText);
                        Process.Start("Results.txt");



                        Application.Exit();
                    }
                }
                agent.Location = new Point(x + 100, y); //right
                x = x + 100;
            }
            else if (agent.Location == new Point(101, 101)) // διασταύρωση
            {
                visited[1, 1] += 1;
                if ((visited[0, 1] < visited[1, 0]) && (visited[0, 1] < visited[1, 2]) && (visited[0, 1] < visited[2, 1])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[1, 0] < visited[0, 1]) && (visited[1, 0] < visited[1, 2]) && (visited[1, 0] < visited[2, 1]))
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[1, 2] < visited[0, 1]) && (visited[1, 2] < visited[1, 0]) && (visited[1, 2] < visited[2, 1]))
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if ((visited[2, 1] < visited[0, 1]) && (visited[2, 1] < visited[1, 0]) && (visited[2, 1] < visited[1, 2]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if ((visited[1, 2] == visited[2, 1]) && (visited[2, 1] == visited[1, 0]) && (visited[1, 0] < visited[0, 1])) // εάν τα 3 μικρότερα απο το άλλο 1
                {    
                    switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                 }
                 else if ((visited[0, 1] == visited[1, 2]) && (visited[1, 2] == visited[2, 1]) && (visited[0, 1] < visited[1, 0])) // εάν τα 3 μικρότερα απο το άλλο 1
                 {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                 }
                 else if ((visited[1, 0] == visited[0, 1]) && (visited[0, 1] == visited[1, 2]) && (visited[1, 2] < visited[2, 1])) // εάν τα 3 μικρότερα απο το άλλο 1
                 {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                   }
                  else if (((visited[2, 1] == visited[0, 1]) && (visited[2, 1] == visited[1, 0])) && (visited[1, 0] < visited[1, 2])) // εάν τα 3 μικρότερα απο το άλλο 1
                  {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[2, 1] == visited[0, 1]) && (visited[0, 1] < visited[1, 0]) && (visited[0, 1] < visited[1, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[2, 1] == visited[1, 0]) && (visited[2, 1] < visited[0, 1]) && (visited[2, 1] < visited[1, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[2, 1] == visited[1, 2]) && (visited[2, 1] < visited[0, 1]) && (visited[2, 1] < visited[1, 0])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                    else if ((visited[1, 0] == visited[1, 2]) && (visited[1, 0] < visited[0, 1]) && (visited[1, 0] < visited[2, 1])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                        }
                    }
                    else if ((visited[0, 1] == visited[1, 2]) && (visited[0, 1] < visited[1, 0]) && (visited[0, 1] < visited[2, 1])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                    else if ((visited[0, 1] == visited[1, 0]) && (visited[0, 1] < visited[1, 2]) && (visited[0, 1] < visited[2, 1])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[2, 1] == visited[0, 1]) && (visited[2, 1] == visited[1, 0]) && (visited[2, 1] == visited[1, 2]))
                {
                    switch (path)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                        case 4:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(201, 101)) // διασταύρωση
            {
                visited[1, 2] += 1;
                if ((visited[0, 2] < visited[1, 1]) && (visited[0, 2] < visited[1, 3]) && (visited[0, 2] < visited[2, 2])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.)
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[1, 1] < visited[0, 2]) && (visited[1, 1] < visited[1, 3]) && (visited[1, 1] < visited[2, 2]))
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[1, 3] < visited[0, 2]) && (visited[1, 3] < visited[1, 1]) && (visited[1, 3] < visited[2, 2]))
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if ((visited[2, 2] < visited[0, 2]) && (visited[2, 2] < visited[1, 1]) && (visited[2, 2] < visited[1, 3]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (((visited[1, 3] == visited[2, 2]) && (visited[2, 2] == visited[1, 1])) && (visited[1, 1] < visited[0, 2])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if (((visited[0, 2] == visited[1, 3]) && (visited[1, 3] == visited[2, 2])) && (visited[0, 2] < visited[1, 1])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                else if (((visited[1, 1] == visited[0, 2]) && (visited[0, 2] == visited[1, 3])) && (visited[1, 3] < visited[2, 2])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if (((visited[2, 2] == visited[0, 2]) && (visited[2, 2] == visited[1, 1])) && (visited[1, 1] < visited[1, 3])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                  else if ((visited[2, 2] == visited[0, 2]) && (visited[0, 2] < visited[1, 1]) && (visited[0, 2] < visited[1, 3])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[2, 2] == visited[1, 1]) && (visited[2, 2] < visited[0, 2]) && (visited[2, 2] < visited[1, 3])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                else if ((visited[2, 2] == visited[1, 3]) && (visited[2, 2] < visited[0, 2]) && (visited[2, 2] < visited[1, 1])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                else if ((visited[1, 1] == visited[1, 3]) && (visited[1, 1] < visited[0, 2]) && (visited[1, 1] < visited[2, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                        }
                    }
                else if ((visited[0, 2] == visited[1, 3]) && (visited[0, 2] < visited[1, 1]) && (visited[0, 2] < visited[2, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[0, 2] == visited[1, 1]) && (visited[0, 2] < visited[1, 3]) && (visited[0, 2] < visited[2, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[0, 2] == visited[1, 1]) && (visited[1, 1] == visited[1, 3]) && (visited[1, 1] == visited[2, 2]))
                {
                    switch (path)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                        case 4:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(301, 101)) // down απο το cafe και up απο την κλινική
            {
                visited[1, 3] += 1;
                cafe.ForeColor = System.Drawing.Color.Green;
                visited[0, 2] = 999;
                visited[0, 4] = 999;
                if (visited[1, 4] < visited[1, 2])
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if (visited[1, 2] < visited[1, 4])
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if (visited[1, 4] == visited[1, 2])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(401, 101))
            {
                visited[1, 4] += 1;  // προσθέτει 1 κάθε φορά που επισκέπτετε το τετράγωνο
                if ((visited[0, 4] < visited[1, 3]) && (visited[0, 4] < visited[2, 4])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[1, 3] < visited[0, 4]) && (visited[1, 3] < visited[2, 4])) // ποιο από τα 3 γειτονικά είναι μικρότερο αν είναι διαφορετικοί αριθμοί ή αν 2 είναι ίσα αν το ένα είναι μικρότερο
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[2, 4] < visited[1, 3]) && (visited[2, 4] < visited[0, 4]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if ((visited[0, 4] == visited[2, 4]) && (visited[2, 4] < visited[1, 3]))  //αν τα 2 ίσα μικρότερα απο το άλλο
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }                
                 else if ((visited[1, 3] == visited[2, 4]) && (visited[2, 4] < visited[0, 4]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                  else if ((visited[1, 3] == visited[0, 4]) && (visited[1, 3] < visited[2, 4]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[0, 4] == visited[2, 4]) && (visited[1, 3] == visited[0, 4]) && (visited[2, 4] == visited[1, 3]))
                {
                    switch (direction)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(201, 201)) //left απο την κλινική
            {
                visited[2, 2] += 1; // προσθέτει 1 κάθε φορά που επισκέπτετε το τετράγωνο
                if ((visited[1, 2] < visited[3, 2]) && (visited[1, 2] < visited[2, 1])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[2, 1] < visited[1, 2]) && (visited[2, 1] < visited[3, 2])) // ποιο από τα 3 γειτονικά είναι μικρότερο αν είναι διαφορετικοί αριθμοί ή αν 2 είναι ίσα αν το ένα είναι μικρότερο
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[3, 2] < visited[2, 1]) && (visited[3, 2] < visited[1, 2]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if ((visited[1, 2] == visited[3, 2]) && (visited[3, 2] < visited[2, 1]))  // αν τα 2 ίσα μικρότερα απο το άλλο
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[2, 1] == visited[3, 2]) && (visited[3, 2] < visited[1, 2]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                  else if ((visited[2, 1] == visited[1, 2]) && (visited[1, 2] < visited[3, 2]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[1, 2] == visited[2, 1]) && (visited[1, 2] == visited[3, 2]) && (visited[2, 1] == visited[3, 2]))
                {
                    switch (direction)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(401, 201)) // right από την κλινική
            {
                visited[2, 4] += 1;
                if (visited[1, 4] < visited[3, 4])
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if (visited[3, 4] < visited[1, 4])
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (visited[1, 4] == visited[3, 4])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(1, 301)) // down απο το σπίτι του
            {
                visited[3, 0] += 1;
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green))
                {
                    home.ForeColor = System.Drawing.Color.Green;
                    move.Visible = false;
                    label5.Text = DateTime.Now.ToLongTimeString();
                }
                if ((cafe.ForeColor == System.Drawing.Color.Green) && (po.ForeColor == System.Drawing.Color.Green) && (bank.ForeColor == System.Drawing.Color.Green) && (home.ForeColor == System.Drawing.Color.Green))
                {
                    time_end = DateTime.Now.ToLongTimeString();

                    var result = MessageBox.Show("Ο πρακτορας επισκέφθηκε ολους τις απαραιτητες τοποθεσιες.Πατήστε ΟΚ για να δείτε τα αποτελέσματα του πράκτορα και να κλείσετε την εφαρμογή.", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    if (result == DialogResult.OK)
                    {
                        string createText = "Αποτελέσματα του πράκτορα:" + Environment.NewLine + "Ωρα εκκινησης: " + time_start + Environment.NewLine + "Ωρα τελους: " + time_end + Environment.NewLine + "Αριθμος κινησεων: " + counter.ToString();
                        File.WriteAllText("Results.txt", createText);
                        Process.Start( "Results.txt");



                        Application.Exit();
                    }
                }

                if (visited[3, 1] < visited[4, 0])
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if (visited[4, 0] < visited[3, 1])
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (visited[4, 0] == visited[3, 1])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(101, 301)) // up από ταχυδρομείο
            {
                visited[3, 1] += 1; // προσθέτει 1 κάθε φορά που επισκέπτετε το τετράγωνο
                if (cafe.ForeColor == System.Drawing.Color.Green)
                {
                    po.ForeColor = System.Drawing.Color.Green;
                    visited[4, 0] = 999;
                    visited[4, 2] = 999;
                }
                if ((visited[2, 1] < visited[3, 2]) && (visited[2, 1] < visited[3, 0])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[3, 2] < visited[2, 1]) && (visited[3, 2] < visited[3, 0]))
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if ((visited[3, 0] < visited[2, 1]) && (visited[3, 0] < visited[3, 2]))
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[3, 2] == visited[2, 1]) && (visited[2, 1] < visited[3, 0])) // αν τα 2 ίσα μικρότερα απο το άλλο
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if ((visited[2, 1] == visited[3, 0]) && (visited[3, 0] < visited[3, 2]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                 else if ((visited[3, 2] == visited[3, 0]) && (visited[3, 2] < visited[2, 1]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                else if ((visited[3, 0] == visited[2, 1]) && (visited[2, 1] == visited[3, 2]) && (visited[3, 0] == visited[3, 2]))
                {
                    switch (direction)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(201, 301)) //διασταύρωση
            {
                visited[3, 2] += 1;
                if ((visited[2, 2] < visited[3, 1]) && (visited[2, 2] < visited[3, 3]) && (visited[2, 2] < visited[4, 2])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if ((visited[3, 1] < visited[2, 2]) && (visited[3, 1] < visited[3, 3]) && (visited[3, 1] < visited[4, 2]))
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[3, 3] < visited[2, 2]) && (visited[3, 3] < visited[3, 1]) && (visited[3, 3] < visited[4, 2]))
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if ((visited[4, 2] < visited[2, 2]) && (visited[4, 2] < visited[3, 1]) && (visited[4, 2] < visited[3, 3]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if (((visited[3, 3] == visited[4, 2]) && (visited[4, 2] == visited[3, 1])) && (visited[3, 1] < visited[2, 2])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if (((visited[2, 2] == visited[3, 3]) && (visited[3, 3] == visited[4, 2])) && (visited[2, 2] < visited[3, 1])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if (((visited[3, 1] == visited[2, 2]) && (visited[2, 2] == visited[3, 3])) && (visited[3, 3] < visited[4, 2])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if (((visited[4, 2] == visited[2, 2]) && (visited[4, 2] == visited[3, 1])) && (visited[3, 1] < visited[3, 3])) // εάν τα 3 μικρότερα απο το άλλο 1
                    {
                        switch (direction)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 3:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                else if ((visited[4, 2] == visited[2, 2]) && (visited[2, 2] < visited[3, 1]) && (visited[2, 2] < visited[3, 3])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[4, 2] == visited[3, 1]) && (visited[4, 2] < visited[2, 2]) && (visited[4, 2] < visited[3, 3])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[4, 2] == visited[3, 3]) && (visited[4, 2] < visited[2, 2]) && (visited[4, 2] < visited[3, 1])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[3, 1] == visited[3, 3]) && (visited[3, 1] < visited[2, 2]) && (visited[3, 1] < visited[4, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                        }
                    }
                 else if ((visited[2, 2] == visited[3, 3]) && (visited[2, 2] < visited[3, 1]) && (visited[2, 2] < visited[4, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                 else if ((visited[2, 2] == visited[3, 1]) && (visited[2, 2] < visited[3, 3]) && (visited[2, 2] < visited[4, 2])) // εάν 2 τα ίδια και μικρότερα από τα άλλα επέλεξε τυχαία ανάμεσα στα 2
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y - 100); //up
                                y = y - 100;
                                break;
                        }
                    }
                else if ((visited[2, 2] == visited[3, 1]) && (visited[2, 2] == visited[3, 3]) && (visited[2,2] == visited[4, 2]))
                {
                    switch (path)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                        case 4:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(301, 301)) // down απο κλινική
            {
                visited[3, 3] += 1; // προσθέτει 1 κάθε φορά που επισκέπτετε το τετράγωνο
                if ((visited[3, 4] < visited[3, 2]) && (visited[3, 4] < visited[4, 3])) // έλέγχουμε πιο κοντινό τετράγωνο έχουμε περάσει λιγότερες φορές.
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if ((visited[3, 2] < visited[3, 4]) && (visited[3, 2] < visited[4, 3])) // ποιο από τα 3 γειτονικά είναι μικρότερο αν είναι διαφορετικοί αριθμοί ή αν 2 είναι ίσα αν το ένα είναι μικρότερο
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if ((visited[4, 3] < visited[3, 2]) && (visited[4, 3] < visited[3, 4]))
                {
                    agent.Location = new Point(x, y + 100); //down
                    y = y + 100;
                }
                else if ((visited[3, 2] == visited[4, 3]) && (visited[4, 3] < visited[3, 4])) // αν τα 2 ίσα μικρότερα απο το άλλο
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                 else if ((visited[3, 2] == visited[3, 4]) && (visited[3, 4] < visited[4, 3]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x - 100, y); //left
                                x = x - 100;
                                break;
                            case 2:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                        }
                    }
                 else if ((visited[3, 4] == visited[4, 3]) && (visited[4, 3] < visited[3, 2]))
                    {
                        switch (way)
                        {
                            case 1:
                                agent.Location = new Point(x + 100, y); //right
                                x = x + 100;
                                break;
                            case 2:
                                agent.Location = new Point(x, y + 100); //down
                                y = y + 100;
                                break;
                        }
                    }
                else if ((visited[4, 3] == visited[3, 2]) && (visited[4, 3] == visited[3, 4]))
                {
                    switch (direction)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 3:
                            agent.Location = new Point(x, y + 100); //down
                            y = y + 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(401, 301))
            {
                visited[3, 4] += 1;
                if (visited[3, 3] < visited[2, 4])
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if (visited[2, 4] < visited[3, 3])
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if (visited[2, 4] == visited[3, 3])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(1, 401)) // left απο ταχυδρομείο
            {
                visited[4, 0] += 1;
                if (cafe.ForeColor == System.Drawing.Color.Green)
                {
                    po.ForeColor = System.Drawing.Color.Green;
                    visited[4, 0] = 999;
                    visited[4, 2] = 999;
                }
                agent.Location = new Point(x, y - 100); //up
                y = y - 100;
            }
            else if (agent.Location == new Point(201, 401)) // right απο ταχυδρομείο
            {
                visited[4, 2] += 1;
                if (cafe.ForeColor == System.Drawing.Color.Green)
                {
                    po.ForeColor = System.Drawing.Color.Green;
                    visited[4, 0] = 999;
                    visited[4, 2] = 999;
                }
                if (visited[3, 2] < visited[4, 3])
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if (visited[4, 3] < visited[3, 2])
                {
                    agent.Location = new Point(x + 100, y); //right
                    x = x + 100;
                }
                else if (visited[4, 3] == visited[3, 2])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x + 100, y); //right
                            x = x + 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            else if (agent.Location == new Point(301, 401)) //left από το κατάστημα
            {
                visited[4, 3] += 1;
                if (visited[3, 3] < visited[4, 2])
                {
                    agent.Location = new Point(x, y - 100); //up
                    y = y - 100;
                }
                else if (visited[4, 2] < visited[3, 3])
                {
                    agent.Location = new Point(x - 100, y); //left
                    x = x - 100;
                }
                else if (visited[3, 3] == visited[4, 2])
                {
                    switch (way)
                    {
                        case 1:
                            agent.Location = new Point(x - 100, y); //left
                            x = x - 100;
                            break;
                        case 2:
                            agent.Location = new Point(x, y - 100); //up
                            y = y - 100;
                            break;
                    }
                }
            }
            } 
        
        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void agent_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void myLabel_Click(object sender, EventArgs e)
        {
           
        }

        private void usermanual_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Για να ξεκινήσετε, πατάτε το START. Αφού πατήσετε START, ο πράκτορας θα κινείτε μέσα στην πόλη, ένα τετράγωνο το δευτερόλεπτο. Οι υποχρεώσεις του πράκτορα στην πόλη, δηλώνονται στην λίστα υποχρεώσεων δεξιά με σειρά προτεραιότητας. Αποστολή του πράκτορα είναι να επισκεφτεί όλους τους προορισμούς του με την σωστή σειρά. Αφού ο πράκτορας ολοκληρώσει την αποστολή του, δημιουργείτε αρχείο RESULTS.txt, το οποίο ανοίγει, με τα στατιστικά του πράκτορα.", "Οδηγίες", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        
    }
}
