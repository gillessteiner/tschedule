using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrainScheduler.UI
{
    public partial class BaseView : UserControl
    {
        public BaseView()
        {
            InitializeComponent();
        }

        public virtual void Setup()
        {
            grpDefinition.Enabled = MainForm.CurrentProblem != null;
            txtLabel.Text = MainForm.CurrentProblem?.Label;

        }

    }
}
