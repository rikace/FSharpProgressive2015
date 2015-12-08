namespace TodoListApp

open System
open UIKit
open Foundation
open System.Drawing
open FSharp.Data

type AddTodoItemViewController(applicationURL:string, applicationKey:string) =
    inherit UIViewController ()
    override this.ViewDidLoad () =
        base.ViewDidLoad ()

        let addView = new UIView(this.View.Bounds, BackgroundColor = UIColor.White)

        let addedTextField = new UITextField()
        do
            addedTextField.Frame <- CoreGraphics.CGRect(20., 64., 280.,  50.)
            addedTextField.Text <- "Here something todo"
        addView.Add addedTextField

        // This is a label used to indicate the current state
        let statusLabel = new UILabel()

        let addUpdateButton = UIButton.FromType(UIButtonType.RoundedRect)

        // TODO
        // 1. Create a button using the UIButton object, the purpose of the button is to add new todo item (created in the Text field)
        // 2. set the Frame of the button just created using the 'CoreGraphics' object
        // 3. set the rectangle with an appropriate fitting size (suggested 20 64 280 50)
        // 4. add the UIButton created to the addView
        // 5. set the button with a title (text)
        // 6. add an event ('AddHandler') to the button 
              // there are several events that you can try, for example 'TouchUpInside' and 'TouchDown'
        // 7. when the event is fired, the item from the Text Field (created previously) 
              // should be sent to the Web API using 'Http.RequestString' (FSharp.Data)
              // to store the new item into the back-end
              // when the operation is completed, the text-box content should be erased

        do
            addUpdateButton.SetTitle("Save", UIControlState.Normal)
            addView.Add addUpdateButton

     

        this.View.Add addView
