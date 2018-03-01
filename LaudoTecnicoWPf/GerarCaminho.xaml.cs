using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaudoTecnicoWPF
{
    /// <summary>
    /// Lógica interna para GerarCaminho.xaml
    /// </summary>
    public partial class GerarCaminho : Window
    {
        public GerarCaminho()
        {
            InitializeComponent();
        }


        private void pch_Checked(object sender, RoutedEventArgs e)
        {
            if (this.pch.IsChecked == true)
            {
                this.emissor.Text = "";
                this.emissor.IsEnabled = false;
            }
        }

        private void especifico_Checked(object sender, RoutedEventArgs e)
        {
            if (this.especifico.IsChecked == true)
            {
                this.emissor.Text = "";
                this.emissor.IsEnabled = true;
            }
        }

        private void gerar_Click(object sender, RoutedEventArgs e)
        {
            var numero_gmud = this.numerogmud.Text;
            var gera_pch = this.pch.IsChecked;
            var gera_especifico = this.especifico.IsChecked;
            var emissor = this.emissor.Text;
            var reversao = this.reversao.IsChecked;
            var caminho = this.caminho.Text;

            if (!Directory.Exists(caminho + "\\" + numero_gmud))
            {
                var caminhoraiz = caminho + "\\" + numero_gmud;
                Directory.CreateDirectory(caminhoraiz);
                Directory.CreateDirectory(caminhoraiz + "\\Script\\Data");
                if (gera_pch == true)
                {
                    Directory.CreateDirectory(caminhoraiz + "\\Script\\Data\\PCH");
                }
                if (gera_especifico == true)
                {
                    Directory.CreateDirectory(caminhoraiz + "\\Script\\Data\\" + emissor);
                }
                if (reversao == true)
                {
                    Directory.CreateDirectory(caminhoraiz + "\\Reversao\\Data\\");
                    if (gera_especifico == true)
                    {
                        Directory.CreateDirectory(caminhoraiz + "\\Reversao\\Data\\" + emissor);
                    }
                    if (gera_pch == true)
                    {
                        Directory.CreateDirectory(caminhoraiz + "\\Reversao\\Data\\PCH");
                    }
                }
            }
            else
            {
                this.emissor.Text = "Diretorio ja existente";
            }
            this.emissor.Text = "Gerado com sucesso!";


        }
    }
}
