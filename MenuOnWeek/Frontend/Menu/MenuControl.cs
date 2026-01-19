using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MenuOnWeek.Clients.Menus;
using MenuOnWeek.Contracts.Menus;
using Microsoft.Extensions.DependencyInjection;
using MenuOnWeek.Domain.Menus;
using Utils;

namespace MenuOnWeek.Frontend.Menu
{
    public partial class MenuControl : UserControl
    {

        private IMenuClient menuclient;

        private MenuForm? menuForm;

        public MenuControl()
        {
            menuclient = Program.ServiceProvider.GetRequiredService<IMenuClient>();
            InitializeComponent();

            RefreshMenusList();
        }

        private void RefreshMenusList()
        {
            MenusList.Items.Clear();

            MenusList.Items.AddRange(menuclient.GetAll(0, 100, CancellationToken.None).Result.Required().Select(x => x.Name.Required()).OrderBy(x => x).ToArray());
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddMenuForm addMenuForm = new AddMenuForm();
            addMenuForm.ShowDialog();

            RefreshMenusList();

        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (menuForm is not null)
            {
                var menuDto = menuForm.GetMenuDto();
                if (String.IsNullOrEmpty(menuDto.Name))
                {
                    statusStrip1.Items[0].Text = "У меню нет имени";
                    return;
                }
                if (menuDto.Recipes.Any(x => (menuDto.MenuType == MenuType.MenuOnWeek & x.Date == null)
                    || (menuDto.MenuType != MenuType.MenuOnEvent & x.Meal == null) || (x.ServeCount == 0)))
                {
                    statusStrip1.Items[0].Text = "Заполнены не все ячейки таблицы";
                    return;
                }

                statusStrip1.Items[0].Text = "";

                var updateRequest = new MenuUpdateRequest()
                {
                    Id = menuclient.GetByName(MenusList.SelectedItem.Required().ToString().Required(), CancellationToken.None).Result.Required().Id,
                    Name = menuDto.Name,
                    MenuType = menuDto.MenuType,
                    MenuRecipes = menuDto.Recipes.Select(x => new MenuRecipesCreateOrUpdateRequest() { 
                        RecipeId = x.RecipeId,
                        Serve = x.ServeCount,
                        DaysOfWeek = x.Date,
                        Meal = x.Meal
                    }).ToList()
                };
                menuclient.Update(updateRequest, CancellationToken.None);

                Controls.Remove(menuForm);
                menuForm = new MenuForm(menuclient.GetByName(MenusList.SelectedItem.Required().ToString().Required(), CancellationToken.None).Result.Required());
                Controls.Add(menuForm);
                menuForm.Location = new Point(200, 5);
            }
        }

        private void MenusList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MenusList.SelectedItem is not null)
            {
                if (menuForm is not null)
                {
                    Controls.Remove(menuForm);
                }

                menuForm = new MenuForm(menuclient.GetByName(MenusList.SelectedItem.ToString().Required(), CancellationToken.None).Result.Required());
                Controls.Add(menuForm);

                menuForm.Location = new Point(200, 5);
            }
        }

        private void MenusList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MenusList.SelectedItem is not null)
                {
                    menuclient.Remove(menuclient.GetByName(MenusList.SelectedItem.ToString().Required(), CancellationToken.None).Result.Required().Id, CancellationToken.None).Required();
                    RefreshMenusList();
                    Controls.Remove(menuForm);
                    menuForm = null;
                }
            }
        }
    }
}
