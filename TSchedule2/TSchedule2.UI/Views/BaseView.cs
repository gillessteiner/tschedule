using System.Windows.Forms;

namespace TSchedule2.Views
{
   public partial class BaseView : UserControl
   {
      public BaseView() {
         InitializeComponent();
      }

      public virtual void Setup() {
         grpDefinition.Enabled = Program.MainForm?.CurrentProblem != null;
         txtLabel.Text = Program.MainForm?.CurrentProblem?.Label;

      }
   }
}
