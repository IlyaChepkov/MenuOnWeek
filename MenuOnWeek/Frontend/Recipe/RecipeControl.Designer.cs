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
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // RecipesList
            // 
            RecipesList.FormattingEnabled = true;
            RecipesList.ItemHeight = 15;
            RecipesList.Location = new Point(25, 79);
            RecipesList.Name = "RecipesList";
            RecipesList.Size = new Size(120, 94);
            RecipesList.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(54, 50);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(155, 50);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // RecipeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(RecipesList);
            Name = "RecipeControl";
            Size = new Size(849, 539);
            ResumeLayout(false);
        }

        #endregion

        private ListBox RecipesList;
        private Button button1;
        private Button button2;
    }
}
