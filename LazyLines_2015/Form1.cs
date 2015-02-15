using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LazyLines_2015
{
  

    public partial class Form1 : Form
    {
        static private ModelControll model;
        public Form1()
        {
            InitializeComponent();
            model = new ModelControll();
            //0-8
            //0
            model.addCellToBoard(button1);
            //1
            model.addCellToBoard(button2);
            //2
            model.addCellToBoard(button3);
            //3
            model.addCellToBoard(button6);
            //4
            model.addCellToBoard(button5);
            //5
            model.addCellToBoard(button4);
            //6
            model.addCellToBoard(button9);
            //7
            model.addCellToBoard(button8);
            //8
            model.addCellToBoard(button7);
           
           

            //9-17
            model.addCellToBoard(button18);
            model.addCellToBoard(button17);
            model.addCellToBoard(button16);
            model.addCellToBoard(button15);
            model.addCellToBoard(button14);
            model.addCellToBoard(button13);
            model.addCellToBoard(button12);
            model.addCellToBoard(button11);
            model.addCellToBoard(button10);

            //18-26
            model.addCellToBoard(button27);
            model.addCellToBoard(button26);
            model.addCellToBoard(button25);
            model.addCellToBoard(button24);
            model.addCellToBoard(button23);
            model.addCellToBoard(button22);
            model.addCellToBoard(button21);
            model.addCellToBoard(button20);
            model.addCellToBoard(button19);

            //27-35
            model.addCellToBoard(button54);
            model.addCellToBoard(button53);
            model.addCellToBoard(button52);
            model.addCellToBoard(button51);
            model.addCellToBoard(button50);
            model.addCellToBoard(button49);
            model.addCellToBoard(button48);
            model.addCellToBoard(button47);
            model.addCellToBoard(button46);

            //36-44
            model.addCellToBoard(button45);
            model.addCellToBoard(button44);
            model.addCellToBoard(button43);
            model.addCellToBoard(button42);
            model.addCellToBoard(button41);
            model.addCellToBoard(button40);
            model.addCellToBoard(button39);
            model.addCellToBoard(button38);
            model.addCellToBoard(button37);

            //45-53
            model.addCellToBoard(button36);
            model.addCellToBoard(button35);
            model.addCellToBoard(button34);
            model.addCellToBoard(button33);
            model.addCellToBoard(button32);
            model.addCellToBoard(button31);
            model.addCellToBoard(button30);
            model.addCellToBoard(button29);
            model.addCellToBoard(button28);
      
            //54-62
            model.addCellToBoard(button81);
            model.addCellToBoard(button80);
            model.addCellToBoard(button79);
            model.addCellToBoard(button78);
            model.addCellToBoard(button77);
            model.addCellToBoard(button76);
            model.addCellToBoard(button75);
            model.addCellToBoard(button74);
            model.addCellToBoard(button73);

            //63-71
            model.addCellToBoard(button72);
            model.addCellToBoard(button71);
            model.addCellToBoard(button70);
            model.addCellToBoard(button69);
            model.addCellToBoard(button68);
            model.addCellToBoard(button67);
            model.addCellToBoard(button66);
            model.addCellToBoard(button65);
            model.addCellToBoard(button64);

            //72-80
            model.addCellToBoard(button63);
            model.addCellToBoard(button62);
            model.addCellToBoard(button61);
            model.addCellToBoard(button60);
            model.addCellToBoard(button59);
            model.addCellToBoard(button58);
            model.addCellToBoard(button57);
            model.addCellToBoard(button56);
            model.addCellToBoard(button55);
   
            model.startGame();
        }
        
        //Sending messages to model
        private void loadGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //load saved game from file
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.startGame();
            //suggest user to save this one (if started)
        }

        private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //save game to file
        }
               
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Save game before exit?", "Exit game", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                saveGameToolStripMenuItem_Click(sender, e);
                this.Close();
            }
            else
            {
                this.Close();
            }
        }

        private void showNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showNextToolStripMenuItem.Checked == true)
            {
                showNextToolStripMenuItem.Checked = false;
                model.setNextBalls(false);
            }

            else
            {
                showNextToolStripMenuItem.Checked = true;
                model.setNextBalls(true);
            }
        }

        private void userClikedButton_MouseClick(object sender, MouseEventArgs e)
        {
            model.userClickedButton(sender);  

            int currentScore = ModelControll.Score;
            int currentDigit = currentScore % 10;
            Label[] digits = {digit1, digit2, digit3, digit4, digit5};

			if (currentScore > 0 && currentScore < 10)
				digit1.Image = getDigitImage (currentScore);

			else if (currentScore == 0) {
				foreach (var item in digits) {
					item.Image = Properties.Resources._0;
				}
			}

			else 
			{
				for (int t = 0; currentScore > 0; t++) 
				{
					digits [t].Image = getDigitImage (currentDigit);
					currentScore /= 10;
					currentDigit = currentScore % 10;
				}
			}
        }

        private Image getDigitImage(int digit)
        {
            switch (digit)
            {
                case 0:
                default:
                    return Properties.Resources._0;
                case 1:
                    return Properties.Resources._1;
                case 2:
                    return Properties.Resources._2;
                case 3:
                    return Properties.Resources._3;
                case 4:
                    return Properties.Resources._4;
                case 5:
                    return Properties.Resources._5;
                case 6:
                    return Properties.Resources._6;
                case 7:
                    return Properties.Resources._7;
                case 8:
                    return Properties.Resources._8;
                case 9:
                    return Properties.Resources._9;
            }
        }
    }

   

 
}
