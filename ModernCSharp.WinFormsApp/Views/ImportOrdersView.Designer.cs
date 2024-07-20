namespace ModernCSharp.WinFormsApp.Views
{
    partial class ImportOrdersView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            OpenOrderFilesButton = new Button();
            OrderFilePathTextBox = new TextBox();
            label1 = new Label();
            ImportOrderButton = new Button();
            openOrderFilesDialog = new OpenFileDialog();
            SuspendLayout();
            // 
            // OpenOrderFilesButton
            // 
            OpenOrderFilesButton.Location = new Point(28, 43);
            OpenOrderFilesButton.Name = "OpenOrderFilesButton";
            OpenOrderFilesButton.Size = new Size(75, 23);
            OpenOrderFilesButton.TabIndex = 0;
            OpenOrderFilesButton.Text = "Open";
            OpenOrderFilesButton.UseVisualStyleBackColor = true;
            OpenOrderFilesButton.Click += OpenOrderFilesButton_Click;
            // 
            // OrderFilePathTextBox
            // 
            OrderFilePathTextBox.Location = new Point(131, 96);
            OrderFilePathTextBox.Name = "OrderFilePathTextBox";
            OrderFilePathTextBox.Size = new Size(388, 23);
            OrderFilePathTextBox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 104);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 2;
            label1.Text = "Order file path:";
            // 
            // ImportOrderButton
            // 
            ImportOrderButton.Location = new Point(541, 96);
            ImportOrderButton.Name = "ImportOrderButton";
            ImportOrderButton.Size = new Size(75, 23);
            ImportOrderButton.TabIndex = 3;
            ImportOrderButton.Text = "Import";
            ImportOrderButton.UseVisualStyleBackColor = true;
            ImportOrderButton.Click += ImportOrderButton_Click;
            // 
            // openOrderFilesDialog
            // 
            openOrderFilesDialog.FileName = "openOrderFilesDialog";
            // 
            // ImportOrdersView
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ImportOrderButton);
            Controls.Add(label1);
            Controls.Add(OrderFilePathTextBox);
            Controls.Add(OpenOrderFilesButton);
            Name = "ImportOrdersView";
            Text = "ImportOrdersForm";
            Load += ImportOrdersForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button OpenOrderFilesButton;
        private TextBox OrderFilePathTextBox;
        private Label label1;
        private Button ImportOrderButton;
        private OpenFileDialog openOrderFilesDialog;
    }
}