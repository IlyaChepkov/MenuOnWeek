using MenuOnWeek.Frontend.Recipe;

namespace MenuOnWeek.Frontend;

public partial class Form1 : Form
{
    private Control? nowPanel = null;

    public Form1()
    {
        InitializeComponent();
    }

    private void ingredientsListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (nowPanel is not null)
        {
            Controls.Remove(nowPanel);
            nowPanel.Dispose();
        }
        nowPanel = new IngredientsControl();
        nowPanel.Location = new Point(0, 25);
        Controls.Add(nowPanel);
    }

    private void recipeListToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (nowPanel is not null)
        {
            Controls.Remove(nowPanel);
            nowPanel.Dispose();
        }
        nowPanel = new RecipeControl();
        nowPanel.Location = new Point(0, 25);
        Controls.Add(nowPanel);
    }
}
