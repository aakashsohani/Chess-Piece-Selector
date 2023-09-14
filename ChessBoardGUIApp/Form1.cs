using ChessBoardModel;

namespace ChessBoardGUIApp
{
    public partial class Form1 : Form
    {
        //refernce to the class Board. Contains the value of the board.
        static Board myBoard = new Board(8);

        //2D arrray of button whose values are determined by myBoard.
        public Button[,] btnGrid = new Button[myBoard.Size, myBoard.Size];
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            populateGrid();
        }

        private void populateGrid()
        {
            //create buttons and place them into panel1
            int buttonSize = panel1.Width / myBoard.Size;

            //make the panel a perfect square
            panel1.Height = panel1.Width;

            //nested loops. create buttons and print them to the screen
            for (int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j] = new Button();
                    btnGrid[i, j].Height = buttonSize;
                    btnGrid[i, j].Width = buttonSize;

                    //add a click event to each button
                    btnGrid[i, j].Click += Grid_Button_Click;

                    //add the new button to the panel
                    panel1.Controls.Add(btnGrid[i, j]);

                    //set the location of the new button
                    btnGrid[i, j].Location = new Point(i * buttonSize, j * buttonSize);
                    btnGrid[i, j].Text = i + "|" + j;
                    btnGrid[i, j].Tag = new Point(i, j);
                }
            }
        }

        private void Grid_Button_Click(object sender, EventArgs e)
        {
            //get the selected chess piece from the ComboBox
            string selectedChessPiece = comboBox1.SelectedItem.ToString();

            //get the row and col number of the button clicked
            Button clickButton = (Button)sender;
            Point location = (Point)clickButton.Tag;

            int x = location.X;
            int y = location.Y;

            Cell currentCell = myBoard.TheGrid[x, y];

            //determine legal moves
            myBoard.MarkNextLegalMoves(currentCell, selectedChessPiece);


            //update the text on each button
            for(int i = 0; i < myBoard.Size; i++)
            {
                for (int j = 0; j < myBoard.Size; j++)
                {
                    btnGrid[i, j].Text = "";
                    btnGrid[i,j].BackColor = SystemColors.Control;
                    if (myBoard.TheGrid[i,j].LegalNextMove == true)
                    {
                        btnGrid[i, j].Text = "Legal";
                        btnGrid[i, j].BackColor = Color.LightGray;
                    }
                    else if (myBoard.TheGrid[i,j].CurrentlyOccupied == true)
                    {
                        btnGrid[i, j].Text = selectedChessPiece;
                        btnGrid[i, j].BackColor = Color.Wheat;
                    }
                }
            }
        }


    }
}