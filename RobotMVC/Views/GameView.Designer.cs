namespace RobotMVC
{
    partial class GameView
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
            Wall = new Button();
            Start = new Button();
            End = new Button();
            Begin = new Button();
            SuspendLayout();
            // 
            // Wall
            // 
            Wall.Location = new Point(29, 942);
            Wall.Name = "Wall";
            Wall.Size = new Size(293, 138);
            Wall.TabIndex = 0;
            Wall.Text = "Wall";
            Wall.UseVisualStyleBackColor = true;
            Wall.Click += Wall_Click;
            // 
            // Start
            // 
            Start.Location = new Point(364, 942);
            Start.Name = "Start";
            Start.Size = new Size(293, 138);
            Start.TabIndex = 1;
            Start.Text = "Start";
            Start.UseVisualStyleBackColor = true;
            Start.Click += Start_Click;
            // 
            // End
            // 
            End.Location = new Point(693, 942);
            End.Name = "End";
            End.Size = new Size(293, 138);
            End.TabIndex = 2;
            End.Text = "End";
            End.UseVisualStyleBackColor = true;
            End.Click += End_Click;
            // 
            // Begin
            // 
            Begin.Location = new Point(1039, 942);
            Begin.Name = "Begin";
            Begin.Size = new Size(293, 138);
            Begin.TabIndex = 3;
            Begin.Text = "Begin";
            Begin.UseVisualStyleBackColor = true;
            Begin.Click += Begin_Click;
            // 
            // GameView
            // 
            AutoScaleDimensions = new SizeF(17F, 41F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1789, 1126);
            Controls.Add(Begin);
            Controls.Add(End);
            Controls.Add(Start);
            Controls.Add(Wall);
            Name = "GameView";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Wall;
        private Button Start;
        private Button End;
        private Button Begin;
    }
}
