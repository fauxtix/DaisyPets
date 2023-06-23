namespace DaisyPets.UI.Expenses
{
    partial class frmCalculator
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
            calculator = new Syncfusion.Windows.Forms.Tools.CalculatorControl();
            SuspendLayout();
            // 
            // calculator
            // 
            calculator.AccessibleDescription = "Calculator control";
            calculator.AccessibleName = "Calculator Control";
            calculator.BeforeTouchSize = new Size(363, 325);
            calculator.BorderStyle = Border3DStyle.Flat;
            calculator.Culture = new System.Globalization.CultureInfo("pt-PT");
            calculator.DoubleValue = 0D;
            calculator.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            calculator.ForeColor = SystemColors.ControlText;
            calculator.LayoutType = Syncfusion.Windows.Forms.Tools.CalculatorLayoutTypes.Financial;
            calculator.Location = new Point(21, 14);
            calculator.MetroColor = SystemColors.Control;
            calculator.Name = "calculator";
            calculator.RightToLeft = RightToLeft.No;
            calculator.Size = new Size(363, 325);
            calculator.TabIndex = 0;
            calculator.UseVisualStyle = true;
            // 
            // frmCalculator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(417, 361);
            Controls.Add(calculator);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmCalculator";
            StartPosition = FormStartPosition.CenterScreen;
            Style.BackColor = Color.WhiteSmoke;
            Style.MdiChild.IconHorizontalAlignment = HorizontalAlignment.Center;
            Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            Text = "Calculadora";
            ResumeLayout(false);
        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.CalculatorControl calculator;
    }
}