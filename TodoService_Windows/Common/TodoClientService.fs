namespace PortableLibrary

open System
open System.Collections.Generic
open System.Text
open Newtonsoft.Json
open System.Threading.Tasks
open FSharp.Data

type TodoItem() =
    member val Id = "" with get,set
    
    [<JsonProperty(PropertyName = "text")>]
    member val Text = "" with get,set
    
    [<JsonProperty(PropertyName = "complete")>]
    member val Complete = false with get,set

type TodoClientService() = 
    let azureKey = "HeFYhsVWOPskihprwIWsTFELBYsXPp65"
    let key = "ktT3brtjsL1mX+1uwfUKITK/SZfA3wEv1GpYClMep35b0i8nT3dRAQwpbkR9yQT4pRJ22Drghpgik9hAvjLBGQ=="
    let azureUrl = "https://bugghina.azure-mobile.net/Tables/TodoItem"
    

    let todoItems = new System.Collections.Generic.List<TodoItem>()

    member this.InsertTodoItem(todoItem:TodoItem) : Task<List<TodoItem>> =
            // Create 'Insert Logic' to Insert the 'TodoItem'
            // using the F# Data: HTTP Utilities
            // https://fsharp.github.io/FSharp.Data/library/Http.html
            // Use the Async Version 
            let op = 
                async {
                    return! Http.AsyncRequest(azureUrl, 
                                    query=["api_key", key], 
                                    httpMethod="POST",
                                    headers = [ // "Host", "bugghina.azure-mobile.net"
                                                "X-ZUMO-APPLICATION", azureKey ],
                                    body = FormValues ["Text", todoItem.Text
                                                       "Complete", string(todoItem.Complete)]) 
            }
            op |> Async.RunSynchronously |> ignore

            todoItems.Add(todoItem)
            Task.FromResult(todoItems)

    member this.RefreshTodoItems() : Task<List<TodoItem>> =
            // 1.
            // Create 'Get Logic' to retrieve all the 'TodoItem's from the service
            // using the F# Data: HTTP Utilities
            // https://fsharp.github.io/FSharp.Data/library/Http.html
            // Use the Async Version 

            // 2.
            // Deserialize the Json string into a collection of 'TodoItem's

            // 3. 
            // Update the 'todoItems' list with the new data

            let op = 
                async {
                    return! Http.AsyncRequestString(azureUrl, 
                                    query=["api_key", key], 
                                    httpMethod="GET",
                                    headers = [ // "Host", "bugghina.azure-mobile.net"
                                                "X-ZUMO-APPLICATION", azureKey ])
                }
            let items = op |> Async.RunSynchronously

            let todoItemsArray = JsonConvert.DeserializeObject<TodoItem[]>(items)
            
            todoItems.Clear()
            todoItems.AddRange(todoItemsArray)
            Task.FromResult(todoItems)
            
    member x.UpdateCheckedTodoItem(item:TodoItem) : Task<bool> =   
            let op = 
                async {
                    return! Http.AsyncRequest(azureUrl, 
                                    query=["api_key", key], 
                                    httpMethod="UPDATE",
                                    headers = [ // "Host", "bugghina.azure-mobile.net"
                                                "X-ZUMO-APPLICATION", azureKey
                                                "Access-Control-Allow-Origin", "*" ],
                                    body = FormValues [ "Id", item.Id ])
                                                        }
            op |> Async.RunSynchronously |> ignore  
            Task.FromResult(todoItems.Remove(item))

   