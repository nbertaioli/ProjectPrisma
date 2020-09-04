using OfficeOpenXml;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppp
{
    public partial class MainWindow : Window
    {
        const string urlApi = "http://localhost:9010/";
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGerar.IsEnabled = false;
                btnGerar.Content = "Gerando planilha...";
                var procEstado = RestService.For<IEstado>(urlApi);
                var jsonEstado = await procEstado.GetEstadoAsync();
                List<Municipio> listMunicipios = new List<Municipio>();

                foreach (var estados in jsonEstado)
                {
                    var procMunicipio = RestService.For<IMunicipio>(urlApi);
                    var jsonMunicipio = await procMunicipio.GetMunicipioAsync(estados.Sigla);

                    foreach (var municipios in jsonMunicipio)
                    {
                        listMunicipios.Add(municipios);
                    }
                }

                gerarPlanilha(listMunicipios);
            }
            catch (Exception ex)
            {
                btnGerar.IsEnabled = true;
                btnGerar.Content = "Gerar planilha de Estados/Municípios";
                MessageBox.Show("Ocorreu um erro ao efetuar o processo. " + ex.Message);
            }
        }

        private void gerarPlanilha(List<Municipio> municipios)
        {
            btnGerar.Content = "Gerando planilha.";
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var excelPackage = new ExcelPackage())
                {
                    excelPackage.Workbook.Properties.Author = "HostDy";
                    excelPackage.Workbook.Properties.Title = "Planilha Estados/Cidades";

                    var sheet = excelPackage.Workbook.Worksheets.Add("Planilha Principal");
                    sheet.Name = "Planilha Principal";

                    var celula = 1;
                    var linha = 1;
                    var cabecalho = new String[] { "Estado", "Região", "Cidade", "Mesorregião", "Cidade/UF" };
                    foreach (var escrita in cabecalho)
                    {
                        sheet.Cells[linha, celula++].Value = escrita;
                    }

                    linha = 2;
                    celula = 1;

                    foreach (var conteudo in municipios)
                    {
                        var valores = new String[] 
                        { 
                            conteudo.Microrregiao.Mesorregiao.Uf.Sigla,
                            conteudo.Microrregiao.Mesorregiao.Uf.Regiao.RegiaoNome,
                            conteudo.MunicipioNome,
                            conteudo.Microrregiao.Mesorregiao.MesorregiaoNome,
                            conteudo.MunicipioNome + " / " + conteudo.Microrregiao.Mesorregiao.Uf.Sigla
                        };
                        foreach (var resultado in valores)
                        {
                            sheet.Cells[linha, celula++].Value = resultado;
                        }
                        linha++;
                        celula = 1;
                    }

                    string path = Directory.GetCurrentDirectory() + @"\PlanilhaPrincipal.xlsx";
                    
                    File.WriteAllBytes(path, excelPackage.GetAsByteArray());

                    SendMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro. " + ex.Message);
                btnGerar.Content = "Gerar planilha de Estados/Municípios";
                btnGerar.IsEnabled = true;
            }
        }

        private void SendMessage()
        {
            btnGerar.Content = "Enviando e-mail.";
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("dyhost8@gmail.com", "Y5451711!");

                MailMessage mail = new MailMessage();
                mail.Sender = new MailAddress("dyhost8@gmail.com", "HostDy");
                mail.From = new MailAddress("dyhost8@gmail.com", "HostDy");
                mail.To.Add(new MailAddress(txtEmail.Text));
                mail.Subject = "HostDy - Planilha dos Estados/Cidades";
                mail.Body = "Segue anexo planilha dos Estados/Cidades que foram gerados através de dados do IBGE. <br><br><br> Um oferecimento HostDy.";

                Attachment attachment = new Attachment(Directory.GetCurrentDirectory() + @"\PlanilhaPrincipal.xlsx");

                mail.Attachments.Add(attachment);
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
            
                client.Send(mail);
                MessageBox.Show("Processo concluído com sucesso.");
                btnGerar.Content = "Gerar planilha de Estados/Municípios";
                btnGerar.IsEnabled = true;
                mail = null;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao enviar o e-mail. " + ex.Message);
                btnGerar.IsEnabled = true;
                btnGerar.Content = "Gerar planilha de Estados/Municípios";
            }
        }
    }
}
