using MenuOnWeek.Clients.Menus;
using MenuOnWeek.Clients.Recipes;
using MenuOnWeek.Contracts.Menus;
using MenuOnWeek.Domain;
using MenuOnWeek.Domain.Menus;
using Microsoft.Extensions.DependencyInjection;
using Utils;

namespace MenuOnWeek.Frontend.Menu;

public partial class MenuForm : UserControl
{

    private IMenuClient menuClient;
    private IRecipeClient recipeClient;

    private List<DaysOfWeekLocalizationType> daysOfWeekList;
    private List<MealLocalizationType> mealList;

    private MenuType menuType;

    private MenuResponse currentMenu = new() {

        Id = Guid.Empty,
        Name = "",
        MenuType = MenuType.MenuOnWeek,
        Price = 0,
        MenuRecipes = []
    };

    public MenuForm()
    {

        menuClient = Program.ServiceProvider.GetRequiredService<IMenuClient>();
        recipeClient = Program.ServiceProvider.GetRequiredService<IRecipeClient>();

        daysOfWeekList = new List<DaysOfWeekLocalizationType>();
        foreach (DaysOfWeek dt in Enum.GetValues(typeof(DaysOfWeek)))
        {
            daysOfWeekList.Add(new DaysOfWeekLocalizationType(dt));
        }
        mealList = new List<MealLocalizationType>();
        foreach (Meal mt in Enum.GetValues(typeof(Meal)))
        {
            mealList.Add(new MealLocalizationType(mt));
        }

        menuType = MenuType.MenuOnWeek;

        InitializeComponent();
        MenuOnWeekButton.Checked = true;

        GridRefresh();
    }

    public MenuForm(MenuResponse menu)
    {
        menuClient = Program.ServiceProvider.GetRequiredService<IMenuClient>();
        recipeClient = Program.ServiceProvider.GetRequiredService<IRecipeClient>();

        currentMenu = menu;

        daysOfWeekList = new List<DaysOfWeekLocalizationType>();
        foreach (DaysOfWeek? dt in Enum.GetValues(typeof(DaysOfWeek)))
        {
            daysOfWeekList.Add(new DaysOfWeekLocalizationType(dt));
        }
        mealList = new List<MealLocalizationType>();
        foreach (Meal? mt in Enum.GetValues(typeof(Meal)))
        {
            mealList.Add(new MealLocalizationType(mt));
        }
        
        InitializeComponent();

        //currentMenu.Id = menu.Id;
        //currentMenu.Name = menu.Name;
        //currentMenu.Price = menu.Price;
        //currentMenu.Recipes = menu.Recipes.Select(x => x).ToList();

        MenuPrice.Text = menu.Price.ToString();

        MenuName.Text = menu.Name;
        menuType = menu.MenuType.Required();

        switch (menuType)
        {
            case MenuType.MenuOnWeek:
                {
                    MenuOnWeekButton.Checked = true;
                }
                break;

            case MenuType.MenuOnDay:
                {
                    MenuOnDayButton.Checked = true;
                }
                break;

            case MenuType.MenuOnEvent:
                {
                    MenuOnEventButton.Checked = true;
                }
                break;

        }

        GridRefresh();
    }

    private void GridRefresh()
    {

        RecipesTable.Rows.Clear();

        RecipesTable.Rows
            .AddRange(new DataGridViewRow[currentMenu.MenuRecipes.Required().Count]
                .Select(x => x = new DataGridViewRow()).ToArray());

        for (int i = 0; i < RecipesTable.Rows.Count; i++)
        {
            var recipeComboBoxCell = (RecipesTable.Rows[i].Cells[0] as DataGridViewComboBoxCell).Required();
            recipeComboBoxCell.DataSource = recipeClient
                .GetAll(0, 100, CancellationToken.None).Result.Required()
                .Select(x => x.Name)
                .ToList();

            var dateComboBoxCell = (RecipesTable.Rows[i].Cells[1] as DataGridViewComboBoxCell).Required();

            dateComboBoxCell.DataSource = daysOfWeekList.Select(x => x.ToString()).ToList();

            var mealComboBoxCell = (RecipesTable.Rows[i].Cells[2] as DataGridViewComboBoxCell).Required();

            mealComboBoxCell.DataSource = mealList.Select(x => x.ToString()).ToList();

            if (i + 1 < RecipesTable.Rows.Count)
            {
                var serveTextBoxCell = (RecipesTable.Rows[i].Cells[3] as DataGridViewTextBoxCell).Required();
                serveTextBoxCell.Value = currentMenu.MenuRecipes[i].Serve.Required();
                switch (menuType)
                {
                    case MenuType.MenuOnWeek:
                        {
                            if (currentMenu.MenuRecipes[i].DaysOfWeek is not null)
                            {
                                dateComboBoxCell.Value ??= daysOfWeekList.Single(x => x.DayName == new DaysOfWeekLocalizationType(currentMenu.MenuRecipes[i].DaysOfWeek).DayName)?.ToString();
                            }
                            if (currentMenu.MenuRecipes[i].Meal is not null)
                            {
                                mealComboBoxCell.Value ??= mealList.Single(x => x.MealName == new MealLocalizationType(currentMenu.MenuRecipes[i].Meal).MealName)?.ToString();
                            }
                            
                        }
                        break;

                    case MenuType.MenuOnDay:
                        {
                            if (currentMenu.MenuRecipes[i].Meal is not null)
                            {
                                mealComboBoxCell.Value ??= mealList.Single(x => x.MealName == new MealLocalizationType(currentMenu.MenuRecipes[i].Meal).MealName)?.ToString();
                            }
                        }
                        break;
                }
                recipeComboBoxCell.Value = recipeClient.GetById(currentMenu.MenuRecipes[i].RecipeId, CancellationToken.None).Result.Required().Name;
            }
        }
        switch (menuType)
        {
            case MenuType.MenuOnWeek:
                {
                    RecipesTable.Columns[1].Visible = true;
                    RecipesTable.Columns[2].Visible = true;
                }
                break;

            case MenuType.MenuOnDay:
                {
                    RecipesTable.Columns[1].Visible = false;
                    RecipesTable.Columns[2].Visible = true;
                }
                break;

            case MenuType.MenuOnEvent:
                {
                    RecipesTable.Columns[1].Visible = false;
                    RecipesTable.Columns[2].Visible = false;
                }
                break;
        }
    }

    private void RecipesTable_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
    {
        var recipeComboBoxCell = (RecipesTable.Rows[^1].Cells[0] as DataGridViewComboBoxCell).Required();
        recipeComboBoxCell.DataSource = recipeClient
            .GetAll(0, 100, CancellationToken.None).Result.Required()
            .Select(x => x.Name)
            .ToList();

        var dateComboBoxCell = (RecipesTable.Rows[^1].Cells[1] as DataGridViewComboBoxCell).Required();

        dateComboBoxCell.DataSource = daysOfWeekList.Select(x => x.ToString()).ToList();

        var mealComboBoxCell = (RecipesTable.Rows[^1].Cells[2] as DataGridViewComboBoxCell).Required();

        mealComboBoxCell.DataSource = mealList.Select(x => x.ToString()).ToList();
    }

    internal MenuDto GetMenuDto()
    {
        MenuDto menuDto = new(MenuName.Text, new List<MenuElementDto>(), menuType);

        for (int i = 0; i < RecipesTable.Rows.Count - 1; i++)
        {
            var cells = RecipesTable.Rows[i].Cells;

            DaysOfWeek? day = null;
            if (cells[1].Value is not null
                && daysOfWeekList.FirstOrDefault(cells[1].Value) is not null
                && menuType == MenuType.MenuOnWeek)
            {
                day = daysOfWeekList.FirstOrDefault(x => x.DayName == cells[1].Value.ToString()).Required().DaysOfWeekType;
            }

            Meal? meal = null;
            if (cells[2].Value is not null
                && mealList.FirstOrDefault(cells[2].Value) is not null
                && menuType != MenuType.MenuOnEvent)
            {
                meal = mealList.FirstOrDefault(x => x.MealName == cells[2].Value.ToString()).Required().MealType;
            }
            int count = 0;
            if (cells[3].Value is not null)
            {
               Int32.TryParse(cells[3].Value.ToString().Required(), out count);
            }
            menuDto.Recipes.Add(new MenuElementDto(
                recipeClient.GetByName(cells[0].Value.ToString().Required(), CancellationToken.None).Result.Required().Id.Required(),
                count,
                day,
                meal));
        }

        return menuDto;
    }

    private void MenuTypeClick(object sender, EventArgs e)
    {
        switch ((sender as RadioButton).Required().Name)
        {
            case "MenuOnWeekButton":
                {
                    menuType = MenuType.MenuOnWeek;
                }
                break;

            case "MenuOnDayButton":
                {
                    menuType = MenuType.MenuOnDay;
                }
                break;

            case "MenuOnEventButton":
                {
                    menuType = MenuType.MenuOnEvent;
                }
                break;
        }

        GridRefresh();
    }
}
