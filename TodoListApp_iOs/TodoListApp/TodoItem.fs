namespace TodoListApp
open System

type TodoItem() = 
    member val Id = "" with get,set
    member val Text = "" with get,set
    member val Complete = false with get,set
  