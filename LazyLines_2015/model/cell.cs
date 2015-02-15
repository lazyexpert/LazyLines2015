using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace LazyLines_2015
{
    class cell:System.Windows.Forms.Button
    {
        private bool empty; // if we have a ball here
        public ball currBall; 
       
        //waypoint algorithm fields
        public cell up;
        public cell down;
        public cell right;
        public cell left;
        public int cellNumber;
        public int trackNumber;
        public bool currTick; //if the cell is currently on tick, for waypoint algorithm        
        //waypoint end

        public cell()
        {
            empty = true;            
            currBall = null;
           
            up = null;
            down = null;
            right = null;
            left = null;
            trackNumber = -1;
            currTick = false;
        }
        
        public bool isEmpty()
        {            
            return empty;
        }

        public bool isCurrTick()
        {
            return currTick;
        }

        public void changeEmptyState()
        {
            if (empty == false) empty = true;
            else empty = false;
        }

        public void showBall()
        {
            //ball color pick magic will be here
            currBall = new ball();
            currBall.draw(this);
            empty = false;
        }
        public void showBall(ballColors color)
        {
            currBall = new ball(color);
            currBall.draw(this);
            empty = false;
        }

        public void showBallNext() //usuall method
        {
            if(currBall == null) currBall = new ball();
            currBall.drawNext(this);
        }

        public void showBallNext(ballColors color) //the 1 case method, when the line blowed and the next ball was on that line
        {
            if (currBall == null) currBall = new ball();
            currBall.drawNext(this, color);
        }

        public void deleteBall()
        {
            empty = true;
            if (currBall != null) currBall.remove(this);
            currBall = null;            
        }
    }
}
