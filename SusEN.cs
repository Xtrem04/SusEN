using SusEN.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SusEN
{
    public partial class SusEN : Form
    {
        #region Global Variables
        private readonly Point ResetY = new Point(0, -60);
        private readonly Point AdjustX = new Point(30, 0);
        private readonly Font Special = new Font("Microsoft Sans Serif", 16.0f, FontStyle.Bold);
        private readonly Font Normal = new Font("Arial", 16.0f, FontStyle.Regular);
        private readonly Random RandomX = new Random();
        private readonly Random RandomGoodFood = new Random();
        private readonly Random RandomBadFood = new Random();
        private readonly string[] UserAccount;
        private readonly string[] UserGameInfo;
        private readonly string[] UserRecipes;
        private readonly string[] GoodFood = { "Apple", "Avocado", "Brocolli", "Cabbage", "Onion", "Orange", "Peach", "Pepper", "Potato", "Tomato", "Watermelon" };
        private readonly string[] BadFood = { "Burger", "Croissant", "Donut", "Fries", "Pizza", "Soda" };
        private readonly string Accounts = "Accounts.txt";
        private readonly string GameInfo = "Game Info.txt";
        private readonly string Recipes = "Recipes.txt";
        private readonly string Password = "Enter Password";
        private readonly string AdminPassword = File.ReadAllText("Admin.txt");
        private readonly int CoinsPerScore = 5;
        private readonly int Border = 75;
        private readonly int AbacateOvoPrice = 400;
        private readonly int CarilGambasPrice = 600;
        private readonly int DouradinhosPrice = 350;
        private readonly int EspinafreAtumPrice = 500;
        private readonly int HamburgerFrangoCogumelosPrice = 550;
        private readonly int KebabPrice = 650;
        private readonly int MousseChocolateAbacatePrice = 400;
        private readonly int PenneCapresePrice = 500;
        private readonly int PudimChiaPrice = 300;
        private readonly int QuinoaFrangoPrice = 550;
        private readonly int SaladaAtumPrice = 450;
        private readonly int SojaBolonhesaPrice = 300;
        private readonly int SopaLentilhasPrice = 500;
        private readonly int TofuBrásPrice = 700;
        private readonly int YakisobaPrice = 800;
        private Point Food1Point, Food2Point, Food3Point, Food4Point, Food5Point, Food6Point, Food7Point, Food8Point;
        private DialogResult WantBuy;
        private DialogResult AdminWantBuy;
        private DialogResult WantRevoke;
        private string DecryptedPassword;
        private string CurrentRecipe;
        private string RecipeName;
        private int CurrentFrame = 0;
        private int i = 0;
        private int k = 1;
        private int n = 1;
        private int InfoPage = 1;
        private int x;
        private int Food1X, Food2X, Food3X, Food4X, Food5X, Food6X, Food7X, Food8X;
        private int Score;
        private int UpdatedCoins;
        private int CurrentCoins;
        private int CurrentPrice;
        private int Frames;
        private int HeightValue;
        private int WeightValue;
        private bool Food1Ready, Food2Ready, Food3Ready, Food4Ready, Food5Ready, Food6Ready, Food7Ready, Food8Ready;
        #endregion

        #region Constructor
        public SusEN(string[] Account, string[] GameInfo, string[] Recipes)
        {
            InitializeComponent();
            UserAccount = Account;
            UserGameInfo = GameInfo;
            UserRecipes = Recipes;
            FrameDimension Dimension = new FrameDimension(LockerGIF_PictureBox.Image.FrameDimensionsList[0]);
            Frames = LockerGIF_PictureBox.Image.GetFrameCount(Dimension);
        }
        #endregion

        #region Menu Tab
        private void Menu_Tab_Enter(object sender, EventArgs e)
        {
            Menu_Coins_Label.Text = UserGameInfo[1];
            Menu_Account_Label.Text = UserAccount[0];
        }

        #region Buttons
        private void Menu_Game_Button_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(1);
        }
        private void Menu_Recipes_Button_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(2);
        }
        private void Menu_Account_Button_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(3);
        }
        private void MenuHelp_Button_Click(object sender, EventArgs e)
        {
            InfoPage = 1;
            TabControl.SelectTab(4);
        }
        #endregion

        #endregion

        #region Game Tab
        private void Game_Tab_Enter(object sender, EventArgs e)
        {
            Game_Panel.Size = Game_Tab.Size;
            GameResults_Panel.Size = Game_Tab.Size;
            Game_Panel.Visible = true;
            GameResults_Panel.Visible = false;
        }
        private void StartGame_Button_Click(object sender, EventArgs e)
        {
            Game_Panel.Visible = false;
            Start();
        }
        private void GameHelp_Button_Click(object sender, EventArgs e)
        {
            InfoPage = 2;
            TabControl.SelectTab(4);
        }
        private void GameStartHelp_Button_Click(object sender, EventArgs e)
        {
            InfoPage = 2;
            TabControl.SelectTab(4);
        }
        private void ExitGame_Button_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(0);
        }

        #region Mouse Move
        private void Game_Tab_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X > 105 && e.X < 671)
            {
                Pot_PictureBox.Location = new Point(e.X - 100, 330);
                Active_PictureBox.Location = n > 2 ? Pot_PictureBox.Location + (Size)AdjustX : Pot_PictureBox.Location;
            }
        }
        #endregion

        #region Timers
        private void Levels_Timer_Tick(object sender, EventArgs e)
        {
            if (n < 3)
            {
                if (n == 2)
                {
                    Active_PictureBox.Width = 140;
                }
                NewFood_Timer.Interval -= 2100 / ((4 * n) - 1);
                k++;
            }
            n++;
        }
        private void NewFood_Timer_Tick(object sender, EventArgs e)
        {
            switch (i)
            {
                case 1:
                    {
                        Food1Ready = true;
                        GoodFood1_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food1X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food8X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 2:
                    {
                        Food2Ready = true;
                        GoodFood2_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food2X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food1X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 3:
                    {
                        Food3Ready = true;
                        GoodFood3_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food3X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food2X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 4:
                    {
                        Food4Ready = true;
                        BadFood1_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(BadFood[RandomBadFood.Next(6)]);
                        Food4X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food3X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 5:
                    {
                        Food5Ready = true;
                        GoodFood4_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food5X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food4X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 6:
                    {
                        Food6Ready = true;
                        GoodFood5_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food6X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food5X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 7:
                    {
                        Food7Ready = true;
                        BadFood2_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(BadFood[RandomBadFood.Next(6)]);
                        Food7X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food6X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        break;
                    }
                case 8:
                    {
                        Food8Ready = true;
                        GoodFood6_PictureBox.Image = (Image)Resources.ResourceManager.GetObject(GoodFood[RandomGoodFood.Next(11)]);
                        Food8X = RandomX.Next(Border, 776 - (2 * Border));
                        if (n > 2)
                        {
                            Food7X = RandomX.Next(Border, 776 - (2 * Border));
                        }
                        i = 0;
                        break;
                    }
            }
            i++;
        }
        private void YDown_Timer_Tick(object sender, EventArgs e)
        {
            if (Food1Ready)
            {
                GoodFood1_PictureBox.Location = new Point(Food1X, GoodFood1_PictureBox.Location.Y + k);
                Food1Point = new Point(Food1X + 30, GoodFood1_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food1Point))
                {
                    Score += CoinsPerScore;
                    GoodFood1_PictureBox.Location = ResetY;
                    Food1Ready = false;
                }
                if (GoodFood1_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
            if (Food2Ready)
            {
                GoodFood2_PictureBox.Location = new Point(Food2X, GoodFood2_PictureBox.Location.Y + k);
                Food2Point = new Point(Food2X + 30, GoodFood2_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food2Point))
                {
                    Score += CoinsPerScore;
                    GoodFood2_PictureBox.Location = ResetY;
                    Food2Ready = false;
                }
                if (GoodFood2_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
            if (Food3Ready)
            {
                GoodFood3_PictureBox.Location = new Point(Food3X, GoodFood3_PictureBox.Location.Y + k);
                Food3Point = new Point(GoodFood3_PictureBox.Location.X + 30, GoodFood3_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food3Point))
                {
                    Score += CoinsPerScore;
                    GoodFood3_PictureBox.Location = ResetY;
                    Food3Ready = false;
                }
                if (GoodFood3_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
            if (Food4Ready)
            {
                BadFood1_PictureBox.Location = new Point(Food4X, BadFood1_PictureBox.Location.Y + k);
                Food4Point = new Point(BadFood1_PictureBox.Location.X + 30, BadFood1_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food4Point))
                {
                    GameOver();
                }
                if (BadFood1_PictureBox.Location.Y >= 423)
                {
                    BadFood1_PictureBox.Location = ResetY;
                    Food4Ready = false;
                }
            }
            if (Food5Ready)
            {
                GoodFood4_PictureBox.Location = new Point(Food5X, GoodFood4_PictureBox.Location.Y + k);
                Food5Point = new Point(GoodFood4_PictureBox.Location.X + 30, GoodFood4_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food5Point))
                {
                    Score += CoinsPerScore;
                    GoodFood4_PictureBox.Location = ResetY;
                    Food5Ready = false;
                }
                if (GoodFood4_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
            if (Food6Ready)
            {
                GoodFood5_PictureBox.Location = new Point(Food6X, GoodFood5_PictureBox.Location.Y + k);
                Food6Point = new Point(GoodFood5_PictureBox.Location.X + 30, GoodFood5_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food6Point))
                {
                    Score += CoinsPerScore;
                    GoodFood5_PictureBox.Location = ResetY;
                    Food6Ready = false;
                }
                if (GoodFood5_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
            if (Food7Ready)
            {
                BadFood2_PictureBox.Location = new Point(Food7X, BadFood2_PictureBox.Location.Y + k);
                Food7Point = new Point(BadFood2_PictureBox.Location.X + 30, BadFood2_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food7Point))
                {
                    GameOver();
                }
                if (BadFood2_PictureBox.Location.Y >= 423)
                {
                    BadFood2_PictureBox.Location = ResetY;
                    Food7Ready = false;
                }
            }
            if (Food8Ready)
            {
                GoodFood6_PictureBox.Location = new Point(Food8X, GoodFood6_PictureBox.Location.Y + k);
                Food8Point = new Point(GoodFood6_PictureBox.Location.X + 30, GoodFood6_PictureBox.Location.Y + 60);
                if (Active_PictureBox.Bounds.Contains(Food8Point))
                {
                    Score += CoinsPerScore;
                    GoodFood6_PictureBox.Location = ResetY;
                    Food8Ready = false;
                }
                if (GoodFood6_PictureBox.Location.Y >= 423)
                {
                    GameOver();
                }
            }
        }
        #endregion

        #region Functions
        private void Start()
        {
            Score = 0;
            NewFood_Timer.Interval = 1500;
            Levels_Timer.Start();
            NewFood_Timer.Start();
            YDown_Timer.Start();
            Reset();
        }
        private void Stop()
        {
            Levels_Timer.Stop();
            NewFood_Timer.Stop();
            YDown_Timer.Stop();
            Reset();
        }
        private void GameOver()
        {
            Stop();
            NewRecord_PictureBox.Image = null;
            GameResults_Panel.Visible = true;
            UpdatedCoins = int.Parse(UserGameInfo[1]) + Score;
            Game_Coins_Label.Text = UpdatedCoins.ToString();
            string AllGameInfo = File.ReadAllText(GameInfo);
            string UpdatedGameInfo = AllGameInfo.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}", $"{UserGameInfo[0]}|{UpdatedCoins}");
            File.WriteAllText(GameInfo, UpdatedGameInfo);
            UserGameInfo[1] = UpdatedCoins.ToString();
            if (Score > int.Parse(UserGameInfo[2]))
            {
                NewRecord_PictureBox.Image = Resources.New_Record;
                string AllUpdatedGameInfo = File.ReadAllText(GameInfo);
                string UpdatedRecord = AllUpdatedGameInfo.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}|{UserGameInfo[2]}", $"{UserGameInfo[0]}|{UserGameInfo[1]}|{Score}");
                File.WriteAllText(GameInfo, UpdatedRecord);
                UserGameInfo[2] = Score.ToString();
            }
            Score_Label.Text = $"Score = {Score}!{Environment.NewLine}Record = {UserGameInfo[2]}";
        }
        private void Reset()
        {
            i = 0;
            n = k = 1;
            GoodFood1_PictureBox.Location = GoodFood2_PictureBox.Location = GoodFood3_PictureBox.Location = GoodFood4_PictureBox.Location = GoodFood5_PictureBox.Location = GoodFood6_PictureBox.Location = BadFood1_PictureBox.Location = BadFood2_PictureBox.Location = ResetY;
            Food1Ready = Food2Ready = Food3Ready = Food4Ready = Food5Ready = Food6Ready = Food7Ready = Food8Ready = false;
        }
        #endregion

        private void NewGame_Button_Click(object sender, EventArgs e)
        {
            GameResults_Panel.Visible = false;
            Start();
        }
        private void GameOverExit_Button_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(0);
        }
        private void Game_Tab_Leave(object sender, EventArgs e)
        {
            Stop();
        }
        #endregion
        
        #region Recipes Tab
        private void Recipes_Tab_Enter(object sender, EventArgs e)
        {
            Recipes_Coins_Label.Text = UserGameInfo[1];
            CurrentCoins = int.Parse(UserGameInfo[1]);
            Default();
        }
        private void RecipesHelp_Button_Click(object sender, EventArgs e)
        {
            InfoPage = 3;
            TabControl.SelectTab(4);
        }

        #region Recipes
        private void AbacateOvo_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "AbacateOvoRecipe";
            CurrentRecipe = AbacateOvo_Label.Text;
            CurrentPrice = AbacateOvoPrice;
            x = 1;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        AbacateOvoPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= AbacateOvoPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        AbacateOvoPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void AbacateOvoPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = AbacateOvo_Label.Text;
            CurrentPrice = AbacateOvoPrice;
            x = 1;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    AbacateOvoPrice_Label.Text = AbacateOvoPrice.ToString();
                }
            }
        }
        private void CarilGambas_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "CarilGambasRecipe";
            CurrentRecipe = CarilGambas_Label.Text;
            CurrentPrice = CarilGambasPrice;
            x = 2;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        CarilGambasPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= CarilGambasPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        CarilGambasPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void CarilGambasPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = CarilGambas_Label.Text;
            CurrentPrice = CarilGambasPrice;
            x = 2;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    CarilGambasPrice_Label.Text = CarilGambasPrice.ToString();
                }
            }
        }
        private void Douradinhos_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "DouradinhosRecipe";
            CurrentRecipe = Douradinhos_Label.Text;
            CurrentPrice = DouradinhosPrice;
            x = 3;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        DouradinhosPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= DouradinhosPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        DouradinhosPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void DouradinhosPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = Douradinhos_Label.Text;
            CurrentPrice = DouradinhosPrice;
            x = 3;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    DouradinhosPrice_Label.Text = DouradinhosPrice.ToString();
                }
            }
        }
        private void EspinafreAtum_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "EspinafreAtumRecipe";
            CurrentRecipe = EspinafreAtum_Label.Text;
            CurrentPrice = EspinafreAtumPrice;
            x = 4;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        EspinafreAtumPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= EspinafreAtumPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        EspinafreAtumPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void EspinafreAtumPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = EspinafreAtum_Label.Text;
            CurrentPrice = EspinafreAtumPrice;
            x = 4;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    EspinafreAtumPrice_Label.Text = EspinafreAtumPrice.ToString();
                }
            }
        }
        private void HamburgerFrangoCogumelos_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "HamburgerFrangoCogumelosRecipe";
            CurrentRecipe = HamburgerFrangoCogumelos_Label.Text;
            CurrentPrice = HamburgerFrangoCogumelosPrice;
            x = 5;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        HamburgerFrangoCogumelosPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= HamburgerFrangoCogumelosPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        HamburgerFrangoCogumelosPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void HamburgerFrangoCogumelosPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = HamburgerFrangoCogumelos_Label.Text;
            CurrentPrice = HamburgerFrangoCogumelosPrice;
            x = 5;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    HamburgerFrangoCogumelosPrice_Label.Text = HamburgerFrangoCogumelosPrice.ToString();
                }
            }
        }
        private void Kebab_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "KebabRecipe";
            CurrentRecipe = Kebab_Label.Text;
            CurrentPrice = KebabPrice;
            x = 6;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        KebabPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= KebabPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        KebabPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void KebabPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = Kebab_Label.Text;
            CurrentPrice = KebabPrice;
            x = 6;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    KebabPrice_Label.Text = KebabPrice.ToString();
                }
            }
        }
        private void MousseChocolateAbacate_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "MousseChocolateAbacateRecipe";
            CurrentRecipe = MousseChocolateAbacate_Label.Text;
            CurrentPrice = MousseChocolateAbacatePrice;
            x = 7;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        MousseChocolateAbacatePrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= MousseChocolateAbacatePrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        MousseChocolateAbacatePrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void MousseChocolateAbacatePrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = MousseChocolateAbacate_Label.Text;
            CurrentPrice = MousseChocolateAbacatePrice;
            x = 7;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    MousseChocolateAbacatePrice_Label.Text = MousseChocolateAbacatePrice.ToString();
                }
            }
        }
        private void PenneCaprese_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "PenneCapreseRecipe";
            CurrentRecipe = PenneCaprese_Label.Text;
            CurrentPrice = PenneCapresePrice;
            x = 8;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        PenneCapresePrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= PenneCapresePrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        PenneCapresePrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void PenneCapresePrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = PenneCaprese_Label.Text;
            CurrentPrice = PenneCapresePrice;
            x = 8;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    PenneCapresePrice_Label.Text = PenneCapresePrice.ToString();
                }
            }
        }
        private void PudimChia_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "PudimChiaRecipe";
            CurrentRecipe = PudimChia_Label.Text;
            CurrentPrice = PudimChiaPrice;
            x = 9;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        PudimChiaPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= PudimChiaPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        PudimChiaPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void PudimChiaPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = PudimChia_Label.Text;
            CurrentPrice = PudimChiaPrice;
            x = 9;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    PudimChiaPrice_Label.Text = PudimChiaPrice.ToString();
                }
            }
        }
        private void QuinoaFrango_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "QuinoaFrangoRecipe";
            CurrentRecipe = QuinoaFrango_Label.Text;
            CurrentPrice = QuinoaFrangoPrice;
            x = 10;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        QuinoaFrangoPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= QuinoaFrangoPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        QuinoaFrangoPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void QuinoaFrangoPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = QuinoaFrango_Label.Text;
            CurrentPrice = QuinoaFrangoPrice;
            x = 10;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    QuinoaFrangoPrice_Label.Text = QuinoaFrangoPrice.ToString();
                }
            }
        }
        private void SaladaAtum_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "SaladaAtumRecipe";
            CurrentRecipe = SaladaAtum_Label.Text;
            CurrentPrice = SaladaAtumPrice;
            x = 11;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        SaladaAtumPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= SaladaAtumPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        SaladaAtumPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void SaladaAtumPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = SaladaAtum_Label.Text;
            CurrentPrice = SaladaAtumPrice;
            x = 11;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    SaladaAtumPrice_Label.Text = SaladaAtumPrice.ToString();
                }
            }
        }
        private void SojaBolonhesa_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "SojaBolonhesaRecipe";
            CurrentRecipe = SojaBolonhesa_Label.Text;
            CurrentPrice = SojaBolonhesaPrice;
            x = 12;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        SojaBolonhesaPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= SojaBolonhesaPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        SojaBolonhesaPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void SojaBolonhesaPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = SojaBolonhesa_Label.Text;
            CurrentPrice = SojaBolonhesaPrice;
            x = 12;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    SojaBolonhesaPrice_Label.Text = SojaBolonhesaPrice.ToString();
                }
            }
        }
        private void SopaLentilhas_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "SopaLentilhasRecipe";
            CurrentRecipe = SopaLentilhas_Label.Text;
            CurrentPrice = SopaLentilhasPrice;
            x = 13;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        SopaLentilhasPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= SopaLentilhasPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        SopaLentilhasPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void SopaLentilhasPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = SopaLentilhas_Label.Text;
            CurrentPrice = SopaLentilhasPrice;
            x = 13;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    SopaLentilhasPrice_Label.Text = SopaLentilhasPrice.ToString();
                }
            }
        }
        private void TofuBrás_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "TofuBrásRecipe";
            CurrentRecipe = TofuBrás_Label.Text;
            CurrentPrice = TofuBrásPrice;
            x = 14;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        TofuBrásPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= TofuBrásPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        TofuBrásPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void TofuBrásPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = TofuBrás_Label.Text;
            CurrentPrice = TofuBrásPrice;
            x = 14;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
                if (WantRevoke == DialogResult.Yes)
                {
                    TofuBrásPrice_Label.Text = TofuBrásPrice.ToString();
                }
            }
        }
        private void Yakisoba_PictureBox_DoubleClick(object sender, EventArgs e)
        {
            RecipeName = "YakisobaRecipe";
            CurrentRecipe = Yakisoba_Label.Text;
            CurrentPrice = YakisobaPrice;
            x = 15;
            if (UserRecipes[x] != "1")
            {
                if (UserAccount[2] == "Admin")
                {
                    Admin();
                    if (AdminWantBuy == DialogResult.Yes)
                    {
                        YakisobaPrice_Label.Text = "Revoke";
                        Purchased();
                        return;
                    }
                }
                if (CurrentCoins >= YakisobaPrice)
                {
                    Available();
                    if (WantBuy == DialogResult.Yes)
                    {
                        YakisobaPrice_Label.Text = UserAccount[2] == "Admin" ? "Revoke" : String.Empty;
                        Purchased();
                    }
                }
                else
                {
                    MessageBox.Show("You do not possess enough funds for this recipe", "Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        private void YakisobaPrice_Label_Click(object sender, EventArgs e)
        {
            CurrentRecipe = Yakisoba_Label.Text;
            CurrentPrice = YakisobaPrice;
            x = 15;
            if (UserAccount[2] == "Admin" && UserRecipes[x] == "1")
            {
                Revoke();
  if (WantRevoke == DialogResult.Yes)
                {
                    YakisobaPrice_Label.Text = YakisobaPrice.ToString();
                }
            }
        }
        #endregion

        #region Default
        private void Default()
        {
            if (UserAccount[2] == "Admin")
            {
                AbacateOvo_Label.Text = "Abacate com Ovo";
                AbacateOvoPrice_Label.Text = UserRecipes[1] == "1" ? "Revoke" : AbacateOvoPrice.ToString();
                CarilGambas_Label.Text = "Caril com Gambas";
                CarilGambasPrice_Label.Text = UserRecipes[2] == "1" ? "Revoke" : CarilGambasPrice.ToString();
                Douradinhos_Label.Text = "Douradinhos sem Farinha";
                DouradinhosPrice_Label.Text = UserRecipes[3] == "1" ? "Revoke" : DouradinhosPrice.ToString();
                EspinafreAtum_Label.Text = "Rolo de Espinafre e Atum";
                EspinafreAtumPrice_Label.Text = UserRecipes[4] == "1" ? "Revoke" : EspinafreAtumPrice.ToString();
                HamburgerFrangoCogumelos_Label.Text = "Hamburger de Frango e Cogumelos";
                HamburgerFrangoCogumelosPrice_Label.Text = UserRecipes[5] == "1" ? "Revoke" : HamburgerFrangoCogumelosPrice.ToString();
                Kebab_Label.Text = "Kebab";
                KebabPrice_Label.Text = UserRecipes[6] == "1" ? "Revoke" : KebabPrice.ToString();
                MousseChocolateAbacate_Label.Text = "Mousse de Chocolate e Abacate";
                MousseChocolateAbacatePrice_Label.Text = UserRecipes[7] == "1" ? "Revoke" : MousseChocolateAbacatePrice.ToString();
                PenneCaprese_Label.Text = "Penne Caprese";
                PenneCapresePrice_Label.Text = UserRecipes[8] == "1" ? "Revoke" : PenneCapresePrice.ToString();
                PudimChia_Label.Text = "Pudim de Chia";
                PudimChiaPrice_Label.Text = UserRecipes[9] == "1" ? "Revoke" : PudimChiaPrice.ToString();
                QuinoaFrango_Label.Text = "Quinoa com Frango";
                QuinoaFrangoPrice_Label.Text = UserRecipes[10] == "1" ? "Revoke" : QuinoaFrangoPrice.ToString();
                SaladaAtum_Label.Text = "Salada de Atum";
                SaladaAtumPrice_Label.Text = UserRecipes[11] == "1" ? "Revoke" : SaladaAtumPrice.ToString();
                SojaBolonhesa_Label.Text = "Soja à Bolonhesa";
                SojaBolonhesaPrice_Label.Text = UserRecipes[12] == "1" ? "Revoke" : SojaBolonhesaPrice.ToString();
                SopaLentilhas_Label.Text = "Sopa de Lentilhas";
                SopaLentilhasPrice_Label.Text = UserRecipes[13] == "1" ? "Revoke" : SopaLentilhasPrice.ToString();
                TofuBrás_Label.Text = "Tofu à Brás";
                TofuBrásPrice_Label.Text = UserRecipes[14] == "1" ? "Revoke" : TofuBrásPrice.ToString();
                Yakisoba_Label.Text = "Yakisoba";
                YakisobaPrice_Label.Text = UserRecipes[15] == "1" ? "Revoke" : YakisobaPrice.ToString();
            }
            if (UserAccount[2] == "User")
            {
                AbacateOvo_Label.Text = "Abacate com Ovo";
                AbacateOvoPrice_Label.Text = UserRecipes[1] == "1" ? String.Empty : AbacateOvoPrice.ToString();  
                CarilGambas_Label.Text = "Caril com Gambas";
                CarilGambasPrice_Label.Text = UserRecipes[2] == "1" ? String.Empty : CarilGambasPrice.ToString();
                Douradinhos_Label.Text = "Douradinhos sem Farinha";
                DouradinhosPrice_Label.Text = UserRecipes[3] == "1" ? String.Empty : DouradinhosPrice.ToString();
                EspinafreAtum_Label.Text = "Rolo de Espinafre e Atum";
                EspinafreAtumPrice_Label.Text = UserRecipes[4] == "1" ? String.Empty : EspinafreAtumPrice.ToString();
                HamburgerFrangoCogumelos_Label.Text = "Hamburger de Frango e Cogumelos";
                HamburgerFrangoCogumelosPrice_Label.Text = UserRecipes[5] == "1" ? String.Empty : HamburgerFrangoCogumelosPrice.ToString();
                Kebab_Label.Text = "Kebab";
                KebabPrice_Label.Text = UserRecipes[6] == "1" ? String.Empty : KebabPrice.ToString();
                MousseChocolateAbacate_Label.Text = "Mousse de Chocolate e Abacate";
                MousseChocolateAbacatePrice_Label.Text = UserRecipes[7] == "1" ? String.Empty : MousseChocolateAbacatePrice.ToString();
                PenneCaprese_Label.Text = "Penne Caprese";
                PenneCapresePrice_Label.Text = UserRecipes[8] == "1" ? String.Empty : PenneCapresePrice.ToString();
                PudimChia_Label.Text = "Pudim de Chia";
                PudimChiaPrice_Label.Text = UserRecipes[9] == "1" ? String.Empty : PudimChiaPrice.ToString();
                QuinoaFrango_Label.Text = "Quinoa com Frango";
                QuinoaFrangoPrice_Label.Text = UserRecipes[10] == "1" ? String.Empty : QuinoaFrangoPrice.ToString();
                SaladaAtum_Label.Text = "Salada de Atum";
                SaladaAtumPrice_Label.Text = UserRecipes[11] == "1" ? String.Empty : SaladaAtumPrice.ToString();
                SojaBolonhesa_Label.Text = "Soja à Bolonhesa";
                SojaBolonhesaPrice_Label.Text = UserRecipes[12] == "1" ? String.Empty : SojaBolonhesaPrice.ToString();
                SopaLentilhas_Label.Text = "Sopa de Lentilhas";
                SopaLentilhasPrice_Label.Text = UserRecipes[13] == "1" ? String.Empty : SopaLentilhasPrice.ToString();
                TofuBrás_Label.Text = "Tofu à Brás";
                TofuBrásPrice_Label.Text = UserRecipes[14] == "1" ? String.Empty : TofuBrásPrice.ToString();
                Yakisoba_Label.Text = "Yakisoba";
                YakisobaPrice_Label.Text = UserRecipes[15] == "1" ? String.Empty : YakisobaPrice.ToString();
            }
        }
        #endregion

        #region Purchase
        private void Admin()
        {
            AdminWantBuy = MessageBox.Show($"Get {CurrentRecipe} for free", "Admin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (AdminWantBuy == DialogResult.Yes)
            {
                string AllRecipes = File.ReadAllText(Recipes);
                string OldInfo = UserRecipes[0];
                for (int i = 1; i <= x; i++)
                {
                    OldInfo += $"|{UserRecipes[i]}";
                }
                string NewInfo = UserRecipes[0];
                for (int i = 1; i < x; i++)
                {
                    NewInfo += $"|{UserRecipes[i]}";
                }
                NewInfo += "|1";
                string AllUpdatedRecipes = AllRecipes.Replace(OldInfo, NewInfo);
                File.WriteAllText(Recipes, AllUpdatedRecipes);
                UserRecipes[x] = "1";
            }
        }
        private void Available()
        {
            WantBuy = MessageBox.Show($"Are you sure you want to buy {CurrentRecipe} for {CurrentPrice}$", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (WantBuy == DialogResult.Yes)
            {
                string AllRecipes = File.ReadAllText(Recipes);
                string OldInfo = UserRecipes[0];
                for (int i = 1; i <= x; i++)
                {
                    OldInfo += $"|{UserRecipes[i]}";
                }
                string NewInfo = UserRecipes[0];
                for (int i = 1; i < x; i++)
                {
                    NewInfo += $"|{UserRecipes[i]}";
                }
                NewInfo += "|1";
                string AllUpdatedRecipes = AllRecipes.Replace(OldInfo, NewInfo);
                File.WriteAllText(Recipes, AllUpdatedRecipes);
                UserRecipes[x] = "1";
                CurrentCoins -= CurrentPrice;
                string AllCoins = File.ReadAllText(GameInfo);
                string AllUpdatedCoins = AllCoins.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}", $"{UserGameInfo[0]}|{CurrentCoins}");
                File.WriteAllText(GameInfo, AllUpdatedCoins);
                UserGameInfo[1] = CurrentCoins.ToString();
                Recipes_Coins_Label.Text = UserGameInfo[1];
            }
        }
        private void Revoke()
        {
            WantRevoke = MessageBox.Show($"Are you sure you want to revoke the purchase of {CurrentRecipe}", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (WantRevoke == DialogResult.Yes)
            {
                DialogResult WantRefund = MessageBox.Show($"Get a refund of {CurrentPrice}$", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (WantRefund == DialogResult.Yes)
                {
                    CurrentCoins += CurrentPrice;
                    string AllCoins = File.ReadAllText(GameInfo);
                    string AllUpdatedCoins = AllCoins.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}", $"{UserGameInfo[0]}|{CurrentCoins}");
                    File.WriteAllText(GameInfo, AllUpdatedCoins);
                    UserGameInfo[1] = CurrentCoins.ToString();
                    Recipes_Coins_Label.Text = UserGameInfo[1];
                }
                string AllRecipes = File.ReadAllText(Recipes);
                string OldInfo = UserRecipes[0];
                for (int i = 1; i <= x; i++)
                {
                    OldInfo += $"|{UserRecipes[i]}";
                }
                string NewInfo = UserRecipes[0];
                for (int i = 1; i < x; i++)
                {
                    NewInfo += $"|{UserRecipes[i]}";
                }
                NewInfo += "|0";
                string AllUpdatedRecipes = AllRecipes.Replace(OldInfo, NewInfo);
                File.WriteAllText(Recipes, AllUpdatedRecipes);
                UserRecipes[x] = "0";
            }
        }
        private void Purchased()
        {
            DialogResult Bought = MessageBox.Show($"You bought the {CurrentRecipe} recipe", "Successful", MessageBoxButtons.OK);
            if (Bought == DialogResult.OK)
            {
                Recipe Form = new Recipe(RecipeName);
                Form.Show();
            }
        }
        #endregion

        #endregion

        #region Account Tab
        private void Account_Tab_Enter(object sender, EventArgs e)
        {
            FrameDimension Dimension = new FrameDimension(LockerGIF_PictureBox.Image.FrameDimensionsList[0]);
            Frames = LockerGIF_PictureBox.Image.GetFrameCount(Dimension);
            CurrentFrame = 0;
            Locker_Panel.Visible = true;
            Locker_Panel.Size = Account_Tab.Size;
            LockerGIF_PictureBox.Image.SelectActiveFrame(Dimension, 0);
            LockerGIF_PictureBox.Enabled = false;
            LockerGIF_PictureBox.Visible = true;
            LockerGIF_PictureBox.Size = Account_Tab.Size;
            Password_TextBox.Visible = true;
            Password_TextBox.PasswordChar = (char)0;
            Account_Username_TextBox.Enabled = false;
            Account_Username_TextBox.Font = Special;
            Account_NewPassword_TextBox.Enabled = false;
            Account_NewPassword_TextBox.Font = Special;
            ReOrderUp();
        }
        private void AccountHelp_Button_Click(object sender, EventArgs e)
        {
            InfoPage = 4;
            TabControl.SelectTab(4);
        }

        #region Current Data

        #region GIF
        private void LockerGIF_PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (CurrentFrame == Frames)
            {
                LockerGIF_PictureBox.Enabled = false;
                Locker_Panel.Visible = false;
            }
            CurrentFrame++;
        }
        #endregion

        #region Password
        private void Password_TextBox_Enter(object sender, EventArgs e)
        {
            if (Password_TextBox.Text == Password)
            {
                Password_TextBox.PasswordChar = (char)0x25CF;
                Password_TextBox.Text = String.Empty;
                Password_TextBox.ForeColor = Color.Black;
                Password_TextBox.Font = Normal;
            }
        }
        private void Password_TextBox_Leave(object sender, EventArgs e)
        {
            if (Password_TextBox.Text == String.Empty)
            {
                Password_TextBox.PasswordChar = (char)0;
                Password_TextBox.Text = Password;
                Password_TextBox.ForeColor = Color.Gray;
                Password_TextBox.Font = Special;
            }
        }
        private void Password_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
        }
        private void Password_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (Encrypter.EncryptString(Password_TextBox.Text).Replace('|', '_').Replace('\n', '_').Replace('\r', '_') == UserAccount[1])
            {
                Account_Username_TextBox.Text = UserAccount[0];
                DecryptedPassword = Password_TextBox.Text;
                Account_NewPassword_TextBox.Text = DecryptedPassword;
                LockerGIF_PictureBox.Enabled = true;
                Password_TextBox.Visible = false;
                LockerGIF_PictureBox.Invalidate();
            }
        }
        #endregion

        #endregion

        #region New Data

        #region Username
        private void Account_Username_Button_Click(object sender, EventArgs e)
        {
            Account_Username_TextBox.Enabled = true;
            Account_Username_TextBox.Font = Normal;
            Username_Error.SetError(Account_Username_TextBox, null);
        }
        private void Account_Username_TextBox_Leave(object sender, EventArgs e)
        {
            string[] AllAccounts = File.ReadAllLines(Accounts);
            IEnumerable<string> AccountFound = AllAccounts.Where(x => x.Split('|')[0] == Account_Username_TextBox.Text);
            Account_Username_TextBox.Enabled = false;
            Account_Username_TextBox.Font = Special;
            if (Account_Username_TextBox.Text == String.Empty)
            {
                Account_Username_TextBox.Text = UserAccount[0];
                Username_Error.SetError(Account_Username_TextBox, "Username cannot be blank");
            }
            else
            {
                if (AccountFound.Any() && Account_Username_TextBox.Text != UserAccount[0])
                {
                    Username_Error.SetError(Account_Username_TextBox, "Username already exists");
                }
                if (Account_Username_TextBox.Text.Length < 5)
                {
                    Username_Error.SetError(Account_Username_TextBox, "Username must contain more than 5 characters");
                }
            }
        }
        private void Account_Username_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
        #endregion

        #region New Password
        private void Account_NewPassword_Button_Click(object sender, EventArgs e)
        {
            if (!Account_ConfirmNewPassword_TextBox.Visible)
            {
                Account_ConfirmNewPassword_TextBox.Text = String.Empty;
            }
            ReOrderDown();
            Account_NewPassword_TextBox.Enabled = true;
            Account_NewPassword_TextBox.Font = Normal;
            NewPassword_Error.SetError(Account_NewPassword_TextBox, null);
            Account_ConfirmNewPassword_TextBox.Enabled = true;
            Account_ConfirmNewPassword_TextBox.Font = Normal;
            ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, null);
        }
        private void Account_NewPassword_TextBox_Leave(object sender, EventArgs e)
        {
            Account_NewPassword_TextBox.Enabled = false;
            Account_NewPassword_TextBox.Font = Special;
            if (Account_NewPassword_TextBox.Text == Account_ConfirmNewPassword_TextBox.Text)
            {
                ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, null);
                Account_ConfirmNewPassword_TextBox.Enabled = false;
            }
            if (Account_NewPassword_TextBox.Text == String.Empty)
            {
                Account_NewPassword_TextBox.Text = DecryptedPassword;
                NewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, "Password cannot be blank");
                ReOrderUp();
            }
            else
            {
                if (Account_NewPassword_TextBox.Text == DecryptedPassword)
                {
                    ReOrderUp();
                }
                else if (Account_NewPassword_TextBox.Text.Length < 8)
                {
                    NewPassword_Error.SetError(Account_NewPassword_TextBox, "Password must contain more than 8 characters");
                    ReOrderUp();
                }
                else if (!Account_NewPassword_TextBox.Text.Any(char.IsLower))
                {
                    NewPassword_Error.SetError(Account_NewPassword_TextBox, "Password must contain a lower case letter");
                    ReOrderUp();
                }
                else if (!Account_NewPassword_TextBox.Text.Any(char.IsUpper))
                {
                    NewPassword_Error.SetError(Account_NewPassword_TextBox, "Password must contain an upper case letter");
                    ReOrderUp();
                }
                else if (Account_NewPassword_TextBox.Text.All(char.IsLetter))
                {
                    NewPassword_Error.SetError(Account_NewPassword_TextBox, "Password must contain a digit or symbol");
                    ReOrderUp();
                }
                else if (Account_ConfirmNewPassword_TextBox.Text != Account_NewPassword_TextBox.Text && Account_ConfirmNewPassword_TextBox.Text != String.Empty)
                {
                    ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, "Does not match the new password");
                }
            }
        }
        private void Account_NewPassword_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
        }
        #endregion

        #region Confirm New Password
        private void Account_ConfirmNewPassword_TextBox_Leave(object sender, EventArgs e)
        {
            if (Account_ConfirmNewPassword_TextBox.Text == Account_NewPassword_TextBox.Text)
            {
                Account_ConfirmNewPassword_TextBox.Enabled = false;
                Account_ConfirmNewPassword_TextBox.Font = Special;
            }
            else
            {
                ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, "Does not match the new password");
            }
        }
        private void Account_ConfirmNewPassword_TextBox_Enter(object sender, EventArgs e)
        {
            if (Account_NewPassword_TextBox.Text == DecryptedPassword)
            {
                Account_NewPassword_TextBox.Enabled = false;
                Account_NewPassword_TextBox.Font = Special;
                ReOrderUp();
            }
        }
        private void Account_ConfirmNewPassword_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
            ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, null);
        }
        #endregion

        #region ReOrders
        private void ReOrderUp()
        {
            SaveChanges_Button.Top = 280;
            Account_ConfirmNewPassword_Label.Visible = false;
            Account_ConfirmNewPassword_TextBox.Visible = false;
            Line3.Visible = false;
        }
        private void ReOrderDown()
        {
            SaveChanges_Button.Top = 350;
            Account_ConfirmNewPassword_Label.Visible = true;
            Account_ConfirmNewPassword_TextBox.Visible = true;
            Line3.Visible = true;
        }
        #endregion

        #endregion

        #region Save Changes
        private void SaveChanges_Button_Click(object sender, EventArgs e)
        {
            string[] AllAccountsCheck = File.ReadAllLines(Accounts);
            IEnumerable<string> AccountFound = AllAccountsCheck.Where(x => x.Split('|')[0] == Account_Username_TextBox.Text);
            bool UsernameNotReady = Account_Username_TextBox.Text == String.Empty || Account_Username_TextBox.Text.Length < 5 || AccountFound.Any();
            bool PasswordNotReady = Account_NewPassword_TextBox.Text == String.Empty || Account_NewPassword_TextBox.Text.Length < 8 || !Account_NewPassword_TextBox.Text.Any(char.IsLower) || !Account_NewPassword_TextBox.Text.Any(char.IsUpper) || Account_NewPassword_TextBox.Text.All(char.IsLetter);
            bool ConfirmPasswordNotReady = (Account_ConfirmNewPassword_TextBox.Text != Account_NewPassword_TextBox.Text) && Account_ConfirmNewPassword_TextBox.Visible;
            if (UsernameNotReady || PasswordNotReady || ConfirmPasswordNotReady)
            {
                MessageBox.Show("Information provided does not meet the requirements", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string AllAccounts = File.ReadAllText(Accounts);
            string UpdatedAccount = AllAccounts.Replace($"{UserAccount[0]}|{UserAccount[1]}", $"{Account_Username_TextBox.Text}|{Encrypter.EncryptString(Account_NewPassword_TextBox.Text).Replace('|', '_').Replace('\n', '_').Replace('\r', '_')}");
            string AllGameInfos = File.ReadAllText(GameInfo);
            string UpdatedGameInfo = AllGameInfos.Replace(UserGameInfo[0], Account_Username_TextBox.Text);
            string AllRecipes = File.ReadAllText(Recipes);
            string UpdatedRecipes = AllRecipes.Replace(UserRecipes[0], Account_Username_TextBox.Text);
            DialogResult Message = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Message == DialogResult.Yes)
            {
                File.WriteAllText(Accounts, UpdatedAccount);
                File.WriteAllText(GameInfo, UpdatedGameInfo);
                File.WriteAllText(Recipes, UpdatedRecipes);
                UserAccount[0] = Account_Username_TextBox.Text;
                UserAccount[1] = Encrypter.EncryptString(Account_NewPassword_TextBox.Text).Replace('|', '_').Replace('\n', '_').Replace('\r', '_');
                UserGameInfo[0] = Account_Username_TextBox.Text;
                UserRecipes[0] = Account_Username_TextBox.Text;
                Message = MessageBox.Show("Account Updated Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Message == DialogResult.OK)
                {
                    TabControl.SelectTab(0);
                }
            }
        }
        #endregion

        private void Account_Tab_Leave(object sender, EventArgs e)
        {
            Password_TextBox.Text = Password;
            Password_TextBox.ForeColor = Color.Gray;
            Password_TextBox.Font = Special;
            Username_Error.SetError(Account_Username_TextBox, null);
            NewPassword_Error.SetError(Account_NewPassword_TextBox, null);
            ConfirmNewPassword_Error.SetError(Account_ConfirmNewPassword_TextBox, null);
        }
        #endregion

        #region Information Tab
        private void Info_Tab_Enter(object sender, EventArgs e)
        {
            InfoDefault();
            if (InfoPage == 1)
            {
                InfoSusEN_Panel.Visible = true;
                LeftArrow_Button.BackgroundImage = Resources.Left_Arrow_Disabled;
                LeftArrow_Button.Enabled = false;
            }
            if (InfoPage == 2)
            {
                InfoGame_Panel.Visible = true;
            }
            if (InfoPage == 3)
            {
                InfoRecipes_Panel.Visible = true;
            }
            if (InfoPage == 4)
            {
                InfoAccount_Panel.Visible = true;
            }
        }
        private void Info_Tab_Leave(object sender, EventArgs e)
        {
            InfoPage = 1;
            InfoDefault();
        }

        #region Arrows
        private void LeftArrow_Button_Click(object sender, EventArgs e)
        {
            InfoPage--;
            if (InfoPage == 1)
            {
                InfoGame_Panel.Visible = false;
                InfoSusEN_Panel.Visible = true;
                LeftArrow_Button.BackgroundImage = Resources.Left_Arrow_Disabled;
                LeftArrow_Button.Enabled = false;
                return;
            }
            if (InfoPage == 2)
            {
                InfoRecipes_Panel.Visible = false;
                InfoGame_Panel.Visible = true;
                return;
            }
            if (InfoPage == 3)
            {
                InfoAccount_Panel.Visible = false;
                InfoRecipes_Panel.Visible = true;
                return;
            }
            if (InfoPage == 4)
            {
                InfoBMI_Panel.Visible = false;
                InfoAccount_Panel.Visible = true;
                RightArrow_Button.BackgroundImage = Resources.Right_Arrow;
                RightArrow_Button.Enabled = true;
                return;
            }
        }
        private void RightArrow_Button_Click(object sender, EventArgs e)
        {
            InfoPage++;
            if(InfoPage == 2)
            {
                InfoSusEN_Panel.Visible = false;
                InfoGame_Panel.Visible = true;
                LeftArrow_Button.BackgroundImage = Resources.Left_Arrow;
                LeftArrow_Button.Enabled = true;
                return;
            }
            if (InfoPage == 3)
            {
                InfoGame_Panel.Visible = false;
                InfoRecipes_Panel.Visible = true;
                return;
            }
            if (InfoPage == 4)
            {
                InfoRecipes_Panel.Visible = false;
                InfoAccount_Panel.Visible = true;
                return;
            }
            if (InfoPage == 5)
            {
                InfoAccount_Panel.Visible = false;
                InfoBMI_Panel.Visible = true;
                RightArrow_Button.BackgroundImage = Resources.Right_Arrow_Disabled;
                RightArrow_Button.Enabled = false;
                return;
            }
        }
        #endregion

        #region BMI

        #region Height
        private void InfoBMIHeight_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) || (InfoBMIHeight_TextBox.TextLength == 0 && e.KeyChar == '0');
        }
        private void InfoBMIHeight_TextBox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(InfoBMIHeight_TextBox.Text, out HeightValue);
            if (HeightValue == 0)
            {
                InfoBMIHeight_TextBox.Text = String.Empty;
                InfoBMIConfirm_Button.BackColor = Color.FromArgb(230, 50, 56);
                return;
            }
            if (WeightValue != 0)
            {
                InfoBMIConfirm_Button.BackColor = Color.LimeGreen;
            }
        }
        #endregion

        #region Weight
        private void InfoBMIWeight_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back) || (InfoBMIWeight_TextBox.TextLength == 0 && e.KeyChar == '0');
        }
        private void InfoBMIWeight_TextBox_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(InfoBMIWeight_TextBox.Text, out WeightValue);
            if (WeightValue == 0)
            {
                InfoBMIWeight_TextBox.Text = String.Empty;
                InfoBMIConfirm_Button.BackColor = Color.FromArgb(230, 50, 56);
                return;
            }
            if (HeightValue != 0)
            {
                InfoBMIConfirm_Button.BackColor = Color.LimeGreen;
            }
        }
        #endregion

        #region Buttons
        private void InfoBMIConfirm_Button_Click(object sender, EventArgs e)
        {
            if (HeightValue * WeightValue != 0)
            {
                double Meters = (double)HeightValue / 100;
                double BMI = WeightValue / (Meters * Meters);
                InfoBMIIndicator_Label.Text = Math.Round(BMI, 1).ToString();
                if (BMI < 16)
                {
                    InfoBMICategory_Label.Text = "Category: Severely Underweight";
                    InfoBMICategory_Label.BackColor = Color.DeepSkyBlue;
                    return;
                }
                if (BMI < 18.5)
                {
                    InfoBMICategory_Label.Text = "Category: Underweight";
                    InfoBMICategory_Label.BackColor = Color.Turquoise;
                    return;
                }
                if (BMI < 25)
                {
                    InfoBMICategory_Label.Text = "Category: Healthy";
                    InfoBMICategory_Label.BackColor = Color.Lime;
                    return;
                }
                if (BMI < 30)
                {
                    InfoBMICategory_Label.Text = "Category: Overweight";
                    InfoBMICategory_Label.BackColor = Color.Orange;
                    return;
                }
                InfoBMICategory_Label.BackColor = Color.Red;
                InfoBMICategory_Label.Text = "Category: Obese";
            }
            else
            {
                MessageBox.Show("Provide the required information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InfoBMIReset_Button_Click(object sender, EventArgs e)
        {
            InfoBMIHeight_TextBox.Text = String.Empty;
            InfoBMIWeight_TextBox.Text = String.Empty;
            InfoBMIIndicator_Label.Text = String.Empty;
            InfoBMICategory_Label.Text = String.Empty;
            InfoBMICategory_Label.BackColor = Color.Empty;
        }
        #endregion

        #endregion

        #region Default
        private void InfoDefault()
        {
            InfoSusEN_Panel.Visible = false;
            InfoGame_Panel.Visible = false;
            InfoRecipes_Panel.Visible = false;
            InfoAccount_Panel.Visible = false;
            InfoBMI_Panel.Visible = false;
            LeftArrow_Button.BackgroundImage = Resources.Left_Arrow;
            LeftArrow_Button.Enabled = true;
            RightArrow_Button.BackgroundImage = Resources.Right_Arrow;
            RightArrow_Button.Enabled = true;
        }
        #endregion

        #endregion

        #region Admin Tab
        private void Admin_Tab_Enter(object sender, EventArgs e)
        {
            Admin_AdminPassword_TextBox.PasswordChar = (char)0x25CF;
            if (UserAccount[2] == "User")
            {
                Admin_User_Panel.Visible = true;
                Admin_Admin_Panel.Visible = false;
            }
            if (UserAccount[2] == "Admin")
            {
                Admin_User_Panel.Visible = false;
                Admin_Admin_Panel.Visible = true;
            }
            Admin_Coins_TextBox.Text = UserGameInfo[1];
            Admin_Record_TextBox.Text = UserGameInfo[2];
        }
        private void Admin_AdminPassword_TextBox_TextChanged(object sender, EventArgs e)
        {
            if (Encrypter.EncryptString(Admin_AdminPassword_TextBox.Text) == AdminPassword)
            {
                Admin_AdminPassword_TextBox.Text = String.Empty;
                Admin_Admin_Panel.Visible = true;
                Admin_User_Panel.Visible = false;
                string AllAccountsInfo = File.ReadAllText(Accounts);
                string UpdatedAccountsInfo = AllAccountsInfo.Replace($"{UserAccount[0]}|{UserAccount[1]}|{UserAccount[2]}", $"{UserAccount[0]}|{UserAccount[1]}|Admin");
                File.WriteAllText(Accounts, UpdatedAccountsInfo);
                UserAccount[2] = "Admin";
            }
        }

        #region KeyPresses
        private void Admin_AdminPassword_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Space;
        }
        private void Admin_Coins_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
        private void Admin_Record_TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }
        #endregion
        
        #region New Data
        private void Admin_CoinSave_Button_Click(object sender, EventArgs e)
        {
            string AllGameInfo = File.ReadAllText(GameInfo);
            string UpdatedGameInfo = AllGameInfo.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}", $"{UserGameInfo[0]}|{Admin_Coins_TextBox.Text}");
            File.WriteAllText(GameInfo, UpdatedGameInfo);
            UserGameInfo[1] = Admin_Coins_TextBox.Text;
        }
        private void Admin_RecordSave_Button_Click(object sender, EventArgs e)
        {
            string AllUpdatedGameInfo = File.ReadAllText(GameInfo);
            string UpdatedRecord = AllUpdatedGameInfo.Replace($"{UserGameInfo[0]}|{UserGameInfo[1]}|{UserGameInfo[2]}", $"{UserGameInfo[0]}|{UserGameInfo[1]}|{Admin_Record_TextBox.Text}");
            File.WriteAllText(GameInfo, UpdatedRecord);
            UserGameInfo[2] = Admin_Record_TextBox.Text;
        }
        #endregion

        private void Admin_AbandonAdmin_Button_Click(object sender, EventArgs e)
        {
            Admin_User_Panel.Visible = true;
            Admin_Admin_Panel.Visible = false;
            string AllAccountsInfo = File.ReadAllText(Accounts);
            string UpdatedAccountsInfo = AllAccountsInfo.Replace($"{UserAccount[0]}|{UserAccount[1]}|{UserAccount[2]}", $"{UserAccount[0]}|{UserAccount[1]}|User");
            File.WriteAllText(Accounts, UpdatedAccountsInfo);
            UserAccount[2] = "User";
        }
        #endregion

        #region Log Out
        private void LogOut_Button_Click(object sender, EventArgs e)
        {
            Home Form = new Home();
            Form.Show();
            Hide();
        }
        #endregion
    }
}