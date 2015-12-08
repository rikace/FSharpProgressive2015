namespace TodoListApp

open System

open UIKit
open Foundation
open System
open System.Collections.Generic
open System.Drawing
open Microsoft.WindowsAzure.MobileServices
open Microsoft.WindowsAzure
open System.IO
open System.Net.Http
open FSharp.Data

module AzureAccountInfo =
    let applicationURL = @"https://bugghina.azure-mobile.net/";
    let applicationKey = @"HeFYhsVWOPskihprwIWsTFELBYsXPp65";


type RenderTodoList(items:TodoItem[], navigation: UINavigationController) = 
    inherit UITableViewSource()
    let cellIdentifier = "WebsiteCell"

    override x.RowsInSection(view, section) = nint (items.Length)

    override x.GetCell(view, indexPath) = 
        let item = items.[int indexPath.Item]
        let text = item.Text
        let cell =
            match view.DequeueReusableCell cellIdentifier with 
            | null -> let cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier)
                      // Render the Items with 'Checkmark' if they are completed otherwise 'None'
                      // set the property 'Accessory' for the cell 
                      if item.Complete = true then
                        cell.Accessory <- UITableViewCellAccessory.Checkmark
                      else
                        cell.Accessory <- UITableViewCellAccessory.None
                      cell

            | cell -> cell
        
        cell.TextLabel.Text <- text
        cell

    override x.RowSelected (tableView, indexPath) = 
        tableView.DeselectRow (indexPath, false)
        // when the item is selected this function is called
        // Add navigation controller to "PushViewController" and navigate to "AddTodoItemViewController"
        navigation.PushViewController (new AddTodoItemViewController(AzureAccountInfo.applicationURL, AzureAccountInfo.applicationKey), true)
    
    override x.CanEditRow (view, indexPath) = true
  
type TodoListViewController () as this =
    inherit UIViewController ()


    let refresh() = 
        // let mobileService = new Microsoft.WindowsAzure.MobileServices.MobileServiceClient("", "")
        // let todoTable = client.GetSyncTable<ToDoItem>()

        // TODO
        // Reload the data from the WebApi each time the 'refresh' function is called
        // A better aproach should be using a local storage like 'SQLite'
        // to avoid to reload the data each time, but this will be used for demo purposes
        // Implement the refresh() function.
            // 1. Load the FSharp Data
            // 2. Use the 'Http.RequestString' functionality 
            //    to load the data from the Web API 
            //    this is a UI, therefofe use the async version of the request http - https://fsharp.github.io/FSharp.Data/library/Http.html 
            // 3. parse the data from string/json to the type 'TodoItem'
            // 4. return the item list to the navigator Controller 'RenderTodoList'              

                
        let item1 = TodoItem()
        item1.Id <- "1"
        item1.Text <- "Item One"
        item1.Complete <- false

        let item2 = TodoItem()
        item2.Id <- "2"
        item2.Text <- "Item Two"
        item2.Complete <- true

        new RenderTodoList([|item1; item2|], this.NavigationController)

    let table = new UITableView()


    // button (UIButton) to remove completed Items
    let removeCompletedItemsButton = UIButton.FromType(UIButtonType.System)
    do 
        this.Title <- "Todo Service"
            
        // TODO
        // Set the button create to remove the completed items
        // 1. set the button Frame using the 'CoreGraphics' object
        // 2. set the rectangle with a fitting size, the rectangle should be at the 'bottom/center' of the view (suggested 30 400 280 50)
        // 3. set the text (title) of the button 
        // Two implementation options:
            // Implementation with Web API
                // 1. add an event to the button to send the request to the Web API to remove the completed items
                // 2. after the call to the Api call the refresh button
                // 3. modify the refresh functionality to display only the 'not-completed' items
            // Implementation with Toggle button
                // 1. add an event to the button for toggling the view of the items
                // 2. the toggle functionality could be implemented passing a new argument into the 'RenderTodoList'

        this.View.Add removeCompletedItemsButton

    override this.ViewDidLoad () =
        base.ViewDidLoad ()

        let addNewTask = 
            EventHandler(fun sender eventargs -> 
                this.NavigationController.PushViewController (new AddTodoItemViewController(AzureAccountInfo.applicationURL, AzureAccountInfo.applicationKey), true))
        let refreshTasks =
            EventHandler(fun sender eventargs -> 
                table.Source <- refresh()
                table.ReloadData())

        this.NavigationItem.SetRightBarButtonItem (new UIBarButtonItem(UIBarButtonSystemItem.Add, addNewTask), false)
        this.NavigationItem.SetLeftBarButtonItem(new UIBarButtonItem(UIBarButtonSystemItem.Refresh, refreshTasks), true)
        table.Frame <- this.View.Bounds

        this.View.Add table 

    override this.ViewWillAppear animated =
        base.ViewWillAppear animated

        // to render the todo item list
        // the table 'Source' is refreshed and reload the data
        table.Source <- refresh()
        table.ReloadData()
    