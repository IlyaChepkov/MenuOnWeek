namespace MenuOnWeek.Frontend.Recipe
{
    partial class RecipeControl
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
            RecipesList = new ListBox();
            AddButton = new Button();
            UpdateButton = new Button();
            SuspendLayout();
            // 
            // RecipesList
            // 
            RecipesList.FormattingEnabled = true;
            RecipesList.ItemHeight = 15;
            RecipesList.Location = new Point(25, 44);
            RecipesList.Name = "RecipesList";
            RecipesList.Size = new Size(120, 214);
            RecipesList.TabIndex = 0;
            RecipesList.SelectedIndexChanged += RecipesList_SelectedIndexChanged;
            RecipesList.KeyDown += RecipesList_KeyDown;
            // 
            // AddButton
            // 
            AddButton.Location = new Point(25, 15);
            AddButton.Name = "AddButton";
            AddButton.Size = new Size(57, 23);
            AddButton.TabIndex = 1;
            AddButton.Text = "+";
            AddButton.UseVisualStyleBackColor = true;
            AddButton.Click += AddButton_Click;
            // 
            // UpdateButton
            // 
            UpdateButton.Location = new Point(88, 15);
            UpdateButton.Name = "UpdateButton";
            UpdateButton.Size = new Size(57, 23);
            UpdateButton.TabIndex = 2;
            UpdateButton.Text = "Update";
            UpdateButton.UseVisualStyleBackColor = true;
            UpdateButton.Click += UpdateButton_Click;
            // 
            // RecipeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(RecipesList);
            Name = "RecipeControl";
            Size = new Size(1225, 539);
            ResumeLayout(false);
        }

        #endregion

        private ListBox RecipesList;
        private Button AddButton;
        private Button UpdateButton;
    }
}
