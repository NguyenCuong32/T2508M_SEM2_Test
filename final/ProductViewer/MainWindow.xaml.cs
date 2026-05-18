using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ProductViewer.Models;

namespace ProductViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Product> _allProducts = new List<Product>();

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        /// <summary>
        /// Reads and deserializes product.json. Fallbacks to sample data if reading fails.
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "product.json");
                
                if (File.Exists(jsonPath))
                {
                    string jsonString = File.ReadAllText(jsonPath);
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    _allProducts = JsonSerializer.Deserialize<List<Product>>(jsonString, options) ?? new List<Product>();
                }
                else
                {
                    // Secondary safety fallback if the JSON is somehow not copied
                    _allProducts = GetFallbackProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product database: {ex.Message}\nUsing sample data instead.", "Database Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                _allProducts = GetFallbackProducts();
            }

            ProductListBox.ItemsSource = _allProducts;
        }

        private List<Product> GetFallbackProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, ProductName = "iPhone 15 Pro", Price = 1000.99m },
                new Product { Id = 2, ProductName = "Macbook air", Price = 1500.99m },
                new Product { Id = 3, ProductName = "iPod", Price = 100.50m }
            };
        }

        #region Title Bar & Window Management Actions

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Search and Selection Filtering

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim().ToLower();
            
            if (string.IsNullOrEmpty(query))
            {
                ProductListBox.ItemsSource = _allProducts;
            }
            else
            {
                ProductListBox.ItemsSource = _allProducts.Where(p => 
                    p.ProductName.ToLower().Contains(query) || 
                    p.Category.ToLower().Contains(query)
                ).ToList();
            }
        }

        private void ProductListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ProductListBox.SelectedItem as Product;
            
            if (selected != null)
            {
                // Hide Welcome / Show Details
                WelcomePanel.Visibility = Visibility.Collapsed;
                DetailsPanel.Visibility = Visibility.Visible;

                // Bind Details Control Values
                DetailIcon.Text = selected.Icon;
                DetailCategory.Text = selected.Category.ToUpper();
                DetailBrand.Text = selected.Brand;
                DetailName.Text = selected.ProductName;
                DetailPrice.Text = selected.FormattedPrice;
                DetailRating.Text = selected.RatingStars;
                DetailDescription.Text = selected.Description;
                DetailId.Text = selected.Id.ToString();
                
                ActionBtn.Content = $"ADD {selected.ProductName.ToUpper()} TO INVENTORY";
            }
            else
            {
                // Show Welcome / Hide Details
                WelcomePanel.Visibility = Visibility.Visible;
                DetailsPanel.Visibility = Visibility.Collapsed;
            }
        }

        #endregion

        private void ActionBtn_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductListBox.SelectedItem as Product;
            if (selected != null)
            {
                MessageBox.Show(
                    $"Successfully registered {selected.ProductName} in the retail catalog database!\n\nUnit Cost: {selected.FormattedPrice}\nCategory: {selected.Category}\nProduct ID Ref: #{selected.Id:D4}", 
                    "System Inventory Update", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Information);
            }
        }
    }
}