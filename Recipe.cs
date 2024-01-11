using SusEN.Properties;
using System.Drawing;
using System.Windows.Forms;

namespace SusEN
{
    public partial class Recipe : Form
    {
        #region Global Variables
        private readonly string RecipeName;
        #endregion

        #region Constructor
        public Recipe(string Recipe)
        {
            InitializeComponent();
            RecipeName = Recipe;
            object ImageToShow = Resources.ResourceManager.GetObject(RecipeName);
            Recipe_PictureBox.Image = (Image)ImageToShow;
        }
        #endregion
    }
}