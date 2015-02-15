using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LazyLines_2015
{
    //main logic class composition
    class ModelControll
    {
        cell selected;
        public static int Score { get; private set; }

        List<cell> allBoard; //all cells of the board
        List<int> currentlyEmpty; //each round we refresh this list
        int []nextNum; //number of balls for next round
        ballColors []nextColours; //colours of balls for next round

        readonly int boardWidth;
        readonly int boardHeight;
        bool nextBalls;
        private bool onGameStart;
        static private Random random;

        public ModelControll()
        {            
            selected = null;
            nextBalls = true;
            boardHeight = boardWidth = 9;
            allBoard = new List<cell>();
            currentlyEmpty = new List<int>();
            nextNum = new int[3];
            nextColours = new ballColors[3];      
        }
        
        static ModelControll()
        {
            Score = 0;
            random = new Random();
        }

        public void startGame()
        {
            //Clear the game board
            foreach (cell currCell in allBoard) currCell.deleteBall();
            Score = 0;
            onGameStart = true;
            nextNum[0] = -1;
            generateBalls();                 
        }

        public void userClickedButton(object button)
        {
            cell pressedButton = button as cell;
            if (!pressedButton.isEmpty())
            {
                selected = pressedButton;
                //cell make selected image/or bouncy ball/who cares input later here :D
            }
            else if (selected != null && pressedButton.isEmpty() && ifCanMove(pressedButton))
            {
                makeMove(pressedButton);
                pressedButton.changeEmptyState();
                selected = null;
                if (ifCanBlow(pressedButton))
                {
                    showNextBalls();
                    return;
                }
                generateBalls();                
            }
        }

        public void makeMove(cell  clicked)
        {
            clicked.currBall = selected.currBall;
            clicked.currBall.draw(clicked);
            selected.deleteBall();           
        }

        public void setNextBalls(bool value)
        {
            nextBalls = value;            
        }

        public void addCellToBoard(cell button)
        {
            allBoard.Add(button);
            if (allBoard.Count == boardHeight * boardWidth) generatePointers();           
        }

        private void generateBalls()
        {
            if(ifLost()) return;
            //CurrentlyEmpty - List of indexes of empty cells in allBoard List
 
            int ball1, ball2, ball3, ball4, ball5;
            
            //if its game start, we need 5 balls instead of 3 and we yet have no nextBalls color and positions
            if (onGameStart)
            {
                ball1 = generateNewBall();
                allBoard[ball1].showBall();   
                ball2 = generateNewBall();
                allBoard[ball2].showBall();
                ball3 = generateNewBall();
                allBoard[ball3].showBall();
                ball4 = generateNewBall();
                allBoard[ball4].showBall();
                ball5 = generateNewBall();
                allBoard[ball5].showBall();
                onGameStart = false;
            }
            else
            {
                if (allBoard[nextNum[0]].isEmpty()) ball1 = nextNum[0];
                else  ball1 = generateNewBall();
                if (allBoard[nextNum[1]].isEmpty()) ball2 = nextNum[1];
                else ball2 = generateNewBall();
                if (allBoard[nextNum[2]].isEmpty()) ball3 = nextNum[2];
                else ball3 = generateNewBall();
               
                allBoard[ball1].showBall(nextColours[0]);
                allBoard[ball2].showBall(nextColours[1]);
                allBoard[ball3].showBall(nextColours[2]);
            }
            ifCanBlow(allBoard[ball1]);
            ifCanBlow(allBoard[ball2]);
            ifCanBlow(allBoard[ball3]);                      
            
            //for next balls 
            ball1 = generateNewBall();
            ball2 = generateNewBall();
            ball3 = generateNewBall();
                            
            //save number of cell on the game board for next time
            nextNum[0] = ball1;
            nextNum[1] = ball2;
            nextNum[2] = ball3;

            //show the next round balls(little balls)
            showNext();
            
            //Saving generated colors for next round balls            
            nextColours[0] = allBoard[ball1].currBall.color;
            nextColours[1] = allBoard[ball2].currBall.color;
            nextColours[2] = allBoard[ball3].currBall.color;            
        }

        private void showNext() //inner model method
        {
            allBoard[nextNum[0]].showBallNext();
            allBoard[nextNum[1]].showBallNext();
            allBoard[nextNum[2]].showBallNext();
        }

        private void showNextBalls() //correct ball colors, bug fix//
        {
            allBoard[nextNum[0]].showBallNext(nextColours[0]);
            allBoard[nextNum[1]].showBallNext(nextColours[1]);
            allBoard[nextNum[2]].showBallNext(nextColours[2]);
        }

        private int randomBallPosition(int upperBound)
        {
            int position = 0;
            
            do{
               position = random.Next(upperBound);
            }while(!allBoard[currentlyEmpty[position]].isEmpty());

            return position;
        }

        private int generateNewBall()
        {
            int tempRandomValue = randomBallPosition(currentlyEmpty.Count);
            int ball = allBoard[currentlyEmpty[tempRandomValue]].cellNumber;
            currentlyEmpty.Remove(tempRandomValue);
            return ball;
        }

        private bool ifLost()
        {
            currentlyEmpty.Clear();
            for (int t = 0; t < boardHeight * boardWidth; t++)
            {
                if (allBoard[t].isEmpty()) currentlyEmpty.Add(t);
            }
            if (currentlyEmpty.Count < 3)
            {
                //defeat method
                MessageBox.Show("Game Over :D");
                return true;
            }
            return false;
        }

        private void generatePointers()
        {
            for (int t = 0; t < boardHeight * boardWidth; t++)
            {
                allBoard[t].cellNumber = t;
                if (t > boardWidth-1) allBoard[t].up = allBoard[t - boardWidth];
                if (t % boardWidth != 0 && t > 0) allBoard[t].left = allBoard[t - 1];
                if (t < boardWidth * boardHeight && t % boardWidth != boardWidth-1) allBoard[t].right = allBoard[t + 1];
                if (t < boardHeight * boardWidth - boardWidth) allBoard[t].down = allBoard[t + boardWidth];
            }
        }

        private bool makeTick(cell destination)
        {
            List<cell> tempList = new List<cell>();
            for (int t = 0; t < boardHeight * boardWidth; t++)
            {
                if (allBoard[t].currTick == true)
                {
                    allBoard[t].currTick = false;
                    allBoard[t].trackNumber = 1;

                    if (allBoard[t].up != null && allBoard[t].up.trackNumber == -1 && allBoard[t].up.isEmpty()) tempList.Add(allBoard[t].up);
                    if (allBoard[t].down != null && allBoard[t].down.trackNumber == -1 && allBoard[t].down.isEmpty()) 
                        tempList.Add(allBoard[t].down);
                    if (allBoard[t].right != null && allBoard[t].right.trackNumber == -1 && allBoard[t].right.isEmpty()) 
                        tempList.Add(allBoard[t].right);
                    if (allBoard[t].left != null && allBoard[t].left.trackNumber == -1 && allBoard[t].left.isEmpty()) tempList.Add(allBoard[t].left);

                    if (allBoard[t].up == destination || allBoard[t].down == destination || allBoard[t].right == destination ||
                        allBoard[t].left == destination) return true;                  
                }
            }

            foreach (cell currCell in tempList) currCell.currTick = true;
            
            tempList.Clear();
            return false;
        }

        private bool ifCanMove(cell destinationCell)
        {

            for (int t = 0; t < boardHeight * boardWidth; t++)
            {
                allBoard[t].currTick = false;
                allBoard[t].trackNumber = -1;
            }

            selected.currTick = true;
            for (int t = 0; t < boardHeight * boardWidth; t++)
            {
                if (makeTick(destinationCell) == true)
                {
                    for (int j = 0; j < boardHeight * boardWidth; j++) 
                    { 
                        allBoard[j].currTick = false; 
                        allBoard[j].trackNumber = -1; 
                    }
                    return true;
                }
            }
            return false;
        }

        private bool ifCanBlow(cell targetCell)
        {
            //int num = targetCell.cellNumber;
            int score2Add = 0;
            ballColors color = targetCell.currBall.color;
            List<cell> toBlowHorizontal = new List<cell>();
            List<cell> toBlowVertical = new List<cell>();
            List<cell> toBlowDiagonal1 = new List<cell>();
            List<cell> toBlowDiagonal2 = new List<cell>();
            
            //check horizontal blow
            int start = targetCell.cellNumber - (targetCell.cellNumber % boardWidth);
            int end = start + boardWidth;
            int counter = 1;

            for (int t = start; t < end; t++)
            {
                if (!allBoard[t].isEmpty() && allBoard[t].currBall.color == color)
                {
                    toBlowHorizontal.Add(allBoard[t]);
                    counter++;
                }
                else if (counter >= 5) break;
                else
                {
                    counter = 1;
                    toBlowHorizontal.Clear();
                }
            }
            if (counter <= 5) toBlowHorizontal.Clear();

            //check vertical blow
            start = targetCell.cellNumber % boardWidth;
            end = boardWidth * boardHeight - (boardWidth - start);
            counter = 1;
            for (int t = start; t < end+1; t += boardWidth)
            {
                if (!allBoard[t].isEmpty() && allBoard[t].currBall.color == color)
                {
                    toBlowVertical.Add(allBoard[t]);
                    counter++;
                }
                else if (counter >= 5) 
                    break;
                else
                {
                    counter = 1;
                    toBlowVertical.Clear();
                }
            }
            if (counter <= 5) toBlowVertical.Clear();
            
            //check diagonal 1 from top-left corner to bottom-right
            if (targetCell.cellNumber % boardWidth == 0 || targetCell.cellNumber < boardWidth) start = targetCell.cellNumber;
            else for (int t = targetCell.cellNumber; ; t -= boardWidth + 1)
                {
                    start = t; 
                    if (allBoard[t].cellNumber % boardWidth == 0 || allBoard[t].cellNumber < boardWidth) break;    
                }
            counter = 1;

            for (int t = start;; t+=boardWidth+1)
            {
                if (t > boardHeight*boardHeight-1) break;
                if (!allBoard[t].isEmpty() && allBoard[t].currBall.color == color)
                {
                    toBlowDiagonal1.Add(allBoard[t]);
                    counter++;
                }
                else if (counter >= 5) break;
                else
                {
                    counter = 1;
                    toBlowDiagonal1.Clear();
                }  
            }
            if (counter <= 5) toBlowDiagonal1.Clear();

            //check diagonal 1 from top-right corner to bottom-left
            if (targetCell.cellNumber % boardWidth == boardWidth-1 || targetCell.cellNumber < boardWidth) start = targetCell.cellNumber;
            else for (int t = targetCell.cellNumber; ; t -= boardWidth - 1)
                {
                    start = t;
                    if (allBoard[t].cellNumber % boardWidth == boardWidth - 1 || allBoard[t].cellNumber < boardWidth) break;
                }

            counter = 1;

            for (int t = start; ; t += boardWidth - 1)
            {
                if (t > boardHeight * boardHeight - 1) break;
                if (!allBoard[t].isEmpty() && allBoard[t].currBall.color == color)
                {
                    toBlowDiagonal2.Add(allBoard[t]);
                    counter++;
                }
                else if (counter >= 5) break;
                else
                {
                    counter = 1;
                    toBlowDiagonal2.Clear();
                }
            }
            if (counter <= 5) toBlowDiagonal2.Clear();

            //start blow
            if (toBlowHorizontal.Count != 0)
            {
                foreach (var item in toBlowHorizontal) item.deleteBall();
                score2Add += toBlowHorizontal.Count;
            }
            if (toBlowVertical.Count != 0)
            {
                foreach (var item in toBlowVertical) item.deleteBall();
                score2Add += toBlowVertical.Count;
            }
            if (toBlowDiagonal1.Count != 0)
            {
                foreach (var item in toBlowDiagonal1) item.deleteBall();
                score2Add += toBlowDiagonal1.Count;
            }
            if (toBlowDiagonal2.Count != 0)
            {
                foreach (var item in toBlowDiagonal2) item.deleteBall();
                score2Add += toBlowDiagonal2.Count;
            }

            Score += score2Add;

            if (toBlowVertical.Count == 0 && toBlowHorizontal.Count == 0 && toBlowDiagonal1.Count == 0 && toBlowDiagonal2.Count == 0)
                return false;
            else return true;
        }
    }
}