using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using PortableLibrary;

namespace bugghina
{
    sealed partial class MainPage : Page
    {
        TodoClientService service = new TodoClientService();
        
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async Task InsertTodoItem(TodoItem todoItem)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Service has assigned an Id, the item is added to the CollectionView
            var todoItems = await service.InsertTodoItem(todoItem);
            ListItems.ItemsSource = todoItems;
        }

        private async Task RefreshTodoItems()
        {
            var todoItems = await service.RefreshTodoItems();

            if (todoItems == null && todoItems.Count == 0)
            {
                todoItems.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item One" });
                todoItems.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item Two" });
                todoItems.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item Three" });
            }
            ListItems.ItemsSource = todoItems;
            this.ButtonSave.IsEnabled = true;
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database.
           // await service.UpdateCheckedTodoItem(item);
        }
        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ButtonRefresh.IsEnabled = false;
            await RefreshTodoItems();

            ButtonRefresh.IsEnabled = true;
        }

        private async void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var todoItem = new TodoItem { Text = TextInput.Text };
            await InsertTodoItem(todoItem);
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            await UpdateCheckedTodoItem(item);
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
           // RefreshTodoItems().Wait();
           await RefreshTodoItems();
        }
    }
}
