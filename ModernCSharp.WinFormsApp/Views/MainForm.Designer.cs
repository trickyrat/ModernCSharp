namespace ModernCSharp.WinFormsApp.Views
{
    partial class MainForm
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
            importOrdersToolStripMenuItem = new ToolStripMenuItem();
            orderListToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { importOrdersToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // importOrdersToolStripMenuItem
            // 
            importOrdersToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { orderListToolStripMenuItem });
            importOrdersToolStripMenuItem.Name = "importOrdersToolStripMenuItem";
            importOrdersToolStripMenuItem.Size = new Size(54, 20);
            importOrdersToolStripMenuItem.Text = "Orders";
            // 
            // orderListToolStripMenuItem
            // 
            orderListToolStripMenuItem.Name = "orderListToolStripMenuItem";
            orderListToolStripMenuItem.Size = new Size(180, 22);
            orderListToolStripMenuItem.Text = "Order List";
            orderListToolStripMenuItem.Click += orderListToolStripMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "MainForm";
            Load += MainForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem importOrdersToolStripMenuItem;
        private ToolStripMenuItem orderListToolStripMenuItem;
    }
}
