import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import SyncfusionToDoLists from "./components/ToDoLists/SyncfusionTodoLists";
import ToDoLists from "./components/ToDoLists/TodoLists";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
    },
    {
        path: '/todo-lists',
        element: <ToDoLists/>
    },
    {
        path: '/syncfusion-todo-lists',
        element: <SyncfusionToDoLists/>
    }
];

export default AppRoutes;
