using DOY.dataFiles;
using DOY.Pages.Add;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Word = Microsoft.Office.Interop.Word;
using System.Text;
using System.Security.Principal;
using System.Data;
using System.Globalization;


namespace DOY.Pages.View
{
    /// <summary>
    /// Логика взаимодействия для PageContract.xaml
    /// </summary>
    public partial class PageContract : Page
    {
        private static Contract ContractObj;
        string TemplateFileName = Directory.GetCurrentDirectory() + @"\Макет_договора.docx";
        // TemplateFileName = @".\Resources\Макет_договора.docx";
        public PageContract()
        {
            InitializeComponent();
            lbContract.ItemsSource = ConnectHelper.entObj.Contract.ToList();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddContract windowAddContract = new WindowAddContract(); 
            windowAddContract.ShowDialog();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbContract.ItemsSource = ConnectHelper.entObj.Contract.ToList();
        }

        private void btnSaveToWord_Click(object sender, RoutedEventArgs e)
        {
            var wordApp = new Word.Application();
            wordApp.Visible = false;
            ContractObj = lbContract.SelectedItem as Contract;
            if (ContractObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {

                var Contract = ConnectHelper.entObj.ParentChildren.FirstOrDefault(x => x.ID_Contract == ContractObj.ID_Contract);
                try
                {
                    DateTime dateOfBirthChild = (DateTime)Contract.DateOfBirthChild;
                    DateTime dateContract = (DateTime)Contract.DateContract;

                    var wordDocument = wordApp.Documents.Open(TemplateFileName);
                    ReplaceWordStud("{FIO Children}", Contract.FIOChild, wordDocument);
                    ReplaceWordStud("{Date_of_Birth}", dateOfBirthChild.ToString("D"), wordDocument);
                    ReplaceWordStud("{Pay}", Contract.Pay.ToString(), wordDocument);

                    ReplaceWordStud("{Date}", dateContract.ToString("D"), wordDocument);

                    ReplaceWordStud("{FIO Parent}", Contract.FIOPar, wordDocument);
                    ReplaceWordStud("{FIO Parent}", Contract.FIOPar, wordDocument);

                    ReplaceWordStud("{Phone}", Contract.PhonePar, wordDocument);
                    ReplaceWordStud("{id}", Contract.ID_Contract.ToString(), wordDocument);

                    wordDocument.SaveAs2(Directory.GetCurrentDirectory() + @"\Договор.docx");
                    wordApp.Visible = true;
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при добавлении!");
                }
            }
        }
        private void ReplaceWordStud(string studToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: studToReplace, ReplaceWith: text);
        }
        private void miRefresh_Click(object sender, RoutedEventArgs e)
        {
            lbContract.ItemsSource = ConnectHelper.entObj.Contract.ToList();
        }

        private void miDel_Click(object sender, RoutedEventArgs e)
        {
            elementDelete();
        }

        private void elementDelete()
        {
            ContractObj = lbContract.SelectedItem as Contract;
            if (ContractObj == null) MessageBox.Show("Запись не выбрана!", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (MessageBox.Show("Вы точно хотите удалить запись ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ConnectHelper.entObj.Contract.Remove(ContractObj);
                    ConnectHelper.entObj.SaveChanges();
                    MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    lbContract.ItemsSource = ConnectHelper.entObj.Contract.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
