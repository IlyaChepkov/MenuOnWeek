namespace MenuOnWeek.Frontend.Menu
{
    partial class MenuForm
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            RecipesTable = new DataGridView();
            Recipe = new DataGridViewComboBoxColumn();
            Date = new DataGridViewComboBoxColumn();
            Meal = new DataGridViewComboBoxColumn();
            ServeCount = new DataGridViewTextBoxColumn();
            MenuPrice = new TextBox();
            MenuName = new TextBox();
            MenuOnWeekButton = new RadioButton();
            MenuOnDayButton = new RadioButton();
            MenuOnEventButton = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)RecipesTable).BeginInit();
            SuspendLayout();
            // 
            // RecipesTable
            // 
            RecipesTable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RecipesTable.Columns.AddRange(new DataGridViewColumn[] { Recipe, Date, Meal, ServeCount });
            RecipesTable.Location = new Point(143, 36);
            RecipesTable.Name = "RecipesTable";
            RecipesTable.Size = new Size(442, 245);
            RecipesTable.TabIndex = 0;
            RecipesTable.RowsAdded += RecipesTable_RowsAdded;
            // 
            // Recipe
            // 
            Recipe.HeaderText = "Рецепт";
            Recipe.Name = "Recipe";
            // 
            // Date
            // 
            Date.HeaderText = "День недели";
            Date.Name = "Date";
            // 
            // Meal
            // 
            Meal.HeaderText = "Прием пищи";
            Meal.Name = "Meal";
            // 
            // ServeCount
            // 
            ServeCount.HeaderText = "Количество порций";
            ServeCount.Name = "ServeCount";
            // 
            // MenuPrice
            // 
            MenuPrice.Location = new Point(277, 3);
            MenuPrice.Name = "MenuPrice";
            MenuPrice.ReadOnly = true;
            MenuPrice.Size = new Size(161, 23);
            MenuPrice.TabIndex = 1;
            // 
            // MenuName
            // 
            MenuName.Location = new Point(12, 3);
            MenuName.Name = "MenuName";
            MenuName.Size = new Size(259, 23);
            MenuName.TabIndex = 2;
            // 
            // MenuOnWeekButton
            // 
            MenuOnWeekButton.AutoSize = true;
            MenuOnWeekButton.Location = new Point(12, 37);
            MenuOnWeekButton.Name = "MenuOnWeekButton";
            MenuOnWeekButton.Size = new Size(120, 19);
            MenuOnWeekButton.TabIndex = 3;
            MenuOnWeekButton.Text = "Меню на неделю";
            MenuOnWeekButton.UseVisualStyleBackColor = true;
            MenuOnWeekButton.CheckedChanged += MenuTypeClick;
            // 
            // MenuOnDayButton
            // 
            MenuOnDayButton.AutoSize = true;
            MenuOnDayButton.Location = new Point(12, 62);
            MenuOnDayButton.Name = "MenuOnDayButton";
            MenuOnDayButton.Size = new Size(103, 19);
            MenuOnDayButton.TabIndex = 4;
            MenuOnDayButton.Text = "Меню на день";
            MenuOnDayButton.UseVisualStyleBackColor = true;
            MenuOnDayButton.CheckedChanged += MenuTypeClick;
            // 
            // MenuOnEventButton
            // 
            MenuOnEventButton.AutoSize = true;
            MenuOnEventButton.Location = new Point(12, 87);
            MenuOnEventButton.Name = "MenuOnEventButton";
            MenuOnEventButton.Size = new Size(125, 19);
            MenuOnEventButton.TabIndex = 5;
            MenuOnEventButton.Text = "Меню на событие";
            MenuOnEventButton.UseVisualStyleBackColor = true;
            MenuOnEventButton.CheckedChanged += MenuTypeClick;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MenuOnEventButton);
            Controls.Add(MenuOnDayButton);
            Controls.Add(MenuOnWeekButton);
            Controls.Add(MenuName);
            Controls.Add(MenuPrice);
            Controls.Add(RecipesTable);
            Name = "MenuForm";
            Size = new Size(603, 281);
            ((System.ComponentModel.ISupportInitialize)RecipesTable).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView RecipesTable;
        private DataGridViewComboBoxColumn Recipe;
        private DataGridViewComboBoxColumn Date;
        private DataGridViewComboBoxColumn Meal;
        private TextBox MenuPrice;
        private TextBox MenuName;
        private DataGridViewTextBoxColumn ServeCount;
        private RadioButton MenuOnWeekButton;
        private RadioButton MenuOnDayButton;
        private RadioButton MenuOnEventButton;
    }
}
