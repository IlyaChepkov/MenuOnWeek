﻿namespace MenuOnWeek.Frontend;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        menuStrip1 = new MenuStrip();
        menuListToolStripMenuItem = new ToolStripMenuItem();
        recipeListToolStripMenuItem = new ToolStripMenuItem();
        ingredientsListToolStripMenuItem = new ToolStripMenuItem();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // menuStrip1
        // 
        menuStrip1.Items.AddRange(new ToolStripItem[] { menuListToolStripMenuItem, recipeListToolStripMenuItem, ingredientsListToolStripMenuItem });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1097, 24);
        menuStrip1.TabIndex = 5;
        menuStrip1.Text = "menuStrip1";
        // 
        // menuListToolStripMenuItem
        // 
        menuListToolStripMenuItem.Name = "menuListToolStripMenuItem";
        menuListToolStripMenuItem.Size = new Size(91, 20);
        menuListToolStripMenuItem.Text = "Реестр меню";
        menuListToolStripMenuItem.Click += menuListToolStripMenuItem_Click;
        // 
        // recipeListToolStripMenuItem
        // 
        recipeListToolStripMenuItem.Name = "recipeListToolStripMenuItem";
        recipeListToolStripMenuItem.Size = new Size(110, 20);
        recipeListToolStripMenuItem.Text = "Реестр рецептов";
        recipeListToolStripMenuItem.Click += recipeListToolStripMenuItem_Click;
        // 
        // ingredientsListToolStripMenuItem
        // 
        ingredientsListToolStripMenuItem.Name = "ingredientsListToolStripMenuItem";
        ingredientsListToolStripMenuItem.Size = new Size(135, 20);
        ingredientsListToolStripMenuItem.Text = "Реестр ингредиентов";
        ingredientsListToolStripMenuItem.Click += ingredientsListToolStripMenuItem_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1097, 470);
        Controls.Add(menuStrip1);
        MainMenuStrip = menuStrip1;
        Name = "Form1";
        Text = "Form1";
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private MenuStrip menuStrip1;
    private ToolStripMenuItem menuListToolStripMenuItem;
    private ToolStripMenuItem recipeListToolStripMenuItem;
    private ToolStripMenuItem ingredientsListToolStripMenuItem;
}
