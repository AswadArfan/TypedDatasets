using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TypedDatasets
{

    public partial class MainWindow : Window
    {
        NorthWIindDataSetTableAdapters.ProductsTableAdapter adpProds = new NorthWIindDataSetTableAdapters.ProductsTableAdapter();
        NorthWIindDataSetTableAdapters.CategoriesTableAdapter adpCats = new NorthWIindDataSetTableAdapters.CategoriesTableAdapter();

        NorthWIindDataSet.ProductsDataTable tblProds = new NorthWIindDataSet.ProductsDataTable();
        NorthWIindDataSet.CategoriesDataTable tblCats = new NorthWIindDataSet.CategoriesDataTable();
        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();

        }
        private void LoadProducts()
        {
            //using fill method
            adpProds.Fill(tblProds);

            //using get method to load all products
            //tblProds = adpProds.GetProducts();
            grdProducts.ItemsSource = tblProds;
        }
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var row = tblProds.FindByProductID(id);

            if (row != null)
            {
                txtName.Text = row.ProductName;
                txtPrice.Text = row.UnitPrice.ToString();
                txtQuantity.Text = row.UnitsInStock.ToString();
            }
            else
            {
                txtName.Text = txtPrice.Text = txtQuantity.Text = "";
                MessageBox.Show("Invalid Id, Try Again");
            }
        }

        private void btnLoadAllProducts_Click_1(object sender, RoutedEventArgs e)
        {
            LoadProducts();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);

            adpProds.Insert(name, price, quantity);

            LoadProducts();
            MessageBox.Show("New Product Added");
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse((string)txtId.Text);
            string name = txtName.Text;
            decimal price = decimal.Parse(txtPrice.Text);
            short quantity = short.Parse(txtQuantity.Text);
            adpProds.Update(name, price, quantity, id);

            LoadProducts() ;
            MessageBox.Show("Product Updated");


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //int id = int.Parse(txtId.Text);
             int id = Convert.ToInt32(txtId.Text);
            adpProds.Delete(id);

            LoadProducts();
            MessageBox.Show("Products Deleted");


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            tblProds = adpProds.GetProductByName(name);
            grdProducts.ItemsSource = tblProds;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblCats = adpCats.GetCategories();

            cmbCategories.ItemsSource = tblCats;
            cmbCategories.DisplayMemberPath = "CategoryName";
            cmbCategories.SelectedValuePath = "CategoryID";
        }
    }
}