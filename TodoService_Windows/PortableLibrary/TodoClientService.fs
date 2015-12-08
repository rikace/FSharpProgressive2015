namespace PortableLibrary

open System
open System.Collections.Generic
open System.Text
open Newtonsoft.Json

type TodoItem() =
    member val Id = "" with get,set

    //I'm using F# PCLs (profile 47) in WP8 without problems. 
//I currently have a WP8 app, which depends on a F# profile47 2.3.5.1 lib, which in turn depends on a F# profile47 2.3.5.0 lib (FSharp.Data), and it all works fine.
//I'm also reusing the same lib for a W8 app, and did some initial tests with Xamarin.Android, and was able to deploy and run on the device
//        [JsonProperty(PropertyName = "text")]
//        public string Text { get; set; }
//
//        [JsonProperty(PropertyName = "complete")]
//        public bool Complete { get; set; }
//    }
//}
(*
type TodoClientService() = 
   
//        private MobileServiceCollection<TodoItem, TodoItem> items;
//        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
//        //private IMobileServiceSyncTable<TodoItem> todoTable = App.MobileService.GetSyncTable<TodoItem>(); // offline sync

        member this.InsertTodoItem(todoItem:Todo)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await todoTable.InsertAsync(todoItem);
            items.Add(todoItem);

            //await SyncAsync(); // offline sync
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                var lst = new List<TodoItem>();
                lst.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item One" });
                lst.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item Two" });
                lst.Add(new TodoItem { Complete = false, Id = Guid.NewGuid().ToString(), Text = "Item Three" });

                ListItems.ItemsSource = lst;
                this.ButtonSave.IsEnabled = true;
            }
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            //await SyncAsync(); // offline sync
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            ButtonRefresh.IsEnabled = false;

            //await SyncAsync(); // offline sync
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
            //await InitLocalStoreAsync(); // offline sync
            await RefreshTodoItems();
        }

        #region Offline sync

        //private async Task InitLocalStoreAsync()
        //{
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
        //        var store = new MobileServiceSQLiteStore("localstore.db");
        //        store.DefineTable<TodoItem>();
        //        await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //
        //    await SyncAsync();
        //}

        //private async Task SyncAsync()
        //{
        //    await App.MobileService.SyncContext.PushAsync();
        //    await todoTable.PullAsync("todoItems", todoTable.CreateQuery());
        //}

        #endregion 
    }
    *)