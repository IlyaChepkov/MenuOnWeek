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
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            statusStrip1.SuspendLayout();
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
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 517);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1225, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // RecipeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(UpdateButton);
            Controls.Add(AddButton);
            Controls.Add(RecipesList);
            Name = "RecipeControl";
            Size = new Size(1225, 539);
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox RecipesList;
        private Button AddButton;
        private Button UpdateButton;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}
