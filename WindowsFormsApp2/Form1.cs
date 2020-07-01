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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var assembly = Assembly.LoadFile(textBox1.Text.Trim());

            foreach (var type in assembly.GetTypes())
            {
                TreeNode classNode = new TreeNode();
                classNode.Text = ($"Class {type.Name}:");
                classNode.Text += ($"  Namespace: {type.Namespace}");
                classNode.Text += ($"  Full name: {type.FullName}");

                foreach (var methodInfo in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
                {
                    TreeNode methodNode = new TreeNode();
                    

                    if (methodInfo.IsPublic)
                        methodNode.Text += ($"      Public");

                    if (methodInfo.IsFamily)
                        methodNode.Text += ($"      Protected");

                    if (methodInfo.IsAssembly)
                        methodNode.Text += ($"      Internal");

                    if (methodInfo.IsPrivate)
                        methodNode.Text += ($"      Private");

                    methodNode.Text += ($"      ReturnType {methodInfo.ReturnType}");

                    methodNode.Text += ($"    Method {methodInfo.Name}");

                    methodNode.Text += ($"      Arguments {string.Join(", ", methodInfo.GetParameters().Select(x => x.ParameterType))}");
                    classNode.Nodes.Add(methodNode);
                }

                treeView1.Nodes.Add(classNode);
            }
        }
    }
}
