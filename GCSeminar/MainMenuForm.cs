using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GCSeminar
{
    public partial class MainMenuForm : Form
    {
        private TreeView treeView;
        private Button openButton;

        public MainMenuForm()
        {
            InitializeComponent();
            this.Text = "GCSeminar - Control Panel";
            this.Width = 500;
            this.Height = 600;

            BuildUI();
            LoadSeminars();
        }

        private void BuildUI()
        {
            treeView = new TreeView
            {
                Dock = DockStyle.Fill
            };

            openButton = new Button
            {
                Text = "Open Selected Exercise",
                Dock = DockStyle.Bottom,
                Height = 40
            };

            openButton.Click += OpenButton_Click;

            this.Controls.Add(treeView);
            this.Controls.Add(openButton);
        }

        private void LoadSeminars()
        {
            var forms = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t =>
                    t.IsSubclassOf(typeof(BaseGraphicsForm)) &&
                    !t.IsAbstract);

            var grouped = forms
                .GroupBy(t => t.Namespace)
                .OrderBy(g => g.Key);

            foreach (var group in grouped)
            {
                string seminarName = group.Key.Split('.').Last();

                TreeNode seminarNode = new TreeNode(seminarName);

                foreach (var formType in group.OrderBy(t => t.Name))
                {
                    TreeNode exerciseNode = new TreeNode(formType.Name)
                    {
                        Tag = formType
                    };

                    seminarNode.Nodes.Add(exerciseNode);
                }

                treeView.Nodes.Add(seminarNode);
            }

            treeView.ExpandAll();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (treeView.SelectedNode?.Tag is Type formType)
            {
                Form form = (Form)Activator.CreateInstance(formType);
                form.Show();
            }
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }
    }
}
